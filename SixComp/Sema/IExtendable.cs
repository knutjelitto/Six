using System.Collections.Generic;

namespace SixComp.Sema
{
    public interface IExtendable : INamedDeclaration
    {
        IReadOnlyList<ExtensionDeclaration> Extensions { get; }
        bool Extend(ExtensionDeclaration with);

        Declarations Declarations { get; }
    }
}
