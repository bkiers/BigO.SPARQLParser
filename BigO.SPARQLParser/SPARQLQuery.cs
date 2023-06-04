namespace BigO.SPARQLParser;

using System.Collections.ObjectModel;
using Antlr4.Runtime;
using BigO.SPARQLParser.Exceptions;
using BigO.SPARQLParser.Extensions;
using BigO.SPARQLParser.Parser;

public class SPARQLQuery<C>
  where C : ParserRuleContext
{
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
    this.Context = query.Trim().ParseAs<C>();
    this.tokens = query.Trim().Tokenize();

    if (!this.tokens.AreEqualTokens(this.Context.AllTokens()))
    {
      throw new ParseException($"'{query.Trim()}' is not completely parsed by '{typeof(C).MethodName()}'");
    }
  }

  /// <summary>
  /// The parser context (parse tree)
  /// </summary>
  public C Context { get; }

  /// <summary>
  /// Returns a list of possible `C` parser rule contexts that can be used as generic `SPARQLQuery` parameter
  /// </summary>
  public IEnumerable<Type> ParserRules
    => typeof(SPARQLParser).Assembly
      .GetExportedTypes()
      .Where(t => typeof(ParserRuleContext).IsAssignableFrom(t));

  /// <summary>
  /// A shallow copy of the tokens
  /// </summary>
  public IReadOnlyList<IToken> Tokens(bool includeHiddenTokens = true)
    => new ReadOnlyCollection<IToken>(includeHiddenTokens
      ? this.tokens
      : this.tokens.Where(t => t.Channel != Lexer.Hidden).ToList());

  /// <summary>
  /// TODO
  /// </summary>
  public IReadOnlyList<IToken> FindTokens(int tokenType, string? text = null,
    StringComparison stringComparison = StringComparison.Ordinal) =>
    new ReadOnlyCollection<IToken>(this.Context.FindTokens(tokenType, text, stringComparison).ToList());

  /// <summary>
  /// Inserts a given node `N` query in this current node `C` query-object and returns the result as a new
  /// `R` query-object
  /// </summary>
  public SPARQLQuery<R> InsertQuery<R>(string query, IToken beforeToken, bool before)
    where R : ParserRuleContext // The type of the context to be returned
  {
    var newTokens = this.tokens.InsertTokens(query.Tokenize(), beforeToken, before);
    var completeQuery = newTokens.ToQueryString(skipComments: false);

    // This will throw an exception if the newly formed tokens is not a valid `R` parser context
    return new SPARQLQuery<R>(completeQuery);
  }

  /// <summary>
  /// Return all parse tree nodes of this parser rule context
  /// </summary>
  public IEnumerable<N> FindNodes<N>()
    where N : ParserRuleContext => this.Context.Nodes<N>();

  /// <summary>
  /// Return this parser rule context
  /// </summary>
  public string ToQueryString(bool skipComments = true) => this.tokens.ToQueryString(skipComments);
}
