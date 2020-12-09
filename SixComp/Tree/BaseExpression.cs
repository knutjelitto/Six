namespace SixComp
{
    public partial class Tree
    {
        public class BaseExpression : AnyExpression
        {
            public virtual AnyExpression? LastExpression => this;
        }
    }
}