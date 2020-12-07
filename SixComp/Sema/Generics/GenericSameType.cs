using SixComp.Support;

namespace SixComp.Sema
{
    public class GenericSameType : GenericRestriction
    {
        public GenericSameType(IScoped outer, ITypeDefinition left, ITypeDefinition right)
            : base(outer)
        {
            Left = left;
            Right = right;
        }

        public ITypeDefinition Left { get; }
        public ITypeDefinition Right { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Left, Right);
        }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.SameType))
            {
                Left.Report(writer, Strings.Head.Type);
                Right.Report(writer, Strings.Head.Type);
            }
        }
    }
}
