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
      const string queryString = "1 + 2 .";

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
      var updatedExpression = expression.Insert<ExpressionContext>(
        " - 3", two, before: false);

      Assert.Equal("1 + 2 - 3", updatedExpression.ToQueryString());
    }

    [Fact]
    public void AfterTrue_AppendsExpression()
    {
      const string query = "1 + 2";

      var expression = new SPARQLQuery<ExpressionContext>(query);
      var two = expression.FindTokens(SPARQLLexer.INTEGER).Last();
      var updatedExpression = expression.Insert<ExpressionContext>(
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
      Assert.Throws<ParseException>(() => expression.Insert<ExpressionContext>(" 4", two, before: false));
    }
  }

  public class NodesTests
  {
    [Fact]
    public void WithNodes_ReturnsAllNodes()
    {
      const string query = "(1 + (2 * 42)) / 666";

      var expression = new SPARQLQuery<ExpressionContext>(query);
      var nodes = expression.Nodes<ExpressionContext>().ToList();

      Assert.Equal(3, nodes.Count);
    }

    [Fact]
    public void WithoutNodes_ReturnsNoNodes()
    {
      const string query = "(1 + (2 * 42)) / 666";

      var expression = new SPARQLQuery<ExpressionContext>(query);
      var nodes = expression.Nodes<QueryUnitContext>().ToList();

      Assert.Empty(nodes);
    }
  }

  public class SourcesOfTests
  {
    [Fact]
    public void NoSources_ReturnsEmptyList()
    {
      const string SparqlQuery = """
                                 CONSTRUCT {
                                   ?objectUri rdf:type q42:LibraryObject.
                                   ?objectUri q42:isAboutSubject ?subjectUri.
                                   ?objectUri q42:nodeId ?objectId.
                                   ?subjectUri rdf:type q42:Subject.
                                   ?subjectUri q42:nlValue ?subjectValue.
                                   ?subjectUri q42:enValue ?subjectValue.
                                   ?subjectUri q42:nodeId ?subjectId.
                                 } WHERE {
                                   VALUES ?type { schema:Book schema:CreativeWork }
                                 
                                   {
                                     SELECT ?objectUri ?subjectUri
                                     WHERE {
                                       ?objectUri rdf:type ?type.
                                       ?objectUri schema:about ?subjectUri.
                                     }
                                     ORDER BY ?objectUri
                                     LIMIT 100
                                     OFFSET 0
                                   }
                                 
                                   ?subjectUri rdf:type schema:DefinedTerm.
                                   ?subjectUri schema:name ?subjectValue.
                                   FILTER(isIRI(?subjectUri))
                                 
                                   BIND(MD5(STR(?objectUri)) as ?objectId)
                                   BIND(MD5(STR(?subjectUri)) as ?subjectId)
                                 }
                                 """;

      var query = new SPARQLQuery<QueryUnitContext>(SparqlQuery);
      var prefixDeclarations = query.SourcesOf<PrefixDeclContext>();

      Assert.Empty(prefixDeclarations);
    }

    [Fact]
    public void SingleSource_ReturnsSingle()
    {
      const string SparqlQuery = """
                                 # SKIP
                                 #
                                 # PREFIX schema: <http://schema.org/> SELECT (COUNT(?objectUri) AS ?total) WHERE { VALUES ?type { schema:Book schema:CreativeWork } ?objectUri rdf:type ?type. ?objectUri schema:about ?subjectUri. }
                                 # 1412660
                                 #
                                 # 100 -> timeout/error
                                 PREFIX schema: <http://schema.org/>

                                 CONSTRUCT {
                                   ?objectUri rdf:type q42:LibraryObject.
                                   ?objectUri q42:isAboutSubject ?subjectUri.
                                   ?objectUri q42:nodeId ?objectId.
                                   ?subjectUri rdf:type q42:Subject.
                                   ?subjectUri q42:nlValue ?subjectValue.
                                   ?subjectUri q42:enValue ?subjectValue.
                                   ?subjectUri q42:nodeId ?subjectId.
                                 } WHERE {
                                   VALUES ?type { schema:Book schema:CreativeWork }
                                 
                                   {
                                     SELECT ?objectUri ?subjectUri
                                     WHERE {
                                       ?objectUri rdf:type ?type.
                                       ?objectUri schema:about ?subjectUri.
                                     }
                                     ORDER BY ?objectUri
                                     LIMIT 100
                                     OFFSET 0
                                   }
                                 
                                   ?subjectUri rdf:type schema:DefinedTerm.
                                   ?subjectUri schema:name ?subjectValue.
                                   FILTER(isIRI(?subjectUri))
                                 
                                   BIND(MD5(STR(?objectUri)) as ?objectId)
                                   BIND(MD5(STR(?subjectUri)) as ?subjectId)
                                 }
                                 """;

      var query = new SPARQLQuery<QueryUnitContext>(SparqlQuery);
      var prefixDeclarations = query.SourcesOf<PrefixDeclContext>();

      Assert.Single(prefixDeclarations);
      Assert.Equal("PREFIX schema: <http://schema.org/>", prefixDeclarations[0]);
    }

    [Fact]
    public void MultipleSources_ReturnsMultiple()
    {
      const string SparqlQuery = """
                                 # SKIP
                                 #
                                 # PREFIX schema: <http://schema.org/> SELECT (COUNT(?objectUri) AS ?total) WHERE { VALUES ?type { schema:Book schema:CreativeWork } ?objectUri rdf:type ?type. ?objectUri schema:about ?subjectUri. }
                                 # 1412660
                                 #
                                 # 100 -> timeout/error
                                 PREFIX schema: <http://schema.org/>
                                 # Dummy comment
                                 PREFIX q42: <http://www.example.com/>

                                 CONSTRUCT {
                                   ?objectUri rdf:type q42:LibraryObject.
                                   ?objectUri q42:isAboutSubject ?subjectUri.
                                   ?objectUri q42:nodeId ?objectId.
                                   ?subjectUri rdf:type q42:Subject.
                                   ?subjectUri q42:nlValue ?subjectValue.
                                   ?subjectUri q42:enValue ?subjectValue.
                                   ?subjectUri q42:nodeId ?subjectId.
                                 } WHERE {
                                   VALUES ?type { schema:Book schema:CreativeWork }
                                 
                                   {
                                     SELECT ?objectUri ?subjectUri
                                     WHERE {
                                       ?objectUri rdf:type ?type.
                                       ?objectUri schema:about ?subjectUri.
                                     }
                                     ORDER BY ?objectUri
                                     LIMIT 100
                                     OFFSET 0
                                   }
                                 
                                   ?subjectUri rdf:type schema:DefinedTerm.
                                   ?subjectUri schema:name ?subjectValue.
                                   FILTER(isIRI(?subjectUri))
                                 
                                   BIND(MD5(STR(?objectUri)) as ?objectId)
                                   BIND(MD5(STR(?subjectUri)) as ?subjectId)
                                 }
                                 """;

      var query = new SPARQLQuery<QueryUnitContext>(SparqlQuery);
      var prefixDeclarations = query.SourcesOf<PrefixDeclContext>();

      Assert.Equal(2, prefixDeclarations.Count);
      Assert.Equal("PREFIX schema: <http://schema.org/>", prefixDeclarations[0]);
      Assert.Equal("PREFIX q42: <http://www.example.com/>", prefixDeclarations[1]);
    }

    [Fact]
    public void CreateCountQuery_Test()
    {
      const string SparqlQuery = """
                                 # SKIP
                                 #
                                 # PREFIX schema: <http://schema.org/> SELECT (COUNT(?objectUri) AS ?total) WHERE { VALUES ?type { schema:Book schema:CreativeWork } ?objectUri rdf:type ?type. ?objectUri schema:about ?subjectUri. }
                                 # 1412660
                                 #
                                 # 100 -> timeout/error
                                 PREFIX schema: <http://schema.org/>
                                 # Dummy comment
                                 PREFIX q42: <http://www.example.com/>

                                 CONSTRUCT {
                                   ?objectUri rdf:type q42:LibraryObject.
                                   ?objectUri q42:isAboutSubject ?subjectUri.
                                   ?objectUri q42:nodeId ?objectId.
                                   ?subjectUri rdf:type q42:Subject.
                                   ?subjectUri q42:nlValue ?subjectValue.
                                   ?subjectUri q42:enValue ?subjectValue.
                                   ?subjectUri q42:nodeId ?subjectId.
                                 } WHERE {
                                   VALUES ?type { schema:Book schema:CreativeWork }
                                 
                                   {
                                     SELECT ?objectUri ?subjectUri
                                     WHERE {
                                       ?objectUri rdf:type ?type.
                                       ?objectUri schema:about ?subjectUri.
                                     }
                                     ORDER BY ?objectUri
                                     LIMIT 100
                                     OFFSET 0
                                   }
                                 
                                   ?subjectUri rdf:type schema:DefinedTerm.
                                   ?subjectUri schema:name ?subjectValue.
                                   FILTER(isIRI(?subjectUri))
                                 
                                   BIND(MD5(STR(?objectUri)) as ?objectId)
                                   BIND(MD5(STR(?subjectUri)) as ?subjectId)
                                 }
                                 """;

      var query = new SPARQLQuery<QueryUnitContext>(SparqlQuery);

      var prefixDeclarations = query.SourcesOf<PrefixDeclContext>();
      var inlineData = query.SourcesOf<InlineDataContext>().SingleOrDefault() ?? string.Empty;
      var subSelectWhere = query.SourcesOf<SubSelectContext, WhereClauseContext>().SingleOrDefault() ?? string.Empty;

      var countSparqlQuery = new SPARQLQuery<QueryUnitContext>($"{string.Join("\n", prefixDeclarations)}\nSELECT (COUNT(?objectUri) AS ?total)\n{subSelectWhere}");

      if (!string.IsNullOrWhiteSpace(inlineData))
      {
        var openBrace = countSparqlQuery.Tokens().First(t => t.Type == SPARQLLexer.OBRACE);
        countSparqlQuery = countSparqlQuery.Insert<QueryUnitContext>(inlineData, openBrace, before: false);
      }

      const string expected = """
                     PREFIX schema: <http://schema.org/>
                     PREFIX q42: <http://www.example.com/>
                     SELECT (COUNT(?objectUri) AS ?total)
                     WHERE {VALUES ?type { schema:Book schema:CreativeWork }
                           ?objectUri rdf:type ?type.
                           ?objectUri schema:about ?subjectUri.
                         }
                     """;

      Assert.Equal(expected, countSparqlQuery.ToQueryString());
    }

    [Fact]
    public void CreateCountQuery_NoInlineDataTest()
    {
      const string SparqlQuery = """
                                 PREFIX schema: <http://schema.org/>
                                 PREFIX q42: <http://www.example.com/>

                                 CONSTRUCT {
                                   ?objectUri rdf:type q42:LibraryObject.
                                   ?objectUri q42:isAboutSubject ?subjectUri.
                                   ?objectUri q42:nodeId ?objectId.
                                   ?subjectUri rdf:type q42:Subject.
                                   ?subjectUri q42:nlValue ?subjectValue.
                                   ?subjectUri q42:enValue ?subjectValue.
                                   ?subjectUri q42:nodeId ?subjectId.
                                 } WHERE {
                                   {
                                     SELECT ?objectUri ?subjectUri
                                     WHERE {
                                       ?objectUri rdf:type ?type.
                                       ?objectUri schema:about ?subjectUri.
                                     }
                                     ORDER BY ?objectUri
                                     LIMIT 100
                                     OFFSET 0
                                   }
                                 
                                   ?subjectUri rdf:type schema:DefinedTerm.
                                   ?subjectUri schema:name ?subjectValue.
                                   FILTER(isIRI(?subjectUri))
                                 
                                   BIND(MD5(STR(?objectUri)) as ?objectId)
                                   BIND(MD5(STR(?subjectUri)) as ?subjectId)
                                 }
                                 """;

      var query = new SPARQLQuery<QueryUnitContext>(SparqlQuery);

      var prefixDeclarations = query.SourcesOf<PrefixDeclContext>();
      var inlineData = query.SourcesOf<InlineDataContext>().SingleOrDefault() ?? string.Empty;
      var subSelectWhere = query.SourcesOf<SubSelectContext, WhereClauseContext>().SingleOrDefault() ?? string.Empty;

      var countSparqlQuery = new SPARQLQuery<QueryUnitContext>($"{string.Join("\n", prefixDeclarations)}\nSELECT (COUNT(?objectUri) AS ?total)\n{subSelectWhere}");

      if (!string.IsNullOrWhiteSpace(inlineData))
      {
        var openBrace = countSparqlQuery.Tokens().First(t => t.Type == SPARQLLexer.OBRACE);
        countSparqlQuery = countSparqlQuery.Insert<QueryUnitContext>(inlineData, openBrace, before: false);
      }

      const string expected = """
                     PREFIX schema: <http://schema.org/>
                     PREFIX q42: <http://www.example.com/>
                     SELECT (COUNT(?objectUri) AS ?total)
                     WHERE {
                           ?objectUri rdf:type ?type.
                           ?objectUri schema:about ?subjectUri.
                         }
                     """;

      Assert.Equal(expected, countSparqlQuery.ToQueryString());
    }

    [Fact]
    public void CreateCountQuery_MissingParentNodeThrowsException()
    {
      const string SparqlQuery = """
                                 PREFIX schema: <http://schema.org/>
                                 PREFIX q42: <http://www.example.com/>

                                 CONSTRUCT {
                                   ?objectUri rdf:type q42:LibraryObject.
                                   ?objectUri q42:isAboutSubject ?subjectUri.
                                   ?objectUri q42:nodeId ?objectId.
                                   ?subjectUri rdf:type q42:Subject.
                                   ?subjectUri q42:nlValue ?subjectValue.
                                   ?subjectUri q42:enValue ?subjectValue.
                                   ?subjectUri q42:nodeId ?subjectId.
                                 } WHERE {
                                   ?subjectUri rdf:type schema:DefinedTerm.
                                   ?subjectUri schema:name ?subjectValue.
                                   FILTER(isIRI(?subjectUri))
                                 
                                   BIND(MD5(STR(?objectUri)) as ?objectId)
                                   BIND(MD5(STR(?subjectUri)) as ?subjectId)
                                 }
                                 """;

      var query = new SPARQLQuery<QueryUnitContext>(SparqlQuery);

      Assert.Throws<ArgumentException>(() => query.SourcesOf<SubSelectContext, WhereClauseContext>().SingleOrDefault());
    }

    [Fact]
    public void CreateCountQuery_MissingChildNodeThrowsException()
    {
      const string SparqlQuery = """
                                 PREFIX schema: <http://schema.org/>
                                 PREFIX q42: <http://www.example.com/>

                                 CONSTRUCT {
                                   ?objectUri rdf:type q42:LibraryObject.
                                   ?objectUri q42:isAboutSubject ?subjectUri.
                                   ?objectUri q42:nodeId ?objectId.
                                   ?subjectUri rdf:type q42:Subject.
                                   ?subjectUri q42:nlValue ?subjectValue.
                                   ?subjectUri q42:enValue ?subjectValue.
                                   ?subjectUri q42:nodeId ?subjectId.
                                 } WHERE {
                                   {
                                     SELECT ?objectUri ?subjectUri
                                     WHERE {
                                       ?objectUri rdf:type ?type.
                                       ?objectUri schema:about ?subjectUri.
                                     }
                                     ORDER BY ?objectUri
                                     LIMIT 100
                                     OFFSET 0
                                   }
                                 
                                   ?subjectUri rdf:type schema:DefinedTerm.
                                   ?subjectUri schema:name ?subjectValue.
                                   FILTER(isIRI(?subjectUri))
                                 
                                   BIND(MD5(STR(?objectUri)) as ?objectId)
                                   BIND(MD5(STR(?subjectUri)) as ?subjectId)
                                 }
                                 """;

      var query = new SPARQLQuery<QueryUnitContext>(SparqlQuery);

      Assert.Throws<ArgumentException>(() => query.SourcesOf<SubSelectContext, FilterContext>().FirstOrDefault());
    }
  }

  public class ReplaceTests
  {
    [Fact]
    public void SameContexts_ThrowsException()
    {
      const string queryString = "SELECT ?name WHERE { ?person foaf:name ?name . }";

      var query = new SPARQLQuery<QueryUnitContext>(queryString);
      var contextToRemove = query.Nodes<GroupGraphPatternSubContext>().Single();

      Assert.Throws<Exception>(() => query.Replace(contextToRemove, contextToRemove));
    }

    [Fact]
    public void ContextNotPartOfQuery_ThrowsException()
    {
      const string queryString = "SELECT ?name WHERE { ?person foaf:name ?name . }";

      var query = new SPARQLQuery<QueryUnitContext>(queryString);
      var contextToRemove = query.Nodes<WhereClauseContext>().Single();
      var contextToInsert = new SPARQLQuery<WhereClauseContext>("WHERE { ?p foaf:name ?n . }").Context;

      Assert.Throws<Exception>(() => query.Replace(contextToRemove, contextToInsert));
    }

    [Fact]
    public void ContextsPartOfQuery_ReplacesContext()
    {
      const string queryString = "SELECT ?name WHERE { ?person foaf:name ?name . }";

      var query = new SPARQLQuery<QueryUnitContext>(queryString);
      var varContexts = query.Nodes<VarContext>().ToList();
      var rewrittenQuery = query.Replace(varContexts[0], varContexts[1]);

      Assert.Equal("SELECT ?name WHERE { ?person foaf:name ?name . }", query.ToQueryString());
      Assert.Equal("SELECT ?person WHERE { ?person foaf:name ?name . }", rewrittenQuery.ToQueryString());
    }
  }
}
