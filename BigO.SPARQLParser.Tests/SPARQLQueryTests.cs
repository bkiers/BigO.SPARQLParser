using BigO.SPARQLParser.Exceptions;

namespace BigO.SPARQLParser.Tests;

using Xunit;
using static BigO.SPARQLParser.Parser.SPARQLParser;

public class SPARQLQueryTests
{
  [Fact]
  public void SPARQLQuery_ValidInput_DoesNotThrowException()
  {
    const string queryString = @"SELECT ?name
      WHERE {
          ?person foaf:name ?name .
      }";

    var query = new SPARQLQuery<QueryUnitContext>(queryString);

    Assert.NotNull(query);
  }

  [Fact]
  public void SPARQLQuery_TooManyTokens_ThrowsParseException()
  {
    const string queryString = @"1 + 2 .";

    Assert.Throws<ParseException>(() => new SPARQLQuery<ExpressionContext>(queryString));
  }
}
