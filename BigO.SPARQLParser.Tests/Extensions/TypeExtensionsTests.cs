namespace BigO.SPARQLParser.Tests.Extensions;

using BigO.SPARQLParser.Extensions;
using Xunit;
using static BigO.SPARQLParser.Parser.SPARQLParser;

public static class TypeExtensionsTests
{
  public class MethodNameTests
  {
    [Fact]
    public void WrongType_ThrowsException()
    {
      Assert.Throws<ArgumentException>(() => typeof(string).MethodName());
    }

    [Fact]
    public void CorrectType_ReturnsParserRule()
    {
      Assert.Equal("queryUnit", typeof(QueryUnitContext).MethodName());
    }
  }
}
