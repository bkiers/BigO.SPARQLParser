namespace BigO.SPARQLParser.ErrorStrategies;

using Antlr4.Runtime;
using BigO.SPARQLParser.Exceptions;

/// <summary>
/// An error strategy pretty much like the built-in DefaultErrorStrategy, except that the
/// error message in not printed to the STDERR, but wrapped in an exception so that invalid
/// queries will not be parsed
/// </summary>
internal class DescriptiveBailErrorStrategy : DefaultErrorStrategy
{
  protected override void ReportNoViableAlternative(Parser recognizer, NoViableAltException e)
  {
    var inputStream = (ITokenStream)recognizer.InputStream;
    var message = "no viable alternative at input " + this.EscapeWSAndQuote(inputStream == null
      ? "<unknown input>"
      : e.StartToken.Type == Lexer.Eof ? "<EOF>" : inputStream.GetText(e.StartToken, e.OffendingToken));

    throw new ParseException(message);
  }

  protected override void ReportInputMismatch(Parser recognizer, InputMismatchException e)
  {
    var message = "mismatched input " + this.GetTokenErrorDisplay(e.OffendingToken) + " expecting " +
                  e.GetExpectedTokens().ToString(recognizer.Vocabulary);

    throw new ParseException(message);
  }

  protected override void ReportMissingToken(Parser recognizer)
  {
    var expecting = GetExpectedTokens(recognizer);
    var message = "missing " + expecting.ToString(recognizer.Vocabulary) + " at " +
                  GetTokenErrorDisplay(recognizer.CurrentToken);

    throw new ParseException(message);
  }

  protected override void ReportUnwantedToken(Parser recognizer)
  {
    var message = "extraneous input " + this.GetTokenErrorDisplay(recognizer.CurrentToken) + " expecting " +
                  this.GetExpectedTokens(recognizer).ToString(recognizer.Vocabulary);

    throw new ParseException(message);
  }
}
