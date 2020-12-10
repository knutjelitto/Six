namespace SixComp
{
    public partial class ParseTree
    {
        public class SyntaxNode : ISyntaxNode
        {
            public NodeData? Data { get; set; } = null;
        }
    }
}