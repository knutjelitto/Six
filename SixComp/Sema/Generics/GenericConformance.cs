using Six.Support;

namespace SixComp.Sema
{
    public class GenericConformance : GenericRestriction
    {
        public GenericConformance(IScoped outer, ITypeDefinition left, ITypeDefinition right)
            : base(outer)
        {
            Left = left;
            Right = right;
        }

        public ITypeDefinition Left { get; }
        public ITypeDefinition Right { get; }

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
