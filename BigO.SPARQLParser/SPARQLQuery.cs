namespace BigO.SPARQLParser;

using Antlr4.Runtime;
using BigO.SPARQLParser.Exceptions;
using BigO.SPARQLParser.Extensions;
using BigO.SPARQLParser.Parser;

public class SPARQLQuery<C>
  where C : ParserRuleContext
{
  private readonly C context;
  private readonly IList<IToken> tokens;

  /// <summary>
  /// Creates a new SPARQLQuery instance from a given SPARQL query string. The generic `C` indicates
  /// how the query string should be parsed. If after parsing there are still tokens not consumed, an
  /// exception is thrown. For example, parsing the input string "1 + 2 ." as an expression
  /// (`ExpressionContext` to be precise) will result in an exception because the trailing `.` is not
  /// consumed after parsing "1 + 2" as an expression
  /// </summary>
  public SPARQLQuery(string query)
  {
    var originalQuery = query.Trim();
    this.context = originalQuery.ParseAs<C>();
    this.tokens = originalQuery.Tokens();

    if (!this.tokens.AreEqualTokens(this.context.Tokens()))
    {
      throw new ParseException($"'{query}' is not completely parsed by '{typeof(C).MethodName()}'");
    }
  }

  /// <summary>
  /// Returns a list of possible `C` parser rule contexts that can be used as generic `SPARQLQuery` parameter
  /// </summary>
  public static IEnumerable<Type> ParserRules
    => typeof(SPARQLParser).Assembly
      .GetExportedTypes()
      .Where(t => typeof(ParserRuleContext).IsAssignableFrom(t));

  /// <summary>
  /// Inserts a given node `N` query in this current node `C` query-object and returns the result as a new
  /// `R` query-object
  /// </summary>
  public SPARQLQuery<R> InsertQuery<N, R>(string query, IToken beforeToken, bool before)
    where N : ParserRuleContext // The type of the context `query` should be parsed as
    where R : ParserRuleContext // The type of the context to be returned (what `C` and `N` are together)
  {
    // We're not doing anything with the result of this ParseAs<N>() call, but this makes sure the `query`
    // can really be parsed as an `N` parser rule context (if not, an exception is thrown)
    query.ParseAs<N>();

    var newTokens = this.tokens.InsertQuery(query, beforeToken, before);
    var completeQuery = newTokens.ToQueryString(skipComments: false);

    return new SPARQLQuery<R>(completeQuery);
  }

  /// <summary>
  /// Return all parse tree nodes of this parser rule context
  /// </summary>
  public IEnumerable<N> Nodes<N>()
    where N : ParserRuleContext
    => this.context.Nodes<N>();

  /// <summary>
  /// Return this parser rule context
  /// </summary>
  public string ToQueryString<N>(bool skipComments = true)
    where N : ParserRuleContext
  {
    var query = this.tokens.ToQueryString(skipComments);

    // Make sure the `query` we're about to return is a valid `N` parser rule context
    query.ParseAs<N>();

    return query;
  }
}
