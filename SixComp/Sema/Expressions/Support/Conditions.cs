﻿using Six.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class Conditions : Items<IExpression, ParseTree.ConditionList>
    {
        public Conditions(IScoped outer, ParseTree.ConditionList tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Conditions);
        }

        private static IEnumerable<IExpression> Enum(IScoped outer, ParseTree.ConditionList tree)
        {
            return tree.Select(condition => ICondition.Build(outer, condition));
        }
    }
}
