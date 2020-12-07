using SixComp.Support;

namespace SixComp.Sema
{
    public class TypealiasDeclaration : BaseScoped<Tree.TypealiasDeclaration>, INamedDeclaration, IWhere
    {
        public TypealiasDeclaration(IScoped outer, Tree.TypealiasDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.Parameters);
            Where.Add(this, Tree.Requirements);
            Type = ITypeDefinition.Build(Outer, tree.Assignment);

            Declare(this);
        }

        public BaseName Name { get; }
        public GenericParameters GenericParameters { get; }
        public GenericRestrictions Where { get; }
        public ITypeDefinition Type { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, GenericParameters, Where, Type);
        }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Typealias))
            {
                Name.Report(writer, Strings.Head.Name);
                GenericParameters.Report(writer);
                Where.Report(writer);
                Type.Report(writer, Strings.Head.Type);
            }
        }
    }
}
