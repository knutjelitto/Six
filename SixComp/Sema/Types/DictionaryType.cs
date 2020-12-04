﻿using SixComp.Support;

namespace SixComp.Sema
{
    public class DictionaryType : Base, IType
    {
        public DictionaryType(IScoped outer, Tree.DictionaryType tree)
            : base(outer)
        {
            Tree = tree;

            Key = IType.Build(Outer, Tree.KeyType);
            Value = IType.Build(Outer, Tree.ValueType);
        }

        public Tree.DictionaryType Tree { get; }
        public IType Key { get; }
        public IType Value { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.DictionaryType))
            {
                Key.Report(writer, Strings.Head.Key);
                Value.Report(writer, Strings.Head.Value);
            }
        }
    }
}
