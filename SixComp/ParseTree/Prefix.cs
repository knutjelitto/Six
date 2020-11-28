using SixComp.Support;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixComp.ParseTree
{
    public class Prefix
    {
        private static readonly TokenSet SafeUnsafe = new TokenSet(ToKind.KwSafe, ToKind.KwUnsafe);
        private static readonly TokenSet Access = new TokenSet(ToKind.KwPrivate, ToKind.KwFileprivate, ToKind.KwInternal, ToKind.KwPublic, ToKind.KwOpen);
        public static readonly TokenSet Fixes = new TokenSet(ToKind.KwPrefix, ToKind.KwPostfix, ToKind.KwInfix);
        public static readonly TokenSet Prefixes = new TokenSet(
            ToKind.KwClass, ToKind.KwStruct, ToKind.KwExtension, ToKind.KwEnum, ToKind.KwProtocol, ToKind.KwImport, ToKind.KwLet, ToKind.KwVar,
            ToKind.KwFunc, ToKind.KwInit, ToKind.KwDeinit, ToKind.KwSubscript, ToKind.KwOperator, ToKind.KwTypealias, ToKind.KwAssociatedType,

            ToKind.KwClass, ToKind.KwConvenience, ToKind.KwDynamic, ToKind.KwFinal, ToKind.KwInfix, ToKind.KwLazy, ToKind.KwOptional, ToKind.KwOverride,
            ToKind.KwPostfix, ToKind.KwPrefix, ToKind.KwRequired, ToKind.KwStatic, ToKind.KwUnowned, ToKind.KwWeak, ToKind.KwIndirect,
            ToKind.CdIf,
            ToKind.Kw__Consuming, ToKind.Kw__Shared, ToKind.Kw__Owned,

            ToKind.KwPrivate, ToKind.KwFileprivate, ToKind.KwInternal, ToKind.KwPublic, ToKind.KwOpen,

            ToKind.KwMutating, ToKind.KwNonmutating
            );

        public static readonly TokenSet KwAsAttribute = new TokenSet(
            ToKind.Kw__Consuming, ToKind.Kw__Shared, ToKind.Kw__Owned
            );

        private Prefix(AttributeList attributes, ModifierList? modifiers, List<Token>? preparsed)
        {
            Attributes = attributes;
            Modifiers = modifiers;
            Preparsed = preparsed;
        }

        public AttributeList Attributes { get; }
        public ModifierList? Modifiers { get; }
        public List<Token>? Preparsed { get; }

        public ToKind? Last => Preparsed?.LastOrDefault()?.Kind;

        public static Prefix Parse(Parser parser, bool onlyAttributes = false)
        {
            var attributes = AttributeList.TryParse(parser);
            var modifiers = onlyAttributes ? null : ModifierList.TryParse(parser);

            return new Prefix(attributes, modifiers, null);
        }

        public static Prefix PreParse(Parser parser, bool onlyAttributes = false)
        {
            var attributes = AttributeList.TryParse(parser);

            if (onlyAttributes)
            {
                while (KwAsAttribute.Contains(parser.Current))
                {
                    attributes.Backdoor(parser.ConsumeAny());
                }
            }

            List<Token>? preparsed = null;

            if (!onlyAttributes)
            {
                preparsed = new List<Token>();
                while (Prefixes.Contains(parser.Current))
                {
                    var token = Add();
                    if (token.Kind == ToKind.KwUnowned)
                    {
                        if (parser.Current == ToKind.LParent)
                        {
                            Add();
                            token = parser.Consume(SafeUnsafe);
                            preparsed.Add(token);
                            token = parser.Consume(ToKind.RParent);
                            preparsed.Add(token);
                        }
                    }
                    else if (Access.Contains(token.Kind))
                    {
                        if (token.Kind == ToKind.KwFileprivate)
                        {
                            Debug.Assert(true);
                        }
                        if (parser.Current == ToKind.LParent && parser.Next == ToKind.KwSet && parser.NextNext == ToKind.RParent)
                        {
                            Add();
                            token = parser.Consume(ToKind.KwSet);
                            preparsed.Add(token);
                            token = parser.Consume(ToKind.RParent);
                            preparsed.Add(token);
                        }
                    }
                }

                if (preparsed.Count >= 2)
                {
                    var first = preparsed[^2];
                    var second = preparsed[^1];
                    if (first.Kind == ToKind.KwFunc && second.Kind == ToKind.KwPrefix)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwFunc && second.Kind == ToKind.KwOpen)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwImport && second.Kind == ToKind.KwStruct)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwImport && second.Kind == ToKind.KwClass)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwImport && second.Kind == ToKind.KwFunc)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwCase && second.Kind == ToKind.KwGet)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwCase && second.Kind == ToKind.KwSome)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwLet && second.Kind == ToKind.KwSet)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwLet && second.Kind == ToKind.KwPrefix)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwLet && second.Kind == ToKind.KwDynamic)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwVar && second.Kind == ToKind.KwLazy)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwVar && second.Kind == ToKind.KwOpen)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (first.Kind == ToKind.KwVar && second.Kind == ToKind.KwPrefix)
                    {
                        preparsed.RemoveAt(preparsed.Count - 1);
                        parser.Offset = second.Index;
                    }
                    else if (second.Kind == ToKind.KwOperator)
                    {
                        if (Fixes.Contains(first.Kind))
                        {
                            // backtrack both
                            parser.Offset = first.Index;
                        }
                    }
                }
            }

            return new Prefix(attributes, null, preparsed);

            Token Add()
            {
                var token = parser.ConsumeAny();
                preparsed.Add(token);
                return token;
            }
        }

        public static readonly Prefix Empty = new Prefix(new AttributeList(), null, null);

        public bool Missing => Attributes.Missing && (Modifiers?.Missing ?? false);

        public override string ToString()
        {
            string what = string.Empty;
            if (Modifiers != null)
            {
                what = Modifiers.ToString();
            }
            else if (Preparsed != null)
            {
                what = string.Join(" ", Preparsed);
            }
            if (!string.IsNullOrWhiteSpace(what))
            {
                what += " ";
            }
            return $"{Attributes}{what}";
        }
    }
}
