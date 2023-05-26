using BigO.SPARQLParser.Extensions;
using BigO.SPARQLParser.Parser;

const string objectId = "42";
const string languageCode = "300388256";

const string sparqlQuery = @"PREFIX crm: <http://www.cidoc-crm.org/cidoc-crm/>
SELECT ?objectId ?propertyId (?sortingYear as ?value)
WHERE {
    ?objectId rdf:type crm:E22_Human-Made_Object.
    ?objectId crm:P108i_was_produced_by ?production.
    ?production crm:P2_has_type <http://vocab.getty.edu/aat/300404450>.
    ?production rdf:type crm:E12_Production.
    ?production crm:P4_has_time-span ?timespan.
    {
        ?timespan crm:P82a_begin_of_the_begin ?begin.
    } UNION {
        ?timespan crm:P82b_end_of_the_end ?end.  
    }
    BIND(
        IF(
            STRSTARTS(?begin, '-'),
            concat('-', STRBEFORE(SUBSTR(xsd:string(?begin), 2, STRLEN(?begin)), '-')),
            STRBEFORE(xsd:string(?begin), '-')
        )
    as ?beginYear)
    BIND(
        IF(
            STRSTARTS(?end, '-'),
            concat('-', STRBEFORE(SUBSTR(xsd:string(?end), 2, STRLEN(?end)), '-')),
            STRBEFORE(xsd:string(?end), '-')
        )
    as ?endYear)
    BIND(IF(STRLEN(?endYear) > 0, xsd:integer(?endYear), xsd:integer(?beginYear)) AS ?sortingYear)
    BIND(URI(concat('http://example.com/', MD5(STR(?sortingYear)))) as ?propertyId) # some comment here } # }
}";

var parsedQuery = new ParsedQuery<SPARQLParser.QueryUnitContext>(sparqlQuery);
var whereClause = parsedQuery.FirstNodeOrDefault<SPARQLParser.WhereClauseContext>();
var closingBrace = whereClause!.FindTokens(SPARQLLexer.CBRACE).Last();

Console.WriteLine(parsedQuery.ToQuery());
Console.WriteLine("--------------------------------------------------------------------");
    
parsedQuery
    .InsertBefore<SPARQLParser.FilterContext>(closingBrace, $"\nFILTER(?objectId = <{objectId}>)")
    .InsertBefore<SPARQLParser.FilterContext>(closingBrace, $"\nFILTER(?language = <http://vocab.getty.edu/aat/{languageCode}>)");

Console.WriteLine(parsedQuery.ToQuery());