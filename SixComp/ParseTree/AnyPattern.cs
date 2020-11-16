using System;
using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyPattern : IWriteable
    {
        public static AnyPattern Parse(Parser parser)
        {
            return IdentifierPattern.Parse(parser);
        }
    }
}
