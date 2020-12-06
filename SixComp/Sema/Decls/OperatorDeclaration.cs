using SixComp.Common;
using SixComp.Support;
using System.Linq;

namespace SixComp.Sema
{
    public class OperatorDeclaration : Base<Tree.OperatorDeclaration>, INamedDeclaration
    {
        public OperatorDeclaration(IScoped outer, Tree.OperatorDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(Outer, tree.Operator);

            Fixitivity = tree.Fixitivity;

            if (Fixitivity == Fixitivity.Infix)
            {
                PrecedenceName = BaseName.Maybe(outer, tree.Names.FirstOrDefault());
                TypeNames = new BaseNames(outer, tree.Names.Skip(1));
            }
            else
            {   
                PrecedenceName = null;
                TypeNames = new BaseNames(outer, tree.Names);
            }
            Precedence = null;
        }

        public BaseName Name { get; }
        public Fixitivity Fixitivity { get; }
        public BaseName? PrecedenceName { get; }
        public BaseNames TypeNames { get; }
        public PrecedenceGroupDeclaration? Precedence { get; private set; }

        public void SetPrecedence(PrecedenceGroupDeclaration precedence)
        {
            Precedence = precedence;
        }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Operator))
            {
                Name.Report(writer, Strings.Head.Name);
                Fixitivity.Report(writer, Strings.Head.Fixitivity);
                PrecedenceName.Report(writer, Strings.Head.Precedence);
                TypeNames.ReportList(writer, Strings.Head.Types);
            }
        }
    }
}
