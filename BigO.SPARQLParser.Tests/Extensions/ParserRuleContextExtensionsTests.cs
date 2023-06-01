namespace BigO.SPARQLParser.Tests.Extensions;

using BigO.SPARQLParser.Extensions;
using BigO.SPARQLParser.Parser;
using Xunit;
using static BigO.SPARQLParser.Parser.SPARQLParser;

public class ParserRuleContextExtensionsTests
{
  [Fact]
  public void FindTokens_MissingTokenType_ReturnsEmptyList()
  {
    var tokens = "1 + 2".ParseAs<ExpressionContext>().FindTokens(SPARQLLexer.VAR1);

    Assert.Empty(tokens);
  }

  [Fact]
  public void FindTokens_TokenType_ReturnsExpectedResults()
  {
    var tokens = "1 + 2".ParseAs<ExpressionContext>().FindTokens(SPARQLLexer.INTEGER);

    Assert.Equal(2, tokens.Count());
  }

  [Fact]
  public void FindTokens_TokenTypeAndText_ReturnsExpectedResults()
  {
    var tokens = "?foo + ?FOO".ParseAs<ExpressionContext>().FindTokens(SPARQLLexer.VAR1, text: "?foo");

    Assert.Single(tokens);
  }

  [Fact]
  public void FindTokens_TokenTypeAndTextIgnoreCase_ReturnsExpectedResults()
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

  [Fact]
  public void Tokens_Expression_ReturnsAllTerminalTokens()
  {
    // PrologueContext matches an empty string!
    var tokens = "1 + $mu".ParseAs<ExpressionContext>().Tokens().ToList();

    Assert.Equal(3, tokens.Count);
    Assert.Equal(SPARQLLexer.INTEGER, tokens[0].Type);
    Assert.Equal(SPARQLLexer.ADD, tokens[1].Type);
    Assert.Equal(SPARQLLexer.VAR2, tokens[2].Type);
  }

  [Fact]
  public void Nodes_EmptyString_ReturnsEofToken()
  {
    // PrologueContext matches an empty string!
    var nodes = string.Empty.ParseAs<PrologueContext>().Nodes<ExpressionContext>();

    Assert.Empty(nodes);
  }

  [Fact]
  public void Nodes_NestedExpression_ReturnExpectedNodes()
  {
    var nodes = "1 + 2 - 3"
      .ParseAs<ExpressionContext>()
      .Nodes<AdditiveExpressionContext>()
      .ToList();

    Assert.Single(nodes);
  }

  [Fact]
  public void Nodes_ComplexerQuery_ReturnExpectedNodes()
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
