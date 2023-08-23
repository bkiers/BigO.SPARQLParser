namespace BigO.SPARQLParser.Tests.Extensions;

using BigO.SPARQLParser.Exceptions;
using BigO.SPARQLParser.Extensions;
using BigO.SPARQLParser.Parser;
using Xunit;
using static BigO.SPARQLParser.Parser.SPARQLParser;

public static class StringExtensionsTests
{
  public class TokenizeTests
  {
    [Fact]
    public void EmptyString_ReturnsEofToken()
    {
      var tokens = string.Empty.Tokenize();

      Assert.Single(tokens);
      Assert.Equal(SPARQLLexer.Eof, tokens.Single().Type);
    }

    [Fact]
    public void WithSpaces_KeepsSpacesTokens()
    {
      var tokens = "1 2\t3\n4".Tokenize();

      Assert.Equal(8, tokens.Count);
      Assert.Equal(SPARQLLexer.INTEGER, tokens[0].Type);
      Assert.Equal(SPARQLLexer.SPACE, tokens[1].Type);
      Assert.Equal(SPARQLLexer.INTEGER, tokens[2].Type);
      Assert.Equal(SPARQLLexer.SPACE, tokens[3].Type);
      Assert.Equal(SPARQLLexer.INTEGER, tokens[4].Type);
      Assert.Equal(SPARQLLexer.SPACE, tokens[5].Type);
      Assert.Equal(SPARQLLexer.INTEGER, tokens[6].Type);
      Assert.Equal(SPARQLLexer.Eof, tokens[7].Type);
    }
  }

  public class ParseAsTests
  {
    [Fact]
    public void Expression_ReturnsCorrectContext()
    {
      var expression = "?Q + 42".ParseAs<ExpressionContext>();

      Assert.NotNull(expression);
    }

    [Fact]
    public void ExpressionWithComment_ReturnsCorrectContext()
    {
      var expression = "1 + 2 # ignore me!".ParseAs<ExpressionContext>();

      Assert.NotNull(expression);
    }

    [Fact]
    public void WrongContext_ThrowsException()
    {
      Assert.Throws<ParseException>(() => "1 + 2".ParseAs<VarContext>());
    }

    [Fact]
    public void NotAllTokensConsumed_DoesNotThrowException()
    {
      var varContext = "?Q + 42".ParseAs<VarContext>();

      // Only "?Q" is present
      Assert.Equal(1, varContext.ChildCount);
      Assert.Equal("?Q", varContext.children.Single().GetText());
    }
  }

  public class RewriteTests
  {
    [Fact]
    public void LimitAndOffsetPresent_RewritesBoth()
    {
      const string query = @"PREFIX crm: <http://www.cidoc-crm.org/cidoc-crm/>
        PREFIX la: <https://linked.art/ns/terms/>

        SELECT DISTINCT ?objectUri ?objectId ?propertyUri ?propertyId ?value
        WHERE {
          {
            # ignore this:
            # Limit 4321
            # OFFSET 131415
            {
              Select ?objectUri ?objectId ?usedMaterialUri
              WHERE {
                ?objectUri rdf:type crm:E22_Human-Made_Object.
                ?objectUri crm:P45_consists_of ?usedMaterialUri.
                BIND(MD5(STR(?objectUri)) as ?objectId)
              }
              ORDER BY ?objectUri
              Limit 10000
              OFFSET 0
            }

            ?usedMaterialUri rdf:type crm:E57_Material.
            ?usedMaterialUri crm:P1_is_identified_by ?identifier.
            ?identifier crm:P72_has_language ?language.
            ?identifier crm:P190_has_symbolic_content ?materialName.

            BIND(MD5(STR(?usedMaterialUri)) as ?propertyId)
            BIND(?usedMaterialUri as ?propertyUri)
            BIND(?materialName as ?value)
          }
        }";

      const string expectedQuery = @"PREFIX crm: <http://www.cidoc-crm.org/cidoc-crm/>
        PREFIX la: <https://linked.art/ns/terms/>

        SELECT DISTINCT ?objectUri ?objectId ?propertyUri ?propertyId ?value
        WHERE {
          {
            # ignore this:
            # Limit 4321
            # OFFSET 131415
            {
              Select ?objectUri ?objectId ?usedMaterialUri
              WHERE {
                ?objectUri rdf:type crm:E22_Human-Made_Object.
                ?objectUri crm:P45_consists_of ?usedMaterialUri.
                BIND(MD5(STR(?objectUri)) as ?objectId)
              }
              ORDER BY ?objectUri
              Limit 4200
              OFFSET 666
            }

            ?usedMaterialUri rdf:type crm:E57_Material.
            ?usedMaterialUri crm:P1_is_identified_by ?identifier.
            ?identifier crm:P72_has_language ?language.
            ?identifier crm:P190_has_symbolic_content ?materialName.

            BIND(MD5(STR(?usedMaterialUri)) as ?propertyId)
            BIND(?usedMaterialUri as ?propertyUri)
            BIND(?materialName as ?value)
          }
        }";

      var queryUnit = query.ParseAs<QueryUnitContext>();

      var (newOffsetQuery, newOffsetQueryContext) = query.Rewrite<QueryUnitContext>(queryUnit.Nodes<LimitOffsetClausesContext>().First().limitClause().INTEGER(), "4200");
      var (newQuery, _) = newOffsetQuery.Rewrite<QueryUnitContext>(newOffsetQueryContext.Nodes<LimitOffsetClausesContext>().First().offsetClause().INTEGER(), "666");

      Assert.Equal(expectedQuery, newQuery);
    }
  }
}
