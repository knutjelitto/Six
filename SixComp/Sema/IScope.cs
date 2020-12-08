using SixComp.Entities;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public interface IScope : IReportable
    {
        public static readonly IReadOnlyList<IEntity> NoEntity = new IEntity[0];

        IScoped Parent { get; }
        Module Module { get; }
        Global Global { get; }

        void Declare(IEntity named);
        void Extend(ExtensionDeclaration extension);
        void Extends(IEntity extendee);
        IReadOnlyList<IEntity> Look(INamed named, bool stop = false);
        IReadOnlyList<IEntity> LookUp(INamed named);

        T FindParent<T>(IScoped scoped) where T : notnull, IScoped;
    }
}
