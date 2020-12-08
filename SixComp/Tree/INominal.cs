namespace SixComp.Tree
{
    public interface INominal
    {
        BaseName Name { get; }
        GenericParameterClause Generics { get; }
        RequirementClause Requirements { get; }
    }

    public interface INominalWhithDeclarations : INominal
    {
        TypeInheritanceClause Inheritance { get; }
        DeclarationClause Declarations { get; }
    }
}
