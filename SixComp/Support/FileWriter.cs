using System;
using System.IO;

namespace SixComp.Support
{
    public class FileWriter : IndentWriter, IDisposable
    {
        public FileWriter(string filename) : base(new StreamWriter(filename))
        {
        }

        public void Dispose()
        {
            var stream = (StreamWriter)Writer;
            stream.Flush();
            stream.Close();
        }
    }
}
