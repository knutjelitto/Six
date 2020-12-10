using SixComp.Entities;
using SixComp.Support;
using System.Diagnostics;

namespace SixComp.Sema
{
    public class ExtensionDeclaration : BaseScoped<ParseTree.ExtensionDeclaration>, IDeclaration, IWithRestrictions
    {
        public ExtensionDeclaration(IScoped outer, ParseTree.ExtensionDeclaration tree)
            : base(outer, tree)
        {
            Extended = new TypeIdentifier(Outer, Tree.Name);
            Where = new GenericRestrictions(this);
            Inheritance = new Inheritance(Outer, Tree.Inheritance);
            Where.Add(this, Tree.Requirements);
            Declarations = new Declarations(this, Tree.Declarations);

            Global.Extensions.Add(this);
        }

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

        public void ResolveExtended(IWriter writer)
        {
            Extended.Resolve(writer);
            var entity = Extended.Entity;
            var check = entity != null ? "✓" : "  ";
            writer.WriteLine($"[{check}] {Extended}");

            if (entity != null)
            {
                if (entity.Scope.Extend(with: this))
                {
                    Debug.Assert(true);
                }
                else
                {
                    Debug.Assert(true);
                }
            }
        }
    }
}
