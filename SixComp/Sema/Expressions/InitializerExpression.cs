﻿using Six.Support;
using System.Linq;

namespace SixComp.Sema
{
    public class InitializerExpression : Base<ParseTree.InitializerExpression>, IExpression
    {
        public InitializerExpression(IScoped outer, ParseTree.InitializerExpression tree) : base(outer, tree)
        {
            Left = IExpression.Build(outer, tree.Left);
            ArgumentNames = new BaseNames(outer, tree.Names.Names.Select(n => n.Name));
        }

        public IExpression Left { get; }
        public BaseNames ArgumentNames { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Initializer))
            {
                Left.Report(writer, Strings.Head.Left);
                ArgumentNames.Report(writer);
            }
        }
    }
}
