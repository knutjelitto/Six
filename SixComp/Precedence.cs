namespace SixComp
{
    public static class Precedence
    {
        public const int Prefix = 1000;
        public const int Exponent = 900;
        public const int Multiplication = 800;
        public const int Addition = 700;
        public const int RangeFormation = 600;
        public const int Casting = 500;
        public const int Comparison = 400;
        public const int Conjunction = 300;
        public const int Ternary = 200;
        public const int Assignment = 100;
    }
}
