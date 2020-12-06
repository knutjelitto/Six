using SixComp.Sema.Stmts;
using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class SubscriptDeclaration : BaseScoped<Tree.SubscriptDeclaration>, IDeclaration, IWhere
    {
        public SubscriptDeclaration(IScoped outer, Tree.SubscriptDeclaration tree)
            : base(outer, tree)
        {
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.Generics);
            Parameters = new FuncParameters(this, Tree.Parameters);
            Result = ITypeDefinition.Build(Outer, Tree.Result);
            Where.Add(this, Tree.Requirements);
            Blocks = new PropertyBlocks(Outer, tree.Blocks);
        }

        public GenericParameters GenericParameters { get; }
        public FuncParameters Parameters { get; }
        public ITypeDefinition Result { get; }
        public GenericRestrictions Where { get; }
        public PropertyBlocks Blocks { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Subscript))
            {
                GenericParameters.Report(writer);
                Parameters.Report(writer);
                Result.Report(writer, Strings.Head.Result);
                Where.Report(writer);
                Blocks.Report(writer);
            }
        }
    }
}
