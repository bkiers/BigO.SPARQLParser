namespace BigO.SPARQLParser.Tests.ErrorStrategies;

using BigO.SPARQLParser.Exceptions;
using BigO.SPARQLParser.Extensions;
using Xunit;
using static BigO.SPARQLParser.Parser.SPARQLParser;

public class DescriptiveBailErrorStrategyTests
{
  [Fact]
  public void InvalidQuery_MissingCloseBrace_ExceptionMessageContainsCloseBrace()
  {
    const string query = "SELECT ?name WHERE { ?person foaf:name ?name . ";

    var exception = Assert.Throws<ParseException>(() => query.ParseAs<QueryUnitContext>());

    Assert.Contains("'}'", exception.Message);
  }

  [Fact]
  public void InvalidQuery_WithSmileyChar_ExceptionMessageContainsSmileyChar()
  {
    const string query = "SELECT ?name WHERE { ?person foaf:name ?name . } 😀";

    var exception = Assert.Throws<ParseException>(() => query.ParseAs<QueryUnitContext>());

    Assert.Contains("'😀'", exception.Message);
  }

  [Fact]
  public void ValidQuery_WithNonAscii_NoExceptionIsThrown()
  {
    const string query = "SELECT ?name WHERE { ?person foaf:name ?name . } # 😀";

    var queryContext = query.ParseAs<QueryUnitContext>();

    Assert.NotNull(queryContext);
  }
}
