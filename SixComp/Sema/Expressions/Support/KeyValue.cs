using SixComp.Support;

namespace SixComp.Sema
{
    public class KeyValue : Base
    {
        public KeyValue(IScoped outer, IExpression key, IExpression value)
            : base(outer)
        {
            Key = key;
            Value = value;
        }

        public IExpression Key { get; }
        public IExpression Value { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.KeyValue))
            {
                Key.Report(writer, Strings.Head.Key);
                Value.Report(writer, Strings.Head.Value);
            }
        }
    }
}
