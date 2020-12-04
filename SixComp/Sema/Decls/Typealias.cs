using SixComp.Support;

namespace SixComp.Sema
{
    public class Typealias : Base<Tree.TypealiasDeclaration>, IDeclaration, IWhere, INamed
    {
        public Typealias(IScoped outer, Tree.TypealiasDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.Parameters);
            Where.Add(this, Tree.Requirements);
            Type = IType.Build(Outer, tree.Assignment);
        }

        public BaseName Name { get; }
        public GenericParameters GenericParameters { get; }
        public GenericRestrictions Where { get; }
        public IType Type { get; }

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
