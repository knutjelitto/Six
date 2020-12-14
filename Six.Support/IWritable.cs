namespace Six.Support
{
    public interface IWritable
    {
        public void Write(IWriter writer) { writer.WriteLine(ToString() ?? "<<<huh?>>>"); }
    }
}
