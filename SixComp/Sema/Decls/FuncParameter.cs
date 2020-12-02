using SixComp.Support;

namespace SixComp.Sema
{
    public class FuncParameter: Base<Tree.Parameter>, INamed
    {
        public FuncParameter(IOwner owner, Tree.Parameter tree)
            : base(owner, tree)
        {
            Owner = owner;

            Intern = new BaseName(owner, Tree.Intern);
            Extern = Tree.Extern == null ? Intern : new BaseName(owner, Tree.Extern);
            OptionalExtern = Extern.Name.ToString() == "_";
            Type = IType.Build(Outer, Tree.Type);
            Variadic = Tree.Variadic;
            Init = IExpression.MaybeBuild(Owner, Tree.Initializer);
        }

        public IOwner Owner { get; }

        public BaseName Intern { get; }
        public BaseName Extern { get; }
        public bool OptionalExtern { get; }
        public bool Variadic { get; }

        public IType Type { get; }
        public IExpression? Init { get; }

        public BaseName Name => Intern;

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Parameter))
            {
                Intern.Text.Report(writer, Strings.Head.Intern);
                Extern.Text.Report(writer, Strings.Head.Extern);
                Variadic.Report(writer, Strings.Head.Variadic);
                Type.Report(writer, Strings.Head.Type);
                Init.Report(writer, Strings.Head.Initializer);
            }
        }
    }
}
