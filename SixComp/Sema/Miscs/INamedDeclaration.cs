namespace SixComp.Sema
{
    public interface INamedDeclaration : IDeclaration, INamed
    {
        public bool IsParentScope => this is IParentScope;
    }
}
