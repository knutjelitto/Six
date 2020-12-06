using SixComp.Support;

namespace SixComp.Sema
{
    public class FuncParameter: Base<Tree.Parameter>, INamedDeclaration
    {
        public FuncParameter(IScoped outer, Tree.Parameter tree)
            : base(outer, tree)
        {
            Intern = new BaseName(Outer, Tree.Intern);
            Extern = Tree.Extern == null ? Intern : new BaseName(Outer, Tree.Extern);
            Omittable = Extern.Name.ToString() == "_";
            Type = ITypeDefinition.Build(Outer, Tree.Type);
            Variadic = Tree.Variadic;
            Init = IExpression.MaybeBuild(Outer, Tree.Initializer);

            Declare(this);
        }

        public BaseName Intern { get; }
        public BaseName Extern { get; }
        public bool Omittable { get; }
        public bool Variadic { get; }

        public ITypeDefinition Type { get; }
        public IExpression? Init { get; }

        public BaseName Name => Intern;

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Parameter))
            {
                Intern.Text.Report(writer, Strings.Head.Intern);
                Extern.Text.Report(writer, Strings.Head.Extern);
                Omittable.Report(writer, Strings.Head.Omittable);
                Variadic.Report(writer, Strings.Head.Variadic);
                Type.Report(writer, Strings.Head.Type);
                Init.Report(writer, Strings.Head.Initializer);
            }
        }

        public override string ToString()
        {
            return $"{Extern.Text}:";
        }
    }
}
