using System.Diagnostics;

namespace SixComp.Sema
{
    public interface ITypeDefinition : IReportable
    {
        public static ITypeDefinition? MaybeBuild(IScoped outer, ParseTree.IType? type)
        {
            if (type == null)
            {
                return null;
            }
            return Visit(outer, (dynamic)type);
        }

        public static ITypeDefinition Build(IScoped outer, ParseTree.IType type)
        {
            return Visit(outer, (dynamic)type);
        }

        public static ITypeDefinition Build(IScoped outer, ParseTree.FunctionResult? type)
        {
            if (type == null)
            {
                return new VoidType();
            }
            return Visit(outer, (dynamic)type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.TypeAnnotation type)
        {
            return Visit(outer, type.Type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.PrefixedType type)
        {
            if (type.Inout)
            {
                return new InoutType(outer, type);
            }

            return Build(outer, type.Type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.TypeIdentifier type)
        {
            Debug.Assert(type.Count >= 1);

            if (type.Count == 1)
            {
                return Build(outer, type[0]);
            }
            return new TypeIdentifier(outer, type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.FullName type)
        {
            return new FullName(outer, type);
        }
        private static ITypeDefinition Visit(IScoped outer, ParseTree.ArrayType type)
        {
            return new ArrayType(outer, type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.DictionaryType type)
        {
            return new DictionaryType(outer, type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.FunctionType type)
        {
            return new FuncType(outer, type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.FunctionResult type)
        {
            return Build(outer, type.Type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.TupleType type)
        {
            return new TupleType(outer, type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.OptionalType type)
        {
            return new OptionalType(outer, type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.GenericRequirement type)
        {
            return Build(outer, type.Composition);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.TypealiasAssignment type)
        {
            return Build(outer, type.Type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.UnwrapType type)
        {
            return new UnwrapType(outer, type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.ProtocolCompositionType type)
        {
            if (type.Count == 1)
            {
                return Build(outer, type[0]);
            }
            return new CompositionType(outer, type);
        }

        private static ITypeDefinition Visit(IScoped outer, ParseTree.IType type)
        {
            return new Dummy(outer, type);
        }
    }
}
