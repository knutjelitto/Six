#if true
using System.Collections.Generic;
using Six.Peg.Runtime;

namespace SixPeg.Pegger.Swift
{
    public abstract class SwiftPegger : Six.Peg.Runtime.Pegger
    {
        public SwiftPegger(Context context)
            : base(context, 547)
        {
        }

        protected const int Cache_Unit = 0;

        public virtual Match Unit(int start)
        {
            if (!Caches[Cache_Unit].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Statement(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = EOF(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Unit", start, match);
                }
                Caches[Cache_Unit].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EOF = 1;

        public virtual Match EOF(int start)
        {
            if (!Caches[Cache_EOF].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = _(next);
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Not_(next, CharacterAny_(next))) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    {
                        // >>> ERROR
                        new Error(Context).Report("EOF", start);
                        throw new BailOutException();
                        // <<< ERROR
                    }
                }
                if (match != null)
                {
                    match = Match.Success("EOF", start, match);
                }
                Caches[Cache_EOF].Cache(start, match);
            }
            return match;
        }

        protected const int Cache__ = 2;

        public virtual Match _(int start)
        {
            if (!Caches[Cache__].Already(start, out var match))
            {
                var zomMatches = new List<Match>();
                var zomNext = start;
                while (true)
                {
                    if ((match = Whitespace(zomNext)) == null)
                    {
                        break;
                    }
                    zomMatches.Add(match);
                    zomNext = match.Next;
                }
                match = Match.Success("*", start, zomMatches);
                match = Match.Success("SPACE", start, match.Next);
                Caches[Cache__].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FullPrefix = 3;

        public virtual Match FullPrefix(int start)
        {
            if (!Caches[Cache_FullPrefix].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, DeclarationModifiers(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("FullPrefix", start, match);
                }
                Caches[Cache_FullPrefix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AccessPrefix = 4;

        public virtual Match AccessPrefix(int start)
        {
            if (!Caches[Cache_AccessPrefix].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, AccessLevelModifier(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("AccessPrefix", start, match);
                }
                Caches[Cache_AccessPrefix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_MutationPrefix = 5;

        public virtual Match MutationPrefix(int start)
        {
            if (!Caches[Cache_MutationPrefix].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, MutationModifier(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("MutationPrefix", start, match);
                }
                Caches[Cache_MutationPrefix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Attributes = 6;

        public virtual Match Attributes(int start)
        {
            if (!Caches[Cache_Attributes].Already(start, out var match))
            {
                var oomMatches = new List<Match>();
                var oomNext = start;
                while (true)
                {
                    if ((match = Attribute(oomNext)) == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", start, oomMatches);
                }
                if (match != null)
                {
                    match = Match.Success("Attributes", start, match);
                }
                Caches[Cache_Attributes].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Attribute = 7;

        public virtual Match Attribute(int start)
        {
            if (!Caches[Cache_Attribute].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_at_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    {
                        // >>> ERROR
                        new Error(Context).Report("attribute", next);
                        throw new BailOutException();
                        // <<< ERROR
                    }
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Attribute", start, match);
                }
                Caches[Cache_Attribute].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DeclarationModifiers = 8;

        public virtual Match DeclarationModifiers(int start)
        {
            if (!Caches[Cache_DeclarationModifiers].Already(start, out var match))
            {
                var oomMatches = new List<Match>();
                var oomNext = start;
                while (true)
                {
                    if ((match = Modifier(oomNext)) == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", start, oomMatches);
                }
                if (match != null)
                {
                    match = Match.Success("DeclarationModifiers", start, match);
                }
                Caches[Cache_DeclarationModifiers].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Modifier = 9;

        public virtual Match Modifier(int start)
        {
            if (!Caches[Cache_Modifier].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ModifierToken(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Modifier", start, match);
                }
                Caches[Cache_Modifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ModifierToken = 10;

        public virtual Match ModifierToken(int start)
        {
            if (!Caches[Cache_ModifierToken].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = AccessLevelModifier(start)) != null)
                    {
                        break;
                    }
                    if ((match = DeclarationModifier(start)) != null)
                    {
                        break;
                    }
                    match = MutationModifier(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ModifierToken", start, match);
                }
                Caches[Cache_ModifierToken].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DeclarationModifier = 11;

        public virtual Match DeclarationModifier(int start)
        {
            if (!Caches[Cache_DeclarationModifier].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_class(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_convenience(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_dynamic(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_final(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_infix(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_lazy(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_optional(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_override(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_postfix(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_prefix(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_required(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_static(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_unowned(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var next2 = next;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_1_/*'('*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            while (true) // ---Choice---
                            {
                                if ((match = Lit_safe(next2)) != null)
                                {
                                    break;
                                }
                                match = Lit_unsafe(next2);
                                break;
                            }
                            if (match == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Lit_2_/*')'*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next, matches2);
                        }
                        match = Match.Optional(next, match);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = Lit_weak(start)) != null)
                    {
                        break;
                    }
                    match = Lit_3_/*'__consuming'*/(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("DeclarationModifier", start, match);
                }
                Caches[Cache_DeclarationModifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AccessLevelModifier = 12;

        public virtual Match AccessLevelModifier(int start)
        {
            if (!Caches[Cache_AccessLevelModifier].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = AccessModifierBase(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_set(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("AccessLevelModifier", start, match);
                }
                Caches[Cache_AccessLevelModifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AccessModifierBase = 13;

        public virtual Match AccessModifierBase(int start)
        {
            if (!Caches[Cache_AccessModifierBase].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_private(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_fileprivate(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_internal(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_public(start)) != null)
                    {
                        break;
                    }
                    match = Lit_open(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("AccessModifierBase", start, match);
                }
                Caches[Cache_AccessModifierBase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_MutationModifier = 14;

        public virtual Match MutationModifier(int start)
        {
            if (!Caches[Cache_MutationModifier].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_mutating(start)) != null)
                    {
                        break;
                    }
                    match = Lit_nonmutating(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("MutationModifier", start, match);
                }
                Caches[Cache_MutationModifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CompilerControlStatement = 15;

        public virtual Match CompilerControlStatement(int start)
        {
            if (!Caches[Cache_CompilerControlStatement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = ConditionalCompilationBlock(start)) != null)
                    {
                        break;
                    }
                    if ((match = LineControlStatement(start)) != null)
                    {
                        break;
                    }
                    match = DiagnosticStatement(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("CompilerControlStatement", start, match);
                }
                Caches[Cache_CompilerControlStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConditionalCompilationBlock = 16;

        public virtual Match ConditionalCompilationBlock(int start)
        {
            if (!Caches[Cache_ConditionalCompilationBlock].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = IfDirectiveClause(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, ElseifDirectiveClauses(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, ElseDirectiveClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = EndifDirective(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ConditionalCompilationBlock", start, match);
                }
                Caches[Cache_ConditionalCompilationBlock].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IfDirectiveClause = 17;

        public virtual Match IfDirectiveClause(int start)
        {
            if (!Caches[Cache_IfDirectiveClause].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = IfDirective(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = CompilationCondition(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        while (true)
                        {
                            if ((match = Statement(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next, zomMatches);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = IfDirective(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("if-directive-clause", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("IfDirectiveClause", start, match);
                }
                Caches[Cache_IfDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseifDirectiveClauses = 18;

        public virtual Match ElseifDirectiveClauses(int start)
        {
            if (!Caches[Cache_ElseifDirectiveClauses].Already(start, out var match))
            {
                var oomMatches = new List<Match>();
                var oomNext = start;
                while (true)
                {
                    if ((match = ElseifDirectiveClause(oomNext)) == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", start, oomMatches);
                }
                if (match != null)
                {
                    match = Match.Success("ElseifDirectiveClauses", start, match);
                }
                Caches[Cache_ElseifDirectiveClauses].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseifDirectiveClause = 19;

        public virtual Match ElseifDirectiveClause(int start)
        {
            if (!Caches[Cache_ElseifDirectiveClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = ElseifDirective(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CompilationCondition(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Statement(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ElseifDirectiveClause", start, match);
                }
                Caches[Cache_ElseifDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseDirectiveClause = 20;

        public virtual Match ElseDirectiveClause(int start)
        {
            if (!Caches[Cache_ElseDirectiveClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = ElseDirective(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Statement(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ElseDirectiveClause", start, match);
                }
                Caches[Cache_ElseDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IfDirective = 21;

        public virtual Match IfDirective(int start)
        {
            if (!Caches[Cache_IfDirective].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_4_/*'#'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    {
                        // >>> ERROR
                        new Error(Context).Report("#if", next);
                        throw new BailOutException();
                        // <<< ERROR
                    }
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("IfDirective", start, match);
                }
                Caches[Cache_IfDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseifDirective = 22;

        public virtual Match ElseifDirective(int start)
        {
            if (!Caches[Cache_ElseifDirective].Already(start, out var match))
            {
                match = Lit_5_/*'#elseif'*/(start);
                if (match != null)
                {
                    match = Match.Success("ElseifDirective", start, match);
                }
                Caches[Cache_ElseifDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseDirective = 23;

        public virtual Match ElseDirective(int start)
        {
            if (!Caches[Cache_ElseDirective].Already(start, out var match))
            {
                match = Lit_6_/*'#else'*/(start);
                if (match != null)
                {
                    match = Match.Success("ElseDirective", start, match);
                }
                Caches[Cache_ElseDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EndifDirective = 24;

        public virtual Match EndifDirective(int start)
        {
            if (!Caches[Cache_EndifDirective].Already(start, out var match))
            {
                match = Lit_7_/*'#endif'*/(start);
                if (match != null)
                {
                    match = Match.Success("EndifDirective", start, match);
                }
                Caches[Cache_EndifDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CompilationConditionPrimary = 25;

        public virtual Match CompilationConditionPrimary(int start)
        {
            if (!Caches[Cache_CompilationConditionPrimary].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = PlatformCondition(start)) != null)
                    {
                        break;
                    }
                    if ((match = Name(start)) != null)
                    {
                        break;
                    }
                    if ((match = BooleanLiteral(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = CompilationCondition(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_8_/*'!'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = CompilationCondition(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("CompilationConditionPrimary", start, match);
                }
                Caches[Cache_CompilationConditionPrimary].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CompilationCondition = 26;

        public virtual Match CompilationCondition(int start)
        {
            if (!Caches[Cache_CompilationCondition].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = CompilationConditionPrimary(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            while (true) // ---Choice---
                            {
                                if ((match = Lit_9_/*'||'*/(next2)) != null)
                                {
                                    break;
                                }
                                match = Lit_10_/*'&&'*/(next2);
                                break;
                            }
                            if (match == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = CompilationConditionPrimary(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("CompilationCondition", start, match);
                }
                Caches[Cache_CompilationCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PlatformCondition = 27;

        public virtual Match PlatformCondition(int start)
        {
            if (!Caches[Cache_PlatformCondition].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_os(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = OperatingSystem(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_arch(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_1_/*'('*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Architecture(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_swift(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_1_/*'('*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        while (true) // ---Choice---
                        {
                            if ((match = Lit_11_/*'>='*/(next3)) != null)
                            {
                                break;
                            }
                            match = Lit_12_/*'<'*/(next3);
                            break;
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = SwiftVersion(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_2_/*')'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_compiler(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = Lit_1_/*'('*/(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        while (true) // ---Choice---
                        {
                            if ((match = Lit_11_/*'>='*/(next4)) != null)
                            {
                                break;
                            }
                            match = Lit_12_/*'<'*/(next4);
                            break;
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = SwiftVersion(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = Lit_2_/*')'*/(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches4);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_canImport(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = Lit_1_/*'('*/(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = ModuleName(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = Lit_2_/*')'*/(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches5);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_targetEnvironment(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = Lit_1_/*'('*/(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = Environment(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = Lit_2_/*')'*/(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches6);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PlatformCondition", start, match);
                }
                Caches[Cache_PlatformCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatingSystem = 28;

        public virtual Match OperatingSystem(int start)
        {
            if (!Caches[Cache_OperatingSystem].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_macOS(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_iOS(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_watchOS(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_tvOS(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_Windows(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_Android(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_Linux(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_OpenBSD(start)) != null)
                    {
                        break;
                    }
                    {
                        // >>> ERROR
                        new Error(Context).Report("unknown operating system", start);
                        throw new BailOutException();
                        // <<< ERROR
                    }
                }
                if (match != null)
                {
                    match = Match.Success("OperatingSystem", start, match);
                }
                Caches[Cache_OperatingSystem].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Architecture = 29;

        public virtual Match Architecture(int start)
        {
            if (!Caches[Cache_Architecture].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_i386(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_13_/*'x86_64'*/(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_arm(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_arm64(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_wasm32(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_powerpc64(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_s390x(start)) != null)
                    {
                        break;
                    }
                    {
                        // >>> ERROR
                        new Error(Context).Report("unknown architecture", start);
                        throw new BailOutException();
                        // <<< ERROR
                    }
                }
                if (match != null)
                {
                    match = Match.Success("Architecture", start, match);
                }
                Caches[Cache_Architecture].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwiftVersion = 30;

        public virtual Match SwiftVersion(int start)
        {
            if (!Caches[Cache_SwiftVersion].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = DecimalDigits(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_dot_(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = DecimalDigits(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SwiftVersion", start, match);
                }
                Caches[Cache_SwiftVersion].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ModuleName = 31;

        public virtual Match ModuleName(int start)
        {
            if (!Caches[Cache_ModuleName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("ModuleName", start, match);
                }
                Caches[Cache_ModuleName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Environment = 32;

        public virtual Match Environment(int start)
        {
            if (!Caches[Cache_Environment].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_simulator(start)) != null)
                    {
                        break;
                    }
                    match = Lit_macCatalyst(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("Environment", start, match);
                }
                Caches[Cache_Environment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LineControlStatement = 33;

        public virtual Match LineControlStatement(int start)
        {
            if (!Caches[Cache_LineControlStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_14_/*'#sourceLocation'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_1_/*'('*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_15_/*'file:'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = FilePath(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_16_/*','*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_17_/*'line:'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = LineNumber(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("LineControlStatement", start, match);
                }
                Caches[Cache_LineControlStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FilePath = 34;

        public virtual Match FilePath(int start)
        {
            if (!Caches[Cache_FilePath].Already(start, out var match))
            {
                match = StaticStringLiteral(start);
                if (match != null)
                {
                    match = Match.Success("FilePath", start, match);
                }
                Caches[Cache_FilePath].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LineNumber = 35;

        public virtual Match LineNumber(int start)
        {
            if (!Caches[Cache_LineNumber].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = NonzeroDecimalLiteral(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("LineNumber", start, match);
                }
                Caches[Cache_LineNumber].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DiagnosticStatement = 36;

        public virtual Match DiagnosticStatement(int start)
        {
            if (!Caches[Cache_DiagnosticStatement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_18_/*'#error'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = DiagnosticMessage(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_19_/*'#warning'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_1_/*'('*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = DiagnosticMessage(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("DiagnosticStatement", start, match);
                }
                Caches[Cache_DiagnosticStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DiagnosticMessage = 37;

        public virtual Match DiagnosticMessage(int start)
        {
            if (!Caches[Cache_DiagnosticMessage].Already(start, out var match))
            {
                match = StaticStringLiteral(start);
                if (match != null)
                {
                    match = Match.Success("DiagnosticMessage", start, match);
                }
                Caches[Cache_DiagnosticMessage].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConstantDeclaration = 38;

        public virtual Match ConstantDeclaration(int start)
        {
            if (!Caches[Cache_ConstantDeclaration].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = FullPrefix(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_let(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = PatternInitializerList(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = FullPrefix(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_let(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("constant-declaration", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ConstantDeclaration", start, match);
                }
                Caches[Cache_ConstantDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternInitializerList = 39;

        public virtual Match PatternInitializerList(int start)
        {
            if (!Caches[Cache_PatternInitializerList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = PatternInitializer(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = PatternInitializer(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PatternInitializerList", start, match);
                }
                Caches[Cache_PatternInitializerList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternInitializer = 40;

        public virtual Match PatternInitializer(int start)
        {
            if (!Caches[Cache_PatternInitializer].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Pattern(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, TypeAnnotation(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Initializer(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PatternInitializer", start, match);
                }
                Caches[Cache_PatternInitializer].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Initializer = 41;

        public virtual Match Initializer(int start)
        {
            if (!Caches[Cache_Initializer].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_20_/*'='*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Expression(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_20_/*'='*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("initializer", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("Initializer", start, match);
                }
                Caches[Cache_Initializer].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EnumDeclaration = 42;

        public virtual Match EnumDeclaration(int start)
        {
            if (!Caches[Cache_EnumDeclaration].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = AccessPrefix(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = UnionStyleEnum(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = AccessPrefix(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = RawValueStyleEnum(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("EnumDeclaration", start, match);
                }
                Caches[Cache_EnumDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnum = 43;

        public virtual Match UnionStyleEnum(int start)
        {
            if (!Caches[Cache_UnionStyleEnum].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Lit_indirect(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_enum(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = EnumName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericParameterClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, TypeInheritanceClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericWhereClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = UnionStyleEnumBody(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnum", start, match);
                }
                Caches[Cache_UnionStyleEnum].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnumBody = 44;

        public virtual Match UnionStyleEnumBody(int start)
        {
            if (!Caches[Cache_UnionStyleEnumBody].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_21_/*'{'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = UnionStyleEnumMember(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_22_/*'}'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnumBody", start, match);
                }
                Caches[Cache_UnionStyleEnumBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnumMember = 45;

        public virtual Match UnionStyleEnumMember(int start)
        {
            if (!Caches[Cache_UnionStyleEnumMember].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = UnionStyleEnumCaseClause(start)) != null)
                    {
                        break;
                    }
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnumMember", start, match);
                }
                Caches[Cache_UnionStyleEnumMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnumCaseClause = 46;

        public virtual Match UnionStyleEnumCaseClause(int start)
        {
            if (!Caches[Cache_UnionStyleEnumCaseClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Lit_indirect(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_case(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = UnionStyleEnumCaseList(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnumCaseClause", start, match);
                }
                Caches[Cache_UnionStyleEnumCaseClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnumCaseList = 47;

        public virtual Match UnionStyleEnumCaseList(int start)
        {
            if (!Caches[Cache_UnionStyleEnumCaseList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = UnionStyleEnumCase(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = UnionStyleEnumCase(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnumCaseList", start, match);
                }
                Caches[Cache_UnionStyleEnumCaseList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnumCase = 48;

        public virtual Match UnionStyleEnumCase(int start)
        {
            if (!Caches[Cache_UnionStyleEnumCase].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = EnumCaseName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, EnumTupleType(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnumCase", start, match);
                }
                Caches[Cache_UnionStyleEnumCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnum = 49;

        public virtual Match RawValueStyleEnum(int start)
        {
            if (!Caches[Cache_RawValueStyleEnum].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_enum(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = EnumName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericParameterClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, TypeInheritanceClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericWhereClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = RawValueStyleEnumBody(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnum", start, match);
                }
                Caches[Cache_RawValueStyleEnum].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnumBody = 50;

        public virtual Match RawValueStyleEnumBody(int start)
        {
            if (!Caches[Cache_RawValueStyleEnumBody].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_21_/*'{'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var oomMatches = new List<Match>();
                    var oomNext = next;
                    while (true)
                    {
                        if ((match = RawValueStyleEnumMember(oomNext)) == null)
                        {
                            break;
                        }
                        oomMatches.Add(match);
                        oomNext = match.Next;
                    }
                    if (oomMatches.Count > 0)
                    {
                        match = Match.Success("+", next, oomMatches);
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_22_/*'}'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnumBody", start, match);
                }
                Caches[Cache_RawValueStyleEnumBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnumMember = 51;

        public virtual Match RawValueStyleEnumMember(int start)
        {
            if (!Caches[Cache_RawValueStyleEnumMember].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = RawValueStyleEnumCaseClause(start)) != null)
                    {
                        break;
                    }
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnumMember", start, match);
                }
                Caches[Cache_RawValueStyleEnumMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnumCaseClause = 52;

        public virtual Match RawValueStyleEnumCaseClause(int start)
        {
            if (!Caches[Cache_RawValueStyleEnumCaseClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_case(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = RawValueStyleEnumCaseList(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnumCaseClause", start, match);
                }
                Caches[Cache_RawValueStyleEnumCaseClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnumCaseList = 53;

        public virtual Match RawValueStyleEnumCaseList(int start)
        {
            if (!Caches[Cache_RawValueStyleEnumCaseList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = RawValueStyleEnumCase(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = RawValueStyleEnumCase(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnumCaseList", start, match);
                }
                Caches[Cache_RawValueStyleEnumCaseList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnumCase = 54;

        public virtual Match RawValueStyleEnumCase(int start)
        {
            if (!Caches[Cache_RawValueStyleEnumCase].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = EnumCaseName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, RawValueAssignment(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnumCase", start, match);
                }
                Caches[Cache_RawValueStyleEnumCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueAssignment = 55;

        public virtual Match RawValueAssignment(int start)
        {
            if (!Caches[Cache_RawValueAssignment].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_20_/*'='*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = RawValueLiteral(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("RawValueAssignment", start, match);
                }
                Caches[Cache_RawValueAssignment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueLiteral = 56;

        public virtual Match RawValueLiteral(int start)
        {
            if (!Caches[Cache_RawValueLiteral].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = NumericLiteral(start)) != null)
                    {
                        break;
                    }
                    if ((match = StaticStringLiteral(start)) != null)
                    {
                        break;
                    }
                    match = BooleanLiteral(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("RawValueLiteral", start, match);
                }
                Caches[Cache_RawValueLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EnumName = 57;

        public virtual Match EnumName(int start)
        {
            if (!Caches[Cache_EnumName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("EnumName", start, match);
                }
                Caches[Cache_EnumName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EnumCaseName = 58;

        public virtual Match EnumCaseName(int start)
        {
            if (!Caches[Cache_EnumCaseName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("EnumCaseName", start, match);
                }
                Caches[Cache_EnumCaseName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ExtensionDeclaration = 59;

        public virtual Match ExtensionDeclaration(int start)
        {
            if (!Caches[Cache_ExtensionDeclaration].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = AccessPrefix(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_extension(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = TypeIdentifier(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, TypeInheritanceClause(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, GenericWhereClause(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = ExtensionBody(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = AccessPrefix(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_extension(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = TypeIdentifier(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("extension-declaration", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = AccessPrefix(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_extension(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("extension-declaration - type-identifier", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ExtensionDeclaration", start, match);
                }
                Caches[Cache_ExtensionDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ExtensionBody = 60;

        public virtual Match ExtensionBody(int start)
        {
            if (!Caches[Cache_ExtensionBody].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_22_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        var oomMatches = new List<Match>();
                        var oomNext = next2;
                        while (true)
                        {
                            if ((match = ExtensionMember(oomNext)) == null)
                            {
                                break;
                            }
                            oomMatches.Add(match);
                            oomNext = match.Next;
                        }
                        if (oomMatches.Count > 0)
                        {
                            match = Match.Success("+", next2, oomMatches);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_22_/*'}'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("extension-body", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ExtensionBody", start, match);
                }
                Caches[Cache_ExtensionBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ExtensionMember = 61;

        public virtual Match ExtensionMember(int start)
        {
            if (!Caches[Cache_ExtensionMember].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ExtensionMember", start, match);
                }
                Caches[Cache_ExtensionMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionDeclaration = 62;

        public virtual Match FunctionDeclaration(int start)
        {
            if (!Caches[Cache_FunctionDeclaration].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = FunctionHead(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = FunctionName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, GenericParameterClause(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = FunctionSignature(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, GenericWhereClause(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = FunctionBody(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = FunctionHead(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("function-declaration", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("FunctionDeclaration", start, match);
                }
                Caches[Cache_FunctionDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionHead = 63;

        public virtual Match FunctionHead(int start)
        {
            if (!Caches[Cache_FunctionHead].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = FullPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_func(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("FunctionHead", start, match);
                }
                Caches[Cache_FunctionHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionSignature = 64;

        public virtual Match FunctionSignature(int start)
        {
            if (!Caches[Cache_FunctionSignature].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = ParameterClause(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Maythrow(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, FunctionResult(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("FunctionSignature", start, match);
                }
                Caches[Cache_FunctionSignature].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Maythrow = 65;

        public virtual Match Maythrow(int start)
        {
            if (!Caches[Cache_Maythrow].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_throws(start)) != null)
                    {
                        break;
                    }
                    match = Lit_rethrows(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("Maythrow", start, match);
                }
                Caches[Cache_Maythrow].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionName = 66;

        public virtual Match FunctionName(int start)
        {
            if (!Caches[Cache_FunctionName].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Name(start)) != null)
                    {
                        break;
                    }
                    if ((match = OperatorName(start)) != null)
                    {
                        break;
                    }
                    {
                        // >>> ERROR
                        new Error(Context).Report("function-name", start);
                        throw new BailOutException();
                        // <<< ERROR
                    }
                }
                if (match != null)
                {
                    match = Match.Success("FunctionName", start, match);
                }
                Caches[Cache_FunctionName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ParameterClause = 67;

        public virtual Match ParameterClause(int start)
        {
            if (!Caches[Cache_ParameterClause].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = ParameterList(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    {
                        // >>> ERROR
                        new Error(Context).Report("parameter-clause", start);
                        throw new BailOutException();
                        // <<< ERROR
                    }
                }
                if (match != null)
                {
                    match = Match.Success("ParameterClause", start, match);
                }
                Caches[Cache_ParameterClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ParameterList = 68;

        public virtual Match ParameterList(int start)
        {
            if (!Caches[Cache_ParameterList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Parameter(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Parameter(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ParameterList", start, match);
                }
                Caches[Cache_ParameterList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Parameter = 69;

        public virtual Match Parameter(int start)
        {
            if (!Caches[Cache_Parameter].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next, Attributes(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = ExternalName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = LocalName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = TypeAnnotation(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = DefaultArgumentClause(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next2, Attributes(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = ExternalName(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = LocalName(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = TypeAnnotation(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_dot_dot_dot_(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next3, Attributes(next3));
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = ExternalName(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = LocalName(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = TypeAnnotation(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next4, Attributes(next4));
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = ExternalName(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = LocalName(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("parameter-1", next4);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches4);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next5, Attributes(next5));
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = LocalName(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = TypeAnnotation(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = DefaultArgumentClause(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches5);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next6, Attributes(next6));
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = LocalName(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = TypeAnnotation(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = Lit_dot_dot_dot_(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches6);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next7 = start;
                    var matches7 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next7, Attributes(next7));
                        matches7.Add(match);
                        next7 = match.Next;
                        if ((match = LocalName(next7)) == null)
                        {
                            break;
                        }
                        matches7.Add(match);
                        next7 = match.Next;
                        if ((match = TypeAnnotation(next7)) == null)
                        {
                            break;
                        }
                        matches7.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches7);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    {
                        // >>> ERROR
                        new Error(Context).Report("parameter", start);
                        throw new BailOutException();
                        // <<< ERROR
                    }
                }
                if (match != null)
                {
                    match = Match.Success("Parameter", start, match);
                }
                Caches[Cache_Parameter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionResult = 70;

        public virtual Match FunctionResult(int start)
        {
            if (!Caches[Cache_FunctionResult].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'->'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Attributes(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Type(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'->'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("function-result", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("FunctionResult", start, match);
                }
                Caches[Cache_FunctionResult].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ExternalName = 71;

        public virtual Match ExternalName(int start)
        {
            if (!Caches[Cache_ExternalName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("ExternalName", start, match);
                }
                Caches[Cache_ExternalName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LocalName = 72;

        public virtual Match LocalName(int start)
        {
            if (!Caches[Cache_LocalName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("LocalName", start, match);
                }
                Caches[Cache_LocalName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DefaultArgumentClause = 73;

        public virtual Match DefaultArgumentClause(int start)
        {
            if (!Caches[Cache_DefaultArgumentClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_20_/*'='*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("DefaultArgumentClause", start, match);
                }
                Caches[Cache_DefaultArgumentClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeAnnotation = 74;

        public virtual Match TypeAnnotation(int start)
        {
            if (!Caches[Cache_TypeAnnotation].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_24_/*':'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Attributes(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Lit_25_/*'__owned'*/(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Lit_inout(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Type(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_24_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, Attributes(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, Lit_25_/*'__owned'*/(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, Lit_inout(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("type-annotation-1", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_24_/*':'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("type-annotation-2", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("TypeAnnotation", start, match);
                }
                Caches[Cache_TypeAnnotation].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionBody = 75;

        public virtual Match FunctionBody(int start)
        {
            if (!Caches[Cache_FunctionBody].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = CodeBlock(start)) != null)
                    {
                        break;
                    }
                    match = Match.Success("ε", start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("FunctionBody", start, match);
                }
                Caches[Cache_FunctionBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InitializerDeclaration = 76;

        public virtual Match InitializerDeclaration(int start)
        {
            if (!Caches[Cache_InitializerDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = InitializerHead(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericParameterClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ParameterClause(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Maythrow(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericWhereClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, InitializerBody(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("InitializerDeclaration", start, match);
                }
                Caches[Cache_InitializerDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InitializerHead = 77;

        public virtual Match InitializerHead(int start)
        {
            if (!Caches[Cache_InitializerHead].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = FullPrefix(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_init(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Not_(next, More(next))) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        while (true) // ---Choice---
                        {
                            if ((match = Lit_26_/*'?'*/(next)) != null)
                            {
                                break;
                            }
                            match = Lit_8_/*'!'*/(next);
                            break;
                        }
                        match = Match.Optional(next, match);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = FullPrefix(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_init(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Not_(next2, More(next2))) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("initializer-head", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("InitializerHead", start, match);
                }
                Caches[Cache_InitializerHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InitializerBody = 78;

        public virtual Match InitializerBody(int start)
        {
            if (!Caches[Cache_InitializerBody].Already(start, out var match))
            {
                match = CodeBlock(start);
                if (match != null)
                {
                    match = Match.Success("InitializerBody", start, match);
                }
                Caches[Cache_InitializerBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorDeclaration = 79;

        public virtual Match OperatorDeclaration(int start)
        {
            if (!Caches[Cache_OperatorDeclaration].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = PrefixOperatorDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = PostfixOperatorDeclaration(start)) != null)
                    {
                        break;
                    }
                    match = InfixOperatorDeclaration(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("OperatorDeclaration", start, match);
                }
                Caches[Cache_OperatorDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrefixOperatorDeclaration = 80;

        public virtual Match PrefixOperatorDeclaration(int start)
        {
            if (!Caches[Cache_PrefixOperatorDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_prefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_operator(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Operator(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_24_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = OperatorRestrictions(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PrefixOperatorDeclaration", start, match);
                }
                Caches[Cache_PrefixOperatorDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PostfixOperatorDeclaration = 81;

        public virtual Match PostfixOperatorDeclaration(int start)
        {
            if (!Caches[Cache_PostfixOperatorDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_postfix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_operator(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Operator(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_24_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = OperatorRestrictions(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PostfixOperatorDeclaration", start, match);
                }
                Caches[Cache_PostfixOperatorDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InfixOperatorDeclaration = 82;

        public virtual Match InfixOperatorDeclaration(int start)
        {
            if (!Caches[Cache_InfixOperatorDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_infix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_operator(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Operator(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, InfixOperatorGroup(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("InfixOperatorDeclaration", start, match);
                }
                Caches[Cache_InfixOperatorDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InfixOperatorGroup = 83;

        public virtual Match InfixOperatorGroup(int start)
        {
            if (!Caches[Cache_InfixOperatorGroup].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = PrecedenceGroupName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_16_/*','*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = OperatorRestrictions(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("InfixOperatorGroup", start, match);
                }
                Caches[Cache_InfixOperatorGroup].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorRestrictions = 84;

        public virtual Match OperatorRestrictions(int start)
        {
            if (!Caches[Cache_OperatorRestrictions].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = OperatorRestriction(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = OperatorRestriction(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("OperatorRestrictions", start, match);
                }
                Caches[Cache_OperatorRestrictions].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorRestriction = 85;

        public virtual Match OperatorRestriction(int start)
        {
            if (!Caches[Cache_OperatorRestriction].Already(start, out var match))
            {
                match = TypeIdentifier(start);
                if (match != null)
                {
                    match = Match.Success("OperatorRestriction", start, match);
                }
                Caches[Cache_OperatorRestriction].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupDeclaration = 86;

        public virtual Match PrecedenceGroupDeclaration(int start)
        {
            if (!Caches[Cache_PrecedenceGroupDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_precedencegroup(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = PrecedenceGroupName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = PrecedenceGroupBody(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupDeclaration", start, match);
                }
                Caches[Cache_PrecedenceGroupDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupBody = 87;

        public virtual Match PrecedenceGroupBody(int start)
        {
            if (!Caches[Cache_PrecedenceGroupBody].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_21_/*'{'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = PrecedenceGroupAttribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_22_/*'}'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupBody", start, match);
                }
                Caches[Cache_PrecedenceGroupBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupAttribute = 88;

        public virtual Match PrecedenceGroupAttribute(int start)
        {
            if (!Caches[Cache_PrecedenceGroupAttribute].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = PrecedenceGroupRelation(start)) != null)
                    {
                        break;
                    }
                    if ((match = PrecedenceGroupAssignment(start)) != null)
                    {
                        break;
                    }
                    match = PrecedenceGroupAssociativity(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupAttribute", start, match);
                }
                Caches[Cache_PrecedenceGroupAttribute].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupRelation = 89;

        public virtual Match PrecedenceGroupRelation(int start)
        {
            if (!Caches[Cache_PrecedenceGroupRelation].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    while (true) // ---Choice---
                    {
                        if ((match = Lit_higherThan(next)) != null)
                        {
                            break;
                        }
                        match = Lit_lowerThan(next);
                        break;
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = PrecedenceGroupNames(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupRelation", start, match);
                }
                Caches[Cache_PrecedenceGroupRelation].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupAssignment = 90;

        public virtual Match PrecedenceGroupAssignment(int start)
        {
            if (!Caches[Cache_PrecedenceGroupAssignment].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_assignment(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = BooleanLiteral(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupAssignment", start, match);
                }
                Caches[Cache_PrecedenceGroupAssignment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupAssociativity = 91;

        public virtual Match PrecedenceGroupAssociativity(int start)
        {
            if (!Caches[Cache_PrecedenceGroupAssociativity].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_associativity(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    while (true) // ---Choice---
                    {
                        if ((match = Lit_left(next)) != null)
                        {
                            break;
                        }
                        if ((match = Lit_right(next)) != null)
                        {
                            break;
                        }
                        match = Lit_none(next);
                        break;
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupAssociativity", start, match);
                }
                Caches[Cache_PrecedenceGroupAssociativity].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupNames = 92;

        public virtual Match PrecedenceGroupNames(int start)
        {
            if (!Caches[Cache_PrecedenceGroupNames].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = PrecedenceGroupName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = PrecedenceGroupName(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupNames", start, match);
                }
                Caches[Cache_PrecedenceGroupNames].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupName = 93;

        public virtual Match PrecedenceGroupName(int start)
        {
            if (!Caches[Cache_PrecedenceGroupName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupName", start, match);
                }
                Caches[Cache_PrecedenceGroupName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolDeclaration = 94;

        public virtual Match ProtocolDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = AccessPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_protocol(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ProtocolName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, TypeInheritanceClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericWhereClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ProtocolBody(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ProtocolDeclaration", start, match);
                }
                Caches[Cache_ProtocolDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolName = 95;

        public virtual Match ProtocolName(int start)
        {
            if (!Caches[Cache_ProtocolName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("ProtocolName", start, match);
                }
                Caches[Cache_ProtocolName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolBody = 96;

        public virtual Match ProtocolBody(int start)
        {
            if (!Caches[Cache_ProtocolBody].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, ProtocolMembers(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_22_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("protocol-body", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ProtocolBody", start, match);
                }
                Caches[Cache_ProtocolBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolMembers = 97;

        public virtual Match ProtocolMembers(int start)
        {
            if (!Caches[Cache_ProtocolMembers].Already(start, out var match))
            {
                var oomMatches = new List<Match>();
                var oomNext = start;
                while (true)
                {
                    if ((match = ProtocolMember(oomNext)) == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", start, oomMatches);
                }
                if (match != null)
                {
                    match = Match.Success("ProtocolMembers", start, match);
                }
                Caches[Cache_ProtocolMembers].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolMember = 98;

        public virtual Match ProtocolMember(int start)
        {
            if (!Caches[Cache_ProtocolMember].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = ProtocolMemberDeclaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ProtocolMember", start, match);
                }
                Caches[Cache_ProtocolMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolMemberDeclaration = 99;

        public virtual Match ProtocolMemberDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolMemberDeclaration].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = ProtocolPropertyDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = ProtocolMethodDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = ProtocolInitializerDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = ProtocolSubscriptDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = ProtocolAssociatedTypeDeclaration(start)) != null)
                    {
                        break;
                    }
                    match = TypealiasDeclaration(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ProtocolMemberDeclaration", start, match);
                }
                Caches[Cache_ProtocolMemberDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolPropertyDeclaration = 100;

        public virtual Match ProtocolPropertyDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolPropertyDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = VariableDeclarationHead(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = VariableName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = TypeAnnotation(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = GetterSetterKeywordBlock(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ProtocolPropertyDeclaration", start, match);
                }
                Caches[Cache_ProtocolPropertyDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolMethodDeclaration = 101;

        public virtual Match ProtocolMethodDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolMethodDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = FunctionHead(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = FunctionName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericParameterClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = FunctionSignature(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericWhereClause(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ProtocolMethodDeclaration", start, match);
                }
                Caches[Cache_ProtocolMethodDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolInitializerDeclaration = 102;

        public virtual Match ProtocolInitializerDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolInitializerDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = InitializerHead(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericParameterClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ParameterClause(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Maythrow(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericWhereClause(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ProtocolInitializerDeclaration", start, match);
                }
                Caches[Cache_ProtocolInitializerDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolSubscriptDeclaration = 103;

        public virtual Match ProtocolSubscriptDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolSubscriptDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = SubscriptHead(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = SubscriptResult(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericWhereClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = GetterSetterKeywordBlock(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ProtocolSubscriptDeclaration", start, match);
                }
                Caches[Cache_ProtocolSubscriptDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolAssociatedTypeDeclaration = 104;

        public virtual Match ProtocolAssociatedTypeDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolAssociatedTypeDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = AccessPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Lit_override(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_associatedtype(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = TypealiasName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, TypeInheritanceClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, TypealiasAssignment(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericWhereClause(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ProtocolAssociatedTypeDeclaration", start, match);
                }
                Caches[Cache_ProtocolAssociatedTypeDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StructDeclaration = 105;

        public virtual Match StructDeclaration(int start)
        {
            if (!Caches[Cache_StructDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = AccessPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_struct(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = StructName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericParameterClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, TypeInheritanceClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericWhereClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = StructBody(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("StructDeclaration", start, match);
                }
                Caches[Cache_StructDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StructName = 106;

        public virtual Match StructName(int start)
        {
            if (!Caches[Cache_StructName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("StructName", start, match);
                }
                Caches[Cache_StructName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StructBody = 107;

        public virtual Match StructBody(int start)
        {
            if (!Caches[Cache_StructBody].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, StructMembers(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_22_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("struct-body", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("StructBody", start, match);
                }
                Caches[Cache_StructBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StructMembers = 108;

        public virtual Match StructMembers(int start)
        {
            if (!Caches[Cache_StructMembers].Already(start, out var match))
            {
                var oomMatches = new List<Match>();
                var oomNext = start;
                while (true)
                {
                    if ((match = StructMember(oomNext)) == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", start, oomMatches);
                }
                if (match != null)
                {
                    match = Match.Success("StructMembers", start, match);
                }
                Caches[Cache_StructMembers].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StructMember = 109;

        public virtual Match StructMember(int start)
        {
            if (!Caches[Cache_StructMember].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("StructMember", start, match);
                }
                Caches[Cache_StructMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SubscriptDeclaration = 110;

        public virtual Match SubscriptDeclaration(int start)
        {
            if (!Caches[Cache_SubscriptDeclaration].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = SubscriptHead(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = SubscriptResult(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, GenericWhereClause(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = GetterSetterBlock(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = SubscriptHead(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = SubscriptResult(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, GenericWhereClause(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = GetterSetterKeywordBlock(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("SubscriptDeclaration", start, match);
                }
                Caches[Cache_SubscriptDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SubscriptHead = 111;

        public virtual Match SubscriptHead(int start)
        {
            if (!Caches[Cache_SubscriptHead].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = FullPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_subscript(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericParameterClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ParameterClause(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SubscriptHead", start, match);
                }
                Caches[Cache_SubscriptHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SubscriptResult = 112;

        public virtual Match SubscriptResult(int start)
        {
            if (!Caches[Cache_SubscriptResult].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_23_/*'->'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Type(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SubscriptResult", start, match);
                }
                Caches[Cache_SubscriptResult].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypealiasDeclaration = 113;

        public virtual Match TypealiasDeclaration(int start)
        {
            if (!Caches[Cache_TypealiasDeclaration].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = AccessPrefix(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_typealias(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = TypealiasName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, GenericParameterClause(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = TypealiasAssignment(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, GenericWhereClause(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = AccessPrefix(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_typealias(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("typealias-declaration", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("TypealiasDeclaration", start, match);
                }
                Caches[Cache_TypealiasDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypealiasName = 114;

        public virtual Match TypealiasName(int start)
        {
            if (!Caches[Cache_TypealiasName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("TypealiasName", start, match);
                }
                Caches[Cache_TypealiasName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypealiasAssignment = 115;

        public virtual Match TypealiasAssignment(int start)
        {
            if (!Caches[Cache_TypealiasAssignment].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_20_/*'='*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Type(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("TypealiasAssignment", start, match);
                }
                Caches[Cache_TypealiasAssignment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_VariableDeclaration = 116;

        public virtual Match VariableDeclaration(int start)
        {
            if (!Caches[Cache_VariableDeclaration].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = VariableDeclarationHead(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = VariableName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = TypeAnnotation(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = GetterSetterBlock(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = VariableDeclarationHead(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = VariableName(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = TypeAnnotation(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = GetterSetterKeywordBlock(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = VariableDeclarationHead(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = VariableName(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = TypeAnnotation(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Initializer(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = WillSetDidSetBlock(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = VariableDeclarationHead(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = VariableName(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = TypeAnnotation(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = WillSetDidSetBlock(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches4);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = VariableDeclarationHead(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = VariableName(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = Initializer(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = WillSetDidSetBlock(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches5);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = VariableDeclarationHead(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = PatternInitializerList(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches6);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next7 = start;
                    var matches7 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = VariableDeclarationHead(next7)) == null)
                        {
                            break;
                        }
                        matches7.Add(match);
                        next7 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("variable-declaration", next7);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches7);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("VariableDeclaration", start, match);
                }
                Caches[Cache_VariableDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_VariableDeclarationHead = 117;

        public virtual Match VariableDeclarationHead(int start)
        {
            if (!Caches[Cache_VariableDeclarationHead].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = FullPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_var(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("VariableDeclarationHead", start, match);
                }
                Caches[Cache_VariableDeclarationHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_VariableName = 118;

        public virtual Match VariableName(int start)
        {
            if (!Caches[Cache_VariableName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("VariableName", start, match);
                }
                Caches[Cache_VariableName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GetterSetterBlock = 119;

        public virtual Match GetterSetterBlock(int start)
        {
            if (!Caches[Cache_GetterSetterBlock].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = GetterClause(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, SetterClause(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, SpecialModifyClause(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_22_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = SetterClause(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = GetterClause(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_22_/*'}'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = CodeBlock(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("GetterSetterBlock", start, match);
                }
                Caches[Cache_GetterSetterBlock].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GetterClause = 120;

        public virtual Match GetterClause(int start)
        {
            if (!Caches[Cache_GetterClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = MutationPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_get(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, CodeBlock(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("GetterClause", start, match);
                }
                Caches[Cache_GetterClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SpecialModifyClause = 121;

        public virtual Match SpecialModifyClause(int start)
        {
            if (!Caches[Cache_SpecialModifyClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = MutationPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_27_/*'_modify'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, CodeBlock(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SpecialModifyClause", start, match);
                }
                Caches[Cache_SpecialModifyClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SetterClause = 122;

        public virtual Match SetterClause(int start)
        {
            if (!Caches[Cache_SetterClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = MutationPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_set(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, SetterName(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, CodeBlock(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SetterClause", start, match);
                }
                Caches[Cache_SetterClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SetterName = 123;

        public virtual Match SetterName(int start)
        {
            if (!Caches[Cache_SetterName].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_1_/*'('*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Name(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SetterName", start, match);
                }
                Caches[Cache_SetterName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GetterSetterKeywordBlock = 124;

        public virtual Match GetterSetterKeywordBlock(int start)
        {
            if (!Caches[Cache_GetterSetterKeywordBlock].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = GetterKeywordClause(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, SetterKeywordClause(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_22_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = SetterKeywordClause(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = GetterKeywordClause(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_22_/*'}'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("GetterSetterKeywordBlock", start, match);
                }
                Caches[Cache_GetterSetterKeywordBlock].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GetterKeywordClause = 125;

        public virtual Match GetterKeywordClause(int start)
        {
            if (!Caches[Cache_GetterKeywordClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = MutationPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_get(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("GetterKeywordClause", start, match);
                }
                Caches[Cache_GetterKeywordClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SetterKeywordClause = 126;

        public virtual Match SetterKeywordClause(int start)
        {
            if (!Caches[Cache_SetterKeywordClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = MutationPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_set(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SetterKeywordClause", start, match);
                }
                Caches[Cache_SetterKeywordClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WillSetDidSetBlock = 127;

        public virtual Match WillSetDidSetBlock(int start)
        {
            if (!Caches[Cache_WillSetDidSetBlock].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = WillSetClause(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, DidSetClause(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_22_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = DidSetClause(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, WillSetClause(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_22_/*'}'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("WillSetDidSetBlock", start, match);
                }
                Caches[Cache_WillSetDidSetBlock].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WillSetClause = 128;

        public virtual Match WillSetClause(int start)
        {
            if (!Caches[Cache_WillSetClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_willSet(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, SetterName(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CodeBlock(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("WillSetClause", start, match);
                }
                Caches[Cache_WillSetClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DidSetClause = 129;

        public virtual Match DidSetClause(int start)
        {
            if (!Caches[Cache_DidSetClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_didSet(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, SetterName(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CodeBlock(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("DidSetClause", start, match);
                }
                Caches[Cache_DidSetClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Declaration = 130;

        public virtual Match Declaration(int start)
        {
            if (!Caches[Cache_Declaration].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = ImportDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = ConstantDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = VariableDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = TypealiasDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = FunctionDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = EnumDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = StructDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = ClassDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = ProtocolDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = InitializerDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = DeinitializerDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = ExtensionDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = SubscriptDeclaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = OperatorDeclaration(start)) != null)
                    {
                        break;
                    }
                    match = PrecedenceGroupDeclaration(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("Declaration", start, match);
                }
                Caches[Cache_Declaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ImportDeclaration = 131;

        public virtual Match ImportDeclaration(int start)
        {
            if (!Caches[Cache_ImportDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_import(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, ImportKind(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ImportPath(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ImportDeclaration", start, match);
                }
                Caches[Cache_ImportDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ImportKind = 132;

        public virtual Match ImportKind(int start)
        {
            if (!Caches[Cache_ImportKind].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_typealias(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_struct(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_class(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_enum(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_protocol(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_let(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_var(start)) != null)
                    {
                        break;
                    }
                    match = Lit_func(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ImportKind", start, match);
                }
                Caches[Cache_ImportKind].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ImportPath = 133;

        public virtual Match ImportPath(int start)
        {
            if (!Caches[Cache_ImportPath].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = ImportPathIdentifier(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_dot_(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = ImportPathIdentifier(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ImportPath", start, match);
                }
                Caches[Cache_ImportPath].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ImportPathIdentifier = 134;

        public virtual Match ImportPathIdentifier(int start)
        {
            if (!Caches[Cache_ImportPathIdentifier].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Name(start)) != null)
                    {
                        break;
                    }
                    match = Operator(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ImportPathIdentifier", start, match);
                }
                Caches[Cache_ImportPathIdentifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClassDeclaration = 135;

        public virtual Match ClassDeclaration(int start)
        {
            if (!Caches[Cache_ClassDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = ClassHead(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ClassName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericParameterClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, TypeInheritanceClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericWhereClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ClassBody(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ClassDeclaration", start, match);
                }
                Caches[Cache_ClassDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClassHead = 136;

        public virtual Match ClassHead(int start)
        {
            if (!Caches[Cache_ClassHead].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next, Attributes(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, AccessLevelModifier(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Lit_final(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_class(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next2, Attributes(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_final(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, AccessLevelModifier(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_class(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ClassHead", start, match);
                }
                Caches[Cache_ClassHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClassName = 137;

        public virtual Match ClassName(int start)
        {
            if (!Caches[Cache_ClassName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("ClassName", start, match);
                }
                Caches[Cache_ClassName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClassBody = 138;

        public virtual Match ClassBody(int start)
        {
            if (!Caches[Cache_ClassBody].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_21_/*'{'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, ClassMembers(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_22_/*'}'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ClassBody", start, match);
                }
                Caches[Cache_ClassBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClassMembers = 139;

        public virtual Match ClassMembers(int start)
        {
            if (!Caches[Cache_ClassMembers].Already(start, out var match))
            {
                var oomMatches = new List<Match>();
                var oomNext = start;
                while (true)
                {
                    if ((match = ClassMember(oomNext)) == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", start, oomMatches);
                }
                if (match != null)
                {
                    match = Match.Success("ClassMembers", start, match);
                }
                Caches[Cache_ClassMembers].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClassMember = 140;

        public virtual Match ClassMember(int start)
        {
            if (!Caches[Cache_ClassMember].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ClassMember", start, match);
                }
                Caches[Cache_ClassMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DeinitializerDeclaration = 141;

        public virtual Match DeinitializerDeclaration(int start)
        {
            if (!Caches[Cache_DeinitializerDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_deinit(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CodeBlock(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("DeinitializerDeclaration", start, match);
                }
                Caches[Cache_DeinitializerDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Expression = 142;

        public virtual Match Expression(int start)
        {
            if (!Caches[Cache_Expression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, TryOperator(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = PrefixExpression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, BinaryExpressions(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Expression", start, match);
                }
                Caches[Cache_Expression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BinaryExpressions = 143;

        public virtual Match BinaryExpressions(int start)
        {
            if (!Caches[Cache_BinaryExpressions].Already(start, out var match))
            {
                var oomMatches = new List<Match>();
                var oomNext = start;
                while (true)
                {
                    if ((match = BinaryExpression(oomNext)) == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", start, oomMatches);
                }
                if (match != null)
                {
                    match = Match.Success("BinaryExpressions", start, match);
                }
                Caches[Cache_BinaryExpressions].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BinaryExpression = 144;

        public virtual Match BinaryExpression(int start)
        {
            if (!Caches[Cache_BinaryExpression].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = AssignmentOperator(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, TryOperator(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = PrefixExpression(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = ConditionalOperator(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, TryOperator(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = PrefixExpression(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = BinaryOperator(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = PrefixExpression(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = TypeCastingOperator(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("BinaryExpression", start, match);
                }
                Caches[Cache_BinaryExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrefixExpression = 145;

        public virtual Match PrefixExpression(int start)
        {
            if (!Caches[Cache_PrefixExpression].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next, PrefixOperator(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = PostfixExpression(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = InOutExpression(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PrefixExpression", start, match);
                }
                Caches[Cache_PrefixExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InOutExpression = 146;

        public virtual Match InOutExpression(int start)
        {
            if (!Caches[Cache_InOutExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_28_/*'&'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Name(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("InOutExpression", start, match);
                }
                Caches[Cache_InOutExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PostfixExpression = 147;

        public virtual Match PostfixExpression(int start)
        {
            if (!Caches[Cache_PostfixExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = PrimaryExpression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = PostfixAppendix(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PostfixExpression", start, match);
                }
                Caches[Cache_PostfixExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PostfixAppendix = 148;

        public virtual Match PostfixAppendix(int start)
        {
            if (!Caches[Cache_PostfixAppendix].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = PostfixOperator(start)) != null)
                    {
                        break;
                    }
                    if ((match = FunctionCall(start)) != null)
                    {
                        break;
                    }
                    if ((match = InitializerAppendix(start)) != null)
                    {
                        break;
                    }
                    if ((match = ExplicitMember(start)) != null)
                    {
                        break;
                    }
                    if ((match = PostfixSelf(start)) != null)
                    {
                        break;
                    }
                    if ((match = Subscript(start)) != null)
                    {
                        break;
                    }
                    if ((match = ForcedValue(start)) != null)
                    {
                        break;
                    }
                    match = OptionalChaining(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PostfixAppendix", start, match);
                }
                Caches[Cache_PostfixAppendix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionCall = 149;

        public virtual Match FunctionCall(int start)
        {
            if (!Caches[Cache_FunctionCall].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = FunctionCallArgumentClause(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = TrailingClosures(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = FunctionCallArgumentClause(start)) != null)
                    {
                        break;
                    }
                    match = TrailingClosures(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("FunctionCall", start, match);
                }
                Caches[Cache_FunctionCall].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionCallArgumentClause = 150;

        public virtual Match FunctionCallArgumentClause(int start)
        {
            if (!Caches[Cache_FunctionCallArgumentClause].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, FunctionCallArgumentList(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("function-call-argument-clause", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("FunctionCallArgumentClause", start, match);
                }
                Caches[Cache_FunctionCallArgumentClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionCallArgumentList = 151;

        public virtual Match FunctionCallArgumentList(int start)
        {
            if (!Caches[Cache_FunctionCallArgumentList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = FunctionCallArgument(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = FunctionCallArgument(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("FunctionCallArgumentList", start, match);
                }
                Caches[Cache_FunctionCallArgumentList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionCallArgument = 152;

        public virtual Match FunctionCallArgument(int start)
        {
            if (!Caches[Cache_FunctionCallArgument].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_24_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    while (true) // ---Choice---
                    {
                        if ((match = Expression(next)) != null)
                        {
                            break;
                        }
                        match = OperatorName(next);
                        break;
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("FunctionCallArgument", start, match);
                }
                Caches[Cache_FunctionCallArgument].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TrailingClosures = 153;

        public virtual Match TrailingClosures(int start)
        {
            if (!Caches[Cache_TrailingClosures].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_24_/*':'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = And_(next, ClosureExpression(next))) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = ClosureExpression(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, LabeledTrailingClosures(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_24_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = And_(next2, Lit_21_/*'{'*/(next2))) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("trailing-closures", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("TrailingClosures", start, match);
                }
                Caches[Cache_TrailingClosures].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LabeledTrailingClosures = 154;

        public virtual Match LabeledTrailingClosures(int start)
        {
            if (!Caches[Cache_LabeledTrailingClosures].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = LabeledTrailingClosure(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = LabeledTrailingClosure(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("LabeledTrailingClosures", start, match);
                }
                Caches[Cache_LabeledTrailingClosures].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LabeledTrailingClosure = 155;

        public virtual Match LabeledTrailingClosure(int start)
        {
            if (!Caches[Cache_LabeledTrailingClosure].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Name(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ClosureExpression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("LabeledTrailingClosure", start, match);
                }
                Caches[Cache_LabeledTrailingClosure].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InitializerAppendix = 156;

        public virtual Match InitializerAppendix(int start)
        {
            if (!Caches[Cache_InitializerAppendix].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_dot_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_init(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, ArgumentNameClause(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("InitializerAppendix", start, match);
                }
                Caches[Cache_InitializerAppendix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArgumentNameClause = 157;

        public virtual Match ArgumentNameClause(int start)
        {
            if (!Caches[Cache_ArgumentNameClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_1_/*'('*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ArgumentNames(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ArgumentNameClause", start, match);
                }
                Caches[Cache_ArgumentNameClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArgumentNames = 158;

        public virtual Match ArgumentNames(int start)
        {
            if (!Caches[Cache_ArgumentNames].Already(start, out var match))
            {
                var oomMatches = new List<Match>();
                var oomNext = start;
                while (true)
                {
                    if ((match = ArgumentName(oomNext)) == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", start, oomMatches);
                }
                if (match != null)
                {
                    match = Match.Success("ArgumentNames", start, match);
                }
                Caches[Cache_ArgumentNames].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArgumentName = 159;

        public virtual Match ArgumentName(int start)
        {
            if (!Caches[Cache_ArgumentName].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Name(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ArgumentName", start, match);
                }
                Caches[Cache_ArgumentName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ExplicitMember = 160;

        public virtual Match ExplicitMember(int start)
        {
            if (!Caches[Cache_ExplicitMember].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_dot_(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = DecimalDigits(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_dot_(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = ArgumentNameClause(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_dot_(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Name(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        match = Match.Optional(next3, GenericArgumentClause(next3));
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ExplicitMember", start, match);
                }
                Caches[Cache_ExplicitMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PostfixSelf = 161;

        public virtual Match PostfixSelf(int start)
        {
            if (!Caches[Cache_PostfixSelf].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_dot_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_self(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PostfixSelf", start, match);
                }
                Caches[Cache_PostfixSelf].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Subscript = 162;

        public virtual Match Subscript(int start)
        {
            if (!Caches[Cache_Subscript].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_29_/*'['*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = FunctionCallArgumentList(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_30_/*']'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Subscript", start, match);
                }
                Caches[Cache_Subscript].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ForcedValue = 163;

        public virtual Match ForcedValue(int start)
        {
            if (!Caches[Cache_ForcedValue].Already(start, out var match))
            {
                match = CharacterExact_(start, '!');
                if (match != null)
                {
                    match = Match.Success("ForcedValue", start, match);
                }
                Caches[Cache_ForcedValue].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OptionalChaining = 164;

        public virtual Match OptionalChaining(int start)
        {
            if (!Caches[Cache_OptionalChaining].Already(start, out var match))
            {
                match = CharacterExact_(start, '?');
                if (match != null)
                {
                    match = Match.Success("OptionalChaining", start, match);
                }
                Caches[Cache_OptionalChaining].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrimaryExpression = 165;

        public virtual Match PrimaryExpression(int start)
        {
            if (!Caches[Cache_PrimaryExpression].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Name(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, GenericArgumentClause(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = LiteralExpression(start)) != null)
                    {
                        break;
                    }
                    if ((match = SelfExpression(start)) != null)
                    {
                        break;
                    }
                    if ((match = SuperclassExpression(start)) != null)
                    {
                        break;
                    }
                    if ((match = ClosureExpression(start)) != null)
                    {
                        break;
                    }
                    if ((match = ParenthesizedExpression(start)) != null)
                    {
                        break;
                    }
                    if ((match = TupleExpression(start)) != null)
                    {
                        break;
                    }
                    if ((match = ImplicitMemberExpression(start)) != null)
                    {
                        break;
                    }
                    if ((match = WildcardExpression(start)) != null)
                    {
                        break;
                    }
                    if ((match = KeyPathExpression(start)) != null)
                    {
                        break;
                    }
                    if ((match = SelectorExpression(start)) != null)
                    {
                        break;
                    }
                    match = KeyPathStringExpression(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PrimaryExpression", start, match);
                }
                Caches[Cache_PrimaryExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LiteralExpression = 166;

        public virtual Match LiteralExpression(int start)
        {
            if (!Caches[Cache_LiteralExpression].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Literal(start)) != null)
                    {
                        break;
                    }
                    if ((match = ArrayLiteral(start)) != null)
                    {
                        break;
                    }
                    if ((match = DictionaryLiteral(start)) != null)
                    {
                        break;
                    }
                    if ((match = PlaygroundLiteral(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_31_/*'#file'*/(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_32_/*'#fileID'*/(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_33_/*'#filePath'*/(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_34_/*'#line'*/(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_35_/*'#column'*/(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_36_/*'#function'*/(start)) != null)
                    {
                        break;
                    }
                    match = Lit_37_/*'#dsohandle'*/(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("LiteralExpression", start, match);
                }
                Caches[Cache_LiteralExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Literal = 167;

        public virtual Match Literal(int start)
        {
            if (!Caches[Cache_Literal].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = NumericLiteral(start)) != null)
                    {
                        break;
                    }
                    if ((match = StringLiteral(start)) != null)
                    {
                        break;
                    }
                    if ((match = BooleanLiteral(start)) != null)
                    {
                        break;
                    }
                    match = NilLiteral(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("Literal", start, match);
                }
                Caches[Cache_Literal].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_NumericLiteral = 168;

        public virtual Match NumericLiteral(int start)
        {
            if (!Caches[Cache_NumericLiteral].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Lit_38_/*'-'*/(next));
                    matches.Add(match);
                    next = match.Next;
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    while (true) // ---Choice---
                    {
                        if ((match = FloatingPointLiteral(next)) != null)
                        {
                            break;
                        }
                        match = IntegerLiteral(next);
                        break;
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("NumericLiteral", start, match);
                }
                Caches[Cache_NumericLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BooleanLiteral = 169;

        public virtual Match BooleanLiteral(int start)
        {
            if (!Caches[Cache_BooleanLiteral].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_true(start)) != null)
                    {
                        break;
                    }
                    match = Lit_false(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("BooleanLiteral", start, match);
                }
                Caches[Cache_BooleanLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_NilLiteral = 170;

        public virtual Match NilLiteral(int start)
        {
            if (!Caches[Cache_NilLiteral].Already(start, out var match))
            {
                match = Lit_nil(start);
                if (match != null)
                {
                    match = Match.Success("NilLiteral", start, match);
                }
                Caches[Cache_NilLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArrayLiteral = 171;

        public virtual Match ArrayLiteral(int start)
        {
            if (!Caches[Cache_ArrayLiteral].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_29_/*'['*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = ArrayLiteralItems(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_30_/*']'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_29_/*'['*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_30_/*']'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ArrayLiteral", start, match);
                }
                Caches[Cache_ArrayLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArrayLiteralItems = 172;

        public virtual Match ArrayLiteralItems(int start)
        {
            if (!Caches[Cache_ArrayLiteralItems].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = ArrayLiteralItem(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = ArrayLiteralItem(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Lit_16_/*','*/(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ArrayLiteralItems", start, match);
                }
                Caches[Cache_ArrayLiteralItems].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArrayLiteralItem = 173;

        public virtual Match ArrayLiteralItem(int start)
        {
            if (!Caches[Cache_ArrayLiteralItem].Already(start, out var match))
            {
                match = Expression(start);
                if (match != null)
                {
                    match = Match.Success("ArrayLiteralItem", start, match);
                }
                Caches[Cache_ArrayLiteralItem].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DictionaryLiteral = 174;

        public virtual Match DictionaryLiteral(int start)
        {
            if (!Caches[Cache_DictionaryLiteral].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_29_/*'['*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = DictionaryLiteralItems(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_30_/*']'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_29_/*'['*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_24_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_30_/*']'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("DictionaryLiteral", start, match);
                }
                Caches[Cache_DictionaryLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DictionaryLiteralItems = 175;

        public virtual Match DictionaryLiteralItems(int start)
        {
            if (!Caches[Cache_DictionaryLiteralItems].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = DictionaryLiteralItem(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = DictionaryLiteralItem(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Lit_16_/*','*/(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("DictionaryLiteralItems", start, match);
                }
                Caches[Cache_DictionaryLiteralItems].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DictionaryLiteralItem = 176;

        public virtual Match DictionaryLiteralItem(int start)
        {
            if (!Caches[Cache_DictionaryLiteralItem].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("DictionaryLiteralItem", start, match);
                }
                Caches[Cache_DictionaryLiteralItem].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PlaygroundLiteral = 177;

        public virtual Match PlaygroundLiteral(int start)
        {
            if (!Caches[Cache_PlaygroundLiteral].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_39_/*'#colorLiteral'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_red(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*':'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Expression(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_16_/*','*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_green(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*':'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Expression(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_16_/*','*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_blue(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*':'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Expression(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_16_/*','*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_alpha(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*':'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Expression(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_40_/*'#fileLiteral'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_1_/*'('*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_resourceName(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_24_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Expression(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_41_/*'#imageLiteral'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_1_/*'('*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_resourceName(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_24_/*':'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Expression(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_2_/*')'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PlaygroundLiteral", start, match);
                }
                Caches[Cache_PlaygroundLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SelfExpression = 178;

        public virtual Match SelfExpression(int start)
        {
            if (!Caches[Cache_SelfExpression].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_self(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_dot_(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_init(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Not_(next, More(next))) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_self(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_dot_(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_self(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_29_/*'['*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = FunctionCallArgumentList(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_30_/*']'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_self(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = Not_(next4, More(next4))) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches4);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("SelfExpression", start, match);
                }
                Caches[Cache_SelfExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SuperclassExpression = 179;

        public virtual Match SuperclassExpression(int start)
        {
            if (!Caches[Cache_SuperclassExpression].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_super(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_dot_(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_init(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Not_(next, More(next))) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_super(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_dot_(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_super(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_29_/*'['*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = FunctionCallArgumentList(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_30_/*']'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("SuperclassExpression", start, match);
                }
                Caches[Cache_SuperclassExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureExpression = 180;

        public virtual Match ClosureExpression(int start)
        {
            if (!Caches[Cache_ClosureExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_21_/*'{'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, ClosureSignature(next));
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Statement(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_22_/*'}'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ClosureExpression", start, match);
                }
                Caches[Cache_ClosureExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureSignature = 181;

        public virtual Match ClosureSignature(int start)
        {
            if (!Caches[Cache_ClosureSignature].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next, CaptureList(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = ClosureParameterClause(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Lit_throws(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, FunctionResult(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_in(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = CaptureList(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_in(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ClosureSignature", start, match);
                }
                Caches[Cache_ClosureSignature].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureParameterClause = 182;

        public virtual Match ClosureParameterClause(int start)
        {
            if (!Caches[Cache_ClosureParameterClause].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = ClosureParameterList(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = IdentifierList(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ClosureParameterClause", start, match);
                }
                Caches[Cache_ClosureParameterClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureParameterList = 183;

        public virtual Match ClosureParameterList(int start)
        {
            if (!Caches[Cache_ClosureParameterList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = ClosureParameter(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = ClosureParameter(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ClosureParameterList", start, match);
                }
                Caches[Cache_ClosureParameterList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureParameter = 184;

        public virtual Match ClosureParameter(int start)
        {
            if (!Caches[Cache_ClosureParameter].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = ClosureParameterName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = TypeAnnotation(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_dot_dot_dot_(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = ClosureParameterName(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = TypeAnnotation(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = ClosureParameterName(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ClosureParameter", start, match);
                }
                Caches[Cache_ClosureParameter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureParameterName = 185;

        public virtual Match ClosureParameterName(int start)
        {
            if (!Caches[Cache_ClosureParameterName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("ClosureParameterName", start, match);
                }
                Caches[Cache_ClosureParameterName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IdentifierList = 186;

        public virtual Match IdentifierList(int start)
        {
            if (!Caches[Cache_IdentifierList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Name(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Name(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("IdentifierList", start, match);
                }
                Caches[Cache_IdentifierList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaptureList = 187;

        public virtual Match CaptureList(int start)
        {
            if (!Caches[Cache_CaptureList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_29_/*'['*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CaptureListItems(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_30_/*']'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("CaptureList", start, match);
                }
                Caches[Cache_CaptureList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaptureListItems = 188;

        public virtual Match CaptureListItems(int start)
        {
            if (!Caches[Cache_CaptureListItems].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = CaptureListItem(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = CaptureListItem(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("CaptureListItems", start, match);
                }
                Caches[Cache_CaptureListItems].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaptureListItem = 189;

        public virtual Match CaptureListItem(int start)
        {
            if (!Caches[Cache_CaptureListItem].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, CaptureSpecifier(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("CaptureListItem", start, match);
                }
                Caches[Cache_CaptureListItem].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaptureSpecifier = 190;

        public virtual Match CaptureSpecifier(int start)
        {
            if (!Caches[Cache_CaptureSpecifier].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_weak(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_unowned(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var next2 = next;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_1_/*'('*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            while (true) // ---Choice---
                            {
                                if ((match = Lit_safe(next2)) != null)
                                {
                                    break;
                                }
                                match = Lit_unsafe(next2);
                                break;
                            }
                            if (match == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Lit_2_/*')'*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next, matches2);
                        }
                        match = Match.Optional(next, match);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("CaptureSpecifier", start, match);
                }
                Caches[Cache_CaptureSpecifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ParenthesizedExpression = 191;

        public virtual Match ParenthesizedExpression(int start)
        {
            if (!Caches[Cache_ParenthesizedExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_1_/*'('*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ParenthesizedExpression", start, match);
                }
                Caches[Cache_ParenthesizedExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleExpression = 192;

        public virtual Match TupleExpression(int start)
        {
            if (!Caches[Cache_TupleExpression].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = TupleElementList(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("TupleExpression", start, match);
                }
                Caches[Cache_TupleExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleElementList = 193;

        public virtual Match TupleElementList(int start)
        {
            if (!Caches[Cache_TupleElementList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = TupleElement(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var oomMatches = new List<Match>();
                    var oomNext = next;
                    while (true)
                    {
                        var next2 = oomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = TupleElement(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", oomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        oomMatches.Add(match);
                        oomNext = match.Next;
                    }
                    if (oomMatches.Count > 0)
                    {
                        match = Match.Success("+", next, oomMatches);
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("TupleElementList", start, match);
                }
                Caches[Cache_TupleElementList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleElement = 194;

        public virtual Match TupleElement(int start)
        {
            if (!Caches[Cache_TupleElement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_24_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("TupleElement", start, match);
                }
                Caches[Cache_TupleElement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ImplicitMemberExpression = 195;

        public virtual Match ImplicitMemberExpression(int start)
        {
            if (!Caches[Cache_ImplicitMemberExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_dot_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Name(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ImplicitMemberExpression", start, match);
                }
                Caches[Cache_ImplicitMemberExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WildcardExpression = 196;

        public virtual Match WildcardExpression(int start)
        {
            if (!Caches[Cache_WildcardExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_42_/*'_'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("WildcardExpression", start, match);
                }
                Caches[Cache_WildcardExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathExpression = 197;

        public virtual Match KeyPathExpression(int start)
        {
            if (!Caches[Cache_KeyPathExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_43_/*'\\'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Type(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_dot_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = KeyPathComponents(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("KeyPathExpression", start, match);
                }
                Caches[Cache_KeyPathExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathComponents = 198;

        public virtual Match KeyPathComponents(int start)
        {
            if (!Caches[Cache_KeyPathComponents].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = KeyPathComponent(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_dot_(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = KeyPathComponent(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("KeyPathComponents", start, match);
                }
                Caches[Cache_KeyPathComponents].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathComponent = 199;

        public virtual Match KeyPathComponent(int start)
        {
            if (!Caches[Cache_KeyPathComponent].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Name(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = KeyPathPostfixes(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = KeyPathPostfixes(start)) != null)
                    {
                        break;
                    }
                    match = Name(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("KeyPathComponent", start, match);
                }
                Caches[Cache_KeyPathComponent].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathPostfixes = 200;

        public virtual Match KeyPathPostfixes(int start)
        {
            if (!Caches[Cache_KeyPathPostfixes].Already(start, out var match))
            {
                var oomMatches = new List<Match>();
                var oomNext = start;
                while (true)
                {
                    if ((match = KeyPathPostfix(oomNext)) == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", start, oomMatches);
                }
                if (match != null)
                {
                    match = Match.Success("KeyPathPostfixes", start, match);
                }
                Caches[Cache_KeyPathPostfixes].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathPostfix = 201;

        public virtual Match KeyPathPostfix(int start)
        {
            if (!Caches[Cache_KeyPathPostfix].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_26_/*'?'*/(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_8_/*'!'*/(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_self(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_29_/*'['*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = FunctionCallArgumentList(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_30_/*']'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("KeyPathPostfix", start, match);
                }
                Caches[Cache_KeyPathPostfix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SelectorExpression = 202;

        public virtual Match SelectorExpression(int start)
        {
            if (!Caches[Cache_SelectorExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_44_/*'#selector'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_1_/*'('*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    while (true) // ---Choice---
                    {
                        if ((match = Lit_45_/*'getter:'*/(next)) != null)
                        {
                            break;
                        }
                        match = Lit_46_/*'setter:'*/(next);
                        break;
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SelectorExpression", start, match);
                }
                Caches[Cache_SelectorExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathStringExpression = 203;

        public virtual Match KeyPathStringExpression(int start)
        {
            if (!Caches[Cache_KeyPathStringExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_47_/*'#keyPath'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_1_/*'('*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("KeyPathStringExpression", start, match);
                }
                Caches[Cache_KeyPathStringExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TryOperator = 204;

        public virtual Match TryOperator(int start)
        {
            if (!Caches[Cache_TryOperator].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_try(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    while (true) // ---Choice---
                    {
                        if ((match = Lit_26_/*'?'*/(next)) != null)
                        {
                            break;
                        }
                        match = Lit_8_/*'!'*/(next);
                        break;
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("TryOperator", start, match);
                }
                Caches[Cache_TryOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AssignmentOperator = 205;

        public virtual Match AssignmentOperator(int start)
        {
            if (!Caches[Cache_AssignmentOperator].Already(start, out var match))
            {
                match = Lit_20_/*'='*/(start);
                if (match != null)
                {
                    match = Match.Success("AssignmentOperator", start, match);
                }
                Caches[Cache_AssignmentOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeCastingOperator = 206;

        public virtual Match TypeCastingOperator(int start)
        {
            if (!Caches[Cache_TypeCastingOperator].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_is(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Not_(next, More(next))) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Type(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_as(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Not_(next2, More(next2))) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        while (true) // ---Choice---
                        {
                            if ((match = Lit_26_/*'?'*/(next2)) != null)
                            {
                                break;
                            }
                            match = Lit_8_/*'!'*/(next2);
                            break;
                        }
                        match = Match.Optional(next2, match);
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Type(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        while (true) // ---Choice---
                        {
                            if ((match = Lit_is(next3)) != null)
                            {
                                break;
                            }
                            match = Lit_as(next3);
                            break;
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Not_(next3, More(next3))) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("type-casting-operator", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("TypeCastingOperator", start, match);
                }
                Caches[Cache_TypeCastingOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConditionalOperator = 207;

        public virtual Match ConditionalOperator(int start)
        {
            if (!Caches[Cache_ConditionalOperator].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    match = null;
                    if (next - 1 >= 0)
                    {
                        match = SpaceBefore(next - 1);
                        if (match != null)
                        {
                            match = Match.Success("<", next);
                        }
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_26_/*'?'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = And_(next, SpaceAfter(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ConditionalOperator", start, match);
                }
                Caches[Cache_ConditionalOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BinaryOperator = 208;

        public virtual Match BinaryOperator(int start)
        {
            if (!Caches[Cache_BinaryOperator].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = _(next);
                        matches.Add(match);
                        next = match.Next;
                        match = null;
                        if (next - 1 >= 0)
                        {
                            match = SpaceBefore(next - 1);
                            if (match != null)
                            {
                                match = Match.Success("<", next);
                            }
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Operator(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = And_(next, SpaceAfter(next))) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Operator(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Not_(next2, SpaceAfter(next2))) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("BinaryOperator", start, match);
                }
                Caches[Cache_BinaryOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrefixOperator = 209;

        public virtual Match PrefixOperator(int start)
        {
            if (!Caches[Cache_PrefixOperator].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    match = null;
                    if (next - 1 >= 0)
                    {
                        match = SpaceBefore(next - 1);
                        if (match != null)
                        {
                            match = Match.Success("<", next);
                        }
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Operator(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, SpaceAfter(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PrefixOperator", start, match);
                }
                Caches[Cache_PrefixOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PostfixOperator = 210;

        public virtual Match PostfixOperator(int start)
        {
            if (!Caches[Cache_PostfixOperator].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Operator(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = And_(next, SpaceAfter(next))) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Operator(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = And_(next2, CharacterExact_(next2, '.'))) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PostfixOperator", start, match);
                }
                Caches[Cache_PostfixOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorName = 211;

        public virtual Match OperatorName(int start)
        {
            if (!Caches[Cache_OperatorName].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Operator(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("OperatorName", start, match);
                }
                Caches[Cache_OperatorName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Operator = 212;

        public virtual Match Operator(int start)
        {
            if (!Caches[Cache_Operator].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = OperatorHead(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        while (true)
                        {
                            if ((match = OperatorCharacter(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next, zomMatches);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = DotOperatorHead(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        var oomMatches = new List<Match>();
                        var oomNext = next2;
                        while (true)
                        {
                            if ((match = DotOperatorCharacter(oomNext)) == null)
                            {
                                break;
                            }
                            oomMatches.Add(match);
                            oomNext = match.Next;
                        }
                        if (oomMatches.Count > 0)
                        {
                            match = Match.Success("+", next2, oomMatches);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("Operator", start, match);
                }
                Caches[Cache_Operator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorHead = 213;

        public virtual Match OperatorHead(int start)
        {
            if (!Caches[Cache_OperatorHead].Already(start, out var match))
            {
                match = CharacterSet_(start, "/=-+!*%<>&|^~?");
                if (match != null)
                {
                    match = Match.Success("OperatorHead", start, match);
                }
                Caches[Cache_OperatorHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorCharacter = 214;

        public virtual Match OperatorCharacter(int start)
        {
            if (!Caches[Cache_OperatorCharacter].Already(start, out var match))
            {
                match = OperatorHead(start);
                if (match != null)
                {
                    match = Match.Success("OperatorCharacter", start, match);
                }
                Caches[Cache_OperatorCharacter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DotOperatorHead = 215;

        public virtual Match DotOperatorHead(int start)
        {
            if (!Caches[Cache_DotOperatorHead].Already(start, out var match))
            {
                match = CharacterExact_(start, '.');
                if (match != null)
                {
                    match = Match.Success("DotOperatorHead", start, match);
                }
                Caches[Cache_DotOperatorHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DotOperatorCharacter = 216;

        public virtual Match DotOperatorCharacter(int start)
        {
            if (!Caches[Cache_DotOperatorCharacter].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = CharacterExact_(start, '.')) != null)
                    {
                        break;
                    }
                    match = OperatorCharacter(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("DotOperatorCharacter", start, match);
                }
                Caches[Cache_DotOperatorCharacter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Puncts = 217;

        public virtual Match Puncts(int start)
        {
            if (!Caches[Cache_Puncts].Already(start, out var match))
            {
                match = CharacterSet_(start, ",;:");
                if (match != null)
                {
                    match = Match.Success("Puncts", start, match);
                }
                Caches[Cache_Puncts].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SpaceBefore = 218;

        public virtual Match SpaceBefore(int start)
        {
            if (!Caches[Cache_SpaceBefore].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = SingleWhitespace(start)) != null)
                    {
                        break;
                    }
                    if ((match = Puncts(start)) != null)
                    {
                        break;
                    }
                    if ((match = CharacterExact_(start, '(')) != null)
                    {
                        break;
                    }
                    if ((match = CharacterExact_(start, '[')) != null)
                    {
                        break;
                    }
                    match = CharacterExact_(start, '{');
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("SpaceBefore", start, match);
                }
                Caches[Cache_SpaceBefore].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SpaceAfter = 219;

        public virtual Match SpaceAfter(int start)
        {
            if (!Caches[Cache_SpaceAfter].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = SingleWhitespace(start)) != null)
                    {
                        break;
                    }
                    if ((match = Puncts(start)) != null)
                    {
                        break;
                    }
                    if ((match = CharacterExact_(start, ')')) != null)
                    {
                        break;
                    }
                    if ((match = CharacterExact_(start, ']')) != null)
                    {
                        break;
                    }
                    match = CharacterExact_(start, '}');
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("SpaceAfter", start, match);
                }
                Caches[Cache_SpaceAfter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericParameterClause = 220;

        public virtual Match GenericParameterClause(int start)
        {
            if (!Caches[Cache_GenericParameterClause].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_12_/*'<'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = GenericParameters(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_48_/*'>'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_12_/*'<'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("generic-parameters", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("GenericParameterClause", start, match);
                }
                Caches[Cache_GenericParameterClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericParameters = 221;

        public virtual Match GenericParameters(int start)
        {
            if (!Caches[Cache_GenericParameters].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = GenericParameter(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = GenericParameter(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("GenericParameters", start, match);
                }
                Caches[Cache_GenericParameters].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericParameter = 222;

        public virtual Match GenericParameter(int start)
        {
            if (!Caches[Cache_GenericParameter].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = TypeName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*':'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = ProtocolCompositionType(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = TypeName(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_24_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = TypeIdentifier(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = TypeName(start)) != null)
                    {
                        break;
                    }
                    {
                        // >>> ERROR
                        new Error(Context).Report("generic-parameter", start);
                        throw new BailOutException();
                        // <<< ERROR
                    }
                }
                if (match != null)
                {
                    match = Match.Success("GenericParameter", start, match);
                }
                Caches[Cache_GenericParameter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericWhereClause = 223;

        public virtual Match GenericWhereClause(int start)
        {
            if (!Caches[Cache_GenericWhereClause].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_where(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = RequirementList(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_where(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("where-clause", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = And_(next3, Lit_where(next3))) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("where-clause", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("GenericWhereClause", start, match);
                }
                Caches[Cache_GenericWhereClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RequirementList = 224;

        public virtual Match RequirementList(int start)
        {
            if (!Caches[Cache_RequirementList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Requirement(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Requirement(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("RequirementList", start, match);
                }
                Caches[Cache_RequirementList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Requirement = 225;

        public virtual Match Requirement(int start)
        {
            if (!Caches[Cache_Requirement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = TypeIdentifier(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*':'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = ProtocolCompositionType(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = TypeIdentifier(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_24_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = TypeIdentifier(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = TypeIdentifier(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_49_/*'=='*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Type(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("Requirement", start, match);
                }
                Caches[Cache_Requirement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericArgumentClause = 226;

        public virtual Match GenericArgumentClause(int start)
        {
            if (!Caches[Cache_GenericArgumentClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_12_/*'<'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = GenericArguments(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_48_/*'>'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("GenericArgumentClause", start, match);
                }
                Caches[Cache_GenericArgumentClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericArguments = 227;

        public virtual Match GenericArguments(int start)
        {
            if (!Caches[Cache_GenericArguments].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = GenericArgument(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = GenericArgument(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("GenericArguments", start, match);
                }
                Caches[Cache_GenericArguments].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericArgument = 228;

        public virtual Match GenericArgument(int start)
        {
            if (!Caches[Cache_GenericArgument].Already(start, out var match))
            {
                match = Type(start);
                if (match != null)
                {
                    match = Match.Success("GenericArgument", start, match);
                }
                Caches[Cache_GenericArgument].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StringLiteral = 229;

        public virtual Match StringLiteral(int start)
        {
            if (!Caches[Cache_StringLiteral].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = InterpolatedStringLiteral(start)) != null)
                    {
                        break;
                    }
                    match = StaticStringLiteral(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("StringLiteral", start, match);
                }
                Caches[Cache_StringLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StaticStringLiteral = 230;

        public virtual Match StaticStringLiteral(int start)
        {
            if (!Caches[Cache_StaticStringLiteral].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = MultilineStringLiteralOpeningDelimiter(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, MultilineQuotedText(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = MultilineStringLiteralClosingDelimiter(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = StringLiteralOpeningDelimiter(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, QuotedText(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = StringLiteralClosingDelimiter(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("StaticStringLiteral", start, match);
                }
                Caches[Cache_StaticStringLiteral].Cache(start, match);
            }
            return match;
        }

        public virtual Match InterpolatedStringLiteral(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = MultilineStringLiteralOpeningDelimiter(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, MultilineInterpolatedText(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = MultilineStringLiteralClosingDelimiter(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    break;
                }
                var next2 = start;
                var matches2 = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = StringLiteralOpeningDelimiter(next2)) == null)
                    {
                        break;
                    }
                    matches2.Add(match);
                    next2 = match.Next;
                    match = Match.Optional(next2, InterpolatedText(next2));
                    matches2.Add(match);
                    next2 = match.Next;
                    if ((match = StringLiteralClosingDelimiter(next2)) == null)
                    {
                        break;
                    }
                    matches2.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches2);
                }
                break;
            }
            if (match != null)
            {
                match = Match.Success("InterpolatedStringLiteral", start, match);
            }
            return match;
        }

        public virtual Match QuotedText(int start)
        {
            Match match;
            var oomMatches = new List<Match>();
            var oomNext = start;
            while (true)
            {
                if ((match = QuotedTextItem(oomNext)) == null)
                {
                    break;
                }
                oomMatches.Add(match);
                oomNext = match.Next;
            }
            if (oomMatches.Count > 0)
            {
                match = Match.Success("+", start, oomMatches);
            }
            if (match != null)
            {
                match = Match.Success("QuotedText", start, match);
            }
            return match;
        }

        public virtual Match QuotedTextItem(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                if ((match = EscapedCharacter(start)) != null)
                {
                    break;
                }
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    while (true) // ---Choice---
                    {
                        if ((match = CharacterExact_(next, '\"')) != null)
                        {
                            break;
                        }
                        if ((match = CharacterExact_(next, '\\')) != null)
                        {
                            break;
                        }
                        match = LineBreakCharacter(next);
                        break;
                    }
                    match = Not_(next, match);
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterAny_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                break;
            }
            if (match != null)
            {
                match = Match.Success("QuotedTextItem", start, match);
            }
            return match;
        }

        public virtual Match MultilineQuotedText(int start)
        {
            Match match;
            var oomMatches = new List<Match>();
            var oomNext = start;
            while (true)
            {
                if ((match = MultilineQuotedTextItem(oomNext)) == null)
                {
                    break;
                }
                oomMatches.Add(match);
                oomNext = match.Next;
            }
            if (oomMatches.Count > 0)
            {
                match = Match.Success("+", start, oomMatches);
            }
            if (match != null)
            {
                match = Match.Success("MultilineQuotedText", start, match);
            }
            return match;
        }

        public virtual Match MultilineQuotedTextItem(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                if ((match = EscapedCharacter(start)) != null)
                {
                    break;
                }
                if ((match = EscapedNewline(start)) != null)
                {
                    break;
                }
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    while (true) // ---Choice---
                    {
                        if ((match = CharacterExact_(next, '\\')) != null)
                        {
                            break;
                        }
                        match = MultilineStringLiteralClosingDelimiter(next);
                        break;
                    }
                    match = Not_(next, match);
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterAny_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                break;
            }
            if (match != null)
            {
                match = Match.Success("MultilineQuotedTextItem", start, match);
            }
            return match;
        }

        public virtual Match InterpolatedText(int start)
        {
            Match match;
            var oomMatches = new List<Match>();
            var oomNext = start;
            while (true)
            {
                if ((match = InterpolatedTextItem(oomNext)) == null)
                {
                    break;
                }
                oomMatches.Add(match);
                oomNext = match.Next;
            }
            if (oomMatches.Count > 0)
            {
                match = Match.Success("+", start, oomMatches);
            }
            if (match != null)
            {
                match = Match.Success("InterpolatedText", start, match);
            }
            return match;
        }

        public virtual Match InterpolatedTextItem(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = CharacterExact_(next, '\\')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '(')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    break;
                }
                match = QuotedTextItem(start);
                break;
            }
            if (match != null)
            {
                match = Match.Success("InterpolatedTextItem", start, match);
            }
            return match;
        }

        public virtual Match MultilineInterpolatedText(int start)
        {
            Match match;
            var oomMatches = new List<Match>();
            var oomNext = start;
            while (true)
            {
                if ((match = MultilineInterpolatedTextItem(oomNext)) == null)
                {
                    break;
                }
                oomMatches.Add(match);
                oomNext = match.Next;
            }
            if (oomMatches.Count > 0)
            {
                match = Match.Success("+", start, oomMatches);
            }
            if (match != null)
            {
                match = Match.Success("MultilineInterpolatedText", start, match);
            }
            return match;
        }

        public virtual Match MultilineInterpolatedTextItem(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = CharacterExact_(next, '\\')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '(')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    break;
                }
                match = MultilineQuotedTextItem(start);
                break;
            }
            if (match != null)
            {
                match = Match.Success("MultilineInterpolatedTextItem", start, match);
            }
            return match;
        }

        public virtual Match EscapedCharacter(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = EscapeSequence(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '0')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    break;
                }
                var next2 = start;
                var matches2 = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = EscapeSequence(next2)) == null)
                    {
                        break;
                    }
                    matches2.Add(match);
                    next2 = match.Next;
                    if ((match = CharacterExact_(next2, '\\')) == null)
                    {
                        break;
                    }
                    matches2.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches2);
                }
                if (match != null)
                {
                    break;
                }
                var next3 = start;
                var matches3 = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = EscapeSequence(next3)) == null)
                    {
                        break;
                    }
                    matches3.Add(match);
                    next3 = match.Next;
                    if ((match = CharacterExact_(next3, 't')) == null)
                    {
                        break;
                    }
                    matches3.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches3);
                }
                if (match != null)
                {
                    break;
                }
                var next4 = start;
                var matches4 = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = EscapeSequence(next4)) == null)
                    {
                        break;
                    }
                    matches4.Add(match);
                    next4 = match.Next;
                    if ((match = CharacterExact_(next4, 'n')) == null)
                    {
                        break;
                    }
                    matches4.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches4);
                }
                if (match != null)
                {
                    break;
                }
                var next5 = start;
                var matches5 = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = EscapeSequence(next5)) == null)
                    {
                        break;
                    }
                    matches5.Add(match);
                    next5 = match.Next;
                    if ((match = CharacterExact_(next5, 'r')) == null)
                    {
                        break;
                    }
                    matches5.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches5);
                }
                if (match != null)
                {
                    break;
                }
                var next6 = start;
                var matches6 = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = EscapeSequence(next6)) == null)
                    {
                        break;
                    }
                    matches6.Add(match);
                    next6 = match.Next;
                    if ((match = CharacterExact_(next6, '\"')) == null)
                    {
                        break;
                    }
                    matches6.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches6);
                }
                if (match != null)
                {
                    break;
                }
                var next7 = start;
                var matches7 = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = EscapeSequence(next7)) == null)
                    {
                        break;
                    }
                    matches7.Add(match);
                    next7 = match.Next;
                    if ((match = CharacterExact_(next7, '\'')) == null)
                    {
                        break;
                    }
                    matches7.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches7);
                }
                if (match != null)
                {
                    break;
                }
                var next8 = start;
                var matches8 = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = EscapeSequence(next8)) == null)
                    {
                        break;
                    }
                    matches8.Add(match);
                    next8 = match.Next;
                    if ((match = CharacterExact_(next8, 'u')) == null)
                    {
                        break;
                    }
                    matches8.Add(match);
                    next8 = match.Next;
                    if ((match = CharacterExact_(next8, '{')) == null)
                    {
                        break;
                    }
                    matches8.Add(match);
                    next8 = match.Next;
                    if ((match = HexDigit(next8)) == null)
                    {
                        break;
                    }
                    matches8.Add(match);
                    next8 = match.Next;
                    match = Match.Optional(next8, HexDigit(next8));
                    matches8.Add(match);
                    next8 = match.Next;
                    match = Match.Optional(next8, HexDigit(next8));
                    matches8.Add(match);
                    next8 = match.Next;
                    match = Match.Optional(next8, HexDigit(next8));
                    matches8.Add(match);
                    next8 = match.Next;
                    match = Match.Optional(next8, HexDigit(next8));
                    matches8.Add(match);
                    next8 = match.Next;
                    match = Match.Optional(next8, HexDigit(next8));
                    matches8.Add(match);
                    next8 = match.Next;
                    match = Match.Optional(next8, HexDigit(next8));
                    matches8.Add(match);
                    next8 = match.Next;
                    match = Match.Optional(next8, HexDigit(next8));
                    matches8.Add(match);
                    next8 = match.Next;
                    if ((match = CharacterExact_(next8, '}')) == null)
                    {
                        break;
                    }
                    matches8.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches8);
                }
                break;
            }
            if (match != null)
            {
                match = Match.Success("EscapedCharacter", start, match);
            }
            return match;
        }

        public virtual Match EscapeSequence(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '\\')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                match = Match.Optional(next, ExtendedStringLiteralDelimiter(next));
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("EscapeSequence", start, match);
            }
            return match;
        }

        public virtual Match EscapedNewline(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = EscapeSequence(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                match = Match.Optional(next, InlineSpaces(next));
                matches.Add(match);
                next = match.Next;
                if ((match = LineBreak(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("EscapedNewline", start, match);
            }
            return match;
        }

        public virtual Match StringLiteralOpeningDelimiter(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                match = _(next);
                matches.Add(match);
                next = match.Next;
                match = Match.Optional(next, ExtendedStringLiteralDelimiter(next));
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, '\"')) == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("StringLiteralOpeningDelimiter", start, match);
            }
            return match;
        }

        public virtual Match StringLiteralClosingDelimiter(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '\"')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                match = Match.Optional(next, ExtendedStringLiteralDelimiter(next));
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("StringLiteralClosingDelimiter", start, match);
            }
            return match;
        }

        public virtual Match MultilineStringLiteralOpeningDelimiter(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                match = _(next);
                matches.Add(match);
                next = match.Next;
                match = Match.Optional(next, ExtendedStringLiteralDelimiter(next));
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, '\"')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, '\"')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, '\"')) == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("MultilineStringLiteralOpeningDelimiter", start, match);
            }
            return match;
        }

        public virtual Match MultilineStringLiteralClosingDelimiter(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '\"')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, '\"')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, '\"')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                match = Match.Optional(next, ExtendedStringLiteralDelimiter(next));
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("MultilineStringLiteralClosingDelimiter", start, match);
            }
            return match;
        }

        public virtual Match ExtendedStringLiteralDelimiter(int start)
        {
            Match match;
            var oomMatches = new List<Match>();
            var oomNext = start;
            while (true)
            {
                if ((match = CharacterExact_(oomNext, '#')) == null)
                {
                    break;
                }
                oomMatches.Add(match);
                oomNext = match.Next;
            }
            if (oomMatches.Count > 0)
            {
                match = Match.Success("+", start, oomMatches);
            }
            if (match != null)
            {
                match = Match.Success("ExtendedStringLiteralDelimiter", start, match);
            }
            return match;
        }

        protected const int Cache_Name = 248;

        public virtual Match Name(int start)
        {
            if (!Caches[Cache_Name].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Identifier(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Name", start, match);
                }
                Caches[Cache_Name].Cache(start, match);
            }
            return match;
        }

        public virtual Match Identifier(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    while (true) // ---Choice---
                    {
                        if ((match = CharacterRange_(next, 'a', 'z')) != null)
                        {
                            break;
                        }
                        if ((match = CharacterRange_(next, 'A', 'Z')) != null)
                        {
                            break;
                        }
                        match = CharacterExact_(next, '_');
                        break;
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        while (true) // ---Choice---
                        {
                            while (true) // ---Choice---
                            {
                                if ((match = CharacterRange_(zomNext, 'a', 'z')) != null)
                                {
                                    break;
                                }
                                if ((match = CharacterRange_(zomNext, 'A', 'Z')) != null)
                                {
                                    break;
                                }
                                match = CharacterExact_(zomNext, '_');
                                break;
                            }
                            if (match != null)
                            {
                                break;
                            }
                            match = CharacterRange_(zomNext, '0', '9');
                            break;
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    break;
                }
                var next2 = start;
                var matches2 = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = CharacterExact_(next2, '`')) == null)
                    {
                        break;
                    }
                    matches2.Add(match);
                    next2 = match.Next;
                    while (true) // ---Choice---
                    {
                        if ((match = CharacterRange_(next2, 'a', 'z')) != null)
                        {
                            break;
                        }
                        if ((match = CharacterRange_(next2, 'A', 'Z')) != null)
                        {
                            break;
                        }
                        match = CharacterExact_(next2, '_');
                        break;
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches2.Add(match);
                    next2 = match.Next;
                    var zomMatches2 = new List<Match>();
                    var zomNext2 = next2;
                    while (true)
                    {
                        while (true) // ---Choice---
                        {
                            while (true) // ---Choice---
                            {
                                if ((match = CharacterRange_(zomNext2, 'a', 'z')) != null)
                                {
                                    break;
                                }
                                if ((match = CharacterRange_(zomNext2, 'A', 'Z')) != null)
                                {
                                    break;
                                }
                                match = CharacterExact_(zomNext2, '_');
                                break;
                            }
                            if (match != null)
                            {
                                break;
                            }
                            match = CharacterRange_(zomNext2, '0', '9');
                            break;
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches2.Add(match);
                        zomNext2 = match.Next;
                    }
                    match = Match.Success("*", next2, zomMatches2);
                    matches2.Add(match);
                    next2 = match.Next;
                    if ((match = CharacterExact_(next2, '`')) == null)
                    {
                        break;
                    }
                    matches2.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches2);
                }
                if (match != null)
                {
                    break;
                }
                var next3 = start;
                var matches3 = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = CharacterExact_(next3, '$')) == null)
                    {
                        break;
                    }
                    matches3.Add(match);
                    next3 = match.Next;
                    if ((match = DecimalDigits(next3)) == null)
                    {
                        break;
                    }
                    matches3.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches3);
                }
                if (match != null)
                {
                    break;
                }
                var next4 = start;
                var matches4 = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = CharacterExact_(next4, '$')) == null)
                    {
                        break;
                    }
                    matches4.Add(match);
                    next4 = match.Next;
                    var oomMatches = new List<Match>();
                    var oomNext = next4;
                    while (true)
                    {
                        while (true) // ---Choice---
                        {
                            while (true) // ---Choice---
                            {
                                if ((match = CharacterRange_(oomNext, 'a', 'z')) != null)
                                {
                                    break;
                                }
                                if ((match = CharacterRange_(oomNext, 'A', 'Z')) != null)
                                {
                                    break;
                                }
                                match = CharacterExact_(oomNext, '_');
                                break;
                            }
                            if (match != null)
                            {
                                break;
                            }
                            match = CharacterRange_(oomNext, '0', '9');
                            break;
                        }
                        if (match == null)
                        {
                            break;
                        }
                        oomMatches.Add(match);
                        oomNext = match.Next;
                    }
                    if (oomMatches.Count > 0)
                    {
                        match = Match.Success("+", next4, oomMatches);
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches4.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches4);
                }
                break;
            }
            if (match != null)
            {
                match = Match.Success("Identifier", start, match);
            }
            return match;
        }

        public virtual Match IdentifierHead(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                if ((match = CharacterRange_(start, 'a', 'z')) != null)
                {
                    break;
                }
                if ((match = CharacterRange_(start, 'A', 'Z')) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            if (match != null)
            {
                match = Match.Success("IdentifierHead", start, match);
            }
            return match;
        }

        public virtual Match IdentifierCharacter(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                while (true) // ---Choice---
                {
                    if ((match = CharacterRange_(start, 'a', 'z')) != null)
                    {
                        break;
                    }
                    if ((match = CharacterRange_(start, 'A', 'Z')) != null)
                    {
                        break;
                    }
                    match = CharacterExact_(start, '_');
                    break;
                }
                if (match != null)
                {
                    break;
                }
                match = CharacterRange_(start, '0', '9');
                break;
            }
            if (match != null)
            {
                match = Match.Success("IdentifierCharacter", start, match);
            }
            return match;
        }

        public virtual Match ImplicitParameterName(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '$')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = DecimalDigits(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("ImplicitParameterName", start, match);
            }
            return match;
        }

        public virtual Match PropertyWrapperProjection(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '$')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                var oomMatches = new List<Match>();
                var oomNext = next;
                while (true)
                {
                    while (true) // ---Choice---
                    {
                        while (true) // ---Choice---
                        {
                            if ((match = CharacterRange_(oomNext, 'a', 'z')) != null)
                            {
                                break;
                            }
                            if ((match = CharacterRange_(oomNext, 'A', 'Z')) != null)
                            {
                                break;
                            }
                            match = CharacterExact_(oomNext, '_');
                            break;
                        }
                        if (match != null)
                        {
                            break;
                        }
                        match = CharacterRange_(oomNext, '0', '9');
                        break;
                    }
                    if (match == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", next, oomMatches);
                }
                if (match == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("PropertyWrapperProjection", start, match);
            }
            return match;
        }

        protected const int Cache_More = 254;

        public virtual Match More(int start)
        {
            if (!Caches[Cache_More].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    while (true) // ---Choice---
                    {
                        if ((match = CharacterRange_(start, 'a', 'z')) != null)
                        {
                            break;
                        }
                        if ((match = CharacterRange_(start, 'A', 'Z')) != null)
                        {
                            break;
                        }
                        match = CharacterExact_(start, '_');
                        break;
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = CharacterRange_(start, '0', '9');
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("More", start, match);
                }
                Caches[Cache_More].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IntegerLiteral = 255;

        public virtual Match IntegerLiteral(int start)
        {
            if (!Caches[Cache_IntegerLiteral].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = BinaryLiteral(start)) != null)
                    {
                        break;
                    }
                    if ((match = OctalLiteral(start)) != null)
                    {
                        break;
                    }
                    if ((match = HexadecimalLiteral(start)) != null)
                    {
                        break;
                    }
                    match = DecimalLiteral(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("IntegerLiteral", start, match);
                }
                Caches[Cache_IntegerLiteral].Cache(start, match);
            }
            return match;
        }

        public virtual Match BinaryLiteral(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '0')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, 'b')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = BinaryDigit(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                var zomMatches = new List<Match>();
                var zomNext = next;
                while (true)
                {
                    if ((match = BinaryLiteralCharacter(zomNext)) == null)
                    {
                        break;
                    }
                    zomMatches.Add(match);
                    zomNext = match.Next;
                }
                match = Match.Success("*", next, zomMatches);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("BinaryLiteral", start, match);
            }
            return match;
        }

        public virtual Match BinaryDigit(int start)
        {
            Match match;
            match = CharacterSet_(start, "01");
            if (match != null)
            {
                match = Match.Success("BinaryDigit", start, match);
            }
            return match;
        }

        public virtual Match BinaryLiteralCharacter(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                if ((match = BinaryDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            if (match != null)
            {
                match = Match.Success("BinaryLiteralCharacter", start, match);
            }
            return match;
        }

        public virtual Match OctalLiteral(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '0')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, 'o')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = OctalDigit(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                var zomMatches = new List<Match>();
                var zomNext = next;
                while (true)
                {
                    if ((match = OctalLiteralCharacter(zomNext)) == null)
                    {
                        break;
                    }
                    zomMatches.Add(match);
                    zomNext = match.Next;
                }
                match = Match.Success("*", next, zomMatches);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("OctalLiteral", start, match);
            }
            return match;
        }

        public virtual Match OctalDigit(int start)
        {
            Match match;
            match = CharacterRange_(start, '0', '7');
            if (match != null)
            {
                match = Match.Success("OctalDigit", start, match);
            }
            return match;
        }

        public virtual Match OctalLiteralCharacter(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                if ((match = OctalDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            if (match != null)
            {
                match = Match.Success("OctalLiteralCharacter", start, match);
            }
            return match;
        }

        public virtual Match DecimalLiteral(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = DecimalDigit(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                var zomMatches = new List<Match>();
                var zomNext = next;
                while (true)
                {
                    if ((match = DecimalLiteralCharacter(zomNext)) == null)
                    {
                        break;
                    }
                    zomMatches.Add(match);
                    zomNext = match.Next;
                }
                match = Match.Success("*", next, zomMatches);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("DecimalLiteral", start, match);
            }
            return match;
        }

        protected const int Cache_NonzeroDecimalLiteral = 263;

        public virtual Match NonzeroDecimalLiteral(int start)
        {
            if (!Caches[Cache_NonzeroDecimalLiteral].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = CharacterExact_(zomNext, '0')) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = DecimalDigitExcept0(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches2 = new List<Match>();
                    var zomNext2 = next;
                    while (true)
                    {
                        if ((match = DecimalDigit(zomNext2)) == null)
                        {
                            break;
                        }
                        zomMatches2.Add(match);
                        zomNext2 = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches2);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("NonzeroDecimalLiteral", start, match);
                }
                Caches[Cache_NonzeroDecimalLiteral].Cache(start, match);
            }
            return match;
        }

        public virtual Match DecimalDigit(int start)
        {
            Match match;
            match = CharacterRange_(start, '0', '9');
            if (match != null)
            {
                match = Match.Success("DecimalDigit", start, match);
            }
            return match;
        }

        public virtual Match DecimalDigitExcept0(int start)
        {
            Match match;
            match = CharacterRange_(start, '1', '9');
            if (match != null)
            {
                match = Match.Success("DecimalDigitExcept0", start, match);
            }
            return match;
        }

        public virtual Match DecimalLiteralCharacter(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                if ((match = DecimalDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            if (match != null)
            {
                match = Match.Success("DecimalLiteralCharacter", start, match);
            }
            return match;
        }

        public virtual Match HexadecimalLiteral(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '0')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, 'x')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = HexadecimalDigit(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                var zomMatches = new List<Match>();
                var zomNext = next;
                while (true)
                {
                    if ((match = HexadecimalLiteralCharacter(zomNext)) == null)
                    {
                        break;
                    }
                    zomMatches.Add(match);
                    zomNext = match.Next;
                }
                match = Match.Success("*", next, zomMatches);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("HexadecimalLiteral", start, match);
            }
            return match;
        }

        public virtual Match HexadecimalDigit(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                if ((match = CharacterRange_(start, '0', '9')) != null)
                {
                    break;
                }
                if ((match = CharacterRange_(start, 'a', 'f')) != null)
                {
                    break;
                }
                match = CharacterRange_(start, 'A', 'F');
                break;
            }
            if (match != null)
            {
                match = Match.Success("HexadecimalDigit", start, match);
            }
            return match;
        }

        public virtual Match HexDigit(int start)
        {
            Match match;
            match = HexadecimalDigit(start);
            if (match != null)
            {
                match = Match.Success("HexDigit", start, match);
            }
            return match;
        }

        public virtual Match HexadecimalLiteralCharacter(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                if ((match = HexadecimalDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            if (match != null)
            {
                match = Match.Success("HexadecimalLiteralCharacter", start, match);
            }
            return match;
        }

        protected const int Cache_FloatingPointLiteral = 271;

        public virtual Match FloatingPointLiteral(int start)
        {
            if (!Caches[Cache_FloatingPointLiteral].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = HexadecimalLiteral(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, HexadecimalFraction(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, HexadecimalExponent(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = DecimalLiteral(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, DecimalFraction(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, DecimalExponent(next2));
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("FloatingPointLiteral", start, match);
                }
                Caches[Cache_FloatingPointLiteral].Cache(start, match);
            }
            return match;
        }

        public virtual Match DecimalFraction(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '.')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = DecimalLiteral(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("DecimalFraction", start, match);
            }
            return match;
        }

        public virtual Match DecimalExponent(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = FloatingPointE(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                match = Match.Optional(next, Sign(next));
                matches.Add(match);
                next = match.Next;
                if ((match = DecimalLiteral(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("DecimalExponent", start, match);
            }
            return match;
        }

        public virtual Match FloatingPointE(int start)
        {
            Match match;
            match = CharacterSet_(start, "eE");
            if (match != null)
            {
                match = Match.Success("FloatingPointE", start, match);
            }
            return match;
        }

        public virtual Match HexadecimalFraction(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '.')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = HexadecimalDigit(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                var zomMatches = new List<Match>();
                var zomNext = next;
                while (true)
                {
                    if ((match = HexadecimalLiteralCharacter(zomNext)) == null)
                    {
                        break;
                    }
                    zomMatches.Add(match);
                    zomNext = match.Next;
                }
                match = Match.Success("*", next, zomMatches);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("HexadecimalFraction", start, match);
            }
            return match;
        }

        public virtual Match HexadecimalExponent(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = FloatingPointP(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                match = Match.Optional(next, Sign(next));
                matches.Add(match);
                next = match.Next;
                if ((match = DecimalLiteral(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("HexadecimalExponent", start, match);
            }
            return match;
        }

        public virtual Match FloatingPointP(int start)
        {
            Match match;
            match = CharacterSet_(start, "pP");
            if (match != null)
            {
                match = Match.Success("FloatingPointP", start, match);
            }
            return match;
        }

        public virtual Match Sign(int start)
        {
            Match match;
            match = CharacterSet_(start, "+-");
            if (match != null)
            {
                match = Match.Success("Sign", start, match);
            }
            return match;
        }

        protected const int Cache_DecimalDigits = 279;

        public virtual Match DecimalDigits(int start)
        {
            if (!Caches[Cache_DecimalDigits].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    var oomMatches = new List<Match>();
                    var oomNext = next;
                    while (true)
                    {
                        if ((match = CharacterRange_(oomNext, '0', '9')) == null)
                        {
                            break;
                        }
                        oomMatches.Add(match);
                        oomNext = match.Next;
                    }
                    if (oomMatches.Count > 0)
                    {
                        match = Match.Success("+", next, oomMatches);
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("DecimalDigits", start, match);
                }
                Caches[Cache_DecimalDigits].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Pattern = 280;

        public virtual Match Pattern(int start)
        {
            if (!Caches[Cache_Pattern].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = PrimaryPattern(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        while (true)
                        {
                            if ((match = PatternPostfix(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next, zomMatches);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = PrimaryPattern(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        while (true) // ---Choice---
                        {
                            if ((match = Lit_26_/*'?'*/(next2)) != null)
                            {
                                break;
                            }
                            if ((match = Lit_dot_(next2)) != null)
                            {
                                break;
                            }
                            match = Lit_as(next2);
                            break;
                        }
                        match = And_(next2, match);
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("pattern - pattern-postfix", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("Pattern", start, match);
                }
                Caches[Cache_Pattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrimaryPattern = 281;

        public virtual Match PrimaryPattern(int start)
        {
            if (!Caches[Cache_PrimaryPattern].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = WildcardPattern(start)) != null)
                    {
                        break;
                    }
                    if ((match = ValueBindingPattern(start)) != null)
                    {
                        break;
                    }
                    if ((match = TuplePattern(start)) != null)
                    {
                        break;
                    }
                    if ((match = EnumCasePattern(start)) != null)
                    {
                        break;
                    }
                    if ((match = IsPattern(start)) != null)
                    {
                        break;
                    }
                    if ((match = TypeIdentifier(start)) != null)
                    {
                        break;
                    }
                    match = Expression(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PrimaryPattern", start, match);
                }
                Caches[Cache_PrimaryPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternPostfix = 282;

        public virtual Match PatternPostfix(int start)
        {
            if (!Caches[Cache_PatternPostfix].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = PatternOptional(start)) != null)
                    {
                        break;
                    }
                    if ((match = PatternCase(start)) != null)
                    {
                        break;
                    }
                    match = PatternAs(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PatternPostfix", start, match);
                }
                Caches[Cache_PatternPostfix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternOptional = 283;

        public virtual Match PatternOptional(int start)
        {
            if (!Caches[Cache_PatternOptional].Already(start, out var match))
            {
                match = Lit_26_/*'?'*/(start);
                if (match != null)
                {
                    match = Match.Success("PatternOptional", start, match);
                }
                Caches[Cache_PatternOptional].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternCase = 284;

        public virtual Match PatternCase(int start)
        {
            if (!Caches[Cache_PatternCase].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_dot_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = EnumCaseName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = TuplePattern(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PatternCase", start, match);
                }
                Caches[Cache_PatternCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternAs = 285;

        public virtual Match PatternAs(int start)
        {
            if (!Caches[Cache_PatternAs].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_as(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Type(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_as(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("pattern-as - type", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PatternAs", start, match);
                }
                Caches[Cache_PatternAs].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WildcardPattern = 286;

        public virtual Match WildcardPattern(int start)
        {
            if (!Caches[Cache_WildcardPattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_42_/*'_'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("WildcardPattern", start, match);
                }
                Caches[Cache_WildcardPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ValueBindingPattern = 287;

        public virtual Match ValueBindingPattern(int start)
        {
            if (!Caches[Cache_ValueBindingPattern].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_var(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Pattern(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_let(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Pattern(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_var(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("value-binding-pattern - pattern", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_let(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("value-binding-pattern - pattern", next4);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches4);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ValueBindingPattern", start, match);
                }
                Caches[Cache_ValueBindingPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TuplePattern = 288;

        public virtual Match TuplePattern(int start)
        {
            if (!Caches[Cache_TuplePattern].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = TuplePatternElementList(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("tuple-pattern", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("TuplePattern", start, match);
                }
                Caches[Cache_TuplePattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TuplePatternElementList = 289;

        public virtual Match TuplePatternElementList(int start)
        {
            if (!Caches[Cache_TuplePatternElementList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = TuplePatternElement(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = TuplePatternElement(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("TuplePatternElementList", start, match);
                }
                Caches[Cache_TuplePatternElementList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TuplePatternElement = 290;

        public virtual Match TuplePatternElement(int start)
        {
            if (!Caches[Cache_TuplePatternElement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_24_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Pattern(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("TuplePatternElement", start, match);
                }
                Caches[Cache_TuplePatternElement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EnumCasePattern = 291;

        public virtual Match EnumCasePattern(int start)
        {
            if (!Caches[Cache_EnumCasePattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, TypeIdentifier(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_dot_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = EnumCaseName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, TuplePattern(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("EnumCasePattern", start, match);
                }
                Caches[Cache_EnumCasePattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IsPattern = 292;

        public virtual Match IsPattern(int start)
        {
            if (!Caches[Cache_IsPattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_is(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Type(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("IsPattern", start, match);
                }
                Caches[Cache_IsPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BreakStatement = 293;

        public virtual Match BreakStatement(int start)
        {
            if (!Caches[Cache_BreakStatement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_break(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var next2 = next;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            while (true) // ---Choice---
                            {
                                if ((match = Lit_case(next2)) != null)
                                {
                                    break;
                                }
                                match = Lit_default(next2);
                                break;
                            }
                            if (match == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Not_(next2, More(next2))) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next, matches2);
                        }
                        match = Not_(next, match);
                        if (match == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, LabelName(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_break(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        var next4 = next3;
                        var matches4 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            while (true) // ---Choice---
                            {
                                if ((match = Lit_case(next4)) != null)
                                {
                                    break;
                                }
                                match = Lit_default(next4);
                                break;
                            }
                            if (match == null)
                            {
                                break;
                            }
                            matches4.Add(match);
                            next4 = match.Next;
                            if ((match = Not_(next4, More(next4))) == null)
                            {
                                break;
                            }
                            matches4.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next3, matches4);
                        }
                        match = Not_(next3, match);
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("break-statement", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("BreakStatement", start, match);
                }
                Caches[Cache_BreakStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConditionList = 294;

        public virtual Match ConditionList(int start)
        {
            if (!Caches[Cache_ConditionList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Condition(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Condition(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ConditionList", start, match);
                }
                Caches[Cache_ConditionList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Condition = 295;

        public virtual Match Condition(int start)
        {
            if (!Caches[Cache_Condition].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = AvailableCondition(start)) != null)
                    {
                        break;
                    }
                    if ((match = CaseCondition(start)) != null)
                    {
                        break;
                    }
                    if ((match = OptionalBindingCondition(start)) != null)
                    {
                        break;
                    }
                    match = Expression(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("Condition", start, match);
                }
                Caches[Cache_Condition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseCondition = 296;

        public virtual Match CaseCondition(int start)
        {
            if (!Caches[Cache_CaseCondition].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_case(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Pattern(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Initializer(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("CaseCondition", start, match);
                }
                Caches[Cache_CaseCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OptionalBindingCondition = 297;

        public virtual Match OptionalBindingCondition(int start)
        {
            if (!Caches[Cache_OptionalBindingCondition].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_let(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Pattern(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, TypeAnnotation(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Initializer(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_var(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Pattern(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, TypeAnnotation(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Initializer(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("OptionalBindingCondition", start, match);
                }
                Caches[Cache_OptionalBindingCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AvailableCondition = 298;

        public virtual Match AvailableCondition(int start)
        {
            if (!Caches[Cache_AvailableCondition].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_50_/*'#available'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_1_/*'('*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = AvailabilityArguments(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("AvailableCondition", start, match);
                }
                Caches[Cache_AvailableCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AvailabilityArguments = 299;

        public virtual Match AvailabilityArguments(int start)
        {
            if (!Caches[Cache_AvailabilityArguments].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = AvailabilityArgument(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = AvailabilityArgument(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("AvailabilityArguments", start, match);
                }
                Caches[Cache_AvailabilityArguments].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AvailabilityArgument = 300;

        public virtual Match AvailabilityArgument(int start)
        {
            if (!Caches[Cache_AvailabilityArgument].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = PlatformName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = PlatformVersion(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = Lit_51_/*'*'*/(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("AvailabilityArgument", start, match);
                }
                Caches[Cache_AvailabilityArgument].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PlatformName = 301;

        public virtual Match PlatformName(int start)
        {
            if (!Caches[Cache_PlatformName].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = Lit_iOS(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_iOSApplicationExtension(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_macOS(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_macOSApplicationExtension(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_macCatalyst(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_macCatalystApplicationExtension(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_watchOS(start)) != null)
                    {
                        break;
                    }
                    match = Lit_tvOS(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PlatformName", start, match);
                }
                Caches[Cache_PlatformName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PlatformVersion = 302;

        public virtual Match PlatformVersion(int start)
        {
            if (!Caches[Cache_PlatformVersion].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = DecimalDigits(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_dot_(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = DecimalDigits(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        var next3 = next2;
                        var matches3 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_dot_(next3)) == null)
                            {
                                break;
                            }
                            matches3.Add(match);
                            next3 = match.Next;
                            if ((match = DecimalDigits(next3)) == null)
                            {
                                break;
                            }
                            matches3.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next2, matches3);
                        }
                        match = Match.Optional(next2, match);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("PlatformVersion", start, match);
                }
                Caches[Cache_PlatformVersion].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ContinueStatement = 303;

        public virtual Match ContinueStatement(int start)
        {
            if (!Caches[Cache_ContinueStatement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_continue(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var next2 = next;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            while (true) // ---Choice---
                            {
                                if ((match = Lit_case(next2)) != null)
                                {
                                    break;
                                }
                                match = Lit_default(next2);
                                break;
                            }
                            if (match == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Not_(next2, More(next2))) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next, matches2);
                        }
                        match = Not_(next, match);
                        if (match == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, LabelName(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_continue(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        var next4 = next3;
                        var matches4 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            while (true) // ---Choice---
                            {
                                if ((match = Lit_case(next4)) != null)
                                {
                                    break;
                                }
                                match = Lit_default(next4);
                                break;
                            }
                            if (match == null)
                            {
                                break;
                            }
                            matches4.Add(match);
                            next4 = match.Next;
                            if ((match = Not_(next4, More(next4))) == null)
                            {
                                break;
                            }
                            matches4.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next3, matches4);
                        }
                        match = Not_(next3, match);
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("continue-statement", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ContinueStatement", start, match);
                }
                Caches[Cache_ContinueStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GuardStatement = 304;

        public virtual Match GuardStatement(int start)
        {
            if (!Caches[Cache_GuardStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_guard(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = ConditionList(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_else(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CodeBlock(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("GuardStatement", start, match);
                }
                Caches[Cache_GuardStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IfStatement = 305;

        public virtual Match IfStatement(int start)
        {
            if (!Caches[Cache_IfStatement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_if(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = ConditionList(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = CodeBlock(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, ElseClause(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_if(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = ConditionList(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("if-statement - code-block", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_if(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("if-statement - condition-list", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("IfStatement", start, match);
                }
                Caches[Cache_IfStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseClause = 306;

        public virtual Match ElseClause(int start)
        {
            if (!Caches[Cache_ElseClause].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_else(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = CodeBlock(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_else(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = IfStatement(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = And_(next3, Lit_else(next3))) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("else-clause", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ElseClause", start, match);
                }
                Caches[Cache_ElseClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ReturnStatement = 307;

        public virtual Match ReturnStatement(int start)
        {
            if (!Caches[Cache_ReturnStatement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_return(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var next2 = next;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            while (true) // ---Choice---
                            {
                                if ((match = Lit_case(next2)) != null)
                                {
                                    break;
                                }
                                match = Lit_default(next2);
                                break;
                            }
                            if (match == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Not_(next2, More(next2))) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next, matches2);
                        }
                        match = Not_(next, match);
                        if (match == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Expression(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_return(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        var next4 = next3;
                        var matches4 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            while (true) // ---Choice---
                            {
                                if ((match = Lit_case(next4)) != null)
                                {
                                    break;
                                }
                                match = Lit_default(next4);
                                break;
                            }
                            if (match == null)
                            {
                                break;
                            }
                            matches4.Add(match);
                            next4 = match.Next;
                            if ((match = Not_(next4, More(next4))) == null)
                            {
                                break;
                            }
                            matches4.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next3, matches4);
                        }
                        match = Not_(next3, match);
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("return-statement", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ReturnStatement", start, match);
                }
                Caches[Cache_ReturnStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchStatement = 308;

        public virtual Match SwitchStatement(int start)
        {
            if (!Caches[Cache_SwitchStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_switch(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = SwitchBody(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SwitchStatement", start, match);
                }
                Caches[Cache_SwitchStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchBody = 309;

        public virtual Match SwitchBody(int start)
        {
            if (!Caches[Cache_SwitchBody].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_22_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = SwitchCases(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_22_/*'}'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("switch-body", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("SwitchBody", start, match);
                }
                Caches[Cache_SwitchBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchCases = 310;

        public virtual Match SwitchCases(int start)
        {
            if (!Caches[Cache_SwitchCases].Already(start, out var match))
            {
                var oomMatches = new List<Match>();
                var oomNext = start;
                while (true)
                {
                    if ((match = SwitchCase(oomNext)) == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", start, oomMatches);
                }
                if (match != null)
                {
                    match = Match.Success("SwitchCases", start, match);
                }
                Caches[Cache_SwitchCases].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchCase = 311;

        public virtual Match SwitchCase(int start)
        {
            if (!Caches[Cache_SwitchCase].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = CaseLabel(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var oomMatches = new List<Match>();
                        var oomNext = next;
                        while (true)
                        {
                            if ((match = Statement(oomNext)) == null)
                            {
                                break;
                            }
                            oomMatches.Add(match);
                            oomNext = match.Next;
                        }
                        if (oomMatches.Count > 0)
                        {
                            match = Match.Success("+", next, oomMatches);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = DefaultLabel(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        var oomMatches2 = new List<Match>();
                        var oomNext2 = next2;
                        while (true)
                        {
                            if ((match = Statement(oomNext2)) == null)
                            {
                                break;
                            }
                            oomMatches2.Add(match);
                            oomNext2 = match.Next;
                        }
                        if (oomMatches2.Count > 0)
                        {
                            match = Match.Success("+", next2, oomMatches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = ConditionalSwitchCase(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("SwitchCase", start, match);
                }
                Caches[Cache_SwitchCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseLabel = 312;

        public virtual Match CaseLabel(int start)
        {
            if (!Caches[Cache_CaseLabel].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next, Attributes(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_case(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = CaseItemList(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*':'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next2, Attributes(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_case(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("case-label", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("CaseLabel", start, match);
                }
                Caches[Cache_CaseLabel].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseItemList = 313;

        public virtual Match CaseItemList(int start)
        {
            if (!Caches[Cache_CaseItemList].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = CaseItem(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        while (true)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            while (true) // ---Sequence---
                            {
                                if ((match = Lit_16_/*','*/(next2)) == null)
                                {
                                    break;
                                }
                                matches2.Add(match);
                                next2 = match.Next;
                                if ((match = CaseItem(next2)) == null)
                                {
                                    break;
                                }
                                matches2.Add(match);
                                break;
                            }
                            if (match != null)
                            {
                                match = Match.Success("_", zomNext, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next, zomMatches);
                        matches.Add(match);
                        next = match.Next;
                        if ((match = And_(next, Lit_24_/*':'*/(next))) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = CaseItem(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        var oomMatches = new List<Match>();
                        var oomNext = next3;
                        while (true)
                        {
                            var next4 = oomNext;
                            var matches4 = new List<Match>();
                            while (true) // ---Sequence---
                            {
                                if ((match = Lit_16_/*','*/(next4)) == null)
                                {
                                    break;
                                }
                                matches4.Add(match);
                                next4 = match.Next;
                                if ((match = CaseItem(next4)) == null)
                                {
                                    break;
                                }
                                matches4.Add(match);
                                break;
                            }
                            if (match != null)
                            {
                                match = Match.Success("_", oomNext, matches4);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            oomMatches.Add(match);
                            oomNext = match.Next;
                        }
                        if (oomMatches.Count > 0)
                        {
                            match = Match.Success("+", next3, oomMatches);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Not_(next3, Lit_24_/*':'*/(next3))) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("case-item-list", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = CaseItem(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("case-item-list: expected `,`", next5);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches5);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("CaseItemList", start, match);
                }
                Caches[Cache_CaseItemList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseItem = 314;

        public virtual Match CaseItem(int start)
        {
            if (!Caches[Cache_CaseItem].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Pattern(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, WhereClause(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("CaseItem", start, match);
                }
                Caches[Cache_CaseItem].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DefaultLabel = 315;

        public virtual Match DefaultLabel(int start)
        {
            if (!Caches[Cache_DefaultLabel].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_default(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("DefaultLabel", start, match);
                }
                Caches[Cache_DefaultLabel].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConditionalSwitchCase = 316;

        public virtual Match ConditionalSwitchCase(int start)
        {
            if (!Caches[Cache_ConditionalSwitchCase].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = SwitchIfDirectiveClause(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, SwitchElseifDirectiveClauses(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, SwitchElseDirectiveClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = EndifDirective(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ConditionalSwitchCase", start, match);
                }
                Caches[Cache_ConditionalSwitchCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchIfDirectiveClause = 317;

        public virtual Match SwitchIfDirectiveClause(int start)
        {
            if (!Caches[Cache_SwitchIfDirectiveClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = IfDirective(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CompilationCondition(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, SwitchCases(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SwitchIfDirectiveClause", start, match);
                }
                Caches[Cache_SwitchIfDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchElseifDirectiveClauses = 318;

        public virtual Match SwitchElseifDirectiveClauses(int start)
        {
            if (!Caches[Cache_SwitchElseifDirectiveClauses].Already(start, out var match))
            {
                var oomMatches = new List<Match>();
                var oomNext = start;
                while (true)
                {
                    if ((match = ElseifDirectiveClause(oomNext)) == null)
                    {
                        break;
                    }
                    oomMatches.Add(match);
                    oomNext = match.Next;
                }
                if (oomMatches.Count > 0)
                {
                    match = Match.Success("+", start, oomMatches);
                }
                if (match != null)
                {
                    match = Match.Success("SwitchElseifDirectiveClauses", start, match);
                }
                Caches[Cache_SwitchElseifDirectiveClauses].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchElseDirectiveClause = 319;

        public virtual Match SwitchElseDirectiveClause(int start)
        {
            if (!Caches[Cache_SwitchElseDirectiveClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = ElseDirective(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, SwitchCases(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SwitchElseDirectiveClause", start, match);
                }
                Caches[Cache_SwitchElseDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WhileStatement = 320;

        public virtual Match WhileStatement(int start)
        {
            if (!Caches[Cache_WhileStatement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_while(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = ConditionList(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = CodeBlock(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_while(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = ConditionList(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("while-statement-1", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_while(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("while-statement-2", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("WhileStatement", start, match);
                }
                Caches[Cache_WhileStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Statement = 321;

        public virtual Match Statement(int start)
        {
            if (!Caches[Cache_Statement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next, StatementLabel(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = FamousStatement(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Lit_52_/*';'*/(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("Statement", start, match);
                }
                Caches[Cache_Statement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FamousStatement = 322;

        public virtual Match FamousStatement(int start)
        {
            if (!Caches[Cache_FamousStatement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = BreakStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = ContinueStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = FallthroughStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = ReturnStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = ThrowStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = ForInStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = WhileStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = RepeatWhileStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = IfStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = GuardStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = SwitchStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = DoStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = DeferStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        var next2 = next;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            while (true) // ---Choice---
                            {
                                if ((match = Lit_case(next2)) != null)
                                {
                                    break;
                                }
                                match = Lit_default(next2);
                                break;
                            }
                            if (match == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Not_(next2, More(next2))) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next, matches2);
                        }
                        match = Not_(next, match);
                        if (match == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Expression(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("statement", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("FamousStatement", start, match);
                }
                Caches[Cache_FamousStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CodeBlock = 323;

        public virtual Match CodeBlock(int start)
        {
            if (!Caches[Cache_CodeBlock].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        while (true)
                        {
                            if ((match = Statement(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next, zomMatches);
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_22_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("code-block", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("CodeBlock", start, match);
                }
                Caches[Cache_CodeBlock].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FallthroughStatement = 324;

        public virtual Match FallthroughStatement(int start)
        {
            if (!Caches[Cache_FallthroughStatement].Already(start, out var match))
            {
                match = Lit_fallthrough(start);
                if (match != null)
                {
                    match = Match.Success("FallthroughStatement", start, match);
                }
                Caches[Cache_FallthroughStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ThrowStatement = 325;

        public virtual Match ThrowStatement(int start)
        {
            if (!Caches[Cache_ThrowStatement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_throw(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Expression(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_throw(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("throw-statement", next2);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("ThrowStatement", start, match);
                }
                Caches[Cache_ThrowStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ForInStatement = 326;

        public virtual Match ForInStatement(int start)
        {
            if (!Caches[Cache_ForInStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_for(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Lit_case(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Pattern(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_in(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, WhereClause(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CodeBlock(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ForInStatement", start, match);
                }
                Caches[Cache_ForInStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RepeatWhileStatement = 327;

        public virtual Match RepeatWhileStatement(int start)
        {
            if (!Caches[Cache_RepeatWhileStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_repeat(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CodeBlock(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_while(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("RepeatWhileStatement", start, match);
                }
                Caches[Cache_RepeatWhileStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DoStatement = 328;

        public virtual Match DoStatement(int start)
        {
            if (!Caches[Cache_DoStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_do(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CodeBlock(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = CatchClause(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("DoStatement", start, match);
                }
                Caches[Cache_DoStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CatchClause = 329;

        public virtual Match CatchClause(int start)
        {
            if (!Caches[Cache_CatchClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_catch(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, CatchPatternList(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CodeBlock(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("CatchClause", start, match);
                }
                Caches[Cache_CatchClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CatchPatternList = 330;

        public virtual Match CatchPatternList(int start)
        {
            if (!Caches[Cache_CatchPatternList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = CatchPattern(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = CatchPattern(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("CatchPatternList", start, match);
                }
                Caches[Cache_CatchPatternList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CatchPattern = 331;

        public virtual Match CatchPattern(int start)
        {
            if (!Caches[Cache_CatchPattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Pattern(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, WhereClause(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("CatchPattern", start, match);
                }
                Caches[Cache_CatchPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DeferStatement = 332;

        public virtual Match DeferStatement(int start)
        {
            if (!Caches[Cache_DeferStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_defer(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CodeBlock(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("DeferStatement", start, match);
                }
                Caches[Cache_DeferStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StatementLabel = 333;

        public virtual Match StatementLabel(int start)
        {
            if (!Caches[Cache_StatementLabel].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = LabelName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("StatementLabel", start, match);
                }
                Caches[Cache_StatementLabel].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LabelName = 334;

        public virtual Match LabelName(int start)
        {
            if (!Caches[Cache_LabelName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("LabelName", start, match);
                }
                Caches[Cache_LabelName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WhereClause = 335;

        public virtual Match WhereClause(int start)
        {
            if (!Caches[Cache_WhereClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_where(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = WhereExpression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("WhereClause", start, match);
                }
                Caches[Cache_WhereClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WhereExpression = 336;

        public virtual Match WhereExpression(int start)
        {
            if (!Caches[Cache_WhereExpression].Already(start, out var match))
            {
                match = Expression(start);
                if (match != null)
                {
                    match = Match.Success("WhereExpression", start, match);
                }
                Caches[Cache_WhereExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleType = 337;

        public virtual Match TupleType(int start)
        {
            if (!Caches[Cache_TupleType].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = TupleTypeElementList(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("TupleType", start, match);
                }
                Caches[Cache_TupleType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleTypeElementList = 338;

        public virtual Match TupleTypeElementList(int start)
        {
            if (!Caches[Cache_TupleTypeElementList].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = TupleTypeElement(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var oomMatches = new List<Match>();
                        var oomNext = next;
                        while (true)
                        {
                            var next2 = oomNext;
                            var matches2 = new List<Match>();
                            while (true) // ---Sequence---
                            {
                                if ((match = Lit_16_/*','*/(next2)) == null)
                                {
                                    break;
                                }
                                matches2.Add(match);
                                next2 = match.Next;
                                if ((match = TupleTypeElement(next2)) == null)
                                {
                                    break;
                                }
                                matches2.Add(match);
                                break;
                            }
                            if (match != null)
                            {
                                match = Match.Success("_", oomNext, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            oomMatches.Add(match);
                            oomNext = match.Next;
                        }
                        if (oomMatches.Count > 0)
                        {
                            match = Match.Success("+", next, oomMatches);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = TupleTypeElement(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_16_/*','*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        {
                            // >>> ERROR
                            new Error(Context).Report("tuple-type-element-list - tuple-type-element", next3);
                            throw new BailOutException();
                            // <<< ERROR
                        }
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("TupleTypeElementList", start, match);
                }
                Caches[Cache_TupleTypeElementList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleTypeElement = 339;

        public virtual Match TupleTypeElement(int start)
        {
            if (!Caches[Cache_TupleTypeElement].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = ElementName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = TypeAnnotation(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = Type(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("TupleTypeElement", start, match);
                }
                Caches[Cache_TupleTypeElement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElementName = 340;

        public virtual Match ElementName(int start)
        {
            if (!Caches[Cache_ElementName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("ElementName", start, match);
                }
                Caches[Cache_ElementName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EnumTupleType = 341;

        public virtual Match EnumTupleType(int start)
        {
            if (!Caches[Cache_EnumTupleType].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = TupleType(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = TupleTypeElement(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("EnumTupleType", start, match);
                }
                Caches[Cache_EnumTupleType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Type = 342;

        public virtual Match Type(int start)
        {
            if (!Caches[Cache_Type].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = PrimaryType(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = TypePostfix(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Type", start, match);
                }
                Caches[Cache_Type].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrimaryType = 343;

        public virtual Match PrimaryType(int start)
        {
            if (!Caches[Cache_PrimaryType].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = ProtocolCompositionType(start)) != null)
                    {
                        break;
                    }
                    if ((match = TypeIdentifier(start)) != null)
                    {
                        break;
                    }
                    if ((match = FunctionType(start)) != null)
                    {
                        break;
                    }
                    if ((match = ArrayType(start)) != null)
                    {
                        break;
                    }
                    if ((match = DictionaryType(start)) != null)
                    {
                        break;
                    }
                    if ((match = AnyType(start)) != null)
                    {
                        break;
                    }
                    if ((match = SelfType(start)) != null)
                    {
                        break;
                    }
                    if ((match = OpaqueType(start)) != null)
                    {
                        break;
                    }
                    if ((match = TupleType(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Type(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("PrimaryType", start, match);
                }
                Caches[Cache_PrimaryType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypePostfix = 344;

        public virtual Match TypePostfix(int start)
        {
            if (!Caches[Cache_TypePostfix].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = TypeOptional(start)) != null)
                    {
                        break;
                    }
                    if ((match = TypeUnwrap(start)) != null)
                    {
                        break;
                    }
                    match = TypeMetatype(start);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("TypePostfix", start, match);
                }
                Caches[Cache_TypePostfix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeOptional = 345;

        public virtual Match TypeOptional(int start)
        {
            if (!Caches[Cache_TypeOptional].Already(start, out var match))
            {
                match = CharacterExact_(start, '?');
                if (match != null)
                {
                    match = Match.Success("TypeOptional", start, match);
                }
                Caches[Cache_TypeOptional].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeUnwrap = 346;

        public virtual Match TypeUnwrap(int start)
        {
            if (!Caches[Cache_TypeUnwrap].Already(start, out var match))
            {
                match = CharacterExact_(start, '!');
                if (match != null)
                {
                    match = Match.Success("TypeUnwrap", start, match);
                }
                Caches[Cache_TypeUnwrap].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeMetatype = 347;

        public virtual Match TypeMetatype(int start)
        {
            if (!Caches[Cache_TypeMetatype].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_dot_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    while (true) // ---Choice---
                    {
                        var next2 = next;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_Type(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Not_(next2, More(next2))) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next, matches2);
                        }
                        if (match != null)
                        {
                            break;
                        }
                        var next3 = next;
                        var matches3 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_Protocol(next3)) == null)
                            {
                                break;
                            }
                            matches3.Add(match);
                            next3 = match.Next;
                            if ((match = Not_(next3, More(next3))) == null)
                            {
                                break;
                            }
                            matches3.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next, matches3);
                        }
                        break;
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("TypeMetatype", start, match);
                }
                Caches[Cache_TypeMetatype].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionType = 348;

        public virtual Match FunctionType(int start)
        {
            if (!Caches[Cache_FunctionType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = Match.Optional(next, Attributes(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = FunctionTypeArgumentClause(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Lit_throws(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_23_/*'->'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Type(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("FunctionType", start, match);
                }
                Caches[Cache_FunctionType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionTypeArgumentClause = 349;

        public virtual Match FunctionTypeArgumentClause(int start)
        {
            if (!Caches[Cache_FunctionTypeArgumentClause].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'('*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = FunctionTypeArgumentList(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, Lit_dot_dot_dot_(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("FunctionTypeArgumentClause", start, match);
                }
                Caches[Cache_FunctionTypeArgumentClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionTypeArgumentList = 350;

        public virtual Match FunctionTypeArgumentList(int start)
        {
            if (!Caches[Cache_FunctionTypeArgumentList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = FunctionTypeArgument(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = FunctionTypeArgument(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("FunctionTypeArgumentList", start, match);
                }
                Caches[Cache_FunctionTypeArgumentList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionTypeArgument = 351;

        public virtual Match FunctionTypeArgument(int start)
        {
            if (!Caches[Cache_FunctionTypeArgument].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = Name(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = ArgumentLabel(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = TypeAnnotation(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        if ((match = ArgumentLabel(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = TypeAnnotation(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // ---Sequence---
                    {
                        match = Match.Optional(next3, Attributes(next3));
                        matches3.Add(match);
                        next3 = match.Next;
                        match = Match.Optional(next3, Lit_inout(next3));
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Type(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("FunctionTypeArgument", start, match);
                }
                Caches[Cache_FunctionTypeArgument].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArgumentLabel = 352;

        public virtual Match ArgumentLabel(int start)
        {
            if (!Caches[Cache_ArgumentLabel].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("ArgumentLabel", start, match);
                }
                Caches[Cache_ArgumentLabel].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArrayType = 353;

        public virtual Match ArrayType(int start)
        {
            if (!Caches[Cache_ArrayType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_29_/*'['*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Type(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_30_/*']'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ArrayType", start, match);
                }
                Caches[Cache_ArrayType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DictionaryType = 354;

        public virtual Match DictionaryType(int start)
        {
            if (!Caches[Cache_DictionaryType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_29_/*'['*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Type(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Type(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_30_/*']'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("DictionaryType", start, match);
                }
                Caches[Cache_DictionaryType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeIdentifier = 355;

        public virtual Match TypeIdentifier(int start)
        {
            if (!Caches[Cache_TypeIdentifier].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = TypeIdentifierPart(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_dot_(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = TypeIdentifierPart(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("TypeIdentifier", start, match);
                }
                Caches[Cache_TypeIdentifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeIdentifierPart = 356;

        public virtual Match TypeIdentifierPart(int start)
        {
            if (!Caches[Cache_TypeIdentifierPart].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = TypeName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericArgumentClause(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("TypeIdentifierPart", start, match);
                }
                Caches[Cache_TypeIdentifierPart].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolCompositionType = 357;

        public virtual Match ProtocolCompositionType(int start)
        {
            if (!Caches[Cache_ProtocolCompositionType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = TypeIdentifier(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var oomMatches = new List<Match>();
                    var oomNext = next;
                    while (true)
                    {
                        var next2 = oomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_28_/*'&'*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Not_(next2, OperatorCharacter(next2))) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = TypeIdentifier(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", oomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        oomMatches.Add(match);
                        oomNext = match.Next;
                    }
                    if (oomMatches.Count > 0)
                    {
                        match = Match.Success("+", next, oomMatches);
                    }
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("ProtocolCompositionType", start, match);
                }
                Caches[Cache_ProtocolCompositionType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OpaqueType = 358;

        public virtual Match OpaqueType(int start)
        {
            if (!Caches[Cache_OpaqueType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_some(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Type(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("OpaqueType", start, match);
                }
                Caches[Cache_OpaqueType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AnyType = 359;

        public virtual Match AnyType(int start)
        {
            if (!Caches[Cache_AnyType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_Any(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("AnyType", start, match);
                }
                Caches[Cache_AnyType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SelfType = 360;

        public virtual Match SelfType(int start)
        {
            if (!Caches[Cache_SelfType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_Self(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("SelfType", start, match);
                }
                Caches[Cache_SelfType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeName = 361;

        public virtual Match TypeName(int start)
        {
            if (!Caches[Cache_TypeName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                    match = Match.Success("TypeName", start, match);
                }
                Caches[Cache_TypeName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeInheritanceClause = 362;

        public virtual Match TypeInheritanceClause(int start)
        {
            if (!Caches[Cache_TypeInheritanceClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = Lit_24_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = TypeInheritanceList(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("TypeInheritanceClause", start, match);
                }
                Caches[Cache_TypeInheritanceClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeInheritanceList = 363;

        public virtual Match TypeInheritanceList(int start)
        {
            if (!Caches[Cache_TypeInheritanceList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    if ((match = TypeIdentifier(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        var next2 = zomNext;
                        var matches2 = new List<Match>();
                        while (true) // ---Sequence---
                        {
                            if ((match = Lit_16_/*','*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = TypeIdentifier(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        if (match == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("TypeInheritanceList", start, match);
                }
                Caches[Cache_TypeInheritanceList].Cache(start, match);
            }
            return match;
        }

        public virtual Match Whitespace(int start)
        {
            Match match;
            match = WhitespaceItem(start);
            if (match != null)
            {
                match = Match.Success("Whitespace", start, match);
            }
            return match;
        }

        protected const int Cache_SingleWhitespace = 365;

        public virtual Match SingleWhitespace(int start)
        {
            if (!Caches[Cache_SingleWhitespace].Already(start, out var match))
            {
                while (true) // ---Choice---
                {
                    if ((match = LineBreakCharacter(start)) != null)
                    {
                        break;
                    }
                    if ((match = InlineSpace(start)) != null)
                    {
                        break;
                    }
                    if ((match = CharacterExact_(start, ' ')) != null)
                    {
                        break;
                    }
                    if ((match = CharacterExact_(start, '\v')) != null)
                    {
                        break;
                    }
                    match = CharacterExact_(start, '\f');
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("SingleWhitespace", start, match);
                }
                Caches[Cache_SingleWhitespace].Cache(start, match);
            }
            return match;
        }

        public virtual Match WhitespaceItem(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                if ((match = LineBreak(start)) != null)
                {
                    break;
                }
                if ((match = InlineSpace(start)) != null)
                {
                    break;
                }
                if ((match = Comment(start)) != null)
                {
                    break;
                }
                if ((match = MultilineComment(start)) != null)
                {
                    break;
                }
                if ((match = CharacterExact_(start, ' ')) != null)
                {
                    break;
                }
                if ((match = CharacterExact_(start, '\v')) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '\f');
                break;
            }
            if (match != null)
            {
                match = Match.Success("WhitespaceItem", start, match);
            }
            return match;
        }

        public virtual Match LineBreak(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                if ((match = CharacterExact_(start, '\n')) != null)
                {
                    break;
                }
                if ((match = CharacterSequence_(start, "\r\n")) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '\r');
                break;
            }
            if (match != null)
            {
                match = Match.Success("LineBreak", start, match);
            }
            return match;
        }

        public virtual Match LineBreakCharacter(int start)
        {
            Match match;
            match = CharacterSet_(start, "\n\r");
            if (match != null)
            {
                match = Match.Success("LineBreakCharacter", start, match);
            }
            return match;
        }

        public virtual Match InlineSpaces(int start)
        {
            Match match;
            var oomMatches = new List<Match>();
            var oomNext = start;
            while (true)
            {
                if ((match = InlineSpace(oomNext)) == null)
                {
                    break;
                }
                oomMatches.Add(match);
                oomNext = match.Next;
            }
            if (oomMatches.Count > 0)
            {
                match = Match.Success("+", start, oomMatches);
            }
            if (match != null)
            {
                match = Match.Success("InlineSpaces", start, match);
            }
            return match;
        }

        public virtual Match InlineSpace(int start)
        {
            Match match;
            match = CharacterSet_(start, "\t ");
            if (match != null)
            {
                match = Match.Success("InlineSpace", start, match);
            }
            return match;
        }

        public virtual Match Comment(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '/')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, '/')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CommentText(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = LineBreak(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("Comment", start, match);
            }
            return match;
        }

        public virtual Match CommentText(int start)
        {
            Match match;
            var zomMatches = new List<Match>();
            var zomNext = start;
            while (true)
            {
                if ((match = CommentTextItem(zomNext)) == null)
                {
                    break;
                }
                zomMatches.Add(match);
                zomNext = match.Next;
            }
            match = Match.Success("*", start, zomMatches);
            if (match != null)
            {
                match = Match.Success("CommentText", start, match);
            }
            return match;
        }

        public virtual Match CommentTextItem(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = Not_(next, LineBreakCharacter(next))) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterAny_(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("CommentTextItem", start, match);
            }
            return match;
        }

        public virtual Match MultilineComment(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '/')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, '*')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = MultilineCommentText(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, '*')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                if ((match = CharacterExact_(next, '/')) == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            if (match != null)
            {
                match = Match.Success("MultilineComment", start, match);
            }
            return match;
        }

        public virtual Match MultilineCommentText(int start)
        {
            Match match;
            var zomMatches = new List<Match>();
            var zomNext = start;
            while (true)
            {
                if ((match = MultilineCommentTextItem(zomNext)) == null)
                {
                    break;
                }
                zomMatches.Add(match);
                zomNext = match.Next;
            }
            match = Match.Success("*", start, zomMatches);
            if (match != null)
            {
                match = Match.Success("MultilineCommentText", start, match);
            }
            return match;
        }

        public virtual Match MultilineCommentTextItem(int start)
        {
            Match match;
            while (true) // ---Choice---
            {
                if ((match = MultilineComment(start)) != null)
                {
                    break;
                }
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    while (true) // ---Choice---
                    {
                        if ((match = CharacterSequence_(next, "/*")) != null)
                        {
                            break;
                        }
                        match = CharacterSequence_(next, "*/");
                        break;
                    }
                    match = Not_(next, match);
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterAny_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                break;
            }
            if (match != null)
            {
                match = Match.Success("MultilineCommentTextItem", start, match);
            }
            return match;
        }

        protected const int Cache_Lit_at_ = 377;

        public virtual Match Lit_at_(int start)
        {
            if (!Caches[Cache_Lit_at_].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '@')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_at_", start, match);
                }
                Caches[Cache_Lit_at_].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_class = 378;

        public virtual Match Lit_class(int start)
        {
            if (!Caches[Cache_Lit_class].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "class")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_class", start, match);
                }
                Caches[Cache_Lit_class].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_convenience = 379;

        public virtual Match Lit_convenience(int start)
        {
            if (!Caches[Cache_Lit_convenience].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "convenience")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_convenience", start, match);
                }
                Caches[Cache_Lit_convenience].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_dynamic = 380;

        public virtual Match Lit_dynamic(int start)
        {
            if (!Caches[Cache_Lit_dynamic].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "dynamic")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_dynamic", start, match);
                }
                Caches[Cache_Lit_dynamic].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_final = 381;

        public virtual Match Lit_final(int start)
        {
            if (!Caches[Cache_Lit_final].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "final")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_final", start, match);
                }
                Caches[Cache_Lit_final].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_infix = 382;

        public virtual Match Lit_infix(int start)
        {
            if (!Caches[Cache_Lit_infix].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "infix")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_infix", start, match);
                }
                Caches[Cache_Lit_infix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_lazy = 383;

        public virtual Match Lit_lazy(int start)
        {
            if (!Caches[Cache_Lit_lazy].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "lazy")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_lazy", start, match);
                }
                Caches[Cache_Lit_lazy].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_optional = 384;

        public virtual Match Lit_optional(int start)
        {
            if (!Caches[Cache_Lit_optional].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "optional")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_optional", start, match);
                }
                Caches[Cache_Lit_optional].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_override = 385;

        public virtual Match Lit_override(int start)
        {
            if (!Caches[Cache_Lit_override].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "override")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_override", start, match);
                }
                Caches[Cache_Lit_override].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_postfix = 386;

        public virtual Match Lit_postfix(int start)
        {
            if (!Caches[Cache_Lit_postfix].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "postfix")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_postfix", start, match);
                }
                Caches[Cache_Lit_postfix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_prefix = 387;

        public virtual Match Lit_prefix(int start)
        {
            if (!Caches[Cache_Lit_prefix].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "prefix")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_prefix", start, match);
                }
                Caches[Cache_Lit_prefix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_required = 388;

        public virtual Match Lit_required(int start)
        {
            if (!Caches[Cache_Lit_required].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "required")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_required", start, match);
                }
                Caches[Cache_Lit_required].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_static = 389;

        public virtual Match Lit_static(int start)
        {
            if (!Caches[Cache_Lit_static].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "static")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_static", start, match);
                }
                Caches[Cache_Lit_static].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_unowned = 390;

        public virtual Match Lit_unowned(int start)
        {
            if (!Caches[Cache_Lit_unowned].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "unowned")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_unowned", start, match);
                }
                Caches[Cache_Lit_unowned].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_1_/*'('*/ = 391;

        public virtual Match Lit_1_/*'('*/(int start)
        {
            if (!Caches[Cache_Lit_1_/*'('*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '(')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_1_/*\'(\'*/", start, match);
                }
                Caches[Cache_Lit_1_/*'('*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_safe = 392;

        public virtual Match Lit_safe(int start)
        {
            if (!Caches[Cache_Lit_safe].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "safe")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_safe", start, match);
                }
                Caches[Cache_Lit_safe].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_unsafe = 393;

        public virtual Match Lit_unsafe(int start)
        {
            if (!Caches[Cache_Lit_unsafe].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "unsafe")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_unsafe", start, match);
                }
                Caches[Cache_Lit_unsafe].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_2_/*')'*/ = 394;

        public virtual Match Lit_2_/*')'*/(int start)
        {
            if (!Caches[Cache_Lit_2_/*')'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, ')')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_2_/*\')\'*/", start, match);
                }
                Caches[Cache_Lit_2_/*')'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_weak = 395;

        public virtual Match Lit_weak(int start)
        {
            if (!Caches[Cache_Lit_weak].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "weak")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_weak", start, match);
                }
                Caches[Cache_Lit_weak].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_3_/*'__consuming'*/ = 396;

        public virtual Match Lit_3_/*'__consuming'*/(int start)
        {
            if (!Caches[Cache_Lit_3_/*'__consuming'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "__consuming")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_3_/*\'__consuming\'*/", start, match);
                }
                Caches[Cache_Lit_3_/*'__consuming'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_set = 397;

        public virtual Match Lit_set(int start)
        {
            if (!Caches[Cache_Lit_set].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "set")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_set", start, match);
                }
                Caches[Cache_Lit_set].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_private = 398;

        public virtual Match Lit_private(int start)
        {
            if (!Caches[Cache_Lit_private].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "private")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_private", start, match);
                }
                Caches[Cache_Lit_private].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_fileprivate = 399;

        public virtual Match Lit_fileprivate(int start)
        {
            if (!Caches[Cache_Lit_fileprivate].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "fileprivate")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_fileprivate", start, match);
                }
                Caches[Cache_Lit_fileprivate].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_internal = 400;

        public virtual Match Lit_internal(int start)
        {
            if (!Caches[Cache_Lit_internal].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "internal")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_internal", start, match);
                }
                Caches[Cache_Lit_internal].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_public = 401;

        public virtual Match Lit_public(int start)
        {
            if (!Caches[Cache_Lit_public].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "public")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_public", start, match);
                }
                Caches[Cache_Lit_public].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_open = 402;

        public virtual Match Lit_open(int start)
        {
            if (!Caches[Cache_Lit_open].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "open")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_open", start, match);
                }
                Caches[Cache_Lit_open].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_mutating = 403;

        public virtual Match Lit_mutating(int start)
        {
            if (!Caches[Cache_Lit_mutating].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "mutating")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_mutating", start, match);
                }
                Caches[Cache_Lit_mutating].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_nonmutating = 404;

        public virtual Match Lit_nonmutating(int start)
        {
            if (!Caches[Cache_Lit_nonmutating].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "nonmutating")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_nonmutating", start, match);
                }
                Caches[Cache_Lit_nonmutating].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_4_/*'#'*/ = 405;

        public virtual Match Lit_4_/*'#'*/(int start)
        {
            if (!Caches[Cache_Lit_4_/*'#'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '#')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_4_/*\'#\'*/", start, match);
                }
                Caches[Cache_Lit_4_/*'#'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_5_/*'#elseif'*/ = 406;

        public virtual Match Lit_5_/*'#elseif'*/(int start)
        {
            if (!Caches[Cache_Lit_5_/*'#elseif'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#elseif")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_5_/*\'#elseif\'*/", start, match);
                }
                Caches[Cache_Lit_5_/*'#elseif'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_6_/*'#else'*/ = 407;

        public virtual Match Lit_6_/*'#else'*/(int start)
        {
            if (!Caches[Cache_Lit_6_/*'#else'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#else")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_6_/*\'#else\'*/", start, match);
                }
                Caches[Cache_Lit_6_/*'#else'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_7_/*'#endif'*/ = 408;

        public virtual Match Lit_7_/*'#endif'*/(int start)
        {
            if (!Caches[Cache_Lit_7_/*'#endif'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#endif")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_7_/*\'#endif\'*/", start, match);
                }
                Caches[Cache_Lit_7_/*'#endif'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_8_/*'!'*/ = 409;

        public virtual Match Lit_8_/*'!'*/(int start)
        {
            if (!Caches[Cache_Lit_8_/*'!'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '!')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_8_/*\'!\'*/", start, match);
                }
                Caches[Cache_Lit_8_/*'!'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_9_/*'||'*/ = 410;

        public virtual Match Lit_9_/*'||'*/(int start)
        {
            if (!Caches[Cache_Lit_9_/*'||'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "||")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_9_/*\'||\'*/", start, match);
                }
                Caches[Cache_Lit_9_/*'||'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_10_/*'&&'*/ = 411;

        public virtual Match Lit_10_/*'&&'*/(int start)
        {
            if (!Caches[Cache_Lit_10_/*'&&'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "&&")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_10_/*\'&&\'*/", start, match);
                }
                Caches[Cache_Lit_10_/*'&&'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_os = 412;

        public virtual Match Lit_os(int start)
        {
            if (!Caches[Cache_Lit_os].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "os")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_os", start, match);
                }
                Caches[Cache_Lit_os].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_arch = 413;

        public virtual Match Lit_arch(int start)
        {
            if (!Caches[Cache_Lit_arch].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "arch")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_arch", start, match);
                }
                Caches[Cache_Lit_arch].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_swift = 414;

        public virtual Match Lit_swift(int start)
        {
            if (!Caches[Cache_Lit_swift].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "swift")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_swift", start, match);
                }
                Caches[Cache_Lit_swift].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_11_/*'>='*/ = 415;

        public virtual Match Lit_11_/*'>='*/(int start)
        {
            if (!Caches[Cache_Lit_11_/*'>='*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, ">=")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_11_/*\'>=\'*/", start, match);
                }
                Caches[Cache_Lit_11_/*'>='*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_12_/*'<'*/ = 416;

        public virtual Match Lit_12_/*'<'*/(int start)
        {
            if (!Caches[Cache_Lit_12_/*'<'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '<')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_12_/*\'<\'*/", start, match);
                }
                Caches[Cache_Lit_12_/*'<'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_compiler = 417;

        public virtual Match Lit_compiler(int start)
        {
            if (!Caches[Cache_Lit_compiler].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "compiler")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_compiler", start, match);
                }
                Caches[Cache_Lit_compiler].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_canImport = 418;

        public virtual Match Lit_canImport(int start)
        {
            if (!Caches[Cache_Lit_canImport].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "canImport")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_canImport", start, match);
                }
                Caches[Cache_Lit_canImport].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_targetEnvironment = 419;

        public virtual Match Lit_targetEnvironment(int start)
        {
            if (!Caches[Cache_Lit_targetEnvironment].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "targetEnvironment")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_targetEnvironment", start, match);
                }
                Caches[Cache_Lit_targetEnvironment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_macOS = 420;

        public virtual Match Lit_macOS(int start)
        {
            if (!Caches[Cache_Lit_macOS].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "macOS")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_macOS", start, match);
                }
                Caches[Cache_Lit_macOS].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_iOS = 421;

        public virtual Match Lit_iOS(int start)
        {
            if (!Caches[Cache_Lit_iOS].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "iOS")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_iOS", start, match);
                }
                Caches[Cache_Lit_iOS].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_watchOS = 422;

        public virtual Match Lit_watchOS(int start)
        {
            if (!Caches[Cache_Lit_watchOS].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "watchOS")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_watchOS", start, match);
                }
                Caches[Cache_Lit_watchOS].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_tvOS = 423;

        public virtual Match Lit_tvOS(int start)
        {
            if (!Caches[Cache_Lit_tvOS].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "tvOS")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_tvOS", start, match);
                }
                Caches[Cache_Lit_tvOS].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Windows = 424;

        public virtual Match Lit_Windows(int start)
        {
            if (!Caches[Cache_Lit_Windows].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "Windows")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_Windows", start, match);
                }
                Caches[Cache_Lit_Windows].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Android = 425;

        public virtual Match Lit_Android(int start)
        {
            if (!Caches[Cache_Lit_Android].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "Android")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_Android", start, match);
                }
                Caches[Cache_Lit_Android].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Linux = 426;

        public virtual Match Lit_Linux(int start)
        {
            if (!Caches[Cache_Lit_Linux].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "Linux")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_Linux", start, match);
                }
                Caches[Cache_Lit_Linux].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_OpenBSD = 427;

        public virtual Match Lit_OpenBSD(int start)
        {
            if (!Caches[Cache_Lit_OpenBSD].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "OpenBSD")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_OpenBSD", start, match);
                }
                Caches[Cache_Lit_OpenBSD].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_i386 = 428;

        public virtual Match Lit_i386(int start)
        {
            if (!Caches[Cache_Lit_i386].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "i386")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_i386", start, match);
                }
                Caches[Cache_Lit_i386].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_13_/*'x86_64'*/ = 429;

        public virtual Match Lit_13_/*'x86_64'*/(int start)
        {
            if (!Caches[Cache_Lit_13_/*'x86_64'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "x86_64")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_13_/*\'x86_64\'*/", start, match);
                }
                Caches[Cache_Lit_13_/*'x86_64'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_arm = 430;

        public virtual Match Lit_arm(int start)
        {
            if (!Caches[Cache_Lit_arm].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "arm")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_arm", start, match);
                }
                Caches[Cache_Lit_arm].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_arm64 = 431;

        public virtual Match Lit_arm64(int start)
        {
            if (!Caches[Cache_Lit_arm64].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "arm64")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_arm64", start, match);
                }
                Caches[Cache_Lit_arm64].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_wasm32 = 432;

        public virtual Match Lit_wasm32(int start)
        {
            if (!Caches[Cache_Lit_wasm32].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "wasm32")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_wasm32", start, match);
                }
                Caches[Cache_Lit_wasm32].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_powerpc64 = 433;

        public virtual Match Lit_powerpc64(int start)
        {
            if (!Caches[Cache_Lit_powerpc64].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "powerpc64")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_powerpc64", start, match);
                }
                Caches[Cache_Lit_powerpc64].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_s390x = 434;

        public virtual Match Lit_s390x(int start)
        {
            if (!Caches[Cache_Lit_s390x].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "s390x")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_s390x", start, match);
                }
                Caches[Cache_Lit_s390x].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_dot_ = 435;

        public virtual Match Lit_dot_(int start)
        {
            if (!Caches[Cache_Lit_dot_].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '.')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_dot_", start, match);
                }
                Caches[Cache_Lit_dot_].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_simulator = 436;

        public virtual Match Lit_simulator(int start)
        {
            if (!Caches[Cache_Lit_simulator].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "simulator")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_simulator", start, match);
                }
                Caches[Cache_Lit_simulator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_macCatalyst = 437;

        public virtual Match Lit_macCatalyst(int start)
        {
            if (!Caches[Cache_Lit_macCatalyst].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "macCatalyst")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_macCatalyst", start, match);
                }
                Caches[Cache_Lit_macCatalyst].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_14_/*'#sourceLocation'*/ = 438;

        public virtual Match Lit_14_/*'#sourceLocation'*/(int start)
        {
            if (!Caches[Cache_Lit_14_/*'#sourceLocation'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#sourceLocation")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_14_/*\'#sourceLocation\'*/", start, match);
                }
                Caches[Cache_Lit_14_/*'#sourceLocation'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_15_/*'file:'*/ = 439;

        public virtual Match Lit_15_/*'file:'*/(int start)
        {
            if (!Caches[Cache_Lit_15_/*'file:'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "file:")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_15_/*\'file:\'*/", start, match);
                }
                Caches[Cache_Lit_15_/*'file:'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_16_/*','*/ = 440;

        public virtual Match Lit_16_/*','*/(int start)
        {
            if (!Caches[Cache_Lit_16_/*','*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, ',')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_16_/*\',\'*/", start, match);
                }
                Caches[Cache_Lit_16_/*','*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_17_/*'line:'*/ = 441;

        public virtual Match Lit_17_/*'line:'*/(int start)
        {
            if (!Caches[Cache_Lit_17_/*'line:'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "line:")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_17_/*\'line:\'*/", start, match);
                }
                Caches[Cache_Lit_17_/*'line:'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_18_/*'#error'*/ = 442;

        public virtual Match Lit_18_/*'#error'*/(int start)
        {
            if (!Caches[Cache_Lit_18_/*'#error'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#error")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_18_/*\'#error\'*/", start, match);
                }
                Caches[Cache_Lit_18_/*'#error'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_19_/*'#warning'*/ = 443;

        public virtual Match Lit_19_/*'#warning'*/(int start)
        {
            if (!Caches[Cache_Lit_19_/*'#warning'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#warning")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_19_/*\'#warning\'*/", start, match);
                }
                Caches[Cache_Lit_19_/*'#warning'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_let = 444;

        public virtual Match Lit_let(int start)
        {
            if (!Caches[Cache_Lit_let].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "let")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_let", start, match);
                }
                Caches[Cache_Lit_let].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_20_/*'='*/ = 445;

        public virtual Match Lit_20_/*'='*/(int start)
        {
            if (!Caches[Cache_Lit_20_/*'='*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '=')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_20_/*\'=\'*/", start, match);
                }
                Caches[Cache_Lit_20_/*'='*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_indirect = 446;

        public virtual Match Lit_indirect(int start)
        {
            if (!Caches[Cache_Lit_indirect].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "indirect")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_indirect", start, match);
                }
                Caches[Cache_Lit_indirect].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_enum = 447;

        public virtual Match Lit_enum(int start)
        {
            if (!Caches[Cache_Lit_enum].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "enum")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_enum", start, match);
                }
                Caches[Cache_Lit_enum].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_21_/*'{'*/ = 448;

        public virtual Match Lit_21_/*'{'*/(int start)
        {
            if (!Caches[Cache_Lit_21_/*'{'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '{')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_21_/*\'{\'*/", start, match);
                }
                Caches[Cache_Lit_21_/*'{'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_22_/*'}'*/ = 449;

        public virtual Match Lit_22_/*'}'*/(int start)
        {
            if (!Caches[Cache_Lit_22_/*'}'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '}')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_22_/*\'}\'*/", start, match);
                }
                Caches[Cache_Lit_22_/*'}'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_case = 450;

        public virtual Match Lit_case(int start)
        {
            if (!Caches[Cache_Lit_case].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "case")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_case", start, match);
                }
                Caches[Cache_Lit_case].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_extension = 451;

        public virtual Match Lit_extension(int start)
        {
            if (!Caches[Cache_Lit_extension].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "extension")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_extension", start, match);
                }
                Caches[Cache_Lit_extension].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_func = 452;

        public virtual Match Lit_func(int start)
        {
            if (!Caches[Cache_Lit_func].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "func")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_func", start, match);
                }
                Caches[Cache_Lit_func].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_throws = 453;

        public virtual Match Lit_throws(int start)
        {
            if (!Caches[Cache_Lit_throws].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "throws")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_throws", start, match);
                }
                Caches[Cache_Lit_throws].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_rethrows = 454;

        public virtual Match Lit_rethrows(int start)
        {
            if (!Caches[Cache_Lit_rethrows].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "rethrows")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_rethrows", start, match);
                }
                Caches[Cache_Lit_rethrows].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_dot_dot_dot_ = 455;

        public virtual Match Lit_dot_dot_dot_(int start)
        {
            if (!Caches[Cache_Lit_dot_dot_dot_].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "...")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_dot_dot_dot_", start, match);
                }
                Caches[Cache_Lit_dot_dot_dot_].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_23_/*'->'*/ = 456;

        public virtual Match Lit_23_/*'->'*/(int start)
        {
            if (!Caches[Cache_Lit_23_/*'->'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "->")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_23_/*\'->\'*/", start, match);
                }
                Caches[Cache_Lit_23_/*'->'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_24_/*':'*/ = 457;

        public virtual Match Lit_24_/*':'*/(int start)
        {
            if (!Caches[Cache_Lit_24_/*':'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, ':')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_24_/*\':\'*/", start, match);
                }
                Caches[Cache_Lit_24_/*':'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_25_/*'__owned'*/ = 458;

        public virtual Match Lit_25_/*'__owned'*/(int start)
        {
            if (!Caches[Cache_Lit_25_/*'__owned'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "__owned")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_25_/*\'__owned\'*/", start, match);
                }
                Caches[Cache_Lit_25_/*'__owned'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_inout = 459;

        public virtual Match Lit_inout(int start)
        {
            if (!Caches[Cache_Lit_inout].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "inout")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_inout", start, match);
                }
                Caches[Cache_Lit_inout].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_init = 460;

        public virtual Match Lit_init(int start)
        {
            if (!Caches[Cache_Lit_init].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "init")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_init", start, match);
                }
                Caches[Cache_Lit_init].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_26_/*'?'*/ = 461;

        public virtual Match Lit_26_/*'?'*/(int start)
        {
            if (!Caches[Cache_Lit_26_/*'?'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '?')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_26_/*\'?\'*/", start, match);
                }
                Caches[Cache_Lit_26_/*'?'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_operator = 462;

        public virtual Match Lit_operator(int start)
        {
            if (!Caches[Cache_Lit_operator].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "operator")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_operator", start, match);
                }
                Caches[Cache_Lit_operator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_precedencegroup = 463;

        public virtual Match Lit_precedencegroup(int start)
        {
            if (!Caches[Cache_Lit_precedencegroup].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "precedencegroup")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_precedencegroup", start, match);
                }
                Caches[Cache_Lit_precedencegroup].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_higherThan = 464;

        public virtual Match Lit_higherThan(int start)
        {
            if (!Caches[Cache_Lit_higherThan].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "higherThan")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_higherThan", start, match);
                }
                Caches[Cache_Lit_higherThan].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_lowerThan = 465;

        public virtual Match Lit_lowerThan(int start)
        {
            if (!Caches[Cache_Lit_lowerThan].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "lowerThan")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_lowerThan", start, match);
                }
                Caches[Cache_Lit_lowerThan].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_assignment = 466;

        public virtual Match Lit_assignment(int start)
        {
            if (!Caches[Cache_Lit_assignment].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "assignment")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_assignment", start, match);
                }
                Caches[Cache_Lit_assignment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_associativity = 467;

        public virtual Match Lit_associativity(int start)
        {
            if (!Caches[Cache_Lit_associativity].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "associativity")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_associativity", start, match);
                }
                Caches[Cache_Lit_associativity].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_left = 468;

        public virtual Match Lit_left(int start)
        {
            if (!Caches[Cache_Lit_left].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "left")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_left", start, match);
                }
                Caches[Cache_Lit_left].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_right = 469;

        public virtual Match Lit_right(int start)
        {
            if (!Caches[Cache_Lit_right].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "right")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_right", start, match);
                }
                Caches[Cache_Lit_right].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_none = 470;

        public virtual Match Lit_none(int start)
        {
            if (!Caches[Cache_Lit_none].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "none")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_none", start, match);
                }
                Caches[Cache_Lit_none].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_protocol = 471;

        public virtual Match Lit_protocol(int start)
        {
            if (!Caches[Cache_Lit_protocol].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "protocol")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_protocol", start, match);
                }
                Caches[Cache_Lit_protocol].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_associatedtype = 472;

        public virtual Match Lit_associatedtype(int start)
        {
            if (!Caches[Cache_Lit_associatedtype].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "associatedtype")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_associatedtype", start, match);
                }
                Caches[Cache_Lit_associatedtype].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_struct = 473;

        public virtual Match Lit_struct(int start)
        {
            if (!Caches[Cache_Lit_struct].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "struct")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_struct", start, match);
                }
                Caches[Cache_Lit_struct].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_subscript = 474;

        public virtual Match Lit_subscript(int start)
        {
            if (!Caches[Cache_Lit_subscript].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "subscript")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_subscript", start, match);
                }
                Caches[Cache_Lit_subscript].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_typealias = 475;

        public virtual Match Lit_typealias(int start)
        {
            if (!Caches[Cache_Lit_typealias].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "typealias")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_typealias", start, match);
                }
                Caches[Cache_Lit_typealias].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_var = 476;

        public virtual Match Lit_var(int start)
        {
            if (!Caches[Cache_Lit_var].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "var")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_var", start, match);
                }
                Caches[Cache_Lit_var].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_get = 477;

        public virtual Match Lit_get(int start)
        {
            if (!Caches[Cache_Lit_get].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "get")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_get", start, match);
                }
                Caches[Cache_Lit_get].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_27_/*'_modify'*/ = 478;

        public virtual Match Lit_27_/*'_modify'*/(int start)
        {
            if (!Caches[Cache_Lit_27_/*'_modify'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_modify")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_27_/*\'_modify\'*/", start, match);
                }
                Caches[Cache_Lit_27_/*'_modify'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_willSet = 479;

        public virtual Match Lit_willSet(int start)
        {
            if (!Caches[Cache_Lit_willSet].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "willSet")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_willSet", start, match);
                }
                Caches[Cache_Lit_willSet].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_didSet = 480;

        public virtual Match Lit_didSet(int start)
        {
            if (!Caches[Cache_Lit_didSet].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "didSet")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_didSet", start, match);
                }
                Caches[Cache_Lit_didSet].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_import = 481;

        public virtual Match Lit_import(int start)
        {
            if (!Caches[Cache_Lit_import].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "import")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_import", start, match);
                }
                Caches[Cache_Lit_import].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_deinit = 482;

        public virtual Match Lit_deinit(int start)
        {
            if (!Caches[Cache_Lit_deinit].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "deinit")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_deinit", start, match);
                }
                Caches[Cache_Lit_deinit].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_28_/*'&'*/ = 483;

        public virtual Match Lit_28_/*'&'*/(int start)
        {
            if (!Caches[Cache_Lit_28_/*'&'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '&')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_28_/*\'&\'*/", start, match);
                }
                Caches[Cache_Lit_28_/*'&'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_self = 484;

        public virtual Match Lit_self(int start)
        {
            if (!Caches[Cache_Lit_self].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "self")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_self", start, match);
                }
                Caches[Cache_Lit_self].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_29_/*'['*/ = 485;

        public virtual Match Lit_29_/*'['*/(int start)
        {
            if (!Caches[Cache_Lit_29_/*'['*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '[')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_29_/*\'[\'*/", start, match);
                }
                Caches[Cache_Lit_29_/*'['*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_30_/*']'*/ = 486;

        public virtual Match Lit_30_/*']'*/(int start)
        {
            if (!Caches[Cache_Lit_30_/*']'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, ']')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_30_/*\']\'*/", start, match);
                }
                Caches[Cache_Lit_30_/*']'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_31_/*'#file'*/ = 487;

        public virtual Match Lit_31_/*'#file'*/(int start)
        {
            if (!Caches[Cache_Lit_31_/*'#file'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#file")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_31_/*\'#file\'*/", start, match);
                }
                Caches[Cache_Lit_31_/*'#file'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_32_/*'#fileID'*/ = 488;

        public virtual Match Lit_32_/*'#fileID'*/(int start)
        {
            if (!Caches[Cache_Lit_32_/*'#fileID'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#fileID")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_32_/*\'#fileID\'*/", start, match);
                }
                Caches[Cache_Lit_32_/*'#fileID'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_33_/*'#filePath'*/ = 489;

        public virtual Match Lit_33_/*'#filePath'*/(int start)
        {
            if (!Caches[Cache_Lit_33_/*'#filePath'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#filePath")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_33_/*\'#filePath\'*/", start, match);
                }
                Caches[Cache_Lit_33_/*'#filePath'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_34_/*'#line'*/ = 490;

        public virtual Match Lit_34_/*'#line'*/(int start)
        {
            if (!Caches[Cache_Lit_34_/*'#line'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#line")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_34_/*\'#line\'*/", start, match);
                }
                Caches[Cache_Lit_34_/*'#line'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_35_/*'#column'*/ = 491;

        public virtual Match Lit_35_/*'#column'*/(int start)
        {
            if (!Caches[Cache_Lit_35_/*'#column'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#column")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_35_/*\'#column\'*/", start, match);
                }
                Caches[Cache_Lit_35_/*'#column'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_36_/*'#function'*/ = 492;

        public virtual Match Lit_36_/*'#function'*/(int start)
        {
            if (!Caches[Cache_Lit_36_/*'#function'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#function")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_36_/*\'#function\'*/", start, match);
                }
                Caches[Cache_Lit_36_/*'#function'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_37_/*'#dsohandle'*/ = 493;

        public virtual Match Lit_37_/*'#dsohandle'*/(int start)
        {
            if (!Caches[Cache_Lit_37_/*'#dsohandle'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#dsohandle")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_37_/*\'#dsohandle\'*/", start, match);
                }
                Caches[Cache_Lit_37_/*'#dsohandle'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_38_/*'-'*/ = 494;

        public virtual Match Lit_38_/*'-'*/(int start)
        {
            if (!Caches[Cache_Lit_38_/*'-'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '-')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_38_/*\'-\'*/", start, match);
                }
                Caches[Cache_Lit_38_/*'-'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_true = 495;

        public virtual Match Lit_true(int start)
        {
            if (!Caches[Cache_Lit_true].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "true")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_true", start, match);
                }
                Caches[Cache_Lit_true].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_false = 496;

        public virtual Match Lit_false(int start)
        {
            if (!Caches[Cache_Lit_false].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "false")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_false", start, match);
                }
                Caches[Cache_Lit_false].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_nil = 497;

        public virtual Match Lit_nil(int start)
        {
            if (!Caches[Cache_Lit_nil].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "nil")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_nil", start, match);
                }
                Caches[Cache_Lit_nil].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_39_/*'#colorLiteral'*/ = 498;

        public virtual Match Lit_39_/*'#colorLiteral'*/(int start)
        {
            if (!Caches[Cache_Lit_39_/*'#colorLiteral'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#colorLiteral")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_39_/*\'#colorLiteral\'*/", start, match);
                }
                Caches[Cache_Lit_39_/*'#colorLiteral'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_red = 499;

        public virtual Match Lit_red(int start)
        {
            if (!Caches[Cache_Lit_red].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "red")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_red", start, match);
                }
                Caches[Cache_Lit_red].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_green = 500;

        public virtual Match Lit_green(int start)
        {
            if (!Caches[Cache_Lit_green].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "green")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_green", start, match);
                }
                Caches[Cache_Lit_green].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_blue = 501;

        public virtual Match Lit_blue(int start)
        {
            if (!Caches[Cache_Lit_blue].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "blue")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_blue", start, match);
                }
                Caches[Cache_Lit_blue].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_alpha = 502;

        public virtual Match Lit_alpha(int start)
        {
            if (!Caches[Cache_Lit_alpha].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "alpha")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_alpha", start, match);
                }
                Caches[Cache_Lit_alpha].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_40_/*'#fileLiteral'*/ = 503;

        public virtual Match Lit_40_/*'#fileLiteral'*/(int start)
        {
            if (!Caches[Cache_Lit_40_/*'#fileLiteral'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#fileLiteral")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_40_/*\'#fileLiteral\'*/", start, match);
                }
                Caches[Cache_Lit_40_/*'#fileLiteral'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_resourceName = 504;

        public virtual Match Lit_resourceName(int start)
        {
            if (!Caches[Cache_Lit_resourceName].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "resourceName")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_resourceName", start, match);
                }
                Caches[Cache_Lit_resourceName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_41_/*'#imageLiteral'*/ = 505;

        public virtual Match Lit_41_/*'#imageLiteral'*/(int start)
        {
            if (!Caches[Cache_Lit_41_/*'#imageLiteral'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#imageLiteral")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_41_/*\'#imageLiteral\'*/", start, match);
                }
                Caches[Cache_Lit_41_/*'#imageLiteral'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_super = 506;

        public virtual Match Lit_super(int start)
        {
            if (!Caches[Cache_Lit_super].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "super")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_super", start, match);
                }
                Caches[Cache_Lit_super].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_in = 507;

        public virtual Match Lit_in(int start)
        {
            if (!Caches[Cache_Lit_in].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "in")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_in", start, match);
                }
                Caches[Cache_Lit_in].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_42_/*'_'*/ = 508;

        public virtual Match Lit_42_/*'_'*/(int start)
        {
            if (!Caches[Cache_Lit_42_/*'_'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '_')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_42_/*\'_\'*/", start, match);
                }
                Caches[Cache_Lit_42_/*'_'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_43_/*'\\'*/ = 509;

        public virtual Match Lit_43_/*'\\'*/(int start)
        {
            if (!Caches[Cache_Lit_43_/*'\\'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "\\\\")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_43_/*\'\\\\\'*/", start, match);
                }
                Caches[Cache_Lit_43_/*'\\'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_44_/*'#selector'*/ = 510;

        public virtual Match Lit_44_/*'#selector'*/(int start)
        {
            if (!Caches[Cache_Lit_44_/*'#selector'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#selector")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_44_/*\'#selector\'*/", start, match);
                }
                Caches[Cache_Lit_44_/*'#selector'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_45_/*'getter:'*/ = 511;

        public virtual Match Lit_45_/*'getter:'*/(int start)
        {
            if (!Caches[Cache_Lit_45_/*'getter:'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "getter:")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_45_/*\'getter:\'*/", start, match);
                }
                Caches[Cache_Lit_45_/*'getter:'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_46_/*'setter:'*/ = 512;

        public virtual Match Lit_46_/*'setter:'*/(int start)
        {
            if (!Caches[Cache_Lit_46_/*'setter:'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "setter:")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_46_/*\'setter:\'*/", start, match);
                }
                Caches[Cache_Lit_46_/*'setter:'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_47_/*'#keyPath'*/ = 513;

        public virtual Match Lit_47_/*'#keyPath'*/(int start)
        {
            if (!Caches[Cache_Lit_47_/*'#keyPath'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#keyPath")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_47_/*\'#keyPath\'*/", start, match);
                }
                Caches[Cache_Lit_47_/*'#keyPath'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_try = 514;

        public virtual Match Lit_try(int start)
        {
            if (!Caches[Cache_Lit_try].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "try")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_try", start, match);
                }
                Caches[Cache_Lit_try].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_is = 515;

        public virtual Match Lit_is(int start)
        {
            if (!Caches[Cache_Lit_is].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "is")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_is", start, match);
                }
                Caches[Cache_Lit_is].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_as = 516;

        public virtual Match Lit_as(int start)
        {
            if (!Caches[Cache_Lit_as].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "as")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_as", start, match);
                }
                Caches[Cache_Lit_as].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_48_/*'>'*/ = 517;

        public virtual Match Lit_48_/*'>'*/(int start)
        {
            if (!Caches[Cache_Lit_48_/*'>'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '>')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_48_/*\'>\'*/", start, match);
                }
                Caches[Cache_Lit_48_/*'>'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_where = 518;

        public virtual Match Lit_where(int start)
        {
            if (!Caches[Cache_Lit_where].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "where")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_where", start, match);
                }
                Caches[Cache_Lit_where].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_49_/*'=='*/ = 519;

        public virtual Match Lit_49_/*'=='*/(int start)
        {
            if (!Caches[Cache_Lit_49_/*'=='*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "==")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_49_/*\'==\'*/", start, match);
                }
                Caches[Cache_Lit_49_/*'=='*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_break = 520;

        public virtual Match Lit_break(int start)
        {
            if (!Caches[Cache_Lit_break].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "break")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_break", start, match);
                }
                Caches[Cache_Lit_break].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_default = 521;

        public virtual Match Lit_default(int start)
        {
            if (!Caches[Cache_Lit_default].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "default")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_default", start, match);
                }
                Caches[Cache_Lit_default].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_50_/*'#available'*/ = 522;

        public virtual Match Lit_50_/*'#available'*/(int start)
        {
            if (!Caches[Cache_Lit_50_/*'#available'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#available")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_50_/*\'#available\'*/", start, match);
                }
                Caches[Cache_Lit_50_/*'#available'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_51_/*'*'*/ = 523;

        public virtual Match Lit_51_/*'*'*/(int start)
        {
            if (!Caches[Cache_Lit_51_/*'*'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, '*')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_51_/*\'*\'*/", start, match);
                }
                Caches[Cache_Lit_51_/*'*'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_iOSApplicationExtension = 524;

        public virtual Match Lit_iOSApplicationExtension(int start)
        {
            if (!Caches[Cache_Lit_iOSApplicationExtension].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "iOSApplicationExtension")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_iOSApplicationExtension", start, match);
                }
                Caches[Cache_Lit_iOSApplicationExtension].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_macOSApplicationExtension = 525;

        public virtual Match Lit_macOSApplicationExtension(int start)
        {
            if (!Caches[Cache_Lit_macOSApplicationExtension].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "macOSApplicationExtension")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_macOSApplicationExtension", start, match);
                }
                Caches[Cache_Lit_macOSApplicationExtension].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_macCatalystApplicationExtension = 526;

        public virtual Match Lit_macCatalystApplicationExtension(int start)
        {
            if (!Caches[Cache_Lit_macCatalystApplicationExtension].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "macCatalystApplicationExtension")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_macCatalystApplicationExtension", start, match);
                }
                Caches[Cache_Lit_macCatalystApplicationExtension].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_continue = 527;

        public virtual Match Lit_continue(int start)
        {
            if (!Caches[Cache_Lit_continue].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "continue")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_continue", start, match);
                }
                Caches[Cache_Lit_continue].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_guard = 528;

        public virtual Match Lit_guard(int start)
        {
            if (!Caches[Cache_Lit_guard].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "guard")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_guard", start, match);
                }
                Caches[Cache_Lit_guard].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_else = 529;

        public virtual Match Lit_else(int start)
        {
            if (!Caches[Cache_Lit_else].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "else")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_else", start, match);
                }
                Caches[Cache_Lit_else].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_if = 530;

        public virtual Match Lit_if(int start)
        {
            if (!Caches[Cache_Lit_if].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "if")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_if", start, match);
                }
                Caches[Cache_Lit_if].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_return = 531;

        public virtual Match Lit_return(int start)
        {
            if (!Caches[Cache_Lit_return].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "return")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_return", start, match);
                }
                Caches[Cache_Lit_return].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_switch = 532;

        public virtual Match Lit_switch(int start)
        {
            if (!Caches[Cache_Lit_switch].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "switch")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_switch", start, match);
                }
                Caches[Cache_Lit_switch].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_while = 533;

        public virtual Match Lit_while(int start)
        {
            if (!Caches[Cache_Lit_while].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "while")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_while", start, match);
                }
                Caches[Cache_Lit_while].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_52_/*';'*/ = 534;

        public virtual Match Lit_52_/*';'*/(int start)
        {
            if (!Caches[Cache_Lit_52_/*';'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, ';')) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_52_/*\';\'*/", start, match);
                }
                Caches[Cache_Lit_52_/*';'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_fallthrough = 535;

        public virtual Match Lit_fallthrough(int start)
        {
            if (!Caches[Cache_Lit_fallthrough].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "fallthrough")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_fallthrough", start, match);
                }
                Caches[Cache_Lit_fallthrough].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_throw = 536;

        public virtual Match Lit_throw(int start)
        {
            if (!Caches[Cache_Lit_throw].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "throw")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_throw", start, match);
                }
                Caches[Cache_Lit_throw].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_for = 537;

        public virtual Match Lit_for(int start)
        {
            if (!Caches[Cache_Lit_for].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "for")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_for", start, match);
                }
                Caches[Cache_Lit_for].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_repeat = 538;

        public virtual Match Lit_repeat(int start)
        {
            if (!Caches[Cache_Lit_repeat].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "repeat")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_repeat", start, match);
                }
                Caches[Cache_Lit_repeat].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_do = 539;

        public virtual Match Lit_do(int start)
        {
            if (!Caches[Cache_Lit_do].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "do")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_do", start, match);
                }
                Caches[Cache_Lit_do].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_catch = 540;

        public virtual Match Lit_catch(int start)
        {
            if (!Caches[Cache_Lit_catch].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "catch")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_catch", start, match);
                }
                Caches[Cache_Lit_catch].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_defer = 541;

        public virtual Match Lit_defer(int start)
        {
            if (!Caches[Cache_Lit_defer].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "defer")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_defer", start, match);
                }
                Caches[Cache_Lit_defer].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Type = 542;

        public virtual Match Lit_Type(int start)
        {
            if (!Caches[Cache_Lit_Type].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "Type")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_Type", start, match);
                }
                Caches[Cache_Lit_Type].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Protocol = 543;

        public virtual Match Lit_Protocol(int start)
        {
            if (!Caches[Cache_Lit_Protocol].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "Protocol")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_Protocol", start, match);
                }
                Caches[Cache_Lit_Protocol].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_some = 544;

        public virtual Match Lit_some(int start)
        {
            if (!Caches[Cache_Lit_some].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "some")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_some", start, match);
                }
                Caches[Cache_Lit_some].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Any = 545;

        public virtual Match Lit_Any(int start)
        {
            if (!Caches[Cache_Lit_Any].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "Any")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_Any", start, match);
                }
                Caches[Cache_Lit_Any].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Self = 546;

        public virtual Match Lit_Self(int start)
        {
            if (!Caches[Cache_Lit_Self].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // ---Sequence---
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "Self")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                if (match != null)
                {
                    match = Match.Success("Lit_Self", start, match);
                }
                Caches[Cache_Lit_Self].Cache(start, match);
            }
            return match;
        }

        protected HashSet<string> _keywords = new HashSet<string>
        {
            "class",
            "convenience",
            "dynamic",
            "final",
            "infix",
            "lazy",
            "optional",
            "override",
            "postfix",
            "prefix",
            "required",
            "static",
            "unowned",
            "safe",
            "unsafe",
            "weak",
            "__consuming",
            "set",
            "private",
            "fileprivate",
            "internal",
            "public",
            "open",
            "mutating",
            "nonmutating",
            "os",
            "arch",
            "swift",
            "compiler",
            "canImport",
            "targetEnvironment",
            "macOS",
            "iOS",
            "watchOS",
            "tvOS",
            "Windows",
            "Android",
            "Linux",
            "OpenBSD",
            "i386",
            "x86_64",
            "arm",
            "arm64",
            "wasm32",
            "powerpc64",
            "s390x",
            "simulator",
            "macCatalyst",
            "let",
            "indirect",
            "enum",
            "case",
            "extension",
            "func",
            "throws",
            "rethrows",
            "__owned",
            "inout",
            "init",
            "operator",
            "precedencegroup",
            "higherThan",
            "lowerThan",
            "assignment",
            "associativity",
            "left",
            "right",
            "none",
            "protocol",
            "associatedtype",
            "struct",
            "subscript",
            "typealias",
            "var",
            "get",
            "_modify",
            "willSet",
            "didSet",
            "import",
            "deinit",
            "self",
            "true",
            "false",
            "nil",
            "red",
            "green",
            "blue",
            "alpha",
            "resourceName",
            "super",
            "in",
            "_",
            "try",
            "is",
            "as",
            "where",
            "break",
            "default",
            "iOSApplicationExtension",
            "macOSApplicationExtension",
            "macCatalystApplicationExtension",
            "continue",
            "guard",
            "else",
            "if",
            "return",
            "switch",
            "while",
            "fallthrough",
            "throw",
            "for",
            "repeat",
            "do",
            "catch",
            "defer",
            "Type",
            "Protocol",
            "some",
            "Any",
            "Self",
        }
        ;
    }
}
#endif
