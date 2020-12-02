using System.Diagnostics;

namespace SixComp.Sema
{
    public interface IType : IReportable
    {
        public static IType? MaybeBuild(IScoped outer, Tree.AnyType? type)
        {
            if (type == null)
            {
                return null;
            }
            return Visit(outer, (dynamic)type);
        }

        public static IType Build(IScoped outer, Tree.AnyType type)
        {
            return Visit(outer, (dynamic)type);
        }

        public static IType Build(IScoped outer, Tree.FunctionResult? type)
        {
            if (type == null)
            {
                return new VoidType();
            }
            return Visit(outer, (dynamic)type);
        }

        private static IType Visit(IScoped outer, Tree.TypeAnnotation type)
        {
            return Visit(outer, type.Type);
        }

        private static IType Visit(IScoped outer, Tree.PrefixedType type)
        {
            if (type.Inout)
            {
                return new InoutType(outer, type);
            }

            return Build(outer, type.Type);
        }

        private static IType Visit(IScoped outer, Tree.TypeIdentifier type)
        {
            Debug.Assert(type.Count >= 1);

            if (type.Count == 1)
            {
                return Build(outer, type[0]);
            }
            return new TypeIdentifier(outer, type);
        }

        private static IType Visit(IScoped outer, Tree.FullName type)
        {
            return new FullName(outer, type);
        }
        private static IType Visit(IScoped outer, Tree.ArrayType type)
        {
            return new ArrayType(outer, type);
        }

        private static IType Visit(IScoped outer, Tree.DictionaryType type)
        {
            return new DictionaryType(outer, type);
        }

        private static IType Visit(IScoped outer, Tree.FunctionType type)
        {
            return new FuncType(outer, type);
        }

        private static IType Visit(IScoped outer, Tree.FunctionResult type)
        {
            return Build(outer, type.Type);
        }

        private static IType Visit(IScoped outer, Tree.TupleType type)
        {
            return new TupleType(outer, type);
        }

        private static IType Visit(IScoped outer, Tree.OptionalType type)
        {
            return new OptionalType(outer, type);
        }

        private static IType Visit(IScoped outer, Tree.GenericRequirement type)
        {
            return Build(outer, type.Composition);
        }

        private static IType Visit(IScoped outer, Tree.TypealiasAssignment type)
        {
            return Build(outer, type.Type);
        }

        private static IType Visit(IScoped outer, Tree.UnwrapType type)
        {
            return new UnwrapType(outer, type);
        }

        private static IType Visit(IScoped outer, Tree.ProtocolCompositionType type)
        {
            if (type.Count == 1)
            {
                return Build(outer, type[0]);
            }
            return new CompositionType(outer, type);
        }

        private static IType Visit(IScoped outer, Tree.AnyType type)
        {
            return new Dummy(outer, type);
        }
    }
}
