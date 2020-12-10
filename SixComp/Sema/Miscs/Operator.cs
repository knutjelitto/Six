using SixComp.Common;
using SixComp.Support;
using System;

namespace SixComp.Sema
{
    public interface IOperator : IReportable, INamed
    {
        bool GreaterThanOrEqual(PrecedenceGroupDeclaration? otherPrecedence);
        bool GreaterThan(PrecedenceGroupDeclaration? otherPrecedence);
        PrecedenceGroupDeclaration? Precedence { get; }

        public static IOperator Build(IScoped outer, ParseTree.Operator tree)
        {
            return tree switch
            {
                ParseTree.Operator.NamedOperator named => new Named(outer, named),
                ParseTree.Operator.AssignmentOperator named => new Assignment(outer, named),
                ParseTree.Operator.ConditionalOperator conditional => new Conditional(outer, conditional),
                ParseTree.Operator.CastOperator cast when cast.Kind == CastKind.Is => new CastIs(outer, cast),
                ParseTree.Operator.CastOperator cast when cast.Kind == CastKind.As => new CastAsStatic(outer, cast),
                ParseTree.Operator.CastOperator cast when cast.Kind == CastKind.AsChain => new CastAsDynamicSoft(outer, cast),
                ParseTree.Operator.CastOperator cast when cast.Kind == CastKind.AsForce => new CastAsDynamicHard(outer, cast),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public abstract class Operator<TTree> : Base<TTree>, IOperator
            where TTree : ParseTree.Operator
        {
            public Operator(IScoped outer, TTree tree, BaseName name, PrecedenceGroupDeclaration? precedence)
                : base(outer, tree)
            {
                Name = name;
                Precedence = precedence ?? Outer.Global.InfixOperators[Name].Precedence!;
            }

            public BaseName Name { get; }
            public PrecedenceGroupDeclaration Precedence { get; }

            public bool GreaterThanOrEqual(PrecedenceGroupDeclaration? otherPrecedence)
            {
                if (otherPrecedence == null)
                {
                    return true;
                }

                return Precedence.Name == otherPrecedence.Name
                    || Precedence.HigherThan.Contains(otherPrecedence.Name);
            }

            public bool GreaterThan(PrecedenceGroupDeclaration? otherPrecedence)
            {
                if (otherPrecedence == null)
                {
                    return true;
                }

                return Precedence.HigherThan.Contains(otherPrecedence.Name)
                    || (Precedence.Name == otherPrecedence.Name && Precedence.Assoc == AssociativityKind.Right);
            }

            public override void Report(IWriter writer)
            {
                throw new NotImplementedException();
            }

            public override string ToString()
            {
                return Name.Text;
            }

        }

        public class Assignment : Operator<ParseTree.Operator.AssignmentOperator>
        {
            public Assignment(IScoped outer, ParseTree.Operator.AssignmentOperator tree)
                : base(outer, tree, new BaseName(outer, tree.Name), outer.Global.AssignmentPrecedence)
            {
            }
        }

        public class Named : Operator<ParseTree.Operator.NamedOperator>
        {
            public Named(IScoped outer, ParseTree.Operator.NamedOperator tree)
                : base(outer, tree, new BaseName(outer, tree.Name), null)
            {
            }
        }

        public class Conditional : Operator<ParseTree.Operator.ConditionalOperator>
        {
            public Conditional(IScoped outer, ParseTree.Operator.ConditionalOperator tree)
                : base(outer, tree, new BaseName(outer, "?:"), outer.Global.TernaryPrecedence)
            {
            }
        }

        public class CastIs : Operator<ParseTree.Operator.CastOperator>
        {
            public CastIs(IScoped outer, ParseTree.Operator.CastOperator tree)
                : base(outer, tree, new BaseName(outer, "is"), outer.Global.CastingPrecedence)
            {
            }
        }

        public class CastAsStatic : Operator<ParseTree.Operator.CastOperator>
        {
            public CastAsStatic(IScoped outer, ParseTree.Operator.CastOperator tree)
                : base(outer, tree, new BaseName(outer, "as"), outer.Global.CastingPrecedence)
            {
            }
        }

        public class CastAsDynamicSoft : Operator<ParseTree.Operator.CastOperator>
        {
            public CastAsDynamicSoft(IScoped outer, ParseTree.Operator.CastOperator tree)
                : base(outer, tree, new BaseName(outer, "as?"), outer.Global.CastingPrecedence)
            {
            }
        }

        public class CastAsDynamicHard : Operator<ParseTree.Operator.CastOperator>
        {
            public CastAsDynamicHard(IScoped outer, ParseTree.Operator.CastOperator tree)
                : base(outer, tree, new BaseName(outer, "as!"), outer.Global.CastingPrecedence)
            {
            }
        }
    }
}
