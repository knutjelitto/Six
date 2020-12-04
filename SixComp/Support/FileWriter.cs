using System;
using System.IO;

namespace SixComp.Support
{
    public class FileWriter : IndentWriter, IDisposable
    {
        public FileWriter(string filename)
            : base(new BaseTextWriter(new StreamWriter(filename)))
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
