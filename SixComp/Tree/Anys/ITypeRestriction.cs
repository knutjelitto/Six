namespace SixComp
{
    public partial class ParseTree
    {
        public interface ITypeRestriction
        {
            public static ITypeRestriction Parse(Parser parser)
            {
                TypeIdentifier name = TypeIdentifier.Parse(parser);

                if (parser.Match(ToKind.Equals))
                {
                    var type = IType.Parse(parser);

                    return SameTypeRequirement.From(name, type);
                }

                parser.Match(ToKind.Colon);

                var composition = ProtocolCompositionType.Parse(parser);

                return ConformanceRequirement.From(name, composition);
            }

            public class SameTypeRequirement : ITypeRestriction
            {
                private SameTypeRequirement(IType name, IType type)
                {
                    Name = name;
                    Type = type;
                }

                public IType Name { get; }
                public IType Type { get; }

                public static SameTypeRequirement From(IType name, IType type)
                {
                    return new SameTypeRequirement(name, type);
                }

                public override string ToString()
                {
                    return $"{Name} == {Type}";
                }
            }

            public class ConformanceRequirement : ITypeRestriction
            {
                private ConformanceRequirement(IType name, ProtocolCompositionType composition)
                {
                    Name = name;
                    Composition = composition;
                }

                public IType Name { get; }
                public ProtocolCompositionType Composition { get; }

                public static ConformanceRequirement From(IType name, ProtocolCompositionType composition)
                {
                    return new ConformanceRequirement(name, composition);
                }

                public override string ToString()
                {
                    return $"{Name}: {Composition}";
                }
            }
        }
    }
}