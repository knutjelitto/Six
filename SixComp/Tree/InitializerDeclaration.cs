﻿using Six.Support;
using SixComp.Common;

namespace SixComp
{
    public partial class ParseTree
    {
        public class InitializerDeclaration : IDeclaration
        {
            public InitializerDeclaration(Prefix prefix, InitKind kind, GenericParameterClause genericParameters, ParameterClause parameters, RequirementClause requirements, CodeBlock? block)
            {
                Prefix = prefix;
                Kind = kind;
                GenericParameters = genericParameters;
                Parameters = parameters;
                Requirements = requirements;
                Block = block;
            }

            public Prefix Prefix { get; }
            public InitKind Kind { get; }
            public GenericParameterClause GenericParameters { get; }
            public ParameterClause Parameters { get; }
            public RequirementClause Requirements { get; }
            public CodeBlock? Block { get; }

            public static InitializerDeclaration Parse(Parser parser, Prefix prefix)
            {
                //TODO: is incomplete
                InitKind init = Head(parser);
                var genericParameters = parser.TryList(GenericParameterClause.Firsts, GenericParameterClause.Parse);
                var parameters = ParameterClause.Parse(parser);
                var throws = parser.Match(ToKind.KwThrows);
                var rethrows = parser.Match(ToKind.KwRethrows);
                var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);
                var block = parser.Try(CodeBlock.Firsts, CodeBlock.Parse);

                return new InitializerDeclaration(prefix, InitKind.Init, genericParameters, parameters, requirements, block);
            }

            private static InitKind Head(Parser parser)
            {
                // already parsed //parser.Consume(ToKind.KwInit);

                InitKind kind = InitKind.Init;

                if (parser.Match(ToKind.Quest))
                {
                    kind = InitKind.InitChain;
                }
                else if (parser.Match(ToKind.Bang))
                {
                    kind = InitKind.InitForce;
                }
                else
                {
                    var token = parser.CurrentToken;

                    if (token.IsOperator)
                    {
                        if (token.First == '?')
                        {
                            parser.ConsumeCarefully(ToKind.Quest);
                            kind = InitKind.InitChain;
                        }
                        else if (token.First == '!')
                        {
                            parser.ConsumeCarefully(ToKind.Bang);
                            kind = InitKind.InitForce;
                        }
                    }
                }

                return kind;
            }

            public void Write(IWriter writer)
            {
                writer.WriteLine($"init{GenericParameters}{Parameters}{Requirements}");
                Block?.Write(writer);
            }

            public override string ToString()
            {
                return $"init{GenericParameters}{Parameters}{Requirements}{Block}";
            }
        }
    }
}