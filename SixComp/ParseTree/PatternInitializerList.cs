using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class PatternInitializerList : ItemList<PatternInitializer>
    {
        public PatternInitializerList(List<PatternInitializer> items) : base(items) { }
        public PatternInitializerList() { }

        public static PatternInitializerList Parse(Parser parser)
        {
            var list = new List<PatternInitializer>();

            do
            {
                var patternInitializer = PatternInitializer.Parse(parser);

                list.Add(patternInitializer);
            }
            while (parser.Match(ToKind.Comma));

            return new PatternInitializerList(list);
        }

        public static PatternInitializerList From(List<PatternInitializer> pattInits)
        {
            return new PatternInitializerList(pattInits);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
