using System.Text;
using Antlr4.Runtime;

namespace BigO.SPARQLParser.Extensions;

public static class ListExtensions
{
    public static void InsertBefore(this IList<IToken> tokens, IToken beforeToken, string query)
    {
        var index = tokens.IndexOfToken(beforeToken);
        var n = 0;

        if (index < 0)
        {
            throw new Exception($"Could not find token '{beforeToken}' in query '{query}'");
        }

        var newTokens = query.Tokens();
        
        foreach (var newToken in newTokens)
        {
            tokens.Insert(index - 1 + n, newToken);
            n++;
        }
    }

    public static string ToQuery<T>(this IEnumerable<T> tokens) where T : IToken
    {
        var builder = new StringBuilder();

        foreach (var token in tokens)
        {
            // TODO Eof explanation
            builder.Append(token.Type == Lexer.Eof ? string.Empty : token.Text);
        }
        
        return builder.ToString();
    }
    
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