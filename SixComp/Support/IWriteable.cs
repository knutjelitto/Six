namespace SixComp.Support
{
    public interface IWriteable
    {
        void Write(IWriter writer) { writer.WriteLine(this.ToString() ?? "<<<huh?>>>"); }
    }
}
