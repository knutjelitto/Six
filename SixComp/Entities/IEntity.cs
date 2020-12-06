using SixComp.Sema;
using SixComp.Support;

namespace SixComp.Entities
{
    public interface IEntity : INamedDeclaration
    {
        public abstract class Entity<TDecl> : IEntity
            where TDecl : INamedDeclaration
        {
            protected Entity(TDecl declaration)
            {
                Declaration = declaration;
            }

            public TDecl Declaration { get; }

            public IScope Scope => Declaration.Scope;

            public BaseName Name => Declaration.Name;

            public void Report(IWriter writer) => Declaration.Report(writer);
        }

        public class Function : Entity<FunctionDeclaration>
        {
            public Function(FunctionDeclaration declaration) : base(declaration) { }
        }

        public class Protocol : Entity<ProtocolDeclaration>
        {
            public Protocol(ProtocolDeclaration declaration) : base(declaration) { }
        }

        public class Struct : Entity<StructDeclaration>
        {
            public Struct(StructDeclaration declaration) : base(declaration) { }
        }

        public class Class : Entity<ClassDeclaration>
        {
            public Class(ClassDeclaration declaration) : base(declaration) { }
        }

        public class Enum : Entity<EnumDeclaration>
        {
            public Enum(EnumDeclaration declaration) : base(declaration) { }
        }

        public class Var : Entity<BlockVarDeclaration>
        {
            public Var(BlockVarDeclaration declaration) : base(declaration) { }
        }

        public class Alias : Entity<TypealiasDeclaration>
        {
            public Alias(TypealiasDeclaration declaration) : base(declaration) { }
        }

        public class Associated : Entity<AssociatedTypeDeclaration>
        {
            public Associated(AssociatedTypeDeclaration declaration) : base(declaration) { }
        }

        public class GParameter : Entity<GenericParameter>
        {
            public GParameter(GenericParameter declaration) : base(declaration) { }
        }

        public class FParameter : Entity<FuncParameter>
        {
            public FParameter(FuncParameter declaration) : base(declaration) { }
        }

        public class NamePattern : Entity<IPattern.IdentifierPattern>
        {
            public NamePattern(IPattern.IdentifierPattern declaration) : base(declaration) { }
        }
    }
}
