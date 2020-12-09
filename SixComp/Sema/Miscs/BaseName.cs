using SixComp.Entities;
using SixComp.Support;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixComp.Sema
{
    public sealed class BaseName: Base<object>, INamed, ITypeDefinition, IExpression
    {
        public BaseName(IScoped outer, object tree)
            : base(outer, tree)
        {
            Text = Tree.ToString()!;
        }
        public BaseName Name => this;
        public string Text { get; }

        public static BaseName? Maybe(IScoped outer, object? tree)
        {
            if (tree == null)
            {
                return null;
            }
            return new BaseName(outer, tree);
        }

        public static BaseName Self(IScoped outer)
        {
            return new BaseName(outer, "self");
        }

        public void Resolve(IWriter writer)
        {
            if (Text.StartsWith('$'))
            {
                Scope.FindParent<ClosureExpression>(Outer).Parameters.AddImplicit(this);
            }
            if (Text == "self")
            {
                Debug.Assert(true);
            }
            var decls = Scope.LookUp(this);

            if (decls.Count == 0)
            {
                Global.UnresolvedNamesTodo.Add(Text);
            }
        }

        public IReadOnlyList<IEntity> ResolveIn(IWriter writer, IScoped scoped)
        {
            return scoped.Scope.Look(this);
        }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Text);
        }

        public override bool Equals(object? obj)
        {
            return obj is BaseName other && other.Text == Text;
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
