using SixComp.Entities;
using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public interface IResolveable
    {
        void Resolve(IWriter writer);
    }
}
