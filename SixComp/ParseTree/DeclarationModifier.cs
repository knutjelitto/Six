using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class DeclarationModifier
    {
        public enum ModifierKind
        {
            None,

            Class,
            Convenience,
            Dynamic,
            Final,
            Lazy,
            Optional,
            Override,
            Prefix,
            Postfix,
            Required,
            Static,
            Unowned,
            UnownedSafe,
            UnownedUnsafe,
            Weak,
            __Consuming,
            __Owned,

            // access level
            Private,
            PrivateSet,
            Fileprivate,
            FileprivateSet,
            Internal,
            InternalSet,
            Public,
            PublicSet,
            Open,
            OpenSet,

            // mutation
            Mutating,
            Nonmutating,

        }

        public ModifierKind Modifier { get; }

        private DeclarationModifier(ModifierKind modifier)
        {
            Modifier = modifier;
        }

        public static DeclarationModifier? TryParse(Parser parser, bool exclude)
        {
            var kind = ModifierKind.None;

            switch (parser.Current)
            {
                case ToKind.KwClass when !exclude:
                    parser.ConsumeAny();
                    kind = ModifierKind.Class;
                    break;
                case ToKind.KwConvenience:
                    parser.ConsumeAny();
                    kind = ModifierKind.Convenience;
                    break;
                case ToKind.KwDynamic:
                    parser.ConsumeAny();
                    kind = ModifierKind.Dynamic;
                    break;
                case ToKind.KwFinal:
                    parser.ConsumeAny();
                    kind = ModifierKind.Final;
                    break;
                case ToKind.KwLazy:
                    parser.ConsumeAny();
                    kind = ModifierKind.Lazy;
                    break;
                case ToKind.KwOptional:
                    parser.ConsumeAny();
                    kind = ModifierKind.Optional;
                    break;
                case ToKind.KwOverride:
                    parser.ConsumeAny();
                    kind = ModifierKind.Override;
                    break;
                case ToKind.KwPrefix:
                    parser.ConsumeAny();
                    kind = ModifierKind.Prefix;
                    break;
                case ToKind.KwPostfix:
                    parser.ConsumeAny();
                    kind = ModifierKind.Postfix;
                    break;
                case ToKind.KwRequired:
                    parser.ConsumeAny();
                    kind = ModifierKind.Required;
                    break;
                case ToKind.KwStatic:
                    parser.ConsumeAny();
                    kind = ModifierKind.Static;
                    break;
                case ToKind.KwUnowned:
                    kind = ModifierKind.Unowned;
                    if (Match(ToKind.KwSafe))
                    {
                        kind = ModifierKind.UnownedSafe;
                    }
                    else if (Match(ToKind.KwUnsafe))
                    {
                        kind = ModifierKind.UnownedUnsafe;
                    }
                    break;
                case ToKind.KwWeak:
                    parser.ConsumeAny();
                    kind = ModifierKind.Weak;
                    break;
                case ToKind.Kw__Consuming:
                    parser.ConsumeAny();
                    kind = ModifierKind.__Consuming;
                    break;
                case ToKind.Kw__Owned:
                    parser.ConsumeAny();
                    kind = ModifierKind.__Owned;
                    break;

                case ToKind.KwPrivate:
                    parser.ConsumeAny();
                    kind = ModifierKind.Private;
                    if (Match(ToKind.KwSet))
                    {
                        kind = ModifierKind.PrivateSet;
                    }
                    break;
                case ToKind.KwFileprivate:
                    parser.ConsumeAny();
                    kind = ModifierKind.Fileprivate;
                    if (Match(ToKind.KwSet))
                    {
                        kind = ModifierKind.FileprivateSet;
                    }
                    break;
                case ToKind.KwInternal:
                    parser.ConsumeAny();
                    kind = ModifierKind.Internal;
                    if (Match(ToKind.KwSet))
                    {
                        kind = ModifierKind.InternalSet;
                    }
                    break;
                case ToKind.KwPublic:
                    parser.ConsumeAny();
                    kind = ModifierKind.Public;
                    if (Match(ToKind.KwSet))
                    {
                        kind = ModifierKind.PublicSet;
                    }
                    break;
                case ToKind.KwOpen:
                    parser.ConsumeAny();
                    kind = ModifierKind.Open;
                    if (Match(ToKind.KwSet))
                    {
                        kind = ModifierKind.OpenSet;
                    }
                    break;
                case ToKind.KwMutating:
                    parser.ConsumeAny();
                    kind = ModifierKind.Mutating;
                    break;
                case ToKind.KwNonmutating:
                    parser.ConsumeAny();
                    kind = ModifierKind.Nonmutating;
                    break;

                default:
                    return null;
            }

            return new DeclarationModifier(kind);

            bool Match(ToKind kind)
            {
                if (parser.Current == ToKind.LParent && parser.Next == kind && parser.NextNext == ToKind.RParent)
                {
                    parser.ConsumeAny();
                    parser.ConsumeAny();
                    parser.ConsumeAny();

                    return true;
                }

                return false;
            }
        }

        public override string ToString()
        {
            return $"{Modifier}";
        }
    }
}
