using SixComp.Common;
using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class PrecedenceGroupDeclaration : Base<Tree.PrecGroupDeclaration>, IDeclaration, INamed
    {
        public PrecedenceGroupDeclaration(IScoped outer, Tree.PrecGroupDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Assoc = Associativity.None;
            Assign = false;
            HigherThan = new HashSet<BaseName>();
            LowerThan = new HashSet<BaseName>();
        }

        public BaseName Name { get; }
        public Associativity Assoc { get; private set; }
        public bool Assign { get; private set; }
        public HashSet<BaseName> HigherThan { get; }
        public HashSet<BaseName> LowerThan { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Precedence))
            {
                Name.Report(writer, Strings.Head.Name);
                Assoc.Report(writer, Strings.Head.Assoc);
                Assign.Report(writer, Strings.Head.Assign);

                string.Join(" ", HigherThan.Select(p => p.Name.Text).OrderBy(t => t)).Report(writer, Strings.Head.HigherThan);
                string.Join(" ", LowerThan.Select(p => p.Name.Text).OrderBy(t => t)).Report(writer, Strings.Head.LowerThan);
            }
        }

        public void MakeHigherThan(IEnumerable<BaseName> otherGroups)
        {
            foreach (var otherName in otherGroups)
            {
                if (Scope.Package.Global.Precedences.TryGetValue(otherName, out var other))
                {
                    HigherThan.Add(other.Name);
                    other.LowerThan.Add(Name);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void MakeLowerThan(IEnumerable<BaseName> otherGroups)
        {
            foreach (var otherName in otherGroups)
            {
                if (Scope.Package.Global.Precedences.TryGetValue(otherName, out var other))
                {
                    LowerThan.Add(other.Name);
                    other.HigherThan.Add(Name);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void SetAssign(bool assign)
        {
            Assign = assign;
        }

        public void SetAssoc(Associativity assoc)
        {
            Assoc = assoc;
        }
    }
}
