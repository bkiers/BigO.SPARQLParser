using System.Text.RegularExpressions;
using Antlr4.Runtime;

namespace BigO.SPARQLParser.Extensions;

internal static class TypeExtensions
{
  public static string MethodName(this Type type)
  {
    if (!typeof(ParserRuleContext).IsAssignableFrom(type))
    {
      throw new ArgumentException("Can only be used for types of classes extend ParserRuleContext");
    }

    var name = Regex.Replace(type.Name, @"Context$", string.Empty);
    var camelCased = $"{name[0].ToString().ToLower()}{name[1..]}";

    return camelCased;
  }
}
