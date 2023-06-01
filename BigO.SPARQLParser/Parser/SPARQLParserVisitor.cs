//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ./SPARQLParser.g4 by ANTLR 4.13.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace BigO.SPARQLParser.Parser {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="SPARQLParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.0")]
[System.CLSCompliant(false)]
public interface ISPARQLParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.queryUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitQueryUnit([NotNull] SPARQLParser.QueryUnitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.updateUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUpdateUnit([NotNull] SPARQLParser.UpdateUnitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.query"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitQuery([NotNull] SPARQLParser.QueryContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.prologue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrologue([NotNull] SPARQLParser.PrologueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.baseDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBaseDecl([NotNull] SPARQLParser.BaseDeclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.prefixDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrefixDecl([NotNull] SPARQLParser.PrefixDeclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.selectQuery"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSelectQuery([NotNull] SPARQLParser.SelectQueryContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.subSelect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubSelect([NotNull] SPARQLParser.SubSelectContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.selectClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSelectClause([NotNull] SPARQLParser.SelectClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.constructQuery"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstructQuery([NotNull] SPARQLParser.ConstructQueryContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.describeQuery"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDescribeQuery([NotNull] SPARQLParser.DescribeQueryContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.askQuery"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAskQuery([NotNull] SPARQLParser.AskQueryContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.datasetClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDatasetClause([NotNull] SPARQLParser.DatasetClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.defaultGraphClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDefaultGraphClause([NotNull] SPARQLParser.DefaultGraphClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.namedGraphClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNamedGraphClause([NotNull] SPARQLParser.NamedGraphClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.sourceSelector"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSourceSelector([NotNull] SPARQLParser.SourceSelectorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.whereClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhereClause([NotNull] SPARQLParser.WhereClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.solutionModifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSolutionModifier([NotNull] SPARQLParser.SolutionModifierContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.groupClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupClause([NotNull] SPARQLParser.GroupClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.groupCondition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupCondition([NotNull] SPARQLParser.GroupConditionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.havingClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHavingClause([NotNull] SPARQLParser.HavingClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.havingCondition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHavingCondition([NotNull] SPARQLParser.HavingConditionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.orderClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOrderClause([NotNull] SPARQLParser.OrderClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.orderCondition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOrderCondition([NotNull] SPARQLParser.OrderConditionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.limitOffsetClauses"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLimitOffsetClauses([NotNull] SPARQLParser.LimitOffsetClausesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.limitClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLimitClause([NotNull] SPARQLParser.LimitClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.offsetClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOffsetClause([NotNull] SPARQLParser.OffsetClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.valuesClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitValuesClause([NotNull] SPARQLParser.ValuesClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.update"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUpdate([NotNull] SPARQLParser.UpdateContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.update1"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUpdate1([NotNull] SPARQLParser.Update1Context context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.load"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoad([NotNull] SPARQLParser.LoadContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.clear"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClear([NotNull] SPARQLParser.ClearContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.drop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDrop([NotNull] SPARQLParser.DropContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.create"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCreate([NotNull] SPARQLParser.CreateContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.add"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAdd([NotNull] SPARQLParser.AddContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.move"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMove([NotNull] SPARQLParser.MoveContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.copy"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCopy([NotNull] SPARQLParser.CopyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.insertData"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInsertData([NotNull] SPARQLParser.InsertDataContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.deleteData"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeleteData([NotNull] SPARQLParser.DeleteDataContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.deleteWhere"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeleteWhere([NotNull] SPARQLParser.DeleteWhereContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.modify"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitModify([NotNull] SPARQLParser.ModifyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.deleteClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeleteClause([NotNull] SPARQLParser.DeleteClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.insertClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInsertClause([NotNull] SPARQLParser.InsertClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.usingClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUsingClause([NotNull] SPARQLParser.UsingClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.graphOrDefault"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGraphOrDefault([NotNull] SPARQLParser.GraphOrDefaultContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.graphRef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGraphRef([NotNull] SPARQLParser.GraphRefContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.graphRefAll"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGraphRefAll([NotNull] SPARQLParser.GraphRefAllContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.quadPattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitQuadPattern([NotNull] SPARQLParser.QuadPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.quadData"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitQuadData([NotNull] SPARQLParser.QuadDataContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.quads"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitQuads([NotNull] SPARQLParser.QuadsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.quadsNotTriples"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitQuadsNotTriples([NotNull] SPARQLParser.QuadsNotTriplesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.triplesTemplate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTriplesTemplate([NotNull] SPARQLParser.TriplesTemplateContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.groupGraphPattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupGraphPattern([NotNull] SPARQLParser.GroupGraphPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.groupGraphPatternSub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupGraphPatternSub([NotNull] SPARQLParser.GroupGraphPatternSubContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.triplesBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTriplesBlock([NotNull] SPARQLParser.TriplesBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.graphPatternNotTriples"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGraphPatternNotTriples([NotNull] SPARQLParser.GraphPatternNotTriplesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.optionalGraphPattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOptionalGraphPattern([NotNull] SPARQLParser.OptionalGraphPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.graphGraphPattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGraphGraphPattern([NotNull] SPARQLParser.GraphGraphPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.serviceGraphPattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitServiceGraphPattern([NotNull] SPARQLParser.ServiceGraphPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.bind"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBind([NotNull] SPARQLParser.BindContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.inlineData"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInlineData([NotNull] SPARQLParser.InlineDataContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.dataBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDataBlock([NotNull] SPARQLParser.DataBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.inlineDataOneVar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInlineDataOneVar([NotNull] SPARQLParser.InlineDataOneVarContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.inlineDataFull"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInlineDataFull([NotNull] SPARQLParser.InlineDataFullContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.dataBlockValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDataBlockValue([NotNull] SPARQLParser.DataBlockValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.minusGraphPattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMinusGraphPattern([NotNull] SPARQLParser.MinusGraphPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.groupOrUnionGraphPattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupOrUnionGraphPattern([NotNull] SPARQLParser.GroupOrUnionGraphPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.filter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFilter([NotNull] SPARQLParser.FilterContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.constraint"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstraint([NotNull] SPARQLParser.ConstraintContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] SPARQLParser.FunctionCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.argList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgList([NotNull] SPARQLParser.ArgListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.expressionList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionList([NotNull] SPARQLParser.ExpressionListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.constructTemplate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstructTemplate([NotNull] SPARQLParser.ConstructTemplateContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.constructTriples"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstructTriples([NotNull] SPARQLParser.ConstructTriplesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.triplesSameSubject"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTriplesSameSubject([NotNull] SPARQLParser.TriplesSameSubjectContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.propertyList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPropertyList([NotNull] SPARQLParser.PropertyListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.propertyListNotEmpty"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPropertyListNotEmpty([NotNull] SPARQLParser.PropertyListNotEmptyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.verb"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVerb([NotNull] SPARQLParser.VerbContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.objectList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObjectList([NotNull] SPARQLParser.ObjectListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.object"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObject([NotNull] SPARQLParser.ObjectContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.triplesSameSubjectPath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTriplesSameSubjectPath([NotNull] SPARQLParser.TriplesSameSubjectPathContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.propertyListPath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPropertyListPath([NotNull] SPARQLParser.PropertyListPathContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.propertyListPathNotEmpty"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPropertyListPathNotEmpty([NotNull] SPARQLParser.PropertyListPathNotEmptyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.verbPath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVerbPath([NotNull] SPARQLParser.VerbPathContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.verbSimple"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVerbSimple([NotNull] SPARQLParser.VerbSimpleContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.objectListPath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObjectListPath([NotNull] SPARQLParser.ObjectListPathContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.objectPath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObjectPath([NotNull] SPARQLParser.ObjectPathContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.path"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPath([NotNull] SPARQLParser.PathContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.pathAlternative"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPathAlternative([NotNull] SPARQLParser.PathAlternativeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.pathSequence"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPathSequence([NotNull] SPARQLParser.PathSequenceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.pathElt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPathElt([NotNull] SPARQLParser.PathEltContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.pathEltOrInverse"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPathEltOrInverse([NotNull] SPARQLParser.PathEltOrInverseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.pathMod"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPathMod([NotNull] SPARQLParser.PathModContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.pathPrimary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPathPrimary([NotNull] SPARQLParser.PathPrimaryContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.pathNegatedPropertySet"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPathNegatedPropertySet([NotNull] SPARQLParser.PathNegatedPropertySetContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.pathOneInPropertySet"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPathOneInPropertySet([NotNull] SPARQLParser.PathOneInPropertySetContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.triplesNode"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTriplesNode([NotNull] SPARQLParser.TriplesNodeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.blankNodePropertyList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlankNodePropertyList([NotNull] SPARQLParser.BlankNodePropertyListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.triplesNodePath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTriplesNodePath([NotNull] SPARQLParser.TriplesNodePathContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.blankNodePropertyListPath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlankNodePropertyListPath([NotNull] SPARQLParser.BlankNodePropertyListPathContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.collection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCollection([NotNull] SPARQLParser.CollectionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.collectionPath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCollectionPath([NotNull] SPARQLParser.CollectionPathContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.graphNode"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGraphNode([NotNull] SPARQLParser.GraphNodeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.graphNodePath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGraphNodePath([NotNull] SPARQLParser.GraphNodePathContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.varOrTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarOrTerm([NotNull] SPARQLParser.VarOrTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.varOrIri"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarOrIri([NotNull] SPARQLParser.VarOrIriContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.var"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVar([NotNull] SPARQLParser.VarContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.graphTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGraphTerm([NotNull] SPARQLParser.GraphTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] SPARQLParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.conditionalOrExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConditionalOrExpression([NotNull] SPARQLParser.ConditionalOrExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.conditionalAndExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConditionalAndExpression([NotNull] SPARQLParser.ConditionalAndExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.valueLogical"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitValueLogical([NotNull] SPARQLParser.ValueLogicalContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.relationalExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelationalExpression([NotNull] SPARQLParser.RelationalExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.numericExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumericExpression([NotNull] SPARQLParser.NumericExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.additiveExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAdditiveExpression([NotNull] SPARQLParser.AdditiveExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.multiplicativeExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultiplicativeExpression([NotNull] SPARQLParser.MultiplicativeExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.unaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryExpression([NotNull] SPARQLParser.UnaryExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.primaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrimaryExpression([NotNull] SPARQLParser.PrimaryExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.brackettedExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBrackettedExpression([NotNull] SPARQLParser.BrackettedExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.builtInCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBuiltInCall([NotNull] SPARQLParser.BuiltInCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.regexExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRegexExpression([NotNull] SPARQLParser.RegexExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.substringExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubstringExpression([NotNull] SPARQLParser.SubstringExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.strReplaceExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStrReplaceExpression([NotNull] SPARQLParser.StrReplaceExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.existsFunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExistsFunc([NotNull] SPARQLParser.ExistsFuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.notExistsFunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNotExistsFunc([NotNull] SPARQLParser.NotExistsFuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.aggregate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAggregate([NotNull] SPARQLParser.AggregateContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.iriOrFunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIriOrFunction([NotNull] SPARQLParser.IriOrFunctionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.rdfLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRdfLiteral([NotNull] SPARQLParser.RdfLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.numericLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumericLiteral([NotNull] SPARQLParser.NumericLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.numericLiteralUnsigned"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumericLiteralUnsigned([NotNull] SPARQLParser.NumericLiteralUnsignedContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.numericLiteralPositive"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumericLiteralPositive([NotNull] SPARQLParser.NumericLiteralPositiveContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.numericLiteralNegative"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumericLiteralNegative([NotNull] SPARQLParser.NumericLiteralNegativeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.booleanLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBooleanLiteral([NotNull] SPARQLParser.BooleanLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.string"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitString([NotNull] SPARQLParser.StringContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.iri"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIri([NotNull] SPARQLParser.IriContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.prefixedName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrefixedName([NotNull] SPARQLParser.PrefixedNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="SPARQLParser.blankNode"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlankNode([NotNull] SPARQLParser.BlankNodeContext context);
}
} // namespace BigO.SPARQLParser.Parser
