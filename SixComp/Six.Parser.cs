using Pegasus.Common;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Peg
{
    partial class Parser
    {
        public Parser()
        {
        }

        public interface IAny
        {
            int Start { get; }
            int End { get; }
        }

        public interface IType { }
        public interface ISpace : IAny { }
        public interface IName : IAny { }
        public interface IToken : IName, IAny { }

        public interface IExpr : IAny { }

        public abstract class Any : IAny
        {
            protected Any(IAny first, Cursor end)
            {
                First = first;
                Start = first.Start;
                End = end.Location;
            }

            protected Any(int start, Cursor end)
            {
                Start = start;
                End = end.Location;
            }

            protected Any(IReadOnlyList<IAny> starts,  Cursor end)
            {
                End = end.Location;
                if (starts.Count == 0)
                {
                    Start = End;
                }
                else
                {
                    First = starts[0];
                    Start = First.Start;
                }
            }

            public int Start { get; }
            public int End { get; }
            public IAny? First { get; }
        }

        public static class Funcs
        {
            public class Result : Any, IType
            {
                public Result(IToken arrow, Prefixes.Attributes attributes, IType type, Cursor end)
                    : base(arrow, end)
                {
                    Attributes = attributes;
                    Type = type;
                }

                public Prefixes.Attributes Attributes { get; }
                public IType Type { get; }
            }
        }

        public static class Types
        {
            public class Identifier : IType
            {
                public Identifier(IType first, IList<IType> rest)
                {
                    Parts = Enumerable.Repeat(first, 1).Concat(rest).Cast<IdentifierPart>().ToArray();
                }

                public IReadOnlyList<IdentifierPart> Parts { get; }
            }

            public class IdentifierPart : IType
            {
                public IdentifierPart(IName name, IList<Generics.Arguments> arguments)
                {
                    Name = name;
                    Arguments = arguments.SingleOrDefault();
                }

                public IName Name { get; }
                public Generics.Arguments? Arguments { get; }
            }

            public class Composed : IType
            {
                public Composed(IList<IType> types)
                {
                    Types = types;
                }

                public IList<IType> Types { get; }
            }

            public class PrefixedType : IType
            {
                public PrefixedType(IName prefix, IType type)
                {
                    Prefix = prefix;
                    Type = type;
                }

                public IName Prefix { get; }
                public IType Type { get; }
            }
        }

        public static class Spaces
        {
            public class Space : Any, ISpace
            {
                public Space(Cursor start, IList<string> spaces, Cursor next) 
                    : base(start.Location, next)
                {
                    Spaces = spaces;
                }

                public IList<string> Spaces { get; }
            }
        }

        public class Prefixes
        {
            public class Prefix : Any
            {
                public Prefix(IList<IToken> attributes, Cursor end)
                    : base(attributes.ToList(), end)
                {
                }
            }

            public class Attributes : Any
            {
                public Attributes(IList<Attribute> attributes, Cursor end)
                    : base(attributes.ToList(), end)
                {
                }
            }

            public class Attribute : Any
            {
                public Attribute(IToken first, IName name, Cursor end)
                    : base(first, end)
                {
                    Name = name;
                }

                public IName Name { get; }
            }
        }

        public static class Names
        {           
            public class Name : Any, IName
            {
                public Name(ISpace space, string text, Cursor end)
                    : base(space, end)
                {
                    Text = text;
                }
                public string Text { get; }
            }

            public class Token : Name, IToken
            {
                public Token(ISpace first, string text, Cursor end)
                    : base(first, text, end)
                {
                }
            }

            private class PrefixName : Any, IName
            {
                public PrefixName(IName prefix, IName name, Cursor end)
                    : base(prefix, end)
                {
                    Name = name;
                }
                public IName Name { get; }
            }
        }

        public static class Generics
        {
            public class Parameter
            {
                public Parameter(IName typeName, IName? colon, IType? restriction)
                {
                    TypeName = typeName;
                    Colon = colon;
                    Restriction = restriction;
                }

                public IName TypeName { get; }
                public IName? Colon { get; }
                public IType? Restriction { get; }
            }

            public class Argument : IType
            {
                public Argument(IType type, Cursor state)
                {
                    Type = type;
                    State = state;
                }

                public IType Type { get; }
                public Cursor State { get; }
            }

            public class Arguments
            {
                public Arguments(IName lAngle, Argument first, IList<Argument> rest, IName rAngle, Cursor state)
                    : this(lAngle, Enumerable.Repeat(first, 1).Concat(rest).ToArray(), rAngle, state)
                {

                }

                public Arguments(IName lAngle, IReadOnlyList<Argument> arguments, IName rAngle, Cursor state)
                {
                    LAngle = lAngle;
                    Items = arguments;
                    RAngle = rAngle;
                    State = state;
                }

                public IName LAngle { get; }
                public IReadOnlyList<Argument> Items { get; }
                public IName RAngle { get; }
                public Cursor State { get; }
            }
        }
    }
}

