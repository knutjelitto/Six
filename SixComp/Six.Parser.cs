using Pegasus.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Peg
{
    partial class Parser
    {
        public Parser()
        {
        }


        public interface IType
        {
        }

        public interface ISpace
        {
        }

        public interface IName
        {
        }

        public interface IToken
        {
        }

        public static class Types
        {
            public static IType Identifier(object start, object rest, Cursor state)
            {
                throw new NotImplementedException();
            }

            public static IType IdPart(IName name, Generics.GenericArguments arguments)
            {
                throw new NotImplementedException();
            }

            public static IType IdPart(IName dot, IName name, Generics.GenericArguments arguments)
            {
                throw new NotImplementedException();
            }

            public static IType Compose(IList<IType> types)
            {
                throw new NotImplementedException();
            }

            public static IType PrefixedType(IName prefix, IType type)
            {
                throw new NotImplementedException();
            }
        }

        public static class AnySpace
        {
            public static ISpace Build(IList<string> spaces, Cursor state)
            {
                return new Space(spaces, state);
            }

            private class Space : ISpace
            {
                public Space(IList<string> spaces, Cursor state)
                {
                    Spaces = spaces;
                    State = state;
                }

                public IList<string> Spaces { get; }
                public Cursor State { get; }
            }
        }

        public static class Names
        {
            public static IName Build(ISpace space, string text, Cursor state)
            {
                return new Name(space, text, state);
            }

            private class Name : IName
            {
                public Name(ISpace space, string text, Cursor state)
                {
                    Space = space;
                    Text = text;
                    State = state;
                }

                public ISpace Space { get; }
                public string Text { get; }
                public Cursor State { get; }
            }
        }

        public static class Generics
        {
            public static GenericParameter Parameter(IName typeName, IName? colon, IType? restriction)
            {
                return new GenericParameter(typeName, colon, restriction);

            }

            public static GenericArgument Argument(IType type, Cursor state)
            {
                return new GenericArgument(type);

            }

            public static GenericArgument Argument(IName comma, IType type, Cursor state)
            {
                return new GenericArgument(Types.PrefixedType(comma, type));

            }

            public static GenericArguments Arguments(IName lAngle, GenericArgument first, IList<GenericArgument> rest, IName rAngle, Cursor state)
            {
                return new GenericArguments(lAngle, Enumerable.Repeat(first, 1).Concat(rest).ToArray(), rAngle);

            }

            public class GenericParameter
            {
                public GenericParameter(IName typeName, IName? colon, IType? restriction)
                {
                    TypeName = typeName;
                    Colon = colon;
                    Restriction = restriction;
                }

                public IName TypeName { get; }
                public IName? Colon { get; }
                public IType? Restriction { get; }
            }

            public class GenericArgument : IType
            {
                public GenericArgument(IType type)
                {
                    Type = type;
                }

                public IType Type { get; }
            }

            public class GenericArguments
            {
                public GenericArguments(IName lAngle, IReadOnlyList<GenericArgument> arguments, IName rAngle)
                {
                    LAngle = lAngle;
                    Arguments = arguments;
                    RAngle = rAngle;
                }

                public IName LAngle { get; }
                public IReadOnlyList<GenericArgument> Arguments { get; }
                public IName RAngle { get; }
            }
        }
    }
}
