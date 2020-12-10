using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class IfStatement : IStatement
        {
            public IfStatement(ConditionList conditions, CodeBlock thenPart, IStatement? elsePart)
            {
                Conditions = conditions;
                ThenPart = thenPart;
                ElsePart = elsePart;
            }

            public ConditionList Conditions { get; }
            public CodeBlock ThenPart { get; }
            public IStatement? ElsePart { get; }

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
                IStatement? elsePart = null;
                if (parser.Match(ToKind.KwElse))
                {
                    if (parser.Current == ToKind.KwIf)
                    {
                        elsePart = IfStatement.Parse(parser);
                    }
                    else
                    {
                        elsePart = CodeBlock.Parse(parser);
                    }
                }

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

            public override string ToString()
            {
                var elsePart = ElsePart == null ? string.Empty : $" {ElsePart}";
                return $"if {Conditions} {ThenPart}{elsePart}";
            }
        }
    }
}