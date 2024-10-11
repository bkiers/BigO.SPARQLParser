using BigO.SPARQLParser.Extensions;
using BigO.SPARQLParser.Parser;
using Xunit;

namespace BigO.SPARQLParser.Tests.Parser;

public class SPARQLLexerTests
{
  [Theory]
  [InlineData(@"' \\ '", SPARQLLexer.STRING_LITERAL1)]
  [InlineData(@""" \\ """, SPARQLLexer.STRING_LITERAL2)]
  [InlineData(@"''' foo \\ bar '''", SPARQLLexer.STRING_LITERAL_LONG1)]
  [InlineData(@""""""" mu \\ foo """"""", SPARQLLexer.STRING_LITERAL_LONG2)]
  public void StringLiterals_WithEscapedBackslash_ShouldBeAccepted(string stringLiteral, int expectedTokenType)
  {
    var tokens = stringLiteral.Tokenize();

    Assert.Equal(2, tokens.Count); // the string token + EOF token
    Assert.Equal(expectedTokenType, tokens[0].Type);
  }
}
