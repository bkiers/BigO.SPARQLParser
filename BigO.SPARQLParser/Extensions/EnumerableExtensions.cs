namespace BigO.SPARQLParser.Extensions;

using System.Text;
using Antlr4.Runtime;
using BigO.SPARQLParser.Parser;

internal static class EnumerableExtensions
{
  /// <summary>
  /// Inserts a list of tokens before or after a certain token.
  /// </summary>
  public static IEnumerable<IToken> InsertTokens(this IEnumerable<IToken> tokens, IEnumerable<IToken> extraTokens,
    IToken token, bool before)
  {
    var newTokens = new List<IToken>(tokens);
    var index = newTokens.IndexOfToken(token);

    if (index < 0)
    {
      throw new ArgumentException($"Could not find token '{token}'");
    }

    var indexOffset = before ? 0 : 1;

    foreach (var (newToken, n) in extraTokens.Select((t, i) => (t, i)))
    {
      newTokens.Insert(index + indexOffset + n, newToken);
    }

    return newTokens;
  }

  /// <summary>
  /// Converts a given list of tokens back into the GRAPHQL source, optionally including the comments.
  /// </summary>
  public static string ToQueryString<T>(this IEnumerable<T> tokens, bool skipComments = true)
    where T : IToken
  {
    var builder = new StringBuilder();

    foreach (var token in tokens)
    {
      if (skipComments && token.Type == SPARQLLexer.COMMENT)
      {
        continue;
      }

      // Just skip any end-of-file tokens, do not break out of the loop! It can happen that
      // more than 1 EOF token exists: in case SPARQL code is inserted, the extra pieces of
      // code will all have their own EOF token.
      builder.Append(token.Type == Lexer.Eof ? string.Empty : token.Text);
    }

    return builder.ToString().Trim();
  }

  /// <summary>
  /// Returns true iff two enumerable tokens are equal, optionally ignoring tokens placed on
  /// the `Hidden` channel (like comments and white space chars)
  /// </summary>
  public static bool AreEqualTokens(this IEnumerable<IToken> thisTokens, IEnumerable<IToken> thatTokens,
    bool ignoreHiddenChannel = true)
  {
    var normalizedThisTokens = thisTokens
      .Where(t => t.Type != Lexer.Eof && (!ignoreHiddenChannel || t.Channel != Lexer.Hidden))
      .ToList();

    var normalizedThatTokens = thatTokens
      .Where(t => t.Type != Lexer.Eof && (!ignoreHiddenChannel || t.Channel != Lexer.Hidden))
      .ToList();

    if (normalizedThisTokens.Count != normalizedThatTokens.Count)
    {
      // No need to compare token types when the lists do not have the same amount of tokens
      return false;
    }

    return !normalizedThisTokens.Where((t, i) => t.Type != normalizedThatTokens[i].Type).Any();
  }

  // Get the index of a given token by its type, line- and column-number (which must be unique), or -1
  // if it does not exist
  private static int IndexOfToken<T>(this IList<T> tokens, IToken token) where T : IToken
  {
    for (var i = 0; i < tokens.Count; i++)
    {
      var t = tokens[i];

      if (t.Type == token.Type && t.Line == token.Line && t.Column == token.Column)
      {
        return i;
      }
    }

    return -1;
  }
}
