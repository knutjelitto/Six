namespace SixComp
{
    public static class Precedence
    {
        public const int Prefix = 1100;
        public const int BitwiseShift = 1000;
        public const int Multiplication = 900;
        public const int Addition = 800;
        public const int RangeFormation = 700;
        public const int Casting = 600;
        public const int NilCoalescing = 500;
        public const int Comparison = 400;
        public const int Conjunction = 300;
        public const int Ternary = 200;
        public const int Assignment = 100;
    }
}
