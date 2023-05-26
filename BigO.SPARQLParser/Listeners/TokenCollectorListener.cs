using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BigO.SPARQLParser.Parser;

namespace BigO.SPARQLParser.Listeners;

public class TokenCollectorListener : SPARQLParserBaseListener
{
    private readonly int tokenType;
    private readonly string? text;
    private readonly StringComparison stringComparison;
        
    public TokenCollectorListener(int tokenType, string? text, StringComparison stringComparison)
    {
        this.tokenType = tokenType;
        this.text = text;
        this.stringComparison = stringComparison;
    }

    public List<IToken> Tokens { get; } = new();

    public override void VisitTerminal(ITerminalNode node)
    {
        if (node.Symbol.Type == this.tokenType && (string.IsNullOrEmpty(this.text) || string.Equals(node.Symbol.Text, this.text, this.stringComparison)))
        {
            this.Tokens.Add(node.Symbol);
        }
    }
}