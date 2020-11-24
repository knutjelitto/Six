using System;
using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ConditionList : ExpressionItemList<AnyCondition>, AnyExpression
    {
        public ConditionList(List<AnyCondition> conditions) : base(conditions) { }
        public ConditionList() { }

        public static ConditionList Parse(Parser parser)
        {
            var conditions = new List<AnyCondition>();

            do
            {
                var condition = AnyCondition.TryParse(parser) ?? throw new InvalidOperationException();
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
