using System.Text.RegularExpressions;
using Antlr4.Runtime;

namespace BigO.SPARQLParser.Extensions;

using BigO.SPARQLParser.Parser;

public static class StringExtensions
{
    public static IList<IToken> Tokens(this string sparqlQuery)
    {
        var lexer = new SPARQLLexer(CharStreams.fromString(sparqlQuery));
        var tokenStream = new CommonTokenStream(lexer);
        
        tokenStream.Fill();
        
        return tokenStream.GetTokens();
    }

    public static C ParseAs<C>(this string query) where C : ParserRuleContext
    {
        string MethodName()
        {
            var name = Regex.Replace(typeof(C).Name, @"Context$", "");
            var camelCased = $"{name[0].ToString().ToLower()}{name[1..]}";
        
            return camelCased;
        }
        
        try
        {
            var lexer = new SPARQLLexer(CharStreams.fromString(query));
            var parser = new SPARQLParser(new CommonTokenStream(lexer));

            parser.RemoveErrorListeners();
        
            parser.ErrorHandler = new BailErrorStrategy();

            var method = parser.GetType().GetMethod(MethodName())!;
            var context = method.Invoke(parser, null) as C;
        
            return context!;
        }
        catch (Exception e)
        {
            throw new ArgumentException($"SPARQL code '{query}' could not be parsed as a '{MethodName()}' production: {e.InnerException}");
        }
    }
}