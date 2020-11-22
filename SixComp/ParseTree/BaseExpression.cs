namespace SixComp.ParseTree
{
    public class BaseExpression : AnyExpression
    {
        public virtual AnyExpression? LastExpression => this;
    }
}
