namespace BigO.SPARQLParser.Tests.Extensions;

using BigO.SPARQLParser.Exceptions;
using BigO.SPARQLParser.Extensions;
using BigO.SPARQLParser.Parser;
using Xunit;
using static BigO.SPARQLParser.Parser.SPARQLParser;

public class StringExtensionsTests
{
  [Fact]
  public void Tokens_EmptyString_ReturnsEofToken()
  {
    var tokens = string.Empty.Tokens();

    Assert.Single(tokens);
    Assert.Equal(SPARQLLexer.Eof, tokens.Single().Type);
  }

  [Fact]
  public void Tokens_WithSpaces_KeepsSpacesTokens()
  {
    var tokens = "1 2\t3\n4".Tokens();

    Assert.Equal(8, tokens.Count);
    Assert.Equal(SPARQLLexer.INTEGER, tokens[0].Type);
    Assert.Equal(SPARQLLexer.SPACES, tokens[1].Type);
    Assert.Equal(SPARQLLexer.INTEGER, tokens[2].Type);
    Assert.Equal(SPARQLLexer.SPACES, tokens[3].Type);
    Assert.Equal(SPARQLLexer.INTEGER, tokens[4].Type);
    Assert.Equal(SPARQLLexer.SPACES, tokens[5].Type);
    Assert.Equal(SPARQLLexer.INTEGER, tokens[6].Type);
    Assert.Equal(SPARQLLexer.Eof, tokens[7].Type);
  }

  [Fact]
  public void ParseAs_Expression_ReturnsCorrectContext()
  {
    var expression = "?Q + 42".ParseAs<ExpressionContext>();

    Assert.NotNull(expression);
  }

  [Fact]
  public void ParseAs_ExpressionWithComment_ReturnsCorrectContext()
  {
    var expression = "1 + 2 # ignore me!".ParseAs<ExpressionContext>();

    Assert.NotNull(expression);
  }

  [Fact]
  public void ParseAs_WrongContext_ThrowsException()
  {
    Assert.Throws<ParseException>(() => "1 + 2".ParseAs<VarContext>());
  }

  [Fact]
  public void ParseAs_NotAllTokensConsumed_DoesNotThrowException()
  {
    var varContext = "?Q + 42".ParseAs<VarContext>();

    // Only "?Q" is present
    Assert.Equal(1, varContext.ChildCount);
    Assert.Equal("?Q", varContext.children.Single().GetText());
  }
}
