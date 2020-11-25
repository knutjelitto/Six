﻿using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.ParseTree
{
    public class Prefix
    {
        private static readonly TokenSet SafeUnsafe = new TokenSet(ToKind.KwSafe, ToKind.KwUnsafe);
        private static readonly TokenSet Access = new TokenSet(ToKind.KwPrivate, ToKind.KwFileprivate, ToKind.KwInternal, ToKind.KwPublic, ToKind.KwOpen);
        public static readonly TokenSet Fixes = new TokenSet(ToKind.KwPrefix, ToKind.KwPostfix, ToKind.KwInfix);

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
            List<Token>? preparsed = null;

            if (!onlyAttributes)
            {
                preparsed = new List<Token>();
                while (parser.IsKeyword)
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
                        if (parser.Current == ToKind.LParent)
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
                    if (first.Kind == ToKind.KwFunc && second.Kind == ToKind.KwOpen)
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
                    else if (first.Kind == ToKind.KwVar && second.Kind == ToKind.KwLazy)
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
            return $"{Attributes}{what}";
        }
    }
}
