namespace SixComp
{
    public partial class ParseTree
    {
        public class BaseExpression : IExpression
        {
            public virtual IExpression? LastExpression => this;
        }
    }
}