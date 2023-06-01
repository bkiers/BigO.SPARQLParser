namespace BigO.SPARQLParser.Listeners;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BigO.SPARQLParser.Parser;

internal class AllTokensCollectorListener : SPARQLParserBaseListener
{
  public List<IToken> Tokens { get; } = new();

  public override void VisitTerminal(ITerminalNode node)
  {
    this.Tokens.Add(node.Symbol);
  }
}
