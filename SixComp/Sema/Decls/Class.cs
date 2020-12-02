using SixComp.Support;

namespace SixComp.Sema
{
    public class Class : Base<Tree.ClassDeclaration>, IDeclaration, IOwner
    {
        public Class(IScoped outer, Tree.ClassDeclaration tree)
            : base(outer, tree)
        {
            Name = Tree.Name.ToString();
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.Generics);
            Inheritance = new Inheritance(Outer, Tree.Inheritance);
            Where.Add(this, Tree.Requirements);
            Declarations = new Declarations(this, Tree.Declarations);
        }

        public Tree.Prefix Prefix => Tree.Prefix;
        public string Name { get; }

        public GenericParameters GenericParameters { get; }
        public Inheritance Inheritance { get; }
        public GenericRestrictions Where { get; }
        public Declarations Declarations { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine($";; {Tree}");
            writer.WriteLine($"{Strings.Class}");
            using (writer.Indent())
            {
                writer.WriteLine($"name: {Name}");
                GenericParameters.Report(writer);
                Inheritance.Report(writer);
                Where.Report(writer);
                Declarations.Report(writer);
            }
        }
    }
}
