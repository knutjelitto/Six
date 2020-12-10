using System;
using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ConditionList : ExpressionItemList<ICondition>, IExpression
        {
            public ConditionList(List<ICondition> conditions) : base(conditions) { }
            public ConditionList() { }

            public static ConditionList Parse(Parser parser)
            {
                var conditions = new List<ICondition>();

                do
                {
                    var condition = ICondition.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ConditionList)}");
                    conditions.Add(condition);
                }
                while (parser.Match(ToKind.Comma));

                return new ConditionList(conditions);
            }

            public override string ToString()
            {
                return string.Join(", ", this);
            }
        }
    }
}