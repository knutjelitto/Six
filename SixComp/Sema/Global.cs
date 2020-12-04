using System;
using System.Collections.Generic;
using System.Linq;

using RelationKind = SixComp.Tree.PrecGroupAttribute.Relation.RelationKind;
using AssociativityKind = SixComp.Tree.PrecGroupAttribute.Associativity.AssociativityKind;
using SixComp.Common;

namespace SixComp.Sema
{
    public class Global
    {
        public Global()
        {
            Precedences = new Dictionary<BaseName, PrecedenceGroupDeclaration>();
            PrecedencesTodo = new List<PrecedenceGroupDeclaration>();

            PrefixOperators = new Dictionary<BaseName, OperatorDeclaration>();
            PostfixOperators = new Dictionary<BaseName, OperatorDeclaration>();
            InfixOperators = new Dictionary<BaseName, OperatorDeclaration>();
            OperatorsTodo = new List<OperatorDeclaration>();
            InfixesTodo = new List<InfixList>();
        }

        public Dictionary<BaseName, PrecedenceGroupDeclaration> Precedences { get; }
        public PrecedenceGroupDeclaration? TernaryPrecedence { get; private set; }
        public PrecedenceGroupDeclaration? CastingPrecedence { get; private set; }
        public PrecedenceGroupDeclaration? AssignmentPrecedence { get; private set; }
        public Dictionary<BaseName, OperatorDeclaration> PrefixOperators { get; }
        public Dictionary<BaseName, OperatorDeclaration> PostfixOperators { get; }
        public Dictionary<BaseName, OperatorDeclaration> InfixOperators { get; }


        public List<PrecedenceGroupDeclaration> PrecedencesTodo { get; }
        public List<OperatorDeclaration> OperatorsTodo { get; }
        public List<InfixList> InfixesTodo { get; }


        public void CreatePrecedences(IScoped outer)
        {
            foreach (var group in PrecedencesTodo)
            {
                if (group.Name.Text == "TernaryPrecedence")
                {
                    TernaryPrecedence = group;
                }
                else if (group.Name.Text == "CastingPrecedence")
                {
                    CastingPrecedence = group;
                }
                else if (group.Name.Text == "AssignmentPrecedence")
                {
                    AssignmentPrecedence = group;
                }
                Precedences.Add(group.Name, group);
            }

            // seeding
            foreach (var group in PrecedencesTodo)
            {
                foreach (var attribute in group.Tree.Attributes)
                {
                    switch (attribute)
                    {
                        case Tree.PrecGroupAttribute.Relation relation when relation.Kind == RelationKind.HigherThan:
                            group.MakeHigherThan(relation.Names.Select(name => new BaseName(outer, name)));
                            break;
                        case Tree.PrecGroupAttribute.Relation relation when relation.Kind == RelationKind.LowerThan:
                            group.MakeLowerThan(relation.Names.Select(name => new BaseName(outer, name)));
                            break;
                        case Tree.PrecGroupAttribute.Assignment assignment:
                            group.SetAssign(assignment.IsAssignment);
                            break;
                        case Tree.PrecGroupAttribute.Associativity assoc when assoc.Kind == AssociativityKind.None:
                            group.SetAssoc(Associativity.None);
                            break;
                        case Tree.PrecGroupAttribute.Associativity assoc when assoc.Kind == AssociativityKind.Left:
                            group.SetAssoc(Associativity.Left);
                            break;
                        case Tree.PrecGroupAttribute.Associativity assoc when assoc.Kind == AssociativityKind.Right:
                            group.SetAssoc(Associativity.Right);
                            break;
                    }
                }
            }

            // closure
            var again = true;
            while (again)
            {
                again = false;
                foreach (var group in PrecedencesTodo)
                {
                    foreach (var lowerName in group.LowerThan.ToList())
                    {
                        var lowerGroup = Precedences[lowerName]!;
                        foreach (var lowerLower in lowerGroup.LowerThan)
                        {
                            if (!group.LowerThan.Contains(lowerLower))
                            {
                                group.LowerThan.Add(lowerLower);
                                again = true;
                            }
                        }
                    }
                    foreach (var higherName in group.HigherThan.ToList())
                    {
                        var higherGroup = Precedences[higherName]!;
                        foreach (var higherHigher in higherGroup.HigherThan)
                        {
                            if (!group.HigherThan.Contains(higherHigher))
                            {
                                group.HigherThan.Add(higherHigher);
                                again = true;
                            }
                        }
                    }
                }
            }
        }

        public void CreateOperators()
        {
            foreach (var op in OperatorsTodo)
            {
                switch (op.Fixitivity)
                {
                    case Fixitivity.Prefix:
                        PrefixOperators.Add(op.Operator, op);
                        break;
                    case Fixitivity.Postfix:
                        PostfixOperators.Add(op.Operator, op);
                        break;
                    case Fixitivity.Infix:
                        InfixOperators.Add(op.Operator, op);
                        if (op.PrecedenceName != null)
                        {
                            op.SetPrecedence(Precedences[op.PrecedenceName]);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void CreateInfixes()
        {
            // InfixesTodo may grow during processing

            var i = 0;
            while (i < InfixesTodo.Count)
            {
                var infix = InfixesTodo[i];
                infix.MakeInfix();
                i += 1;
            }
        }
    }
}