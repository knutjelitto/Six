namespace SixComp.Sema
{
    public abstract class GenericRestriction : Base, IReportable
    {
        protected GenericRestriction(IScoped outer)
            : base(outer)
        {
        }
    }
}
