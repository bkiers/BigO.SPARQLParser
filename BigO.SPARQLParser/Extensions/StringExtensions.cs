namespace BigO.SPARQLParser.Extensions;

using Antlr4.Runtime;
using BigO.SPARQLParser.ErrorStrategies;
using BigO.SPARQLParser.Exceptions;
using BigO.SPARQLParser.Parser;

internal static class StringExtensions
{
  /// <summary>
  /// Create a list of tokens given a `sparqlQuery` string
  /// </summary>
  public static IList<IToken> Tokens(this string sparqlQuery)
  {
    var lexer = new SPARQLLexer(CharStreams.fromString(sparqlQuery));
    var tokenStream = new CommonTokenStream(lexer);

    tokenStream.Fill();

    return tokenStream.GetTokens();
  }

  /// <summary>
  /// Parse the given input `sparqlQuery` as a certain `C` parser context. Note that
  /// this will _not_ throw an exception when you parse `?Q + 42` as a `VarContext`
  /// because `?Q` _is_ a valid `VarContext`, leaving `+ 42` as unconsumed tokens
  /// in the token-stream. If something goes wrong, a `ParseException` is thrown with
  /// a (hopefully) meaningful error message :)
  /// </summary>
  public static C ParseAs<C>(this string sparqlQuery)
    where C : ParserRuleContext
  {
    var parserRuleName = typeof(C).MethodName();

    try
    {
      var lexer = new SPARQLLexer(CharStreams.fromString(sparqlQuery));
      var tokenStream = new CommonTokenStream(lexer);
      var parser = new SPARQLParser(tokenStream);

      lexer.RemoveErrorListeners();
      parser.RemoveErrorListeners();
      parser.ErrorHandler = new DescriptiveBailErrorStrategy();

      var method = parser.GetType().GetMethod(typeof(C).MethodName());

      if (method == null)
      {
        // This should never happen
        throw new ParseException($"Could not find method '{parserRuleName}' in the parser");
      }

      if (method.Invoke(parser, null) is not C context)
      {
        // This should never happen
        throw new ParseException($"'{sparqlQuery}' could not be parsed by rule '{parserRuleName}'");
      }

      return context;
    }
    catch (ParseException)
    {
      // A ParseException _should_ already have a meaningful message with it, so just rethrow
      throw;
    }
    catch (Exception e)
    {
      var message = $"SPARQL code '{sparqlQuery}' could not be parsed as a '{parserRuleName}'";

      // The outer exception is the one from the `Invoke(parser, null)` reflection call, so look at the inner exception
      if (e.InnerException is ParseException pe)
      {
        throw new ParseException($"{message}: {pe.Message}");
      }

      throw new ParseException($"{message}: unexpected exception type {e.InnerException?.GetType() ?? e.GetType()}");
    }
  }
}
