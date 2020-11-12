using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SixComp
{
    public static class Precedence
    {
        public const int Assignement = 10;
        public const int Conditional = 20;
        public const int Addition = 30;
        public const int Multiplication = 40;
        public const int Exponent = 50;
        public const int Prefix = 60;
        public const int Postfix = 70;
        public const int Call = 80;
    }
}
