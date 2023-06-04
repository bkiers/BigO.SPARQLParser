namespace BigO.SPARQLParser.Tests.Extensions;

using BigO.SPARQLParser.Extensions;
using BigO.SPARQLParser.Parser;
using Xunit;

public static class EnumerableExtensionsTests
{
  public class InsertTokensTests
  {
    [Fact]
    public void MissingToken_ThrowsException()
    {
      var missingToken = "?name".Tokenize().First();
      var tokens = "1 + 2".Tokenize();

      Assert.Throws<ArgumentException>(() => tokens.InsertTokens("-42".Tokenize(), missingToken, false));
    }

    [Fact]
    public void BeforeFalse_AppendsToken()
    {
      var tokens = "1+".Tokenize();
      var addToken = tokens.Single(t => t.Type == SPARQLLexer.ADD);
      var newTokens = tokens.InsertTokens("?Q".Tokenize(), addToken, false);

      Assert.Equal("1+?Q", newTokens.ToQueryString());
    }

    [Fact]
    public void BeforeTrue_PrependsToken()
    {
      var tokens = "+ 1".Tokenize();
      var addToken = tokens.Single(t => t.Type == SPARQLLexer.ADD);
      var newTokens = tokens.InsertTokens("?Q".Tokenize(), addToken, true);

      Assert.Equal("?Q+ 1", newTokens.ToQueryString());
    }
  }

  public class ToQueryStringTests
  {
    [Fact]
    public void SkipCommentsTrue_RemovesComment()
    {
      var tokens = "1 + 2 # comment".Tokenize();

      Assert.Equal("1 + 2", tokens.ToQueryString(skipComments: true));
    }

    [Fact]
    public void SkipCommentsFalse_LeavesComment()
    {
      var tokens = "1 + 2 # comment".Tokenize();

      Assert.Equal("1 + 2 # comment", tokens.ToQueryString(skipComments: false));
    }
  }

  public class AreEqualTests
  {
    [Fact]
    public void DifferentTokenSequence_ReturnsFalse()
    {
      var tokens1 = "+ 1".Tokenize();
      var tokens2 = "1 +".Tokenize();

      Assert.False(tokens1.AreEqualTokens(tokens2, ignoreHiddenChannel: true));
    }

    [Fact]
    public void IgnoreHiddenChannelTrue_ReturnsTrueForSimilarTokens()
    {
      var tokens1 = "1    +   2 # comment".Tokenize();
      var tokens2 = "1 + 2".Tokenize();

      Assert.True(tokens1.AreEqualTokens(tokens2, ignoreHiddenChannel: true));
    }

    [Fact]
    public void IgnoreHiddenChannelFalse_ReturnsFalseForSimilarTokens()
    {
      var tokens1 = "1    +   2 # comment".Tokenize();
      var tokens2 = "1 + 2".Tokenize();

      Assert.False(tokens1.AreEqualTokens(tokens2, ignoreHiddenChannel: false));
    }
  }
}
