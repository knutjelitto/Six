using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class SubscriptDeclaration : Items<PropertyBlock, Tree.SubscriptDeclaration>, IDeclaration, IWhere
    {
        public SubscriptDeclaration(IScoped outer, Tree.SubscriptDeclaration tree)
            : base(outer, tree, Enum(outer, tree))
        {
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.Generics);
            Parameters = new FuncParameters(this, Tree.Parameters);
            Result = IType.Build(Outer, Tree.Result);
            Where.Add(this, Tree.Requirements);
        }

        public GenericParameters GenericParameters { get; }
        public FuncParameters Parameters { get; }
        public IType Result { get; }
        public GenericRestrictions Where { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Subscript))
            {
                GenericParameters.Report(writer);
                Parameters.Report(writer);
                Result.Report(writer, Strings.Head.Result);
                Where.Report(writer);
                this.ReportList(writer, Strings.Head.Blocks);
            }
        }

        private static IEnumerable<PropertyBlock> Enum(IScoped outer, Tree.SubscriptDeclaration tree)
        {
            return tree.Blocks.OrderBy(b => b.Value.index).Select(block => new PropertyBlock(outer, block.Value.block));
        }
    }
}
