using SixComp.Support;

namespace SixComp.Sema
{
    public class Extension : Base<Tree.ExtensionDeclaration>, IDeclaration, IOwner
    {
        public Extension(IScoped outer, Tree.ExtensionDeclaration tree)
            : base(outer, tree)
        {
            Extendee = new TypeIdentifier(Outer, Tree.Name);
            Where = new GenericRestrictions(this);
            Inheritance = new Inheritance(Outer, Tree.Inheritance);
            Where.Add(this, Tree.Requirements);
            Declarations = new Declarations(this, Tree.Declarations);
        }

        public Tree.Prefix Prefix => Tree.Prefix;
        public TypeIdentifier Extendee { get; }
        public Inheritance Inheritance { get; }
        public GenericRestrictions Where { get; }
        public Declarations Declarations { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Extension))
            {
                Extendee.Report(writer, Strings.Head.Extended);
                Inheritance.Report(writer);
                Where.Report(writer);
                Declarations.Report(writer);
            }
        }

    }
}
