using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BigO.SPARQLParser.Listeners;

namespace BigO.SPARQLParser.Extensions;

public static class ParserRuleContextExtensions
{
    public static IList<IToken> FindTokens(this ParserRuleContext context, int tokenType, string? text = null, StringComparison stringComparison = StringComparison.Ordinal)
    {
        var listener = new TokenCollectorListener(tokenType, text, stringComparison);
        
        ParseTreeWalker.Default.Walk(listener, context);
        
        return listener.Tokens;
    }

    public static C? FirstNodeOrDefault<C>(this ParserRuleContext context) where C : ParserRuleContext
    {
        var listener = new NodeCollectorListener<C>();
        
        ParseTreeWalker.Default.Walk(listener, context);
        
        return listener.Contexts.FirstOrDefault();
    }
}