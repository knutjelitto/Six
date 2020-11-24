using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class OperatorDeclaration : AnyDeclaration
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.KwPrefix, ToKind.KwPostfix, ToKind.KwInfix);

        public Prefix Prefix { get; }
        public OperatorKind Kind { get; }
        public Name Operator { get; }
        public NameList Names { get; }
        public Name? GroupName { get; }

        public enum OperatorKind
        {
            Prefix,
            Postfix,
            Infix,
        }

        protected OperatorDeclaration(Prefix prefix, OperatorKind kind, Name @operator, NameList names)
        {
            Prefix = prefix;
            Kind = kind;
            Operator = @operator;
            Names = names;
        }

        public static OperatorDeclaration Parse(Parser parser, Prefix prefix)
        {
            OperatorKind? kind = null;

            switch (parser.Current)
            {
                case ToKind.KwPrefix:
                    parser.ConsumeAny();
                    kind = OperatorKind.Prefix;
                    break;
                case ToKind.KwPostfix:
                    parser.ConsumeAny();
                    kind = OperatorKind.Postfix;
                    break;
                case ToKind.KwInfix:
                    parser.ConsumeAny();
                    kind = OperatorKind.Infix;
                    break;
            }

            if (kind == null)
            {
                parser.Consume(Firsts);

                throw new InvalidOperationException("<NEVER>");
            }

            parser.Consume(ToKind.KwOperator);

            if (parser.IsOperator)
            {
                var name = Name.Parse(parser, true);

                NameList? names = null;
                if (parser.Match(ToKind.Colon))
                {
                    names = NameList.Parse(parser);
                }
                return new OperatorDeclaration(prefix, kind.Value, name, names ?? new NameList());
            }

            throw new NotImplementedException();
        }
    }
}
