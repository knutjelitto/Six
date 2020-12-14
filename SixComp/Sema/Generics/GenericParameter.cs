using Six.Support;
using SixComp.Entities;

namespace SixComp.Sema
{
    public class GenericParameter : Base<ParseTree.GenericParameter>, INamedDeclaration
    {
        public GenericParameter(IWithRestrictions where, ParseTree.GenericParameter tree)
            : base(where, tree)
        {
            Name = new BaseName(Outer, Tree.Name);

            if (Tree.Requirement != null)
            {
                where.Where.Add(Outer, Tree);
            }

            Declare(this);
        }

        public BaseName Name { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Name.Text);
        }

        public override string ToString()
        {
            return Name.Text;
        }
    }
}
