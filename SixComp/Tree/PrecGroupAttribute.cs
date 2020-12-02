using SixComp.Support;

namespace SixComp.Tree
{
    public abstract class PrecGroupAttribute : SyntaxNode
    {
        public static PrecGroupAttribute? TryParse(Parser parser)
        {
            return parser.CurrentToken.Text switch
            {
                Contextual.LowerThan => Relation.Parse(parser, Relation.RelationKind.LowerThan),
                Contextual.HigherThan => Relation.Parse(parser, Relation.RelationKind.HigherThan),
                Contextual.Assignment => Assignment.Parse(parser),
                Contextual.Associativity => Associativity.Parse(parser),
                _ => null,
            };
        }

        public abstract void Write(IWriter writer);

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

            public override void Write(IWriter writer)
            {
                var hilo = Kind switch
                {
                    RelationKind.LowerThan => Contextual.HigherThan,
                    RelationKind.HigherThan => Contextual.LowerThan,
                    _ => string.Empty,
                };
                writer.WriteLine($"{hilo}: {Names}");
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

            public override void Write(IWriter writer)
            {
                var @is = IsAssignment ? Kw.True : Kw.False;
                writer.WriteLine($"{Contextual.Assignment}: {@is}");
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

            public override void Write(IWriter writer)
            {
                var kind = Kind switch
                {
                    AssociativityKind.Left => Contextual.Left,
                    AssociativityKind.Right => Contextual.Right,
                    AssociativityKind.None => Contextual.None,
                    _ => Contextual.None,
                };
                writer.WriteLine($"{Contextual.Associativity}: {kind}");
            }
        }
    }
}
