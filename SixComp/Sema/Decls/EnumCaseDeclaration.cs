﻿using Six.Support;

namespace SixComp.Sema
{
    public class EnumCaseDeclaration : BaseScoped<ParseTree.EnumCaseItem>, INamedDeclaration
    {
        public EnumCaseDeclaration(IScoped outer, ParseTree.Prefix prefix, ParseTree.EnumCaseItem tree)
            : base(outer, tree)
        {
            Prefix = prefix;

            Name = new BaseName(Outer, tree.Name);
            Tuple = (TupleType?)ITypeDefinition.MaybeBuild(Outer, tree.Tuple);
            Initializer = IExpression.MaybeBuild(outer, tree.Initializer);

            Declare(this);
        }

        public ParseTree.Prefix Prefix { get; }

        public BaseName Name { get; }
        public TupleType? Tuple { get; }
        public IExpression? Initializer { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Case))
            {
                Name.Report(writer, Strings.Head.Name);
                Tuple?.Report(writer);
                Initializer.Report(writer, Strings.Head.Initializer);
            }
        }
    }
}
