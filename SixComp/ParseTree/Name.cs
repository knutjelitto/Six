namespace SixComp.ParseTree
{
    public class Name
    {
        public Name(Token token)
        {
            Token = token;
        }

        public Token Token { get; }

        public static Name Parse(Parser parser, bool withOperators = false)
        {
            if (withOperators)
            {
                if (parser.Current == ToKind.Name || Operator.Firsts.Contains(parser.Current))
                {
                    return new Name(parser.ConsumeAny());
                }
            }

            return new Name(parser.Consume(ToKind.Name));
        }

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}
