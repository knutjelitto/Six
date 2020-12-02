using SixComp.Support;

namespace SixComp.Sema
{
    public class FuncType : Base, IType
    {
        public FuncType(IScoped outer, Tree.FunctionType tree)
            : base(outer)
        {
            Tree = tree;

            Parameters = new FuncTypeParameters(Outer, tree.Arguments);
            Async = tree.Async.Present;
            Throws = tree.Throws.Present;
            Result = IType.Build(Outer, Tree.Result);
        }

        public Tree.FunctionType Tree { get; }
        public Tree.Prefix Prefix => Tree.Prefix;
        public FuncTypeParameters Parameters { get; }
        public bool Async { get; }
        public bool Throws { get; }
        public IType Result { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine("function-type");
            using (writer.Indent())
            {
                writer.WriteLine("parameters");
                Parameters.Report(writer);
                if (Async || Throws)
                {
                    writer.WriteLine($"{Async.Iff("async")}{Throws.Iff("throws")}");
                }
                Result.Report(writer);
            }
        }
    }
}
