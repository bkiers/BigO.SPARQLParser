namespace BigO.SPARQLParser.Tests;

using BigO.SPARQLParser.Exceptions;
using BigO.SPARQLParser.Parser;
using Xunit;
using static BigO.SPARQLParser.Parser.SPARQLParser;

public static class SPARQLQueryTests
{
  public class SPARQLQueryConstructor
  {
    [Fact]
    public void ValidInput_DoesNotThrowException()
    {
      const string queryString = @"SELECT ?name
        WHERE {
            ?person foaf:name ?name .
        }";

      var query = new SPARQLQuery<QueryUnitContext>(queryString);

      Assert.NotNull(query);
    }

    [Fact]
    public void TooManyTokens_ThrowsParseException()
    {
      const string queryString = @"1 + 2 .";

      Assert.Throws<ParseException>(() => new SPARQLQuery<ExpressionContext>(queryString));
    }

    [Fact]
    public void MissingCloseBrace_ExceptionMessageContainsCloseBrace()
    {
      const string query = "SELECT ?name WHERE { ?person foaf:name ?name . ";

      var exception = Assert.Throws<ParseException>(() => new SPARQLQuery<QueryUnitContext>(query));

      Assert.Contains("'}'", exception.Message);
    }

    [Fact]
    public void WithSmileyChar_ExceptionMessageContainsSmileyChar()
    {
      const string query = "SELECT ?name WHERE { ?person foaf:name ?name . } 😀";

      var exception = Assert.Throws<ParseException>(() => new SPARQLQuery<QueryUnitContext>(query));

      Assert.Contains("'😀'", exception.Message);
    }

    [Fact]
    public void ValidQueryWithNonAscii_NoExceptionIsThrown()
    {
      const string query = "SELECT ?name WHERE { ?person foaf:name ?name . } # 😀";

      var queryContext = new SPARQLQuery<QueryUnitContext>(query);

      Assert.NotNull(queryContext);
    }
  }

  public class ContextTests
  {
    [Fact]
    public void ValidQuery_InitializesContext()
    {
      const string query = "SELECT ?name WHERE { ?person foaf:name ?name . }";

      var queryContext = new SPARQLQuery<QueryUnitContext>(query);

      Assert.NotNull(queryContext.Context);
    }
  }

  public class ParserRulesTests
  {
    [Fact]
    public void ValidQuery_ContainsElements()
    {
      const string query = "SELECT ?name WHERE { ?person foaf:name ?name . }";

      var queryContext = new SPARQLQuery<QueryUnitContext>(query);

      Assert.True(queryContext.ParserRules.Any());
    }
  }

  public class TokensTests
  {
    [Fact]
    public void IncludeHiddenTokensFalse_ExcludesSpacesAndComment()
    {
      //                    1      2     3     4 5       6         7     8 9            10=EOF
      const string query = "SELECT ?name WHERE { ?person foaf:name ?name . } # ignore me";

      var tokens = new SPARQLQuery<QueryUnitContext>(query).Tokens(includeHiddenTokens: false);

      Assert.Equal(10, tokens.Count);
    }

    [Fact]
    public void IncludeHiddenTokensTrue_IncludesSpacesAndComment()
    {
      //                    1     .3    .5    .7.9      .11       .13   .....19         20=EOF
      const string query = "SELECT ?name WHERE { ?person foaf:name ?name . } # ignore me";

      var tokens = new SPARQLQuery<QueryUnitContext>(query).Tokens(includeHiddenTokens: true);

      Assert.Equal(20, tokens.Count);
    }
  }

  public class FindTokensTests
  {
    [Fact]
    public void TokenType_FindsAllTokens()
    {
      const string query = "SELECT ?name WHERE { ?person foaf:name ?name . }";

      var queryContext = new SPARQLQuery<QueryUnitContext>(query);

      Assert.Equal(3, queryContext.FindTokens(SPARQLLexer.VAR1).Count);
    }

    [Fact]
    public void TokenTypeAndText_FindsAllTokens()
    {
      const string query = "SELECT ?name WHERE { ?person foaf:name ?name . }";

      var queryContext = new SPARQLQuery<QueryUnitContext>(query);

      Assert.Equal(2, queryContext.FindTokens(SPARQLLexer.VAR1, text: "?name").Count);
    }

    [Fact]
    public void TokenTypeAndTextAndStringComparison_FindsAllTokens()
    {
      const string query = "SELECT ?name WHERE { ?person foaf:name ?name . }";

      var queryContext = new SPARQLQuery<QueryUnitContext>(query);

      Assert.Equal(0, queryContext.FindTokens(SPARQLLexer.VAR1, text: "?NAME").Count);
      Assert.Equal(
        2, queryContext.FindTokens(SPARQLLexer.VAR1, text: "?NAME", StringComparison.OrdinalIgnoreCase).Count);
    }
  }

  public class InsertQueryTests
  {
    [Fact]
    public void AfterFalse_PrependsExpression()
    {
      const string query = "1 + 2";

      var expression = new SPARQLQuery<ExpressionContext>(query);
      var two = expression.FindTokens(SPARQLLexer.INTEGER).Last();
      var updatedExpression = expression.InsertQuery<ExpressionContext>(
        " - 3", two, before: false);

      Assert.Equal("1 + 2 - 3", updatedExpression.ToQueryString());
    }

    [Fact]
    public void AfterTrue_AppendsExpression()
    {
      const string query = "1 + 2";

      var expression = new SPARQLQuery<ExpressionContext>(query);
      var two = expression.FindTokens(SPARQLLexer.INTEGER).Last();
      var updatedExpression = expression.InsertQuery<ExpressionContext>(
        "4 * ", two, before: true);

      Assert.Equal("1 + 4 * 2", updatedExpression.ToQueryString());
    }

    [Fact]
    public void InvalidExpression_ThrowsException()
    {
      const string query = "1 + 2";

      var expression = new SPARQLQuery<ExpressionContext>(query);
      var two = expression.FindTokens(SPARQLLexer.INTEGER).Last();

      // `1 + 2 4` is not a invalid expression
      Assert.Throws<ParseException>(() => expression.InsertQuery<ExpressionContext>(" 4", two, before: false));
    }
  }

  public class NodesTests
  {
    [Fact]
    public void WithNodes_ReturnsAllNodes()
    {
      const string query = "(1 + (2 * 42)) / 666";

      var expression = new SPARQLQuery<ExpressionContext>(query);
      var nodes = expression.FindNodes<ExpressionContext>().ToList();

      Assert.Equal(3, nodes.Count);
    }

    [Fact]
    public void WithoutNodes_ReturnsNoNodes()
    {
      const string query = "(1 + (2 * 42)) / 666";

      var expression = new SPARQLQuery<ExpressionContext>(query);
      var nodes = expression.FindNodes<QueryUnitContext>().ToList();

      Assert.Empty(nodes);
    }
  }
}
