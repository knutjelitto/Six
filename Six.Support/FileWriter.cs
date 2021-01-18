using System;
using System.IO;
using System.Text;

namespace Six.Support
{
    public class FileWriter : IndentWriter, IDisposable
    {
        public FileWriter(string filename, string? indent = null)
            : base(new BaseTextWriter(new StreamWriter(filename, false, Encoding.UTF8)), indent)
        {
        }

        public void Dispose()
        {
            var @base = (BaseTextWriter)Writer;

            var stream = (StreamWriter)@base.Writer;
            stream.Flush();
            stream.Close();
        }
    }
}
