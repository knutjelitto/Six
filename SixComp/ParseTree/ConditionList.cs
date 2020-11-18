using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ConditionList : ItemList<AnyCondition>
    {
        public ConditionList(List<AnyCondition> conditions) : base(conditions) { }
        public ConditionList() { }

        public static ConditionList Parse(Parser parser)
        {
            var conditions = new List<AnyCondition>();

            do
            {
                var condition = AnyCondition.Parse(parser);
                conditions.Add(condition);
            }
            while (parser.Match(ToKind.Comma));

            return new ConditionList(conditions);
        }
    }
}
