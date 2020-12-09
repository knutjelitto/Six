using SixComp.Support;

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
