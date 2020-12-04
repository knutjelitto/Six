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

        public static IOperator Build(IScoped outer, Tree.Operator tree)
        {
            return tree switch
            {
                Tree.Operator.NamedOperator named => new Named(outer, named),
                Tree.Operator.AssignmentOperator named => new Assignment(outer, named),
                Tree.Operator.ConditionalOperator conditional => new Conditional(outer, conditional),
                Tree.Operator.CastOperator cast when cast.Kind == CastKind.Is => new CastIs(outer, cast),
                Tree.Operator.CastOperator cast when cast.Kind == CastKind.As => new CastAs(outer, cast),
                Tree.Operator.CastOperator cast when cast.Kind == CastKind.AsChain => new CastAsSoft(outer, cast),
                Tree.Operator.CastOperator cast when cast.Kind == CastKind.AsForce => new CastAsHard(outer, cast),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public abstract class Operator<TTree> : Base<TTree>, IOperator
            where TTree : Tree.Operator
        {
            public Operator(IScoped outer, TTree tree, BaseName name, PrecedenceGroupDeclaration? precedence)
                : base(outer, tree)
            {
                Name = name;
                Precedence = precedence ?? Outer.Scope.Global.InfixOperators[Name].Precedence!;
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
                    || (Precedence.Name == otherPrecedence.Name && Precedence.Assoc == Associativity.Right);
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

        public class Assignment : Operator<Tree.Operator.AssignmentOperator>
        {
            public Assignment(IScoped outer, Tree.Operator.AssignmentOperator tree)
                : base(outer, tree, new BaseName(outer, tree.Name), outer.Scope.Global.AssignmentPrecedence)
            {
            }
        }

        public class Named : Operator<Tree.Operator.NamedOperator>
        {
            public Named(IScoped outer, Tree.Operator.NamedOperator tree)
                : base(outer, tree, new BaseName(outer, tree.Name), null)
            {
            }
        }

        public class Conditional : Operator<Tree.Operator.ConditionalOperator>
        {
            public Conditional(IScoped outer, Tree.Operator.ConditionalOperator tree)
                : base(outer, tree, new BaseName(outer, "?:"), outer.Scope.Global.TernaryPrecedence)
            {
            }
        }

        public class CastIs : Operator<Tree.Operator.CastOperator>
        {
            public CastIs(IScoped outer, Tree.Operator.CastOperator tree)
                : base(outer, tree, new BaseName(outer, "is"), outer.Scope.Global.CastingPrecedence)
            {
            }
        }

        public class CastAs : Operator<Tree.Operator.CastOperator>
        {
            public CastAs(IScoped outer, Tree.Operator.CastOperator tree)
                : base(outer, tree, new BaseName(outer, "as"), outer.Scope.Global.CastingPrecedence)
            {
            }
        }

        public class CastAsSoft : Operator<Tree.Operator.CastOperator>
        {
            public CastAsSoft(IScoped outer, Tree.Operator.CastOperator tree)
                : base(outer, tree, new BaseName(outer, "as?"), outer.Scope.Global.CastingPrecedence)
            {
            }
        }

        public class CastAsHard : Operator<Tree.Operator.CastOperator>
        {
            public CastAsHard(IScoped outer, Tree.Operator.CastOperator tree)
                : base(outer, tree, new BaseName(outer, "as!"), outer.Scope.Global.CastingPrecedence)
            {
            }
        }
    }
}
