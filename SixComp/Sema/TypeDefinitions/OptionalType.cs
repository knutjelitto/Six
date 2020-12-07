﻿using SixComp.Support;

namespace SixComp.Sema
{
    public class OptionalType : Base<Tree.OptionalType>, ITypeDefinition
    {
        public OptionalType(IScoped outer, Tree.OptionalType tree)
            : base(outer, tree)
        {
            Type = ITypeDefinition.Build(Outer, Tree.Type);
        }

        public ITypeDefinition Type { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Type);
        }

        public override void Report(IWriter writer)
        {
            Type.Report(writer, Strings.Head.Optional);
        }
    }
}
