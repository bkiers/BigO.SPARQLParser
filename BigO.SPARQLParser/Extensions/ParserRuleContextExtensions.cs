using System.Text;

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

  /// <summary>
  /// Returns true iff `root` contains a context that is semantically the same as `child`.
  /// </summary>
  public static bool ContainsContext<C>(this ParserRuleContext root, C child)
    where C : ParserRuleContext
  {
    var candidates = root.Nodes<C>();

    return candidates.Any(c => c.IsEqualTo(child));
  }

  /// <summary>
  /// Returns true iff 2 context-nodes are semantically the same.
  /// </summary>
  public static bool IsEqualTo(this ParserRuleContext? node1, ParserRuleContext? node2)
  {
    if (node1 == node2)
    {
      // Same instance, or both `null`
      return true;
    }

    if (node1 == null || node2 == null || node1.ChildCount != node2.ChildCount || node1.GetType() != node2.GetType())
    {
      return false;
    }

    var result = true;

    for (var index = 0; index < node1.ChildCount; index++)
    {
      var child1 = node1.children[index];
      var child2 = node2.children[index];

      switch (child1)
      {
        case ParserRuleContext context1 when child2 is ParserRuleContext context2:
          result = result && context1.IsEqualTo(context2);
          break;
        case TerminalNodeImpl token1 when child2 is TerminalNodeImpl token2:
          result = result && token1.Payload.Type == token2.Payload.Type && token1.Payload.Text == token2.Payload.Text;
          break;
        default:
          return false;
      }
    }

    return result;
  }
}
