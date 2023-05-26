using System.Text;
using Antlr4.Runtime;
using BigO.SPARQLParser.Parser;

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

    public static string ToQuery<T>(this IEnumerable<T> tokens, bool skipComments = true) where T : IToken
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