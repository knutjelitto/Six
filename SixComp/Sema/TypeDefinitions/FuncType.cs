using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class FuncType : Items<FuncType.Parameter, Tree.FunctionType>, ITypeDefinition
    {
        public FuncType(IScoped outer, Tree.FunctionType tree)
            : base(outer, tree, Enum(outer, tree))
        {
            Async = tree.Async.Present;
            Throws = tree.Throws.Present;
            Result = ITypeDefinition.Build(Outer, Tree.Result);
        }

        public bool Async { get; }
        public bool Throws { get; }
        public ITypeDefinition Result { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine("function-type");
            using (writer.Indent())
            {
                this.ReportList(writer, Strings.Head.Parameters);
                Async.Report(writer, Strings.Head.Async);
                Throws.Report(writer, Strings.Head.Throws);
                Result.Report(writer, Strings.Head.Result);
            }
        }

        public static IEnumerable<Parameter> Enum(IScoped outer, Tree.FunctionType tree)
        {
            return tree.Arguments.Arguments.Select(parameter => new Parameter(outer, parameter));
        }

        public class Parameter : Base<Tree.FunctionTypeArgument>
        {
            public Parameter(IScoped outer, Tree.FunctionTypeArgument tree)
                : base(outer, tree)
            {
                Extern = BaseName.Maybe(Outer, Tree.Extern);
                Type = ITypeDefinition.Build(Outer, Tree.Type);
            }

            public BaseName? Extern { get; }
            public ITypeDefinition Type { get; }

            public override void Report(IWriter writer)
            {
                using (writer.Indent(Strings.Head.Parameter))
                {
                    Extern.Report(writer, Strings.Head.Extern);
                    Type.Report(writer, Strings.Head.Type);
                }
            }
        }

    }
}
