using System.Text.RegularExpressions;
using BigO.SPARQLParser;
using BigO.SPARQLParser.Extensions;
using BigO.SPARQLParser.Parser;
using static BigO.SPARQLParser.Parser.SPARQLParser;

void PrintParserRules()
{
  Console.WriteLine("\nParserRules");

  foreach (var ruleType in SPARQLParser.ParserRules)
  {
    Console.WriteLine($"  {ruleType.MethodName()}, usage: var q = new SPARQLQuery<{ruleType.Name}>(\"...\");");
  }
}

void ListAllVars(string queryString)
{
  Console.WriteLine("\nListAllVars");

  var query = queryString.ParseAs<QueryUnitContext>();

  foreach (var token in query.FindTokens(VAR1))
  {
    Console.WriteLine($"  found {token.Text} on line {token.Line}");
  }
}

void ListWhereVars(string queryString)
{
  Console.WriteLine("\nListWhereVars");

  var whereClause = queryString
    .ParseAs<QueryUnitContext>()
    .Nodes<WhereClauseContext>()
    .First();

  foreach (var token in whereClause.FindTokens(VAR1))
  {
    Console.WriteLine($"  found {token.Text} on line {token.Line}");
  }
}

void ListSelectVars(string queryString)
{
  Console.WriteLine("\nListSelectVars");

  var selectClause = queryString
    .ParseAs<QueryUnitContext>()
    .Nodes<SelectQueryContext>().First()
    .Nodes<SelectClauseContext>().First();

  foreach (var token in selectClause.FindTokens(VAR1))
  {
    Console.WriteLine($"  found {token.Text} on line {token.Line}");
  }
}

void InsertFilter(string queryString)
{
  Console.WriteLine("\nInsertFilter");

  var query = new SPARQLQuery<QueryUnitContext>(queryString);
  var whereClause = query.Nodes<WhereClauseContext>().First();
  var closingBrace = whereClause.FindTokens(CBRACE).Last();

  // Note that `FILTER(?name = 'Someone')` will be parsed as a `Filter` before
  // being inserted. So invalid SPARQL queries will not be accepted.
  var rewritten = query
    .InsertQuery<FilterContext, QueryUnitContext>("  FILTER(?name = 'Someone')\n", closingBrace, before: true)
    .ToQueryString<QueryUnitContext>(skipComments: true);

  Console.WriteLine(Regex.Replace(rewritten, @"(?m)^", "  "));
}

PrintParserRules();

const string q = @"
  PREFIX foaf:  <http://xmlns.com/foaf/0.1/>
  SELECT ?name WHERE {
    ?person foaf:name ?name. # name comment ?var 
  }";

Console.WriteLine(q);

ListAllVars(q);
ListWhereVars(q);
ListSelectVars(q);
InsertFilter(q);
