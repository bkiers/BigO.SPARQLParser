namespace BigO.SPARQLParser.Tests.Extensions;

using BigO.SPARQLParser.Extensions;
using BigO.SPARQLParser.Parser;
using Xunit;
using static BigO.SPARQLParser.Parser.SPARQLParser;

public static class ParserRuleContextExtensionsTests
{
  public class FindTokensTests
  {
    [Fact]
    public void MissingTokenType_ReturnsEmptyList()
    {
      var tokens = "1 + 2".ParseAs<ExpressionContext>().FindTokens(SPARQLLexer.VAR1);

      Assert.Empty(tokens);
    }

    [Fact]
    public void TokenType_ReturnsExpectedResults()
    {
      var tokens = "1 + 2".ParseAs<ExpressionContext>().FindTokens(SPARQLLexer.INTEGER);

      Assert.Equal(2, tokens.Count());
    }

    [Fact]
    public void TokenTypeAndText_ReturnsExpectedResults()
    {
      var tokens = "?foo + ?FOO".ParseAs<ExpressionContext>().FindTokens(SPARQLLexer.VAR1, text: "?foo");

      Assert.Single(tokens);
    }

    [Fact]
    public void TokenTypeAndTextIgnoreCase_ReturnsExpectedResults()
    {
      var tokens = "?foo + ?FOO".ParseAs<ExpressionContext>().FindTokens(SPARQLLexer.VAR1, text: "?foo",
        stringComparison: StringComparison.OrdinalIgnoreCase);

      Assert.Equal(2, tokens.Count());
    }

    [Fact]
    public void Tokens_EmptyString_ReturnsEofToken()
    {
      // PrologueContext matches an empty string!
      var tokens = string.Empty.ParseAs<PrologueContext>().FindTokens(SPARQLLexer.INTEGER);

      Assert.Empty(tokens);
    }
  }

  public class AllTokensTests
  {
    [Fact]
    public void Expression_ReturnsAllTerminalTokens()
    {
      // PrologueContext matches an empty string!
      var tokens = "1 + $mu".ParseAs<ExpressionContext>().AllTokens().ToList();

      Assert.Equal(3, tokens.Count);
      Assert.Equal(SPARQLLexer.INTEGER, tokens[0].Type);
      Assert.Equal(SPARQLLexer.ADD, tokens[1].Type);
      Assert.Equal(SPARQLLexer.VAR2, tokens[2].Type);
    }
  }

  public class NodesTests
  {
    [Fact]
    public void EmptyString_ReturnsEofToken()
    {
      // PrologueContext matches an empty string!
      var nodes = string.Empty.ParseAs<PrologueContext>().Nodes<ExpressionContext>();

      Assert.Empty(nodes);
    }

    [Fact]
    public void NestedExpression_ReturnExpectedNodes()
    {
      var nodes = "1 + 2 - 3"
        .ParseAs<ExpressionContext>()
        .Nodes<AdditiveExpressionContext>()
        .ToList();

      Assert.Single(nodes);
    }

    [Fact]
    public void ComplexerQuery_ReturnExpectedNodes()
    {
      const string q = @"SELECT ?person ?personLabel ?personDescription ?age
      WHERE
      {
        ?person wdt:P31 wd:Q5;
                wdt:P569 ?born;
                wdt:P570 ?died;
                wdt:P1196 wd:Q8454.
        BIND(?died - ?born AS ?ageInDays).
        BIND(?ageInDays/365.2425 AS ?ageInYears).
        BIND(FLOOR(?ageInYears) AS ?age).
      }";

      var nodes = q.ParseAs<QueryUnitContext>()
        .Nodes<ExpressionContext>()
        .ToList();

      Assert.Equal(4, nodes.Count);
      Assert.Equal("?died-?born", nodes[0].GetText());
      Assert.Equal("?ageInDays/365.2425", nodes[1].GetText());
      Assert.Equal("FLOOR(?ageInYears)", nodes[2].GetText());
      Assert.Equal("?ageInYears", nodes[3].GetText());
    }
  }
}
