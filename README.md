## SPARQL Parser

A SPARQL parser generated from an ANTLR grammar that can be used to validate and/or rewrite 
SPARQL queries.

### NuGet

TODO

### Usage

To get the raw ANTLR parse, which can be used to traverse the parse tree of your SPARQL code, do 
the following:

```cs
using BigO.SPARQLParser.Parser;

var lexer = new SPARQLLexer(CharStreams.fromString(sparqlQuery));
var parser = new SPARQLParser(new CommonTokenStream(lexer));
var parseTree = parser.queryUnit();
```

The `parseTree` can now be used to traversed using 
[ANTLR-listeners](https://github.com/antlr/antlr4/blob/master/doc/listeners.md) or 
[-visitors](https://tomassetti.me/listeners-and-visitors/).

This package is nothing more than a wrapper around the ANTLR classes mentioned above, accessible
through the [`SPARQLQuery`]() class.

Validating SPARQL source can simply be done by creating a `SPARQLQuery`:

```csharp
const string queryString = @"
  PREFIX foaf: <http://xmlns.com/foaf/0.1/>
  SELECT ?name WHERE {
    ?person foaf:name ?name.
  }";

// Parse as a `QueryUnitContext`, which will throw an exception if `queryString` is invalid
var query = new SPARQLQuery<QueryUnitContext>(queryString);
```

For example, if you would like to insert a filter clause at the end of a where clause, do something 
like this:

```cs
const string queryString = @"
  PREFIX foaf: <http://xmlns.com/foaf/0.1/>
  SELECT ?name WHERE {
    ?person foaf:name ?name.    # comment }
  }";

// Parse as a `QueryUnitContext`
var query = new SPARQLQuery<QueryUnitContext>(queryString);

// Grab the first `WhereClauseContext`
var whereClause = query.Nodes<WhereClauseContext>().First();

// Inside the `WhereClauseContext`, get the past `'}'` (`CBRACE`) token, which is (of course) not 
// the `'}'` in the comment!
var closingBrace = whereClause.FindTokens(CBRACE).Last();

// Insert new SPARQL code (FILTER) before the `'}'`
var rewritten = query
  .InsertQuery<FilterContext, QueryUnitContext>("  FILTER(?name = 'Someone')\n", closingBrace, before: true)
  .ToQueryString<QueryUnitContext>(skipComments: true);

// Note that `FILTER(?name = 'Someone')` will be parsed as a `Filter` before
// being inserted. So invalid SPARQL queries will not be accepted.

Console.WriteLine(rewritten);
```

All ANTLR generated classes mentioned in the example code, `...Context` are mapped to 1-to-1 to ANTLR
parser rules. For example, the context `WhereClauseContext` is mapped to the `whereClause` parser rule, which 
looks like this:

```antlrv4
// See SPARQLParser.g4
 
whereClause
 : WHERE? groupGraphPattern
 ;

...

groupGraphPattern
 : OBRACE ( subSelect | groupGraphPatternSub ) CBRACE
 ;

...
```

So if you have a reference to the parse tree (context) `WhereClauseContext`, you can invoke methods on it
like this:

```cs
var whereClause = query.Nodes<WhereClauseContext>().First();

var subSelect = whereClause.groupGraphPattern().subSelect(); // watch out, can be null!
```

and use these child parse trees (contexts) in custom listeners or visitors.

The uppercase token types, like `CBRACE`, are mapped to the lexer rules (see SPARQLLexer.g4).

For more examples, checkout the file [`SPARQLQueryTests.cs`]().
