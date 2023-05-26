using System.Collections.Immutable;
using Antlr4.Runtime;
using BigO.SPARQLParser.Parser;

namespace BigO.SPARQLParser.Listeners;

public class NodeCollectorListener<C> : SPARQLParserBaseListener where C : ParserRuleContext
{
    private readonly List<C> contexts = new();
    
    public ImmutableList<C> Contexts => this.contexts.ToImmutableList();

    public override void EnterEveryRule(ParserRuleContext context)
    {
        if (context is C contextType)
        {
            this.contexts.Add(contextType);
        }
    }
}