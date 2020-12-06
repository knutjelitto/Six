using SixComp.Support;

namespace SixComp.Sema
{
    public class ExtensionDeclaration : BaseScoped<Tree.ExtensionDeclaration>, IDeclaration, IWhere
    {
        public ExtensionDeclaration(IScoped outer, Tree.ExtensionDeclaration tree)
            : base(outer, tree)
        {
            Extended = new TypeIdentifier(Outer, Tree.Name);
            Where = new GenericRestrictions(this);
            Inheritance = new Inheritance(Outer, Tree.Inheritance);
            Where.Add(this, Tree.Requirements);
            Declarations = new Declarations(this, Tree.Declarations);
        }

        public Tree.Prefix Prefix => Tree.Prefix;
        public TypeIdentifier Extended { get; }
        public Inheritance Inheritance { get; }
        public GenericRestrictions Where { get; }
        public Declarations Declarations { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Extension))
            {
                Extended.Report(writer, Strings.Head.Extended);
                Inheritance.Report(writer);
                Where.Report(writer);
                Declarations.Report(writer);
            }
        }

    }
}
