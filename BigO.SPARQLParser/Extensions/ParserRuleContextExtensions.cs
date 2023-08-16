namespace BigO.SPARQLParser.Extensions;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BigO.SPARQLParser.Listeners;

public static class ParserRuleContextExtensions
{
  /// <summary>
  /// Returns a list of tokens of a certain type (and optionally with a certain text) from a given context
  /// </summary>
  public static IEnumerable<IToken> FindTokens<C>(this C context, int tokenType, string? text = null,
    StringComparison stringComparison = StringComparison.Ordinal)
    where C : ParserRuleContext
  {
    var listener = new TokenCollectorListener(tokenType, text, stringComparison);

    ParseTreeWalker.Default.Walk(listener, context);

    return listener.Tokens;
  }

  /// <summary>
  /// Returns a list of all tokens from a given `context`, without the tokens from the `Hidden` channel!
  /// </summary>
  public static IEnumerable<IToken> AllTokens<C>(this C context)
    where C : ParserRuleContext
  {
    var listener = new AllTokensCollectorListener();

    ParseTreeWalker.Default.Walk(listener, context);

    return listener.Tokens;
  }

  /// <summary>
  /// Returns a list of (child) `C` nodes/contexts present in the given (parent) node/context
  /// </summary>
  public static IEnumerable<C> Nodes<C>(this ParserRuleContext context)
    where C : ParserRuleContext
  {
    var listener = new NodeCollectorListener<C>();

    ParseTreeWalker.Default.Walk(listener, context);

    return listener.Nodes;
  }
}
