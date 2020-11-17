namespace SixComp.Support
{
    public interface IWritable
    {
        void Write(IWriter writer) { writer.WriteLine(this.ToString() ?? "<<<huh?>>>"); }
    }
}
