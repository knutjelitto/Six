namespace SixComp.Sema
{
    public interface IScope: IReportable
    {
        IScoped Parent { get; }
        Module Module { get; }
        Global Global { get; }

        void Declare(INamedDeclaration named);
    }
}
