using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public abstract class PrecGroupAttribute : SyntaxNode
    {
        public static PrecGroupAttribute? TryParse(Parser parser)
        {
            switch (parser.CurrentToken.Text)
            {
                case Contextual.LowerThan:
                    return Relation.Parse(parser, Relation.RelationKind.LowerThan);
                case Contextual.HigherThan:
                    return Relation.Parse(parser, Relation.RelationKind.HigherThan);
                case Contextual.Assignment:
                    return Assignment.Parse(parser);
                case Contextual.Associativity:
                    return Associativity.Parse(parser);
            }

            return null;
        }

        public class Relation : PrecGroupAttribute
        {
            public RelationKind Kind { get; }
            public NameList Names { get; }

            public enum RelationKind
            {
                HigherThan,
                LowerThan,
            }

            public Relation(RelationKind kind, NameList names)
            {
                Kind = kind;
                Names = names;
            }

            public static PrecGroupAttribute Parse(Parser parser, RelationKind kind)
            {
                parser.ConsumeAny();
                parser.Consume(ToKind.Colon);
                var names = NameList.Parse(parser);

                return new Relation(kind, names);
            }
        }

        public class Assignment : PrecGroupAttribute
        {
            public Assignment(bool isAssignment)
            {
                IsAssignment = isAssignment;
            }

            public bool IsAssignment { get; }

            public static PrecGroupAttribute Parse(Parser parser)
            {
                parser.ConsumeAny();
                parser.Consume(ToKind.Colon);
                var kind = parser.Current == ToKind.KwTrue;
                if (kind)
                {
                    parser.Consume(ToKind.KwTrue);
                }
                else
                {
                    parser.Consume(ToKind.KwFalse);
                }

                return new Assignment(kind);
            }
        }

        public class Associativity : PrecGroupAttribute
        {
            public enum AssociativityKind
            {
                Left,
                Right,
                None,
            }

            private Associativity(AssociativityKind kind)
            {
                Kind = kind;
            }

            public AssociativityKind Kind { get; }

            public static PrecGroupAttribute Parse(Parser parser)
            {
                parser.ConsumeAny();
                parser.Consume(ToKind.Colon);

                var kind = AssociativityKind.None;
                switch (parser.CurrentToken.Text)
                {
                    case Contextual.Left:
                        parser.ConsumeAny();
                        kind = AssociativityKind.Left;
                        break;
                    case Contextual.Right:
                        parser.ConsumeAny();
                        kind = AssociativityKind.Right;
                        break;
                    case Contextual.None:
                        parser.ConsumeAny();
                        kind = AssociativityKind.None;
                        break;
                }

                return new Associativity(kind);
            }
        }
    }
}
