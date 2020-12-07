using SixComp.Support;

namespace SixComp.Sema
{
    public class DictionaryType : Base<Tree.DictionaryType>, ITypeDefinition
    {
        public DictionaryType(IScoped outer, Tree.DictionaryType tree)
            : base(outer, tree)
        {
            Key = ITypeDefinition.Build(Outer, Tree.KeyType);
            Value = ITypeDefinition.Build(Outer, Tree.ValueType);
        }

        public ITypeDefinition Key { get; }
        public ITypeDefinition Value { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Key, Value);
        }

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
