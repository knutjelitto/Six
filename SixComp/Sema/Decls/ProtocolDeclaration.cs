using SixComp.Support;

namespace SixComp.Sema
{
    public class ProtocolDeclaration : BaseScoped<Tree.ProtocolDeclaration>, INamedDeclaration, IWhere
    {
        public ProtocolDeclaration(IScoped outer, Tree.ProtocolDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(Outer, Tree.Name);
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.Generics);
            Inheritance = new Inheritance(Outer, Tree.Inheritance);
            Where.Add(this, Tree.Requirements);
            Declarations = new Declarations(this, Tree.Declarations);

            Declare(this);
        }

        public BaseName Name { get; }
        public GenericParameters GenericParameters { get; }
        public Inheritance Inheritance { get; }
        public GenericRestrictions Where { get; }
        public Declarations Declarations { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent($"{Strings.Head.Protocol} {Name.Text}"))
            {
                GenericParameters.Report(writer);
                Inheritance.Report(writer);
                Where.Report(writer);
                Declarations.Report(writer);
            }
        }

    }
}
