using SixComp.Support;

namespace SixComp.ParseTree
{
    public class IfStatement : AnyStatement
    {
        public IfStatement(ConditionList conditions, CodeBlock thenPart, CodeBlock? elsePart)
        {
            Conditions = conditions;
            ThenPart = thenPart;
            ElsePart = elsePart;
        }

        public ConditionList Conditions { get; }
        public CodeBlock ThenPart { get; }
        public CodeBlock? ElsePart { get; }

        public static IfStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwIf);
            var conditions = ConditionList.Parse(parser);
            CodeBlock thenPart;
            if (parser.Current != ToKind.LBrace && conditions.LastExpression is FunctionCallExpression call && call.Closures.BlockOnly)
            {
                thenPart = call.Closures.ExtractBlock();
            }
            else
            {
                thenPart = CodeBlock.Parse(parser);
            }
            var elsePart = parser.TryMatch(ToKind.KwElse, CodeBlock.Parse);


            return new IfStatement(conditions, thenPart, elsePart);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"if {Conditions.StripParents()}");
            ThenPart.Write(writer);
            if (ElsePart != null)
            {
                writer.WriteLine("else");
                ElsePart.Write(writer);
            }
        }
    }
}
