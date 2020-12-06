using SixComp.Entities;
using SixComp.Support;
using System;
using System.Diagnostics;

namespace SixComp.Sema
{
    public abstract class Base : IScoped, IReportable
    {
        public Base(IScoped outer, IScope? scope = null)
        {
            Outer = outer;
            Scope = scope ?? outer.Scope;
        }

        public IScoped Outer { get; }
        public IScope Scope { get; }
        public abstract void Report(IWriter writer);

        protected void Declare(FunctionDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Function(declaration));
        }

        protected void Declare(ProtocolDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Protocol(declaration));
        }

        protected void Declare(ClassDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Class(declaration));
        }

        protected void Declare(EnumDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Enum(declaration));
        }

        protected void Declare(AssociatedTypeDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Associated(declaration));
        }

        protected void Declare(TypealiasDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Alias(declaration));
        }

        protected void Declare(StructDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Struct(declaration));
        }

        protected void Declare(BlockVarDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Var(declaration));
        }

        protected void Declare(GenericParameter declaration)
        {
            Outer.Scope.Declare(new IEntity.GParameter(declaration));
        }

        protected void Declare(FuncParameter declaration)
        {
            Outer.Scope.Declare(new IEntity.FParameter(declaration));
        }

        protected void Declare(IPattern.IdentifierPattern declaration)
        {
            Outer.Scope.Declare(new IEntity.NamePattern(declaration));
        }

        protected void Declare(INamedDeclaration declaration)
        {
            throw new NotImplementedException();
        }
    }
}
