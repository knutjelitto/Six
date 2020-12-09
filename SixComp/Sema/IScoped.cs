using System.Runtime.CompilerServices;

namespace SixComp.Sema
{
    public interface IScoped 
    {
        Scope Scope { get; }
        IScoped Outer { get; }
        Global Global { get; }

        public string Path
        {
            get
            {
                if (this is Module module)
                {
                    return module.ModuleName;
                }
                var outer = Outer;
                while (outer == this)
                {
                    outer = outer.Outer;
                }
                var name = (this as INamed)?.Name.Text ?? GetType().Name;

                return outer.Path + "." + name;
            }
        }
    }
}
