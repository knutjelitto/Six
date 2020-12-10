using SixComp.Entities;
using SixComp.Support;
using System;

namespace SixComp.Sema
{
    public abstract class Base : IScoped, IReportable
    {
        public Base(IScoped outer, Scope? scope = null)
        {
            Outer = outer;
            Scope = scope ?? outer.Scope;
            Entity = null;
        }

        public IScoped Outer { get; }
        public Scope Scope { get; }
        public Global Global => Scope.Global;

        public IEntity? Entity { get; protected set; }

        public abstract void Report(IWriter writer);

        protected void UnResolve(IWriter writer, BaseName name)
        {
            if (Global.IgnoredNamesForNow.Contains(name.Text))
            {
                return;
            }
            Global.UnresolvedNamesTodo.Add(name.Text);

            if (name.Tree is ParseTree.BaseName treeName)
            {
                treeName.Token.Error(writer, $"can't resolve `{name.Text}`");
            }
        }

        protected void Declare(FunctionDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Function(declaration));
        }

        protected void Declare(ProtocolDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Protocol(declaration));
            declaration.Scope.Declare(new IEntity.SelfInstanceReference<ProtocolDeclaration>(declaration));
        }

        protected void Declare(ClassDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Class(declaration));
            declaration.Scope.Declare(new IEntity.SelfInstanceReference<ClassDeclaration>(declaration));
        }

        protected void Declare(EnumDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.Enum(declaration));
            declaration.Scope.Declare(new IEntity.SelfInstanceReference<EnumDeclaration>(declaration));
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
            declaration.Scope.Declare(new IEntity.SelfInstanceReference<StructDeclaration>(declaration));

            Global.Structs.Add(declaration);
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

        protected void Declare(ClosureExpression.ClosureParameter declaration)
        {
            Outer.Scope.Declare(new IEntity.CParameter(declaration));
        }

        protected void Declare(IPattern.IdentifierPattern declaration)
        {
            Outer.Scope.Declare(new IEntity.NamePattern(declaration));
        }

        protected void Declare(EnumCaseDeclaration declaration)
        {
            Outer.Scope.Declare(new IEntity.EnumCase(declaration));
        }

        protected void Declare(Labeled declaration)
        {
            Outer.Scope.Declare(new IEntity.Label(declaration));
        }

        protected void Declare(INamedDeclaration declaration)
        {
            throw new NotImplementedException();
        }
    }
}
