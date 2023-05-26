using Antlr4.Runtime;
using BigO.SPARQLParser.Extensions;

namespace BigO.SPARQLParser.Parser;

public class ParsedQuery<C> where C : ParserRuleContext
{
    private readonly C context;
    private readonly IList<IToken> tokens;
    
    public ParsedQuery(string query)
    {
        this.context = query.ParseAs<C>();
        this.tokens = query.Tokens();
    }

    public ParsedQuery<C> InsertBefore<N>(IToken beforeToken, string query) where N : ParserRuleContext
    {
        query.ParseAs<N>();
        
        this.tokens.InsertBefore(beforeToken, query);
        
        return this;
    }

    public N? FirstNodeOrDefault<N>() where N : ParserRuleContext => this.context.FirstNodeOrDefault<N>();
    
    public string ToQuery() => this.tokens.ToQuery();
}