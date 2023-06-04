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
      Assert.Equal(SPARQLLexer.SPACES, tokens[1].Type);
      Assert.Equal(SPARQLLexer.INTEGER, tokens[2].Type);
      Assert.Equal(SPARQLLexer.SPACES, tokens[3].Type);
      Assert.Equal(SPARQLLexer.INTEGER, tokens[4].Type);
      Assert.Equal(SPARQLLexer.SPACES, tokens[5].Type);
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
}
