using System.Text;

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
  public SPARQLQuery<R> Insert<R>(string query, IToken beforeOrAfterToken, bool before)
    where R : ParserRuleContext // The type of the context to be returned
  {
    var newTokens = this.tokens.InsertTokens(query.Tokenize(), beforeOrAfterToken, before);
    var completeQuery = newTokens.ToQueryString(skipComments: false);

    // This will throw an exception if the newly formed tokens is not a valid `R` parser context
    return new SPARQLQuery<R>(completeQuery);
  }

  /// <summary>
  /// Return all parse tree nodes of this parser rule context
  /// </summary>
  public IEnumerable<N> Nodes<N>()
    where N : ParserRuleContext => this.Context.Nodes<N>();

  /// <summary>
  /// Return this parser rule context
  /// </summary>
  public string ToQueryString(bool skipComments = true) => this.tokens.ToQueryString(skipComments);

  /// <summary>
  /// TODO
  /// </summary>
  public List<string> SourcesOf<N>()
    where N : ParserRuleContext
  {
    var nodes = this.Nodes<N>();
    var nodesArray = nodes as N[] ?? nodes.ToArray();
    var sources = new List<string>();

    foreach (var node in nodesArray)
    {
      var builder = new StringBuilder();

      for (var index = node.Start.TokenIndex; index <= node.Stop.TokenIndex; index++)
      {
        builder.Append(this.tokens[index].Text);
      }

      sources.Add(builder.ToString());
    }

    return sources;
  }

  public List<string> SourcesOf<P, N>()
    where P : ParserRuleContext
    where N : ParserRuleContext
  {
    var parentNodes = this.Nodes<P>();
    var parentNodesArray = parentNodes as P[] ?? parentNodes.ToArray();

    if (!parentNodesArray.Any())
    {
      throw new ArgumentException($"Could not find a {typeof(P).Name} node");
    }

    var childNodes = parentNodesArray.Where(p => p.Nodes<N>().Any()).Select(p => p.Nodes<N>()).FirstOrDefault();
    var nodesArray = childNodes as N[] ?? childNodes?.ToArray();

    if (nodesArray?.Any() != true)
    {
      throw new ArgumentException($"Could not find a child {typeof(N).Name} node under parent {typeof(P).Name} node");
    }

    var sources = new List<string>();

    foreach (var node in nodesArray)
    {
      var builder = new StringBuilder();

      for (var index = node.Start.TokenIndex; index <= node.Stop.TokenIndex; index++)
      {
        builder.Append(this.tokens[index].Text);
      }

      sources.Add(builder.ToString());
    }

    return sources;
  }
}
