namespace SixComp.Tree
{
    public class BaseExpression : AnyExpression
    {
        public virtual AnyExpression? LastExpression => this;
    }
}
