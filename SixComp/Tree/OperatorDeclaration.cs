using SixComp.Common;
using SixComp.Support;
using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public class OperatorDeclaration : IDeclaration
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.KwPrefix, ToKind.KwPostfix, ToKind.KwInfix);

            public Prefix Prefix { get; }
            public Fixitivity Fixitivity { get; }
            public BaseName Operator { get; }
            public NameList Names { get; }

            protected OperatorDeclaration(Prefix prefix, Fixitivity kind, BaseName @operator, NameList names)
            {
                Prefix = prefix;
                Fixitivity = kind;
                Operator = @operator;
                Names = names;
            }

            public override string ToString()
            {
                var kind = Fixitivity switch
                {
                    Fixitivity.Prefix => ToKind.KwPrefix.GetRep(),
                    Fixitivity.Postfix => ToKind.KwPostfix.GetRep(),
                    Fixitivity.Infix => ToKind.KwInfix.GetRep(),
                    _ => string.Empty,
                };

                var names = Names.Missing ? string.Empty : $" : {Names}";

                return $"{kind} {ToKind.KwOperator.GetRep()} {Operator}{names}";
            }

            public static OperatorDeclaration Parse(Parser parser, Prefix prefix)
            {
                Fixitivity? kind = null;

                switch (parser.Current)
                {
                    case ToKind.KwPrefix:
                        parser.ConsumeAny();
                        kind = Fixitivity.Prefix;
                        break;
                    case ToKind.KwPostfix:
                        parser.ConsumeAny();
                        kind = Fixitivity.Postfix;
                        break;
                    case ToKind.KwInfix:
                        parser.ConsumeAny();
                        kind = Fixitivity.Infix;
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
                    var name = BaseName.Parse(parser, true);

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
}