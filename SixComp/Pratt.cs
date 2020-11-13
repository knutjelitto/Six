using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp
{
    public class Pratt
    {
        private readonly Parser parser;

        public Pratt(Parser parser)
        {
            this.parser = parser;
        }
    }
}
