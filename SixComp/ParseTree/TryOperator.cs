﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class TryOperator : SyntaxNode
    {
        public enum TryKind
        {
            None,

            Try,
            TryForce,
            TryChain,
        }

        public TryOperator(TryKind kind)
        {
            Kind = kind;
        }

        public TryKind Kind { get; }

        public static TryOperator Parse(Parser parser)
        {
            var tryKind = TryKind.None;

            if (parser.Match(ToKind.KwTry))
            {
                if (parser.Match(ToKind.Bang))
                {
                    tryKind = TryKind.TryForce;
                }
                else if (parser.Match(ToKind.Quest))
                {
                    tryKind = TryKind.TryChain;
                }
                else
                {
                    tryKind = TryKind.Try;
                }
            }

            return new TryOperator(tryKind);
        }
    }
}
