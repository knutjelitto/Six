using SixComp.Sema;
using SixComp.Support;

namespace SixComp.Entities
{
    public interface IEntity : INamedDeclaration
    {
        GenericParameters? Generics { get; }
        Declarations? Declarations { get; }
        bool Extend(ExtensionDeclaration extension);

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
            public IScoped Outer => Declaration.Outer;
            public Global Global => Declaration.Global;

            public GenericParameters? Generics => (Declaration as IWithGenerics)?.Generics;
            public Declarations? Declarations => (Declaration as IExtendable)?.Declarations;
            public bool Extend(ExtensionDeclaration extension) => (Declaration as IExtendable)?.Extend(extension) ?? false;

            public void Report(IWriter writer) => Declaration.Report(writer);
            public void Resolve(IWriter writer) { }
        }

        public class Function : Entity<FunctionDeclaration>, IParentScope
        {
            public Function(FunctionDeclaration declaration) : base(declaration) { }
        }

        public class Protocol : Entity<ProtocolDeclaration>, IParentScope
        {
            public Protocol(ProtocolDeclaration declaration) : base(declaration) { }
        }

        public class Struct : Entity<StructDeclaration>, IParentScope
        {
            public Struct(StructDeclaration declaration) : base(declaration) { }
        }

        public class Class : Entity<ClassDeclaration>, IParentScope
        {
            public Class(ClassDeclaration declaration) : base(declaration) { }
        }

        public class Enum : Entity<EnumDeclaration>, IParentScope
        {
            public Enum(EnumDeclaration declaration) : base(declaration) { }
        }

        public class Var : Entity<BlockVarDeclaration>, IParentScope
        {
            public Var(BlockVarDeclaration declaration) : base(declaration) { }
        }

        public class Alias : Entity<TypealiasDeclaration>, IParentScope
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

        public class CParameter : Entity<ClosureExpression.ClosureParameter>
        {
            public CParameter(ClosureExpression.ClosureParameter declaration) : base(declaration) { }
        }

        public class NamePattern : Entity<IPattern.IdentifierPattern>
        {
            public NamePattern(IPattern.IdentifierPattern declaration) : base(declaration) { }
        }

        public class EnumCase : Entity<EnumCaseDeclaration>
        {
            public EnumCase(EnumCaseDeclaration declaration) : base(declaration) { }
        }

        public class Label : Entity<Labeled>
        {
            public Label(Labeled declaration) : base(declaration) { }
        }
    }
}
