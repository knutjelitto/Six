namespace SixComp
{
    public static class Precedence
    {
        public const int Assignement = 10;
        public const int Conditional = 20;
        public const int Relation = 30;
        public const int Addition = 40;
        public const int Multiplication = 50;
        public const int Exponent = 60;
        public const int Prefix = 70;
        public const int Postfix = 80;
        public const int Call = 90;
        public const int Index = 90;
        public const int Select = 90;
    }
}
