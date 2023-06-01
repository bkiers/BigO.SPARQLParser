namespace BigO.SPARQLParser.Tests.Extensions;

using BigO.SPARQLParser.Extensions;
using Xunit;
using static BigO.SPARQLParser.Parser.SPARQLParser;

public class TypeExtensionsTests
{
  [Fact]
  public void MethodName_WrongType_ThrowsException()
  {
    Assert.Throws<ArgumentException>(() => typeof(string).MethodName());
  }

  [Fact]
  public void MethodName_CorrectType_ReturnsParserRule()
  {
    Assert.Equal("queryUnit", typeof(QueryUnitContext).MethodName());
  }
}
