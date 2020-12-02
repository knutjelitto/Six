using SixComp.Support;

namespace SixComp.Sema
{
    public class GenericConformance : GenericRestriction
    {
        public GenericConformance(IScoped outer, IType left, IType right)
            : base(outer)
        {
            Left = left;
            Right = right;
        }

        public IType Left { get; }
        public IType Right { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Conformance))
            {
                Left.Report(writer, Strings.Head.Type);
                Right.Report(writer, Strings.Head.Type);
            }
        }
    }
}
