﻿using Six.Support;
using SixComp.Sema;

namespace SixComp.Entities
{
    public interface IEntity : INamedDeclaration
    {
        GenericParameters? Generics { get; }
        INamedDeclaration Declaration { get; }

        public abstract class Entity<TDecl> : IEntity
            where TDecl : INamedDeclaration
        {
            protected Entity(TDecl declaration)
                : this(declaration, declaration.Name)
            {
                Declaration = declaration;
            }

            protected Entity(TDecl declaration, BaseName name)
            {
                Declaration = declaration;
                Name = name;
            }

            public TDecl Declaration { get; }
            INamedDeclaration IEntity.Declaration => Declaration;
            public Scope Scope => Declaration.Scope;
            public BaseName Name { get; }
            public IScoped Outer => Declaration.Outer;
            public Global Global => Declaration.Global;

            public GenericParameters? Generics => (Declaration as IWithGenerics)?.Generics;

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

        public class SelfInstanceReference<T> : Entity<T>
            where T : INamedDeclaration, IScoped
        {
            public SelfInstanceReference(T declaration) : base(declaration, BaseName.Self(declaration)) { }
        }
    }
}
