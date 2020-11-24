using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public abstract class PrecGroupAttribute : SyntaxNode
    {
        public static TokenSet Firsts = new TokenSet(ToKind.KwHigherThan, ToKind.KwLowerThan, ToKind.KwAssignment, ToKind.KwAssociativity);

        public static PrecGroupAttribute Parse(Parser parser)
        {
            switch (parser.Current)
            {
                case ToKind.KwHigherThan:
                case ToKind.KwLowerThan:
                    return Relation.Parse(parser);
                case ToKind.KwAssignment:
                    return Assignment.Parse(parser);
                case ToKind.KwAssociativity:
                    return Associativity.Parse(parser);
            }

            parser.Consume(Firsts);

            throw new InvalidOperationException("<NEVER>");
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

            public static new PrecGroupAttribute Parse(Parser parser)
            {
                var kind = parser.Current == ToKind.KwHigherThan ? RelationKind.HigherThan : RelationKind.LowerThan;
                if (kind == RelationKind.HigherThan)
                {
                    parser.Consume(ToKind.KwHigherThan);
                }
                else
                {
                    parser.Consume(ToKind.KwLowerThan);
                }
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

            public static new PrecGroupAttribute Parse(Parser parser)
            {
                parser.Consume(ToKind.KwAssignment);
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

            public static new PrecGroupAttribute Parse(Parser parser)
            {
                parser.Consume(ToKind.KwAssociativity);
                parser.Consume(ToKind.Colon);

                var kind = AssociativityKind.None;
                switch (parser.Current)
                {
                    case ToKind.KwLeft:
                        parser.ConsumeAny();
                        kind = AssociativityKind.Left;
                        break;
                    case ToKind.KwRight:
                        parser.ConsumeAny();
                        kind = AssociativityKind.Right;
                        break;
                    default:
                        parser.Consume(ToKind.KwNone);
                        kind = AssociativityKind.None;
                        break;
                }

                return new Associativity(kind);
            }
        }
    }
}
