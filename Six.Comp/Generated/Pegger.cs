#if true
using System;
using System.Diagnostics;
using System.Collections.Generic;
using SixPeg.Runtime;

namespace SixPeg.Pegger.Swift
{
    public abstract class SwiftPegger : Runtime.Pegger
    {
        public SwiftPegger(Context context)
            : base(context, 585)
        {
        }
        protected HashSet<string> _keywords = new HashSet<string>
        {
            "inlinable",
            "frozen",
            "escaping",
            "autoclosure",
            "usableFromInline",
            "discardableResult",
            "nonobjc",
            "unknown",
            "inline",
            "never",
            "__always",
            "available",
            "convention",
            "block",
            "thin",
            "c",
            "objc",
            "_show_in_interface",
            "_fixed_layout",
            "_nonoverride",
            "_borrowed",
            "_transparent",
            "_nonEphemeral",
            "_alwaysEmitIntoClient",
            "_objc_non_lazy_realization",
            "_implements",
            "_specialize",
            "_effects",
            "readnone",
            "readonly",
            "releasenone",
            "_silgen_name",
            "_semantics",
            "_objcRuntimeName",
            "_cdecl",
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
            "defer",
            "do",
            "catch",
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
            "Type",
            "Protocol",
            "some",
            "Any",
            "Self",
        }
        ;

        protected const int Cache_Unit = 0;

        public virtual Match Unit(int start)
        {
            if (!Caches[Cache_Unit].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    match = Match.Optional(next, Statements(next));
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = _space_(next)) == null)
                        {
                            break;
                        }
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    /*>>> Error */
                        throw new NotImplementedException();
                    /*<<< Error */
                    break;
                }
                Caches[Cache_EOF].Cache(start, match);
            }
            return match;
        }

        protected const int Cache__space_ = 2;

        public virtual Match _space_(int start)
        {
            if (!Caches[Cache__space_].Already(start, out var match))
            {
                /*>>> ZeroOrMore */
                    var zomMatches = new List<Match>();
                    var zomNext = start;
                    for (;;)
                    {
                        if ((match = Whitespace(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success(start, zomMatches);
                /*<<< ZeroOrMore */
                Caches[Cache__space_].Cache(start, match);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
                Caches[Cache_Attributes].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Attribute = 7;

        public virtual Match Attribute(int start)
        {
            if (!Caches[Cache_Attribute].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_inlinable(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_frozen(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_escaping(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = Lit_autoclosure(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches4);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = Lit_usableFromInline(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches5);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = Lit_discardableResult(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches6);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next7 = start;
                    var matches7 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next7)) == null)
                        {
                            break;
                        }
                        matches7.Add(match);
                        next7 = match.Next;
                        if ((match = Lit_nonobjc(next7)) == null)
                        {
                            break;
                        }
                        matches7.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches7);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next8 = start;
                    var matches8 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next8)) == null)
                        {
                            break;
                        }
                        matches8.Add(match);
                        next8 = match.Next;
                        if ((match = Lit_unknown(next8)) == null)
                        {
                            break;
                        }
                        matches8.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches8);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next9 = start;
                    var matches9 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next9)) == null)
                        {
                            break;
                        }
                        matches9.Add(match);
                        next9 = match.Next;
                        if ((match = Lit_inline(next9)) == null)
                        {
                            break;
                        }
                        matches9.Add(match);
                        next9 = match.Next;
                        if ((match = Lit_2_/*'('*/(next9)) == null)
                        {
                            break;
                        }
                        matches9.Add(match);
                        next9 = match.Next;
                        for (;;) // ---Choice---
                        {
                            if ((match = Lit_never(next9)) != null)
                            {
                                break;
                            }
                            match = Lit___always(next9);
                            break;
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches9.Add(match);
                        next9 = match.Next;
                        if ((match = Lit_3_/*')'*/(next9)) == null)
                        {
                            break;
                        }
                        matches9.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches9);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next10 = start;
                    var matches10 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next10)) == null)
                        {
                            break;
                        }
                        matches10.Add(match);
                        next10 = match.Next;
                        if ((match = Lit_available(next10)) == null)
                        {
                            break;
                        }
                        matches10.Add(match);
                        next10 = match.Next;
                        if ((match = Lit_2_/*'('*/(next10)) == null)
                        {
                            break;
                        }
                        matches10.Add(match);
                        next10 = match.Next;
                        /*>>> OneOrMore */
                            var oomMatches = new List<Match>();
                            var oomNext = next10;
                            for (;;)
                            {
                                for (;;) // ---Choice---
                                {
                                    if ((match = Name(oomNext)) != null)
                                    {
                                        break;
                                    }
                                    if ((match = SwiftVersion(oomNext)) != null)
                                    {
                                        break;
                                    }
                                    if ((match = Lit_4_/*','*/(oomNext)) != null)
                                    {
                                        break;
                                    }
                                    if ((match = Lit_5_/*':'*/(oomNext)) != null)
                                    {
                                        break;
                                    }
                                    if ((match = Lit_6_/*'*'*/(oomNext)) != null)
                                    {
                                        break;
                                    }
                                    match = StaticStringLiteral(oomNext);
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
                                match = Match.Success(next10, oomMatches);
                            }
                        /*<<< OneOrMore */
                        if (match == null)
                        {
                            break;
                        }
                        matches10.Add(match);
                        next10 = match.Next;
                        if ((match = Lit_3_/*')'*/(next10)) == null)
                        {
                            break;
                        }
                        matches10.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches10);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next11 = start;
                    var matches11 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next11)) == null)
                        {
                            break;
                        }
                        matches11.Add(match);
                        next11 = match.Next;
                        if ((match = Lit_convention(next11)) == null)
                        {
                            break;
                        }
                        matches11.Add(match);
                        next11 = match.Next;
                        if ((match = Lit_2_/*'('*/(next11)) == null)
                        {
                            break;
                        }
                        matches11.Add(match);
                        next11 = match.Next;
                        for (;;) // ---Choice---
                        {
                            if ((match = Lit_block(next11)) != null)
                            {
                                break;
                            }
                            if ((match = Lit_thin(next11)) != null)
                            {
                                break;
                            }
                            match = Lit_c(next11);
                            break;
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches11.Add(match);
                        next11 = match.Next;
                        if ((match = Lit_3_/*')'*/(next11)) == null)
                        {
                            break;
                        }
                        matches11.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches11);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next12 = start;
                    var matches12 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next12)) == null)
                        {
                            break;
                        }
                        matches12.Add(match);
                        next12 = match.Next;
                        if ((match = Lit_objc(next12)) == null)
                        {
                            break;
                        }
                        matches12.Add(match);
                        next12 = match.Next;
                        var next13 = next12;
                        var matches13 = new List<Match>();
                        for (;;) // ---Sequence---
                        {
                            if ((match = Lit_2_/*'('*/(next13)) == null)
                            {
                                break;
                            }
                            matches13.Add(match);
                            next13 = match.Next;
                            /*>>> OneOrMore */
                                var oomMatches2 = new List<Match>();
                                var oomNext2 = next13;
                                for (;;)
                                {
                                    for (;;) // ---Choice---
                                    {
                                        if ((match = Lit_5_/*':'*/(oomNext2)) != null)
                                        {
                                            break;
                                        }
                                        match = Name(oomNext2);
                                        break;
                                    }
                                    if (match == null)
                                    {
                                        break;
                                    }
                                    oomMatches2.Add(match);
                                    oomNext2 = match.Next;
                                }
                                if (oomMatches2.Count > 0)
                                {
                                    match = Match.Success(next13, oomMatches2);
                                }
                            /*<<< OneOrMore */
                            if (match == null)
                            {
                                break;
                            }
                            matches13.Add(match);
                            next13 = match.Next;
                            if ((match = Lit_3_/*')'*/(next13)) == null)
                            {
                                break;
                            }
                            matches13.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success(start, matches13);
                        }
                        match = Match.Optional(next12, match);
                        matches12.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches12);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next14 = start;
                    var matches14 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next14)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        if ((match = Lit__show_in_interface(next14)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches14);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next15 = start;
                    var matches15 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next15)) == null)
                        {
                            break;
                        }
                        matches15.Add(match);
                        next15 = match.Next;
                        if ((match = Lit__fixed_layout(next15)) == null)
                        {
                            break;
                        }
                        matches15.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches15);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next16 = start;
                    var matches16 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next16)) == null)
                        {
                            break;
                        }
                        matches16.Add(match);
                        next16 = match.Next;
                        if ((match = Lit__nonoverride(next16)) == null)
                        {
                            break;
                        }
                        matches16.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches16);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next17 = start;
                    var matches17 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next17)) == null)
                        {
                            break;
                        }
                        matches17.Add(match);
                        next17 = match.Next;
                        if ((match = Lit__borrowed(next17)) == null)
                        {
                            break;
                        }
                        matches17.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches17);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next18 = start;
                    var matches18 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next18)) == null)
                        {
                            break;
                        }
                        matches18.Add(match);
                        next18 = match.Next;
                        if ((match = Lit__transparent(next18)) == null)
                        {
                            break;
                        }
                        matches18.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches18);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next19 = start;
                    var matches19 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next19)) == null)
                        {
                            break;
                        }
                        matches19.Add(match);
                        next19 = match.Next;
                        if ((match = Lit__nonEphemeral(next19)) == null)
                        {
                            break;
                        }
                        matches19.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches19);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next20 = start;
                    var matches20 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next20)) == null)
                        {
                            break;
                        }
                        matches20.Add(match);
                        next20 = match.Next;
                        if ((match = Lit__alwaysEmitIntoClient(next20)) == null)
                        {
                            break;
                        }
                        matches20.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches20);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next21 = start;
                    var matches21 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next21)) == null)
                        {
                            break;
                        }
                        matches21.Add(match);
                        next21 = match.Next;
                        if ((match = Lit__objc_non_lazy_realization(next21)) == null)
                        {
                            break;
                        }
                        matches21.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches21);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next22 = start;
                    var matches22 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next22)) == null)
                        {
                            break;
                        }
                        matches22.Add(match);
                        next22 = match.Next;
                        if ((match = Lit__implements(next22)) == null)
                        {
                            break;
                        }
                        matches22.Add(match);
                        next22 = match.Next;
                        if ((match = Lit_2_/*'('*/(next22)) == null)
                        {
                            break;
                        }
                        matches22.Add(match);
                        next22 = match.Next;
                        if ((match = TypeIdentifier(next22)) == null)
                        {
                            break;
                        }
                        matches22.Add(match);
                        next22 = match.Next;
                        /*>>> ZeroOrMore */
                            var zomMatches = new List<Match>();
                            var zomNext = next22;
                            for (;;)
                            {
                                var next23 = zomNext;
                                var matches23 = new List<Match>();
                                for (;;) // ---Sequence---
                                {
                                    if ((match = Lit_4_/*','*/(next23)) == null)
                                    {
                                        break;
                                    }
                                    matches23.Add(match);
                                    next23 = match.Next;
                                    if ((match = TypeIdentifier(next23)) == null)
                                    {
                                        break;
                                    }
                                    matches23.Add(match);
                                    break;
                                }
                                if (match != null)
                                {
                                    match = Match.Success(start, matches23);
                                }
                                if (match == null)
                                {
                                    break;
                                }
                                zomMatches.Add(match);
                                zomNext = match.Next;
                            }
                            match = Match.Success(next22, zomMatches);
                        /*<<< ZeroOrMore */
                        matches22.Add(match);
                        next22 = match.Next;
                        if ((match = Lit_3_/*')'*/(next22)) == null)
                        {
                            break;
                        }
                        matches22.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches22);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next24 = start;
                    var matches24 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next24)) == null)
                        {
                            break;
                        }
                        matches24.Add(match);
                        next24 = match.Next;
                        if ((match = Lit__specialize(next24)) == null)
                        {
                            break;
                        }
                        matches24.Add(match);
                        next24 = match.Next;
                        if ((match = Lit_2_/*'('*/(next24)) == null)
                        {
                            break;
                        }
                        matches24.Add(match);
                        next24 = match.Next;
                        if ((match = GenericWhereClause(next24)) == null)
                        {
                            break;
                        }
                        matches24.Add(match);
                        next24 = match.Next;
                        if ((match = Lit_3_/*')'*/(next24)) == null)
                        {
                            break;
                        }
                        matches24.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches24);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next25 = start;
                    var matches25 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next25)) == null)
                        {
                            break;
                        }
                        matches25.Add(match);
                        next25 = match.Next;
                        if ((match = Lit__effects(next25)) == null)
                        {
                            break;
                        }
                        matches25.Add(match);
                        next25 = match.Next;
                        if ((match = Lit_2_/*'('*/(next25)) == null)
                        {
                            break;
                        }
                        matches25.Add(match);
                        next25 = match.Next;
                        for (;;) // ---Choice---
                        {
                            if ((match = Lit_readnone(next25)) != null)
                            {
                                break;
                            }
                            if ((match = Lit_readonly(next25)) != null)
                            {
                                break;
                            }
                            match = Lit_releasenone(next25);
                            break;
                        }
                        if (match == null)
                        {
                            break;
                        }
                        matches25.Add(match);
                        next25 = match.Next;
                        if ((match = Lit_3_/*')'*/(next25)) == null)
                        {
                            break;
                        }
                        matches25.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches25);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next26 = start;
                    var matches26 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next26)) == null)
                        {
                            break;
                        }
                        matches26.Add(match);
                        next26 = match.Next;
                        if ((match = Lit__silgen_name(next26)) == null)
                        {
                            break;
                        }
                        matches26.Add(match);
                        next26 = match.Next;
                        if ((match = Lit_2_/*'('*/(next26)) == null)
                        {
                            break;
                        }
                        matches26.Add(match);
                        next26 = match.Next;
                        if ((match = StaticStringLiteral(next26)) == null)
                        {
                            break;
                        }
                        matches26.Add(match);
                        next26 = match.Next;
                        if ((match = Lit_3_/*')'*/(next26)) == null)
                        {
                            break;
                        }
                        matches26.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches26);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next27 = start;
                    var matches27 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next27)) == null)
                        {
                            break;
                        }
                        matches27.Add(match);
                        next27 = match.Next;
                        if ((match = Lit__semantics(next27)) == null)
                        {
                            break;
                        }
                        matches27.Add(match);
                        next27 = match.Next;
                        if ((match = Lit_2_/*'('*/(next27)) == null)
                        {
                            break;
                        }
                        matches27.Add(match);
                        next27 = match.Next;
                        if ((match = StaticStringLiteral(next27)) == null)
                        {
                            break;
                        }
                        matches27.Add(match);
                        next27 = match.Next;
                        if ((match = Lit_3_/*')'*/(next27)) == null)
                        {
                            break;
                        }
                        matches27.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches27);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next28 = start;
                    var matches28 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next28)) == null)
                        {
                            break;
                        }
                        matches28.Add(match);
                        next28 = match.Next;
                        if ((match = Lit__objcRuntimeName(next28)) == null)
                        {
                            break;
                        }
                        matches28.Add(match);
                        next28 = match.Next;
                        if ((match = Lit_2_/*'('*/(next28)) == null)
                        {
                            break;
                        }
                        matches28.Add(match);
                        next28 = match.Next;
                        if ((match = Name(next28)) == null)
                        {
                            break;
                        }
                        matches28.Add(match);
                        next28 = match.Next;
                        if ((match = Lit_3_/*')'*/(next28)) == null)
                        {
                            break;
                        }
                        matches28.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches28);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next29 = start;
                    var matches29 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next29)) == null)
                        {
                            break;
                        }
                        matches29.Add(match);
                        next29 = match.Next;
                        if ((match = Lit__cdecl(next29)) == null)
                        {
                            break;
                        }
                        matches29.Add(match);
                        next29 = match.Next;
                        if ((match = Lit_2_/*'('*/(next29)) == null)
                        {
                            break;
                        }
                        matches29.Add(match);
                        next29 = match.Next;
                        if ((match = StaticStringLiteral(next29)) == null)
                        {
                            break;
                        }
                        matches29.Add(match);
                        next29 = match.Next;
                        if ((match = Lit_3_/*')'*/(next29)) == null)
                        {
                            break;
                        }
                        matches29.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches29);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next30 = start;
                    var matches30 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_1_/*'@'*/(next30)) == null)
                        {
                            break;
                        }
                        matches30.Add(match);
                        next30 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches30.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches30);
                    }
                    break;
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
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
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
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
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
                Caches[Cache_ModifierToken].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DeclarationModifier = 11;

        public virtual Match DeclarationModifier(int start)
        {
            if (!Caches[Cache_DeclarationModifier].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_unowned(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var next2 = next;
                        var matches2 = new List<Match>();
                        for (;;) // ---Sequence---
                        {
                            if ((match = Lit_2_/*'('*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            for (;;) // ---Choice---
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
                            if ((match = Lit_3_/*')'*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success(start, matches2);
                        }
                        match = Match.Optional(next, match);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = Lit_weak(start)) != null)
                    {
                        break;
                    }
                    match = Lit___consuming(start);
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = AccessModifierBase(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next2)) == null)
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
                        if ((match = Lit_3_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
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
                Caches[Cache_AccessModifierBase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_MutationModifier = 14;

        public virtual Match MutationModifier(int start)
        {
            if (!Caches[Cache_MutationModifier].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    if ((match = Lit_mutating(start)) != null)
                    {
                        break;
                    }
                    match = Lit_nonmutating(start);
                    break;
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
                for (;;) // ---Choice---
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Optional(next, Statements(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = IfDirective(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
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
                for (;;) // ---Sequence---
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
                    match = Match.Optional(next, Statements(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = ElseDirective(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Statements(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                match = Lit_7_/*'#if'*/(start);
                Caches[Cache_IfDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseifDirective = 22;

        public virtual Match ElseifDirective(int start)
        {
            if (!Caches[Cache_ElseifDirective].Already(start, out var match))
            {
                match = Lit_8_/*'#elseif'*/(start);
                Caches[Cache_ElseifDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseDirective = 23;

        public virtual Match ElseDirective(int start)
        {
            if (!Caches[Cache_ElseDirective].Already(start, out var match))
            {
                match = Lit_9_/*'#else'*/(start);
                Caches[Cache_ElseDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EndifDirective = 24;

        public virtual Match EndifDirective(int start)
        {
            if (!Caches[Cache_EndifDirective].Already(start, out var match))
            {
                match = Lit_10_/*'#endif'*/(start);
                Caches[Cache_EndifDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CompilationConditionPrimary = 25;

        public virtual Match CompilationConditionPrimary(int start)
        {
            if (!Caches[Cache_CompilationConditionPrimary].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next)) == null)
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
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_11_/*'!'*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = CompilationConditionPrimary(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                for (;;) // ---Choice---
                                {
                                    if ((match = Lit_12_/*'||'*/(next2)) != null)
                                    {
                                        break;
                                    }
                                    match = Lit_13_/*'&&'*/(next2);
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_os(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*'('*/(next)) == null)
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
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_arch(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*'('*/(next2)) == null)
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
                        if ((match = Lit_3_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_swift(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_2_/*'('*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        for (;;) // ---Choice---
                        {
                            if ((match = Lit_14_/*'>='*/(next3)) != null)
                            {
                                break;
                            }
                            match = Lit_15_/*'<'*/(next3);
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
                        if ((match = Lit_3_/*')'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_compiler(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = Lit_2_/*'('*/(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        for (;;) // ---Choice---
                        {
                            if ((match = Lit_14_/*'>='*/(next4)) != null)
                            {
                                break;
                            }
                            match = Lit_15_/*'<'*/(next4);
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
                        if ((match = Lit_3_/*')'*/(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches4);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_canImport(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = Lit_2_/*'('*/(next5)) == null)
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
                        if ((match = Lit_3_/*')'*/(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches5);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_targetEnvironment(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = Lit_2_/*'('*/(next6)) == null)
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
                        if ((match = Lit_3_/*')'*/(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches6);
                    }
                    break;
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
                for (;;) // ---Choice---
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
                    /*>>> Error */
                        throw new NotImplementedException();
                    /*<<< Error */
                    break;
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
                for (;;) // ---Choice---
                {
                    if ((match = Lit_i386(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_x86_64(start)) != null)
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
                    /*>>> Error */
                        throw new NotImplementedException();
                    /*<<< Error */
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = DecimalDigits(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_16_/*'.'*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                Caches[Cache_ModuleName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Environment = 32;

        public virtual Match Environment(int start)
        {
            if (!Caches[Cache_Environment].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    if ((match = Lit_simulator(start)) != null)
                    {
                        break;
                    }
                    match = Lit_macCatalyst(start);
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_17_/*'#sourceLocation'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*'('*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_18_/*'file:'*/(next2)) == null)
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
                        if ((match = Lit_4_/*','*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_19_/*'line:'*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_3_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_20_/*'#error'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*'('*/(next)) == null)
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
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_21_/*'#warning'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*'('*/(next2)) == null)
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
                        if ((match = Lit_3_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                Caches[Cache_DiagnosticMessage].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConstantDeclaration = 38;

        public virtual Match ConstantDeclaration(int start)
        {
            if (!Caches[Cache_ConstantDeclaration].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = PatternInitializer(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_22_/*'='*/(next)) == null)
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_22_/*'='*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_23_/*'{'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            if ((match = UnionStyleEnumMember(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*'}'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = UnionStyleEnumCase(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_23_/*'{'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> OneOrMore */
                        var oomMatches = new List<Match>();
                        var oomNext = next;
                        for (;;)
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
                            match = Match.Success(next, oomMatches);
                        }
                    /*<<< OneOrMore */
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*'}'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = RawValueStyleEnumCase(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_22_/*'='*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
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
                Caches[Cache_EnumCaseName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ExtensionDeclaration = 59;

        public virtual Match ExtensionDeclaration(int start)
        {
            if (!Caches[Cache_ExtensionDeclaration].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> OneOrMore */
                            var oomMatches = new List<Match>();
                            var oomNext = next2;
                            for (;;)
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
                                match = Match.Success(next2, oomMatches);
                            }
                        /*<<< OneOrMore */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_24_/*'}'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
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
                for (;;) // ---Choice---
                {
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = FunctionHead(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    if ((match = Lit_throws(start)) != null)
                    {
                        break;
                    }
                    match = Lit_rethrows(start);
                    break;
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
                for (;;) // ---Choice---
                {
                    if ((match = Name(start)) != null)
                    {
                        break;
                    }
                    if ((match = Operator(start)) != null)
                    {
                        break;
                    }
                    /*>>> Error */
                        throw new NotImplementedException();
                    /*<<< Error */
                    break;
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next2)) == null)
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
                        if ((match = Lit_3_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    /*>>> Error */
                        throw new NotImplementedException();
                    /*<<< Error */
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = Parameter(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        if ((match = Lit_25_/*'...'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches4);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches5);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        if ((match = Lit_25_/*'...'*/(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches6);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next7 = start;
                    var matches7 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches7);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    /*>>> Error */
                        throw new NotImplementedException();
                    /*<<< Error */
                    break;
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_26_/*'->'*/(next)) == null)
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_26_/*'->'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_22_/*'='*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_5_/*':'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Attributes(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Lit___owned(next));
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_5_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, Attributes(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, Lit___owned(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, Lit_inout(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_5_/*':'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
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
                for (;;) // ---Choice---
                {
                    if ((match = CodeBlock(start)) != null)
                    {
                        break;
                    }
                    /*>>> Epsilon */
                        match = Match.Success(start);
                    /*<<< Epsilon */
                    break;
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        for (;;) // ---Choice---
                        {
                            if ((match = Lit_27_/*'?'*/(next)) != null)
                            {
                                break;
                            }
                            match = Lit_11_/*'!'*/(next);
                            break;
                        }
                        match = Match.Optional(next, match);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                Caches[Cache_InitializerBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorDeclaration = 79;

        public virtual Match OperatorDeclaration(int start)
        {
            if (!Caches[Cache_OperatorDeclaration].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                for (;;) // ---Sequence---
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
                    if ((match = _space_(next)) == null)
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
                    var next2 = next;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_5_/*':'*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    if ((match = _space_(next)) == null)
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
                    var next2 = next;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_5_/*':'*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    if ((match = _space_(next)) == null)
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
                    match = Match.Optional(next, InfixOperatorGroup(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_5_/*':'*/(next)) == null)
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
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_4_/*','*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = OperatorRestriction(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_23_/*'{'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            if ((match = PrecedenceGroupAttribute(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*'}'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
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
                for (;;) // ---Sequence---
                {
                    for (;;) // ---Choice---
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
                    if ((match = Lit_5_/*':'*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_assignment(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_5_/*':'*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_associativity(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_5_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    for (;;) // ---Choice---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = PrecedenceGroupName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                Caches[Cache_ProtocolName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolBody = 96;

        public virtual Match ProtocolBody(int start)
        {
            if (!Caches[Cache_ProtocolBody].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, ProtocolMembers(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
                Caches[Cache_ProtocolMembers].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolMember = 98;

        public virtual Match ProtocolMember(int start)
        {
            if (!Caches[Cache_ProtocolMember].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    if ((match = ProtocolMemberDeclaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
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
                for (;;) // ---Choice---
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                Caches[Cache_StructName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StructBody = 107;

        public virtual Match StructBody(int start)
        {
            if (!Caches[Cache_StructBody].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, StructMembers(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
                Caches[Cache_StructMembers].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StructMember = 109;

        public virtual Match StructMember(int start)
        {
            if (!Caches[Cache_StructMember].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_26_/*'->'*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_22_/*'='*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches4);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches5);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches6);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next7 = start;
                    var matches7 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = VariableDeclarationHead(next7)) == null)
                        {
                            break;
                        }
                        matches7.Add(match);
                        next7 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches7.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches7);
                    }
                    break;
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                Caches[Cache_VariableName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GetterSetterBlock = 119;

        public virtual Match GetterSetterBlock(int start)
        {
            if (!Caches[Cache_GetterSetterBlock].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next)) == null)
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
                        if ((match = Lit_24_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next2)) == null)
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
                        if ((match = Lit_24_/*'}'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = CodeBlock(start);
                    break;
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = MutationPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit__modify(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_2_/*'('*/(next)) == null)
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
                    if ((match = Lit_3_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next)) == null)
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
                        if ((match = Lit_24_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next2)) == null)
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
                        if ((match = Lit_24_/*'}'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next)) == null)
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
                        if ((match = Lit_24_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next2)) == null)
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
                        if ((match = Lit_24_/*'}'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
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
                for (;;) // ---Sequence---
                {
                    if ((match = ImportPathIdentifier(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_16_/*'.'*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    if ((match = Name(start)) != null)
                    {
                        break;
                    }
                    match = Operator(start);
                    break;
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_23_/*'{'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, ClassMembers(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*'}'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
                Caches[Cache_ClassMembers].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClassMember = 140;

        public virtual Match ClassMember(int start)
        {
            if (!Caches[Cache_ClassMember].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
                Caches[Cache_BinaryExpressions].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BinaryExpression = 144;

        public virtual Match BinaryExpression(int start)
        {
            if (!Caches[Cache_BinaryExpression].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = TypeCastingOperator(start);
                    break;
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = InOutExpression(start);
                    break;
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = PrimaryExpression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            if ((match = PostfixAppendix(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
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
                Caches[Cache_PostfixAppendix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionCall = 149;

        public virtual Match FunctionCall(int start)
        {
            if (!Caches[Cache_FunctionCall].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
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
                Caches[Cache_FunctionCall].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionCallArgumentClause = 150;

        public virtual Match FunctionCallArgumentClause(int start)
        {
            if (!Caches[Cache_FunctionCallArgumentClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_2_/*'('*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, FunctionCallArgumentList(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_3_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = FunctionCallArgument(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    var next2 = next;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_5_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    for (;;) // ---Choice---
                    {
                        if ((match = Expression(next)) != null)
                        {
                            break;
                        }
                        var next3 = next;
                        var matches3 = new List<Match>();
                        for (;;) // ---Sequence---
                        {
                            if ((match = _space_(next3)) == null)
                            {
                                break;
                            }
                            matches3.Add(match);
                            next3 = match.Next;
                            if ((match = Operator(next3)) == null)
                            {
                                break;
                            }
                            matches3.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success(start, matches3);
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_5_/*':'*/(next)) == null)
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_5_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = And_(next2, Lit_23_/*'{'*/(next2))) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = LabeledTrailingClosure(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            if ((match = LabeledTrailingClosure(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Name(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_5_/*':'*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_16_/*'.'*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_2_/*'('*/(next)) == null)
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
                    if ((match = Lit_3_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
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
                for (;;) // ---Sequence---
                {
                    if ((match = Name(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_5_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_16_/*'.'*/(next)) == null)
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_16_/*'.'*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_16_/*'.'*/(next3)) == null)
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
                        match = Match.Success(start, matches3);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_16_/*'.'*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                Caches[Cache_OptionalChaining].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrimaryExpression = 165;

        public virtual Match PrimaryExpression(int start)
        {
            if (!Caches[Cache_PrimaryExpression].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
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
                Caches[Cache_PrimaryExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LiteralExpression = 166;

        public virtual Match LiteralExpression(int start)
        {
            if (!Caches[Cache_LiteralExpression].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                Caches[Cache_LiteralExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Literal = 167;

        public virtual Match Literal(int start)
        {
            if (!Caches[Cache_Literal].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                for (;;) // ---Sequence---
                {
                    match = Match.Optional(next, Lit_38_/*'-'*/(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    for (;;) // ---Choice---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    if ((match = Lit_true(start)) != null)
                    {
                        break;
                    }
                    match = Lit_false(start);
                    break;
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
                Caches[Cache_NilLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArrayLiteral = 171;

        public virtual Match ArrayLiteral(int start)
        {
            if (!Caches[Cache_ArrayLiteral].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = ArrayLiteralItem(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Lit_4_/*','*/(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                Caches[Cache_ArrayLiteralItem].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DictionaryLiteral = 174;

        public virtual Match DictionaryLiteral(int start)
        {
            if (!Caches[Cache_DictionaryLiteral].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_29_/*'['*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_5_/*':'*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = DictionaryLiteralItem(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Lit_4_/*','*/(next));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_5_/*':'*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_39_/*'#colorLiteral'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_2_/*'('*/(next)) == null)
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
                        if ((match = Lit_5_/*':'*/(next)) == null)
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
                        if ((match = Lit_4_/*','*/(next)) == null)
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
                        if ((match = Lit_5_/*':'*/(next)) == null)
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
                        if ((match = Lit_4_/*','*/(next)) == null)
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
                        if ((match = Lit_5_/*':'*/(next)) == null)
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
                        if ((match = Lit_4_/*','*/(next)) == null)
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
                        if ((match = Lit_5_/*':'*/(next)) == null)
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
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_40_/*'#fileLiteral'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_2_/*'('*/(next2)) == null)
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
                        if ((match = Lit_5_/*':'*/(next2)) == null)
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
                        if ((match = Lit_3_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_41_/*'#imageLiteral'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_2_/*'('*/(next3)) == null)
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
                        if ((match = Lit_5_/*':'*/(next3)) == null)
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
                        if ((match = Lit_3_/*')'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_self(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_16_/*'.'*/(next)) == null)
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_self(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_16_/*'.'*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches4);
                    }
                    break;
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_super(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_16_/*'.'*/(next)) == null)
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_super(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_16_/*'.'*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches3);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_23_/*'{'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, ClosureSignature(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Statements(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_24_/*'}'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next2)) == null)
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
                        if ((match = Lit_3_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = IdentifierList(start);
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = ClosureParameter(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        if ((match = Lit_25_/*'...'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = ClosureParameterName(start);
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = Name(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = CaptureListItem(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    if ((match = Lit_weak(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_unowned(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var next2 = next;
                        var matches2 = new List<Match>();
                        for (;;) // ---Sequence---
                        {
                            if ((match = Lit_2_/*'('*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            for (;;) // ---Choice---
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
                            if ((match = Lit_3_/*')'*/(next2)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success(start, matches2);
                        }
                        match = Match.Optional(next, match);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_2_/*'('*/(next)) == null)
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
                    if ((match = Lit_3_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next2)) == null)
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
                        if ((match = Lit_3_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = TupleElement(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> OneOrMore */
                        var oomMatches = new List<Match>();
                        var oomNext = next;
                        for (;;)
                        {
                            var next2 = oomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
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
                            match = Match.Success(next, oomMatches);
                        }
                    /*<<< OneOrMore */
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    var next2 = next;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_5_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_16_/*'.'*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit__(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_42_/*'\\'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Type(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_16_/*'.'*/(next)) == null)
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = KeyPathComponent(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_16_/*'.'*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
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
                Caches[Cache_KeyPathComponent].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathPostfixes = 200;

        public virtual Match KeyPathPostfixes(int start)
        {
            if (!Caches[Cache_KeyPathPostfixes].Already(start, out var match))
            {
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
                Caches[Cache_KeyPathPostfixes].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathPostfix = 201;

        public virtual Match KeyPathPostfix(int start)
        {
            if (!Caches[Cache_KeyPathPostfix].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    if ((match = Lit_27_/*'?'*/(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_11_/*'!'*/(start)) != null)
                    {
                        break;
                    }
                    if ((match = Lit_self(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_43_/*'#selector'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*'('*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    for (;;) // ---Choice---
                    {
                        if ((match = Lit_44_/*'getter:'*/(next)) != null)
                        {
                            break;
                        }
                        match = Lit_45_/*'setter:'*/(next);
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
                    if ((match = Lit_3_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_46_/*'#keyPath'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*'('*/(next)) == null)
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
                    if ((match = Lit_3_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_try(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    for (;;) // ---Choice---
                    {
                        if ((match = Lit_27_/*'?'*/(next)) != null)
                        {
                            break;
                        }
                        match = Lit_11_/*'!'*/(next);
                        break;
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                match = Lit_22_/*'='*/(start);
                Caches[Cache_AssignmentOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeCastingOperator = 206;

        public virtual Match TypeCastingOperator(int start)
        {
            if (!Caches[Cache_TypeCastingOperator].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        for (;;) // ---Choice---
                        {
                            if ((match = Lit_27_/*'?'*/(next2)) != null)
                            {
                                break;
                            }
                            match = Lit_11_/*'!'*/(next2);
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        for (;;) // ---Choice---
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
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> Before */
                        throw new NotImplementedException();
                    /*<<< Before */
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_27_/*'?'*/(next)) == null)
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
                    if ((match = Lit_5_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = _space_(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        /*>>> Before */
                            throw new NotImplementedException();
                        /*<<< Before */
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    break;
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
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> Before */
                        throw new NotImplementedException();
                    /*<<< Before */
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
                    match = Match.Success(start, matches);
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
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_PostfixOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Operator = 211;

        public virtual Match Operator(int start)
        {
            if (!Caches[Cache_Operator].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = OperatorHead(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        /*>>> ZeroOrMore */
                            var zomMatches = new List<Match>();
                            var zomNext = next;
                            for (;;)
                            {
                                if ((match = OperatorCharacter(zomNext)) == null)
                                {
                                    break;
                                }
                                zomMatches.Add(match);
                                zomNext = match.Next;
                            }
                            match = Match.Success(next, zomMatches);
                        /*<<< ZeroOrMore */
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = DotOperatorHead(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> OneOrMore */
                            var oomMatches = new List<Match>();
                            var oomNext = next2;
                            for (;;)
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
                                match = Match.Success(next2, oomMatches);
                            }
                        /*<<< OneOrMore */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_Operator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorHead = 212;

        public virtual Match OperatorHead(int start)
        {
            if (!Caches[Cache_OperatorHead].Already(start, out var match))
            {
                match = CharacterSet_(start, "/=-+!*%<>&|^~?");
                Caches[Cache_OperatorHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorCharacter = 213;

        public virtual Match OperatorCharacter(int start)
        {
            if (!Caches[Cache_OperatorCharacter].Already(start, out var match))
            {
                match = OperatorHead(start);
                Caches[Cache_OperatorCharacter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DotOperatorHead = 214;

        public virtual Match DotOperatorHead(int start)
        {
            if (!Caches[Cache_DotOperatorHead].Already(start, out var match))
            {
                match = CharacterExact_(start, '.');
                Caches[Cache_DotOperatorHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DotOperatorCharacter = 215;

        public virtual Match DotOperatorCharacter(int start)
        {
            if (!Caches[Cache_DotOperatorCharacter].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    if ((match = CharacterExact_(start, '.')) != null)
                    {
                        break;
                    }
                    match = OperatorCharacter(start);
                    break;
                }
                Caches[Cache_DotOperatorCharacter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Puncts = 216;

        public virtual Match Puncts(int start)
        {
            if (!Caches[Cache_Puncts].Already(start, out var match))
            {
                match = CharacterSet_(start, ",;:");
                Caches[Cache_Puncts].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SpaceBefore = 217;

        public virtual Match SpaceBefore(int start)
        {
            if (!Caches[Cache_SpaceBefore].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                Caches[Cache_SpaceBefore].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SpaceAfter = 218;

        public virtual Match SpaceAfter(int start)
        {
            if (!Caches[Cache_SpaceAfter].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                Caches[Cache_SpaceAfter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericParameterClause = 219;

        public virtual Match GenericParameterClause(int start)
        {
            if (!Caches[Cache_GenericParameterClause].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_15_/*'<'*/(next)) == null)
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
                        if ((match = Lit_47_/*'>'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_15_/*'<'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_GenericParameterClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericParameters = 220;

        public virtual Match GenericParameters(int start)
        {
            if (!Caches[Cache_GenericParameters].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = GenericParameter(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_GenericParameters].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericParameter = 221;

        public virtual Match GenericParameter(int start)
        {
            if (!Caches[Cache_GenericParameter].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = TypeName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_5_/*':'*/(next)) == null)
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = TypeName(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_5_/*':'*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = TypeName(start)) != null)
                    {
                        break;
                    }
                    /*>>> Error */
                        throw new NotImplementedException();
                    /*<<< Error */
                    break;
                }
                Caches[Cache_GenericParameter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericWhereClause = 222;

        public virtual Match GenericWhereClause(int start)
        {
            if (!Caches[Cache_GenericWhereClause].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_where(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = And_(next3, Lit_where(next3))) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_GenericWhereClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RequirementList = 223;

        public virtual Match RequirementList(int start)
        {
            if (!Caches[Cache_RequirementList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = Requirement(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_RequirementList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Requirement = 224;

        public virtual Match Requirement(int start)
        {
            if (!Caches[Cache_Requirement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = TypeIdentifier(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_5_/*':'*/(next)) == null)
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = TypeIdentifier(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_5_/*':'*/(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = TypeIdentifier(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_48_/*'=='*/(next3)) == null)
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
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_Requirement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericArgumentClause = 225;

        public virtual Match GenericArgumentClause(int start)
        {
            if (!Caches[Cache_GenericArgumentClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_15_/*'<'*/(next)) == null)
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
                    if ((match = Lit_47_/*'>'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_GenericArgumentClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericArguments = 226;

        public virtual Match GenericArguments(int start)
        {
            if (!Caches[Cache_GenericArguments].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = GenericArgument(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_GenericArguments].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GenericArgument = 227;

        public virtual Match GenericArgument(int start)
        {
            if (!Caches[Cache_GenericArgument].Already(start, out var match))
            {
                match = Type(start);
                Caches[Cache_GenericArgument].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StringLiteral = 228;

        public virtual Match StringLiteral(int start)
        {
            if (!Caches[Cache_StringLiteral].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    if ((match = InterpolatedStringLiteral(start)) != null)
                    {
                        break;
                    }
                    match = StaticStringLiteral(start);
                    break;
                }
                Caches[Cache_StringLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StaticStringLiteral = 229;

        public virtual Match StaticStringLiteral(int start)
        {
            if (!Caches[Cache_StaticStringLiteral].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_StaticStringLiteral].Cache(start, match);
            }
            return match;
        }

        public virtual Match InterpolatedStringLiteral(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                if (match != null)
                {
                    break;
                }
                var next2 = start;
                var matches2 = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches2);
                }
                break;
            }
            return match;
        }

        public virtual Match QuotedText(int start)
        {
            Match match;
            /*>>> OneOrMore */
                var oomMatches = new List<Match>();
                var oomNext = start;
                for (;;)
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
                    match = Match.Success(start, oomMatches);
                }
            /*<<< OneOrMore */
            return match;
        }

        public virtual Match QuotedTextItem(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                if ((match = EscapedCharacter(start)) != null)
                {
                    break;
                }
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    /*>>> Not */
                        for (;;) // ---Choice---
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
                    /*<<< Not */
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
                    match = Match.Success(start, matches);
                }
                break;
            }
            return match;
        }

        public virtual Match MultilineQuotedText(int start)
        {
            Match match;
            /*>>> OneOrMore */
                var oomMatches = new List<Match>();
                var oomNext = start;
                for (;;)
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
                    match = Match.Success(start, oomMatches);
                }
            /*<<< OneOrMore */
            return match;
        }

        public virtual Match MultilineQuotedTextItem(int start)
        {
            Match match;
            for (;;) // ---Choice---
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
                for (;;) // ---Sequence---
                {
                    /*>>> Not */
                        for (;;) // ---Choice---
                        {
                            if ((match = CharacterExact_(next, '\\')) != null)
                            {
                                break;
                            }
                            match = MultilineStringLiteralClosingDelimiter(next);
                            break;
                        }
                        match = Not_(next, match);
                    /*<<< Not */
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
                    match = Match.Success(start, matches);
                }
                break;
            }
            return match;
        }

        public virtual Match InterpolatedText(int start)
        {
            Match match;
            /*>>> OneOrMore */
                var oomMatches = new List<Match>();
                var oomNext = start;
                for (;;)
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
                    match = Match.Success(start, oomMatches);
                }
            /*<<< OneOrMore */
            return match;
        }

        public virtual Match InterpolatedTextItem(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    if ((match = Lit_3_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                if (match != null)
                {
                    break;
                }
                match = QuotedTextItem(start);
                break;
            }
            return match;
        }

        public virtual Match MultilineInterpolatedText(int start)
        {
            Match match;
            /*>>> OneOrMore */
                var oomMatches = new List<Match>();
                var oomNext = start;
                for (;;)
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
                    match = Match.Success(start, oomMatches);
                }
            /*<<< OneOrMore */
            return match;
        }

        public virtual Match MultilineInterpolatedTextItem(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    if ((match = Lit_3_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                if (match != null)
                {
                    break;
                }
                match = MultilineQuotedTextItem(start);
                break;
            }
            return match;
        }

        public virtual Match EscapedCharacter(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                if (match != null)
                {
                    break;
                }
                var next2 = start;
                var matches2 = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches2);
                }
                if (match != null)
                {
                    break;
                }
                var next3 = start;
                var matches3 = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches3);
                }
                if (match != null)
                {
                    break;
                }
                var next4 = start;
                var matches4 = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches4);
                }
                if (match != null)
                {
                    break;
                }
                var next5 = start;
                var matches5 = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches5);
                }
                if (match != null)
                {
                    break;
                }
                var next6 = start;
                var matches6 = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches6);
                }
                if (match != null)
                {
                    break;
                }
                var next7 = start;
                var matches7 = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches7);
                }
                if (match != null)
                {
                    break;
                }
                var next8 = start;
                var matches8 = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches8);
                }
                break;
            }
            return match;
        }

        public virtual Match EscapeSequence(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match EscapedNewline(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match StringLiteralOpeningDelimiter(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
            {
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match StringLiteralClosingDelimiter(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match MultilineStringLiteralOpeningDelimiter(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
            {
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match MultilineStringLiteralClosingDelimiter(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match ExtendedStringLiteralDelimiter(int start)
        {
            Match match;
            /*>>> OneOrMore */
                var oomMatches = new List<Match>();
                var oomNext = start;
                for (;;)
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
                    match = Match.Success(start, oomMatches);
                }
            /*<<< OneOrMore */
            return match;
        }

        protected const int Cache_Name = 247;

        public virtual Match Name(int start)
        {
            if (!Caches[Cache_Name].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Name].Cache(start, match);
            }
            return match;
        }

        public virtual Match Identifier(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = IdentifierHead(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            if ((match = IdentifierCharacter(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                if (match != null)
                {
                    break;
                }
                var next2 = start;
                var matches2 = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = CharacterExact_(next2, '`')) == null)
                    {
                        break;
                    }
                    matches2.Add(match);
                    next2 = match.Next;
                    if ((match = IdentifierHead(next2)) == null)
                    {
                        break;
                    }
                    matches2.Add(match);
                    next2 = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches2 = new List<Match>();
                        var zomNext2 = next2;
                        for (;;)
                        {
                            if ((match = IdentifierCharacter(zomNext2)) == null)
                            {
                                break;
                            }
                            zomMatches2.Add(match);
                            zomNext2 = match.Next;
                        }
                        match = Match.Success(next2, zomMatches2);
                    /*<<< ZeroOrMore */
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
                    match = Match.Success(start, matches2);
                }
                if (match != null)
                {
                    break;
                }
                if ((match = ImplicitParameterName(start)) != null)
                {
                    break;
                }
                match = PropertyWrapperProjection(start);
                break;
            }
            return match;
        }

        public virtual Match IdentifierHead(int start)
        {
            Match match;
            for (;;) // ---Choice---
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
            return match;
        }

        public virtual Match IdentifierCharacter(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                if ((match = IdentifierHead(start)) != null)
                {
                    break;
                }
                match = CharacterRange_(start, '0', '9');
                break;
            }
            return match;
        }

        protected const int Cache_More = 251;

        public virtual Match More(int start)
        {
            if (!Caches[Cache_More].Already(start, out var match))
            {
                match = IdentifierCharacter(start);
                Caches[Cache_More].Cache(start, match);
            }
            return match;
        }

        public virtual Match ImplicitParameterName(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match PropertyWrapperProjection(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
            {
                if ((match = CharacterExact_(next, '$')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = next;
                    for (;;)
                    {
                        if ((match = IdentifierCharacter(oomNext)) == null)
                        {
                            break;
                        }
                        oomMatches.Add(match);
                        oomNext = match.Next;
                    }
                    if (oomMatches.Count > 0)
                    {
                        match = Match.Success(next, oomMatches);
                    }
                /*<<< OneOrMore */
                if (match == null)
                {
                    break;
                }
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success(start, matches);
            }
            return match;
        }

        protected const int Cache_IntegerLiteral = 254;

        public virtual Match IntegerLiteral(int start)
        {
            if (!Caches[Cache_IntegerLiteral].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                Caches[Cache_IntegerLiteral].Cache(start, match);
            }
            return match;
        }

        public virtual Match BinaryLiteral(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                /*>>> ZeroOrMore */
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    for (;;)
                    {
                        if ((match = BinaryLiteralCharacter(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success(next, zomMatches);
                /*<<< ZeroOrMore */
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match BinaryDigit(int start)
        {
            Match match;
            match = CharacterSet_(start, "01");
            return match;
        }

        public virtual Match BinaryLiteralCharacter(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                if ((match = BinaryDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            return match;
        }

        public virtual Match OctalLiteral(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                /*>>> ZeroOrMore */
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    for (;;)
                    {
                        if ((match = OctalLiteralCharacter(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success(next, zomMatches);
                /*<<< ZeroOrMore */
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match OctalDigit(int start)
        {
            Match match;
            match = CharacterRange_(start, '0', '7');
            return match;
        }

        public virtual Match OctalLiteralCharacter(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                if ((match = OctalDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            return match;
        }

        public virtual Match DecimalLiteral(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
            {
                if ((match = DecimalDigit(next)) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                /*>>> ZeroOrMore */
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    for (;;)
                    {
                        if ((match = DecimalLiteralCharacter(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success(next, zomMatches);
                /*<<< ZeroOrMore */
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success(start, matches);
            }
            return match;
        }

        protected const int Cache_NonzeroDecimalLiteral = 262;

        public virtual Match NonzeroDecimalLiteral(int start)
        {
            if (!Caches[Cache_NonzeroDecimalLiteral].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            if ((match = CharacterExact_(zomNext, '0')) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    next = match.Next;
                    if ((match = DecimalDigitExcept0(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches2 = new List<Match>();
                        var zomNext2 = next;
                        for (;;)
                        {
                            if ((match = DecimalDigit(zomNext2)) == null)
                            {
                                break;
                            }
                            zomMatches2.Add(match);
                            zomNext2 = match.Next;
                        }
                        match = Match.Success(next, zomMatches2);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_NonzeroDecimalLiteral].Cache(start, match);
            }
            return match;
        }

        public virtual Match DecimalDigit(int start)
        {
            Match match;
            match = CharacterRange_(start, '0', '9');
            return match;
        }

        public virtual Match DecimalDigitExcept0(int start)
        {
            Match match;
            match = CharacterRange_(start, '1', '9');
            return match;
        }

        public virtual Match DecimalLiteralCharacter(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                if ((match = DecimalDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            return match;
        }

        public virtual Match HexadecimalLiteral(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                /*>>> ZeroOrMore */
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    for (;;)
                    {
                        if ((match = HexadecimalLiteralCharacter(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success(next, zomMatches);
                /*<<< ZeroOrMore */
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match HexadecimalDigit(int start)
        {
            Match match;
            for (;;) // ---Choice---
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
            return match;
        }

        public virtual Match HexDigit(int start)
        {
            Match match;
            match = HexadecimalDigit(start);
            return match;
        }

        public virtual Match HexadecimalLiteralCharacter(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                if ((match = HexadecimalDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            return match;
        }

        protected const int Cache_FloatingPointLiteral = 270;

        public virtual Match FloatingPointLiteral(int start)
        {
            if (!Caches[Cache_FloatingPointLiteral].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    break;
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
            for (;;) // ---Sequence---
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match DecimalExponent(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match FloatingPointE(int start)
        {
            Match match;
            match = CharacterSet_(start, "eE");
            return match;
        }

        public virtual Match HexadecimalFraction(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                /*>>> ZeroOrMore */
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    for (;;)
                    {
                        if ((match = HexadecimalLiteralCharacter(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success(next, zomMatches);
                /*<<< ZeroOrMore */
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match HexadecimalExponent(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match FloatingPointP(int start)
        {
            Match match;
            match = CharacterSet_(start, "pP");
            return match;
        }

        public virtual Match Sign(int start)
        {
            Match match;
            match = CharacterSet_(start, "+-");
            return match;
        }

        protected const int Cache_DecimalDigits = 278;

        public virtual Match DecimalDigits(int start)
        {
            if (!Caches[Cache_DecimalDigits].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> OneOrMore */
                        var oomMatches = new List<Match>();
                        var oomNext = next;
                        for (;;)
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
                            match = Match.Success(next, oomMatches);
                        }
                    /*<<< OneOrMore */
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_DecimalDigits].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Pattern = 279;

        public virtual Match Pattern(int start)
        {
            if (!Caches[Cache_Pattern].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = PrimaryPattern(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        /*>>> ZeroOrMore */
                            var zomMatches = new List<Match>();
                            var zomNext = next;
                            for (;;)
                            {
                                if ((match = PatternPostfix(zomNext)) == null)
                                {
                                    break;
                                }
                                zomMatches.Add(match);
                                zomNext = match.Next;
                            }
                            match = Match.Success(next, zomMatches);
                        /*<<< ZeroOrMore */
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = PrimaryPattern(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> And */
                            for (;;) // ---Choice---
                            {
                                if ((match = Lit_27_/*'?'*/(next2)) != null)
                                {
                                    break;
                                }
                                if ((match = Lit_16_/*'.'*/(next2)) != null)
                                {
                                    break;
                                }
                                match = Lit_as(next2);
                                break;
                            }
                            match = And_(next2, match);
                        /*<<< And */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_Pattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrimaryPattern = 280;

        public virtual Match PrimaryPattern(int start)
        {
            if (!Caches[Cache_PrimaryPattern].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                Caches[Cache_PrimaryPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternPostfix = 281;

        public virtual Match PatternPostfix(int start)
        {
            if (!Caches[Cache_PatternPostfix].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                Caches[Cache_PatternPostfix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternOptional = 282;

        public virtual Match PatternOptional(int start)
        {
            if (!Caches[Cache_PatternOptional].Already(start, out var match))
            {
                match = Lit_27_/*'?'*/(start);
                Caches[Cache_PatternOptional].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternCase = 283;

        public virtual Match PatternCase(int start)
        {
            if (!Caches[Cache_PatternCase].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_16_/*'.'*/(next)) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_PatternCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternAs = 284;

        public virtual Match PatternAs(int start)
        {
            if (!Caches[Cache_PatternAs].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_as(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_PatternAs].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WildcardPattern = 285;

        public virtual Match WildcardPattern(int start)
        {
            if (!Caches[Cache_WildcardPattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = Lit__(next)) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_WildcardPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ValueBindingPattern = 286;

        public virtual Match ValueBindingPattern(int start)
        {
            if (!Caches[Cache_ValueBindingPattern].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_var(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_let(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches4);
                    }
                    break;
                }
                Caches[Cache_ValueBindingPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TuplePattern = 287;

        public virtual Match TuplePattern(int start)
        {
            if (!Caches[Cache_TuplePattern].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next2)) == null)
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
                        if ((match = Lit_3_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_TuplePattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TuplePatternElementList = 288;

        public virtual Match TuplePatternElementList(int start)
        {
            if (!Caches[Cache_TuplePatternElementList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = TuplePatternElement(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_TuplePatternElementList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TuplePatternElement = 289;

        public virtual Match TuplePatternElement(int start)
        {
            if (!Caches[Cache_TuplePatternElement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    var next2 = next;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_5_/*':'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_TuplePatternElement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EnumCasePattern = 290;

        public virtual Match EnumCasePattern(int start)
        {
            if (!Caches[Cache_EnumCasePattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    match = Match.Optional(next, TypeIdentifier(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_16_/*'.'*/(next)) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_EnumCasePattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IsPattern = 291;

        public virtual Match IsPattern(int start)
        {
            if (!Caches[Cache_IsPattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_IsPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BreakStatement = 292;

        public virtual Match BreakStatement(int start)
        {
            if (!Caches[Cache_BreakStatement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_break(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        /*>>> Not */
                            var next2 = next;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                for (;;) // ---Choice---
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
                                match = Match.Success(start, matches2);
                            }
                            match = Not_(next, match);
                        /*<<< Not */
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_break(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Not */
                            var next4 = next3;
                            var matches4 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                for (;;) // ---Choice---
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
                                match = Match.Success(start, matches4);
                            }
                            match = Not_(next3, match);
                        /*<<< Not */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_BreakStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConditionList = 293;

        public virtual Match ConditionList(int start)
        {
            if (!Caches[Cache_ConditionList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = Condition(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_ConditionList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Condition = 294;

        public virtual Match Condition(int start)
        {
            if (!Caches[Cache_Condition].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                Caches[Cache_Condition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseCondition = 295;

        public virtual Match CaseCondition(int start)
        {
            if (!Caches[Cache_CaseCondition].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_CaseCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OptionalBindingCondition = 296;

        public virtual Match OptionalBindingCondition(int start)
        {
            if (!Caches[Cache_OptionalBindingCondition].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_OptionalBindingCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AvailableCondition = 297;

        public virtual Match AvailableCondition(int start)
        {
            if (!Caches[Cache_AvailableCondition].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_49_/*'#available'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_2_/*'('*/(next)) == null)
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
                    if ((match = Lit_3_/*')'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_AvailableCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AvailabilityArguments = 298;

        public virtual Match AvailabilityArguments(int start)
        {
            if (!Caches[Cache_AvailabilityArguments].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = AvailabilityArgument(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_AvailabilityArguments].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AvailabilityArgument = 299;

        public virtual Match AvailabilityArgument(int start)
        {
            if (!Caches[Cache_AvailabilityArgument].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = Lit_6_/*'*'*/(start);
                    break;
                }
                Caches[Cache_AvailabilityArgument].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PlatformName = 300;

        public virtual Match PlatformName(int start)
        {
            if (!Caches[Cache_PlatformName].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                Caches[Cache_PlatformName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PlatformVersion = 301;

        public virtual Match PlatformVersion(int start)
        {
            if (!Caches[Cache_PlatformVersion].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = DecimalDigits(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_16_/*'.'*/(next2)) == null)
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
                        for (;;) // ---Sequence---
                        {
                            if ((match = Lit_16_/*'.'*/(next3)) == null)
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
                            match = Match.Success(start, matches3);
                        }
                        match = Match.Optional(next2, match);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_PlatformVersion].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ContinueStatement = 302;

        public virtual Match ContinueStatement(int start)
        {
            if (!Caches[Cache_ContinueStatement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_continue(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        /*>>> Not */
                            var next2 = next;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                for (;;) // ---Choice---
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
                                match = Match.Success(start, matches2);
                            }
                            match = Not_(next, match);
                        /*<<< Not */
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_continue(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Not */
                            var next4 = next3;
                            var matches4 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                for (;;) // ---Choice---
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
                                match = Match.Success(start, matches4);
                            }
                            match = Not_(next3, match);
                        /*<<< Not */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_ContinueStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DeferStatement = 303;

        public virtual Match DeferStatement(int start)
        {
            if (!Caches[Cache_DeferStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_DeferStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DoStatement = 304;

        public virtual Match DoStatement(int start)
        {
            if (!Caches[Cache_DoStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            if ((match = CatchClause(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_DoStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CatchClause = 305;

        public virtual Match CatchClause(int start)
        {
            if (!Caches[Cache_CatchClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_CatchClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CatchPatternList = 306;

        public virtual Match CatchPatternList(int start)
        {
            if (!Caches[Cache_CatchPatternList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = CatchPattern(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_CatchPatternList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CatchPattern = 307;

        public virtual Match CatchPattern(int start)
        {
            if (!Caches[Cache_CatchPattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_CatchPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GuardStatement = 308;

        public virtual Match GuardStatement(int start)
        {
            if (!Caches[Cache_GuardStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_GuardStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IfStatement = 309;

        public virtual Match IfStatement(int start)
        {
            if (!Caches[Cache_IfStatement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_if(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_IfStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseClause = 310;

        public virtual Match ElseClause(int start)
        {
            if (!Caches[Cache_ElseClause].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = And_(next3, Lit_else(next3))) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_ElseClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LabeledStatement = 311;

        public virtual Match LabeledStatement(int start)
        {
            if (!Caches[Cache_LabeledStatement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = StatementLabel(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = LoopStatement(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = StatementLabel(next2)) == null)
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = StatementLabel(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = SwitchStatement(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = StatementLabel(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = DoStatement(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches4);
                    }
                    break;
                }
                Caches[Cache_LabeledStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StatementLabel = 312;

        public virtual Match StatementLabel(int start)
        {
            if (!Caches[Cache_StatementLabel].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = LabelName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Lit_5_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_StatementLabel].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LabelName = 313;

        public virtual Match LabelName(int start)
        {
            if (!Caches[Cache_LabelName].Already(start, out var match))
            {
                match = Name(start);
                Caches[Cache_LabelName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ReturnStatement = 314;

        public virtual Match ReturnStatement(int start)
        {
            if (!Caches[Cache_ReturnStatement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_return(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        /*>>> Not */
                            var next2 = next;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                for (;;) // ---Choice---
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
                                match = Match.Success(start, matches2);
                            }
                            match = Not_(next, match);
                        /*<<< Not */
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_return(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Not */
                            var next4 = next3;
                            var matches4 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                for (;;) // ---Choice---
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
                                match = Match.Success(start, matches4);
                            }
                            match = Not_(next3, match);
                        /*<<< Not */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_ReturnStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchStatement = 315;

        public virtual Match SwitchStatement(int start)
        {
            if (!Caches[Cache_SwitchStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_SwitchStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchBody = 316;

        public virtual Match SwitchBody(int start)
        {
            if (!Caches[Cache_SwitchBody].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next2)) == null)
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
                        if ((match = Lit_24_/*'}'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_SwitchBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchCases = 317;

        public virtual Match SwitchCases(int start)
        {
            if (!Caches[Cache_SwitchCases].Already(start, out var match))
            {
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
                Caches[Cache_SwitchCases].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchCase = 318;

        public virtual Match SwitchCase(int start)
        {
            if (!Caches[Cache_SwitchCase].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = CaseLabel(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Statements(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = DefaultLabel(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Statements(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = ConditionalSwitchCase(start);
                    break;
                }
                Caches[Cache_SwitchCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseLabel = 319;

        public virtual Match CaseLabel(int start)
        {
            if (!Caches[Cache_CaseLabel].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        if ((match = Lit_5_/*':'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_CaseLabel].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseItemList = 320;

        public virtual Match CaseItemList(int start)
        {
            if (!Caches[Cache_CaseItemList].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = CaseItem(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        /*>>> ZeroOrMore */
                            var zomMatches = new List<Match>();
                            var zomNext = next;
                            for (;;)
                            {
                                var next2 = zomNext;
                                var matches2 = new List<Match>();
                                for (;;) // ---Sequence---
                                {
                                    if ((match = Lit_4_/*','*/(next2)) == null)
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
                                    match = Match.Success(start, matches2);
                                }
                                if (match == null)
                                {
                                    break;
                                }
                                zomMatches.Add(match);
                                zomNext = match.Next;
                            }
                            match = Match.Success(next, zomMatches);
                        /*<<< ZeroOrMore */
                        matches.Add(match);
                        next = match.Next;
                        if ((match = And_(next, Lit_5_/*':'*/(next))) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = CaseItem(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> OneOrMore */
                            var oomMatches = new List<Match>();
                            var oomNext = next3;
                            for (;;)
                            {
                                var next4 = oomNext;
                                var matches4 = new List<Match>();
                                for (;;) // ---Sequence---
                                {
                                    if ((match = Lit_4_/*','*/(next4)) == null)
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
                                    match = Match.Success(start, matches4);
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
                                match = Match.Success(next3, oomMatches);
                            }
                        /*<<< OneOrMore */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Not_(next3, Lit_5_/*':'*/(next3))) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = CaseItem(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches5);
                    }
                    break;
                }
                Caches[Cache_CaseItemList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseItem = 321;

        public virtual Match CaseItem(int start)
        {
            if (!Caches[Cache_CaseItem].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_CaseItem].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DefaultLabel = 322;

        public virtual Match DefaultLabel(int start)
        {
            if (!Caches[Cache_DefaultLabel].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    if ((match = Lit_5_/*':'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_DefaultLabel].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConditionalSwitchCase = 323;

        public virtual Match ConditionalSwitchCase(int start)
        {
            if (!Caches[Cache_ConditionalSwitchCase].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_ConditionalSwitchCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchIfDirectiveClause = 324;

        public virtual Match SwitchIfDirectiveClause(int start)
        {
            if (!Caches[Cache_SwitchIfDirectiveClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_SwitchIfDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchElseifDirectiveClauses = 325;

        public virtual Match SwitchElseifDirectiveClauses(int start)
        {
            if (!Caches[Cache_SwitchElseifDirectiveClauses].Already(start, out var match))
            {
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
                Caches[Cache_SwitchElseifDirectiveClauses].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchElseDirectiveClause = 326;

        public virtual Match SwitchElseDirectiveClause(int start)
        {
            if (!Caches[Cache_SwitchElseDirectiveClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_SwitchElseDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WhileStatement = 327;

        public virtual Match WhileStatement(int start)
        {
            if (!Caches[Cache_WhileStatement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_while(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_WhileStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Statement = 328;

        public virtual Match Statement(int start)
        {
            if (!Caches[Cache_Statement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = ControlTransferStatement(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Lit_50_/*';'*/(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = CompilerControlStatement(start)) != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = LoopStatement(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, Lit_50_/*';'*/(next2));
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = BranchStatement(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        match = Match.Optional(next3, Lit_50_/*';'*/(next3));
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = DoStatement(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        match = Match.Optional(next4, Lit_50_/*';'*/(next4));
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches4);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = DeferStatement(next5)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        match = Match.Optional(next5, Lit_50_/*';'*/(next5));
                        matches5.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches5);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = LabeledStatement(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        match = Match.Optional(next6, Lit_50_/*';'*/(next6));
                        matches6.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches6);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next7 = start;
                    var matches7 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Declaration(next7)) == null)
                        {
                            break;
                        }
                        matches7.Add(match);
                        next7 = match.Next;
                        match = Match.Optional(next7, Lit_50_/*';'*/(next7));
                        matches7.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches7);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next8 = start;
                    var matches8 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        /*>>> Not */
                            for (;;) // ---Choice---
                            {
                                var next9 = next8;
                                var matches9 = new List<Match>();
                                for (;;) // ---Sequence---
                                {
                                    if ((match = Lit_case(next9)) == null)
                                    {
                                        break;
                                    }
                                    matches9.Add(match);
                                    next9 = match.Next;
                                    if ((match = Not_(next9, More(next9))) == null)
                                    {
                                        break;
                                    }
                                    matches9.Add(match);
                                    break;
                                }
                                if (match != null)
                                {
                                    match = Match.Success(start, matches9);
                                }
                                if (match != null)
                                {
                                    break;
                                }
                                var next10 = next8;
                                var matches10 = new List<Match>();
                                for (;;) // ---Sequence---
                                {
                                    if ((match = Lit_default(next10)) == null)
                                    {
                                        break;
                                    }
                                    matches10.Add(match);
                                    next10 = match.Next;
                                    if ((match = Not_(next10, More(next10))) == null)
                                    {
                                        break;
                                    }
                                    matches10.Add(match);
                                    break;
                                }
                                if (match != null)
                                {
                                    match = Match.Success(start, matches10);
                                }
                                break;
                            }
                            match = Not_(next8, match);
                        /*<<< Not */
                        if (match == null)
                        {
                            break;
                        }
                        matches8.Add(match);
                        next8 = match.Next;
                        if ((match = Expression(next8)) == null)
                        {
                            break;
                        }
                        matches8.Add(match);
                        next8 = match.Next;
                        match = Match.Optional(next8, Lit_50_/*';'*/(next8));
                        matches8.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches8);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next11 = start;
                    var matches11 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next11)) == null)
                        {
                            break;
                        }
                        matches11.Add(match);
                        next11 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches11.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches11);
                    }
                    break;
                }
                Caches[Cache_Statement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CodeBlock = 329;

        public virtual Match CodeBlock(int start)
        {
            if (!Caches[Cache_CodeBlock].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Statements(next));
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_24_/*'}'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_23_/*'{'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_CodeBlock].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Statements = 330;

        public virtual Match Statements(int start)
        {
            if (!Caches[Cache_Statements].Already(start, out var match))
            {
                /*>>> OneOrMore */
                    var oomMatches = new List<Match>();
                    var oomNext = start;
                    for (;;)
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
                        match = Match.Success(start, oomMatches);
                    }
                /*<<< OneOrMore */
                Caches[Cache_Statements].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ControlTransferStatement = 331;

        public virtual Match ControlTransferStatement(int start)
        {
            if (!Caches[Cache_ControlTransferStatement].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                    match = ThrowStatement(start);
                    break;
                }
                Caches[Cache_ControlTransferStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FallthroughStatement = 332;

        public virtual Match FallthroughStatement(int start)
        {
            if (!Caches[Cache_FallthroughStatement].Already(start, out var match))
            {
                match = Lit_fallthrough(start);
                Caches[Cache_FallthroughStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ThrowStatement = 333;

        public virtual Match ThrowStatement(int start)
        {
            if (!Caches[Cache_ThrowStatement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_throw(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_ThrowStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LoopStatement = 334;

        public virtual Match LoopStatement(int start)
        {
            if (!Caches[Cache_LoopStatement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    if ((match = ForInStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = WhileStatement(start)) != null)
                    {
                        break;
                    }
                    match = RepeatWhileStatement(start);
                    break;
                }
                Caches[Cache_LoopStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ForInStatement = 335;

        public virtual Match ForInStatement(int start)
        {
            if (!Caches[Cache_ForInStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_ForInStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RepeatWhileStatement = 336;

        public virtual Match RepeatWhileStatement(int start)
        {
            if (!Caches[Cache_RepeatWhileStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_RepeatWhileStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BranchStatement = 337;

        public virtual Match BranchStatement(int start)
        {
            if (!Caches[Cache_BranchStatement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    if ((match = IfStatement(start)) != null)
                    {
                        break;
                    }
                    if ((match = GuardStatement(start)) != null)
                    {
                        break;
                    }
                    match = SwitchStatement(start);
                    break;
                }
                Caches[Cache_BranchStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WhereClause = 338;

        public virtual Match WhereClause(int start)
        {
            if (!Caches[Cache_WhereClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_WhereClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WhereExpression = 339;

        public virtual Match WhereExpression(int start)
        {
            if (!Caches[Cache_WhereExpression].Already(start, out var match))
            {
                match = Expression(start);
                Caches[Cache_WhereExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleType = 340;

        public virtual Match TupleType(int start)
        {
            if (!Caches[Cache_TupleType].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next2)) == null)
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
                        if ((match = Lit_3_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_TupleType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleTypeElementList = 341;

        public virtual Match TupleTypeElementList(int start)
        {
            if (!Caches[Cache_TupleTypeElementList].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = TupleTypeElement(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        /*>>> OneOrMore */
                            var oomMatches = new List<Match>();
                            var oomNext = next;
                            for (;;)
                            {
                                var next2 = oomNext;
                                var matches2 = new List<Match>();
                                for (;;) // ---Sequence---
                                {
                                    if ((match = Lit_4_/*','*/(next2)) == null)
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
                                    match = Match.Success(start, matches2);
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
                                match = Match.Success(next, oomMatches);
                            }
                        /*<<< OneOrMore */
                        if (match == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = TupleTypeElement(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Lit_4_/*','*/(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        /*>>> Error */
                            throw new NotImplementedException();
                        /*<<< Error */
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_TupleTypeElementList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleTypeElement = 342;

        public virtual Match TupleTypeElement(int start)
        {
            if (!Caches[Cache_TupleTypeElement].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    match = Type(start);
                    break;
                }
                Caches[Cache_TupleTypeElement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElementName = 343;

        public virtual Match ElementName(int start)
        {
            if (!Caches[Cache_ElementName].Already(start, out var match))
            {
                match = Name(start);
                Caches[Cache_ElementName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EnumTupleType = 344;

        public virtual Match EnumTupleType(int start)
        {
            if (!Caches[Cache_EnumTupleType].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    if ((match = TupleType(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next)) == null)
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
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    break;
                }
                Caches[Cache_EnumTupleType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Type = 345;

        public virtual Match Type(int start)
        {
            if (!Caches[Cache_Type].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = PrimaryType(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            if ((match = TypePostfix(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Type].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrimaryType = 346;

        public virtual Match PrimaryType(int start)
        {
            if (!Caches[Cache_PrimaryType].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next)) == null)
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
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    break;
                }
                Caches[Cache_PrimaryType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypePostfix = 347;

        public virtual Match TypePostfix(int start)
        {
            if (!Caches[Cache_TypePostfix].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                Caches[Cache_TypePostfix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeOptional = 348;

        public virtual Match TypeOptional(int start)
        {
            if (!Caches[Cache_TypeOptional].Already(start, out var match))
            {
                match = CharacterExact_(start, '?');
                Caches[Cache_TypeOptional].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeUnwrap = 349;

        public virtual Match TypeUnwrap(int start)
        {
            if (!Caches[Cache_TypeUnwrap].Already(start, out var match))
            {
                match = CharacterExact_(start, '!');
                Caches[Cache_TypeUnwrap].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeMetatype = 350;

        public virtual Match TypeMetatype(int start)
        {
            if (!Caches[Cache_TypeMetatype].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_16_/*'.'*/(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    for (;;) // ---Choice---
                    {
                        var next2 = next;
                        var matches2 = new List<Match>();
                        for (;;) // ---Sequence---
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
                            match = Match.Success(start, matches2);
                        }
                        if (match != null)
                        {
                            break;
                        }
                        var next3 = next;
                        var matches3 = new List<Match>();
                        for (;;) // ---Sequence---
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
                            match = Match.Success(start, matches3);
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_TypeMetatype].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionType = 351;

        public virtual Match FunctionType(int start)
        {
            if (!Caches[Cache_FunctionType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    if ((match = Lit_26_/*'->'*/(next)) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_FunctionType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionTypeArgumentClause = 352;

        public virtual Match FunctionTypeArgumentClause(int start)
        {
            if (!Caches[Cache_FunctionTypeArgumentClause].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Lit_3_/*')'*/(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
                    {
                        if ((match = Lit_2_/*'('*/(next2)) == null)
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
                        match = Match.Optional(next2, Lit_25_/*'...'*/(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Lit_3_/*')'*/(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success(start, matches2);
                    }
                    break;
                }
                Caches[Cache_FunctionTypeArgumentClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionTypeArgumentList = 353;

        public virtual Match FunctionTypeArgumentList(int start)
        {
            if (!Caches[Cache_FunctionTypeArgumentList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = FunctionTypeArgument(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_FunctionTypeArgumentList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionTypeArgument = 354;

        public virtual Match FunctionTypeArgument(int start)
        {
            if (!Caches[Cache_FunctionTypeArgument].Already(start, out var match))
            {
                for (;;) // ---Choice---
                {
                    var next = start;
                    var matches = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches2);
                    }
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    for (;;) // ---Sequence---
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
                        match = Match.Success(start, matches3);
                    }
                    break;
                }
                Caches[Cache_FunctionTypeArgument].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArgumentLabel = 355;

        public virtual Match ArgumentLabel(int start)
        {
            if (!Caches[Cache_ArgumentLabel].Already(start, out var match))
            {
                match = Name(start);
                Caches[Cache_ArgumentLabel].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArrayType = 356;

        public virtual Match ArrayType(int start)
        {
            if (!Caches[Cache_ArrayType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_ArrayType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DictionaryType = 357;

        public virtual Match DictionaryType(int start)
        {
            if (!Caches[Cache_DictionaryType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    if ((match = Lit_5_/*':'*/(next)) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_DictionaryType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeIdentifier = 358;

        public virtual Match TypeIdentifier(int start)
        {
            if (!Caches[Cache_TypeIdentifier].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = TypeIdentifierPart(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_16_/*'.'*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_TypeIdentifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeIdentifierPart = 359;

        public virtual Match TypeIdentifierPart(int start)
        {
            if (!Caches[Cache_TypeIdentifierPart].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_TypeIdentifierPart].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolCompositionType = 360;

        public virtual Match ProtocolCompositionType(int start)
        {
            if (!Caches[Cache_ProtocolCompositionType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = TypeIdentifier(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> OneOrMore */
                        var oomMatches = new List<Match>();
                        var oomNext = next;
                        for (;;)
                        {
                            var next2 = oomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
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
                                match = Match.Success(start, matches2);
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
                            match = Match.Success(next, oomMatches);
                        }
                    /*<<< OneOrMore */
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_ProtocolCompositionType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OpaqueType = 361;

        public virtual Match OpaqueType(int start)
        {
            if (!Caches[Cache_OpaqueType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_OpaqueType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AnyType = 362;

        public virtual Match AnyType(int start)
        {
            if (!Caches[Cache_AnyType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_AnyType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SelfType = 363;

        public virtual Match SelfType(int start)
        {
            if (!Caches[Cache_SelfType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_SelfType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeName = 364;

        public virtual Match TypeName(int start)
        {
            if (!Caches[Cache_TypeName].Already(start, out var match))
            {
                match = Name(start);
                Caches[Cache_TypeName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeInheritanceClause = 365;

        public virtual Match TypeInheritanceClause(int start)
        {
            if (!Caches[Cache_TypeInheritanceClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = Lit_5_/*':'*/(next)) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_TypeInheritanceClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeInheritanceList = 366;

        public virtual Match TypeInheritanceList(int start)
        {
            if (!Caches[Cache_TypeInheritanceList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = TypeIdentifier(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    /*>>> ZeroOrMore */
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        for (;;)
                        {
                            var next2 = zomNext;
                            var matches2 = new List<Match>();
                            for (;;) // ---Sequence---
                            {
                                if ((match = Lit_4_/*','*/(next2)) == null)
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
                                match = Match.Success(start, matches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success(next, zomMatches);
                    /*<<< ZeroOrMore */
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_TypeInheritanceList].Cache(start, match);
            }
            return match;
        }

        public virtual Match Whitespace(int start)
        {
            Match match;
            match = WhitespaceItem(start);
            return match;
        }

        protected const int Cache_SingleWhitespace = 368;

        public virtual Match SingleWhitespace(int start)
        {
            if (!Caches[Cache_SingleWhitespace].Already(start, out var match))
            {
                for (;;) // ---Choice---
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
                Caches[Cache_SingleWhitespace].Cache(start, match);
            }
            return match;
        }

        public virtual Match WhitespaceItem(int start)
        {
            Match match;
            for (;;) // ---Choice---
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
            return match;
        }

        public virtual Match LineBreak(int start)
        {
            Match match;
            for (;;) // ---Choice---
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
            return match;
        }

        public virtual Match LineBreakCharacter(int start)
        {
            Match match;
            match = CharacterSet_(start, "\n\r");
            return match;
        }

        public virtual Match InlineSpaces(int start)
        {
            Match match;
            /*>>> OneOrMore */
                var oomMatches = new List<Match>();
                var oomNext = start;
                for (;;)
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
                    match = Match.Success(start, oomMatches);
                }
            /*<<< OneOrMore */
            return match;
        }

        public virtual Match InlineSpace(int start)
        {
            Match match;
            match = CharacterSet_(start, "\t ");
            return match;
        }

        public virtual Match Comment(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match CommentText(int start)
        {
            Match match;
            /*>>> ZeroOrMore */
                var zomMatches = new List<Match>();
                var zomNext = start;
                for (;;)
                {
                    if ((match = CommentTextItem(zomNext)) == null)
                    {
                        break;
                    }
                    zomMatches.Add(match);
                    zomNext = match.Next;
                }
                match = Match.Success(start, zomMatches);
            /*<<< ZeroOrMore */
            return match;
        }

        public virtual Match CommentTextItem(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match MultilineComment(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            for (;;) // ---Sequence---
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
                match = Match.Success(start, matches);
            }
            return match;
        }

        public virtual Match MultilineCommentText(int start)
        {
            Match match;
            /*>>> ZeroOrMore */
                var zomMatches = new List<Match>();
                var zomNext = start;
                for (;;)
                {
                    if ((match = MultilineCommentTextItem(zomNext)) == null)
                    {
                        break;
                    }
                    zomMatches.Add(match);
                    zomNext = match.Next;
                }
                match = Match.Success(start, zomMatches);
            /*<<< ZeroOrMore */
            return match;
        }

        public virtual Match MultilineCommentTextItem(int start)
        {
            Match match;
            for (;;) // ---Choice---
            {
                if ((match = MultilineComment(start)) != null)
                {
                    break;
                }
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    /*>>> Not */
                        for (;;) // ---Choice---
                        {
                            if ((match = CharacterSequence_(next, "/*")) != null)
                            {
                                break;
                            }
                            match = CharacterSequence_(next, "*/");
                            break;
                        }
                        match = Not_(next, match);
                    /*<<< Not */
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
                    match = Match.Success(start, matches);
                }
                break;
            }
            return match;
        }

        protected const int Cache_Lit_1_/*'@'*/ = 380;

        public virtual Match Lit_1_/*'@'*/(int start)
        {
            if (!Caches[Cache_Lit_1_/*'@'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_1_/*'@'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_inlinable = 381;

        public virtual Match Lit_inlinable(int start)
        {
            if (!Caches[Cache_Lit_inlinable].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "inlinable")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_inlinable].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_frozen = 382;

        public virtual Match Lit_frozen(int start)
        {
            if (!Caches[Cache_Lit_frozen].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "frozen")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_frozen].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_escaping = 383;

        public virtual Match Lit_escaping(int start)
        {
            if (!Caches[Cache_Lit_escaping].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "escaping")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_escaping].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_autoclosure = 384;

        public virtual Match Lit_autoclosure(int start)
        {
            if (!Caches[Cache_Lit_autoclosure].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "autoclosure")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_autoclosure].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_usableFromInline = 385;

        public virtual Match Lit_usableFromInline(int start)
        {
            if (!Caches[Cache_Lit_usableFromInline].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "usableFromInline")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_usableFromInline].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_discardableResult = 386;

        public virtual Match Lit_discardableResult(int start)
        {
            if (!Caches[Cache_Lit_discardableResult].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "discardableResult")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_discardableResult].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_nonobjc = 387;

        public virtual Match Lit_nonobjc(int start)
        {
            if (!Caches[Cache_Lit_nonobjc].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "nonobjc")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_nonobjc].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_unknown = 388;

        public virtual Match Lit_unknown(int start)
        {
            if (!Caches[Cache_Lit_unknown].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "unknown")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_unknown].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_inline = 389;

        public virtual Match Lit_inline(int start)
        {
            if (!Caches[Cache_Lit_inline].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "inline")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_inline].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_2_/*'('*/ = 390;

        public virtual Match Lit_2_/*'('*/(int start)
        {
            if (!Caches[Cache_Lit_2_/*'('*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
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
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_2_/*'('*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_never = 391;

        public virtual Match Lit_never(int start)
        {
            if (!Caches[Cache_Lit_never].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "never")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_never].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit___always = 392;

        public virtual Match Lit___always(int start)
        {
            if (!Caches[Cache_Lit___always].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "__always")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit___always].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_3_/*')'*/ = 393;

        public virtual Match Lit_3_/*')'*/(int start)
        {
            if (!Caches[Cache_Lit_3_/*')'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_3_/*')'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_available = 394;

        public virtual Match Lit_available(int start)
        {
            if (!Caches[Cache_Lit_available].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "available")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_available].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_4_/*','*/ = 395;

        public virtual Match Lit_4_/*','*/(int start)
        {
            if (!Caches[Cache_Lit_4_/*','*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_4_/*','*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_5_/*':'*/ = 396;

        public virtual Match Lit_5_/*':'*/(int start)
        {
            if (!Caches[Cache_Lit_5_/*':'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_5_/*':'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_6_/*'*'*/ = 397;

        public virtual Match Lit_6_/*'*'*/(int start)
        {
            if (!Caches[Cache_Lit_6_/*'*'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
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
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_6_/*'*'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_convention = 398;

        public virtual Match Lit_convention(int start)
        {
            if (!Caches[Cache_Lit_convention].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "convention")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_convention].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_block = 399;

        public virtual Match Lit_block(int start)
        {
            if (!Caches[Cache_Lit_block].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "block")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_block].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_thin = 400;

        public virtual Match Lit_thin(int start)
        {
            if (!Caches[Cache_Lit_thin].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "thin")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_thin].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_c = 401;

        public virtual Match Lit_c(int start)
        {
            if (!Caches[Cache_Lit_c].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterExact_(next, 'c')) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_c].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_objc = 402;

        public virtual Match Lit_objc(int start)
        {
            if (!Caches[Cache_Lit_objc].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "objc")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_objc].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__show_in_interface = 403;

        public virtual Match Lit__show_in_interface(int start)
        {
            if (!Caches[Cache_Lit__show_in_interface].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_show_in_interface")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__show_in_interface].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__fixed_layout = 404;

        public virtual Match Lit__fixed_layout(int start)
        {
            if (!Caches[Cache_Lit__fixed_layout].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_fixed_layout")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__fixed_layout].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__nonoverride = 405;

        public virtual Match Lit__nonoverride(int start)
        {
            if (!Caches[Cache_Lit__nonoverride].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_nonoverride")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__nonoverride].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__borrowed = 406;

        public virtual Match Lit__borrowed(int start)
        {
            if (!Caches[Cache_Lit__borrowed].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_borrowed")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__borrowed].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__transparent = 407;

        public virtual Match Lit__transparent(int start)
        {
            if (!Caches[Cache_Lit__transparent].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_transparent")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__transparent].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__nonEphemeral = 408;

        public virtual Match Lit__nonEphemeral(int start)
        {
            if (!Caches[Cache_Lit__nonEphemeral].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_nonEphemeral")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__nonEphemeral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__alwaysEmitIntoClient = 409;

        public virtual Match Lit__alwaysEmitIntoClient(int start)
        {
            if (!Caches[Cache_Lit__alwaysEmitIntoClient].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_alwaysEmitIntoClient")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__alwaysEmitIntoClient].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__objc_non_lazy_realization = 410;

        public virtual Match Lit__objc_non_lazy_realization(int start)
        {
            if (!Caches[Cache_Lit__objc_non_lazy_realization].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_objc_non_lazy_realization")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__objc_non_lazy_realization].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__implements = 411;

        public virtual Match Lit__implements(int start)
        {
            if (!Caches[Cache_Lit__implements].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_implements")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__implements].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__specialize = 412;

        public virtual Match Lit__specialize(int start)
        {
            if (!Caches[Cache_Lit__specialize].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_specialize")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__specialize].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__effects = 413;

        public virtual Match Lit__effects(int start)
        {
            if (!Caches[Cache_Lit__effects].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_effects")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__effects].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_readnone = 414;

        public virtual Match Lit_readnone(int start)
        {
            if (!Caches[Cache_Lit_readnone].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "readnone")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_readnone].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_readonly = 415;

        public virtual Match Lit_readonly(int start)
        {
            if (!Caches[Cache_Lit_readonly].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "readonly")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_readonly].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_releasenone = 416;

        public virtual Match Lit_releasenone(int start)
        {
            if (!Caches[Cache_Lit_releasenone].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "releasenone")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_releasenone].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__silgen_name = 417;

        public virtual Match Lit__silgen_name(int start)
        {
            if (!Caches[Cache_Lit__silgen_name].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_silgen_name")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__silgen_name].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__semantics = 418;

        public virtual Match Lit__semantics(int start)
        {
            if (!Caches[Cache_Lit__semantics].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_semantics")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__semantics].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__objcRuntimeName = 419;

        public virtual Match Lit__objcRuntimeName(int start)
        {
            if (!Caches[Cache_Lit__objcRuntimeName].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_objcRuntimeName")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__objcRuntimeName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__cdecl = 420;

        public virtual Match Lit__cdecl(int start)
        {
            if (!Caches[Cache_Lit__cdecl].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "_cdecl")) == null)
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__cdecl].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_class = 421;

        public virtual Match Lit_class(int start)
        {
            if (!Caches[Cache_Lit_class].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_class].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_convenience = 422;

        public virtual Match Lit_convenience(int start)
        {
            if (!Caches[Cache_Lit_convenience].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_convenience].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_dynamic = 423;

        public virtual Match Lit_dynamic(int start)
        {
            if (!Caches[Cache_Lit_dynamic].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_dynamic].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_final = 424;

        public virtual Match Lit_final(int start)
        {
            if (!Caches[Cache_Lit_final].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_final].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_infix = 425;

        public virtual Match Lit_infix(int start)
        {
            if (!Caches[Cache_Lit_infix].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_infix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_lazy = 426;

        public virtual Match Lit_lazy(int start)
        {
            if (!Caches[Cache_Lit_lazy].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_lazy].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_optional = 427;

        public virtual Match Lit_optional(int start)
        {
            if (!Caches[Cache_Lit_optional].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_optional].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_override = 428;

        public virtual Match Lit_override(int start)
        {
            if (!Caches[Cache_Lit_override].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_override].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_postfix = 429;

        public virtual Match Lit_postfix(int start)
        {
            if (!Caches[Cache_Lit_postfix].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_postfix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_prefix = 430;

        public virtual Match Lit_prefix(int start)
        {
            if (!Caches[Cache_Lit_prefix].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_prefix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_required = 431;

        public virtual Match Lit_required(int start)
        {
            if (!Caches[Cache_Lit_required].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_required].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_static = 432;

        public virtual Match Lit_static(int start)
        {
            if (!Caches[Cache_Lit_static].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_static].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_unowned = 433;

        public virtual Match Lit_unowned(int start)
        {
            if (!Caches[Cache_Lit_unowned].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_unowned].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_safe = 434;

        public virtual Match Lit_safe(int start)
        {
            if (!Caches[Cache_Lit_safe].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_safe].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_unsafe = 435;

        public virtual Match Lit_unsafe(int start)
        {
            if (!Caches[Cache_Lit_unsafe].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_unsafe].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_weak = 436;

        public virtual Match Lit_weak(int start)
        {
            if (!Caches[Cache_Lit_weak].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_weak].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit___consuming = 437;

        public virtual Match Lit___consuming(int start)
        {
            if (!Caches[Cache_Lit___consuming].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit___consuming].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_set = 438;

        public virtual Match Lit_set(int start)
        {
            if (!Caches[Cache_Lit_set].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_set].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_private = 439;

        public virtual Match Lit_private(int start)
        {
            if (!Caches[Cache_Lit_private].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_private].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_fileprivate = 440;

        public virtual Match Lit_fileprivate(int start)
        {
            if (!Caches[Cache_Lit_fileprivate].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_fileprivate].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_internal = 441;

        public virtual Match Lit_internal(int start)
        {
            if (!Caches[Cache_Lit_internal].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_internal].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_public = 442;

        public virtual Match Lit_public(int start)
        {
            if (!Caches[Cache_Lit_public].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_public].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_open = 443;

        public virtual Match Lit_open(int start)
        {
            if (!Caches[Cache_Lit_open].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_open].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_mutating = 444;

        public virtual Match Lit_mutating(int start)
        {
            if (!Caches[Cache_Lit_mutating].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_mutating].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_nonmutating = 445;

        public virtual Match Lit_nonmutating(int start)
        {
            if (!Caches[Cache_Lit_nonmutating].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_nonmutating].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_7_/*'#if'*/ = 446;

        public virtual Match Lit_7_/*'#if'*/(int start)
        {
            if (!Caches[Cache_Lit_7_/*'#if'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = CharacterSequence_(next, "#if")) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_7_/*'#if'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_8_/*'#elseif'*/ = 447;

        public virtual Match Lit_8_/*'#elseif'*/(int start)
        {
            if (!Caches[Cache_Lit_8_/*'#elseif'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_8_/*'#elseif'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_9_/*'#else'*/ = 448;

        public virtual Match Lit_9_/*'#else'*/(int start)
        {
            if (!Caches[Cache_Lit_9_/*'#else'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_9_/*'#else'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_10_/*'#endif'*/ = 449;

        public virtual Match Lit_10_/*'#endif'*/(int start)
        {
            if (!Caches[Cache_Lit_10_/*'#endif'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_10_/*'#endif'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_11_/*'!'*/ = 450;

        public virtual Match Lit_11_/*'!'*/(int start)
        {
            if (!Caches[Cache_Lit_11_/*'!'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_11_/*'!'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_12_/*'||'*/ = 451;

        public virtual Match Lit_12_/*'||'*/(int start)
        {
            if (!Caches[Cache_Lit_12_/*'||'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_12_/*'||'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_13_/*'&&'*/ = 452;

        public virtual Match Lit_13_/*'&&'*/(int start)
        {
            if (!Caches[Cache_Lit_13_/*'&&'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_13_/*'&&'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_os = 453;

        public virtual Match Lit_os(int start)
        {
            if (!Caches[Cache_Lit_os].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_os].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_arch = 454;

        public virtual Match Lit_arch(int start)
        {
            if (!Caches[Cache_Lit_arch].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_arch].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_swift = 455;

        public virtual Match Lit_swift(int start)
        {
            if (!Caches[Cache_Lit_swift].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_swift].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_14_/*'>='*/ = 456;

        public virtual Match Lit_14_/*'>='*/(int start)
        {
            if (!Caches[Cache_Lit_14_/*'>='*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_14_/*'>='*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_15_/*'<'*/ = 457;

        public virtual Match Lit_15_/*'<'*/(int start)
        {
            if (!Caches[Cache_Lit_15_/*'<'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_15_/*'<'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_compiler = 458;

        public virtual Match Lit_compiler(int start)
        {
            if (!Caches[Cache_Lit_compiler].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_compiler].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_canImport = 459;

        public virtual Match Lit_canImport(int start)
        {
            if (!Caches[Cache_Lit_canImport].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_canImport].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_targetEnvironment = 460;

        public virtual Match Lit_targetEnvironment(int start)
        {
            if (!Caches[Cache_Lit_targetEnvironment].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_targetEnvironment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_macOS = 461;

        public virtual Match Lit_macOS(int start)
        {
            if (!Caches[Cache_Lit_macOS].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_macOS].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_iOS = 462;

        public virtual Match Lit_iOS(int start)
        {
            if (!Caches[Cache_Lit_iOS].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_iOS].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_watchOS = 463;

        public virtual Match Lit_watchOS(int start)
        {
            if (!Caches[Cache_Lit_watchOS].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_watchOS].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_tvOS = 464;

        public virtual Match Lit_tvOS(int start)
        {
            if (!Caches[Cache_Lit_tvOS].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_tvOS].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Windows = 465;

        public virtual Match Lit_Windows(int start)
        {
            if (!Caches[Cache_Lit_Windows].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_Windows].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Android = 466;

        public virtual Match Lit_Android(int start)
        {
            if (!Caches[Cache_Lit_Android].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_Android].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Linux = 467;

        public virtual Match Lit_Linux(int start)
        {
            if (!Caches[Cache_Lit_Linux].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_Linux].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_OpenBSD = 468;

        public virtual Match Lit_OpenBSD(int start)
        {
            if (!Caches[Cache_Lit_OpenBSD].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_OpenBSD].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_i386 = 469;

        public virtual Match Lit_i386(int start)
        {
            if (!Caches[Cache_Lit_i386].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_i386].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_x86_64 = 470;

        public virtual Match Lit_x86_64(int start)
        {
            if (!Caches[Cache_Lit_x86_64].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_x86_64].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_arm = 471;

        public virtual Match Lit_arm(int start)
        {
            if (!Caches[Cache_Lit_arm].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_arm].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_arm64 = 472;

        public virtual Match Lit_arm64(int start)
        {
            if (!Caches[Cache_Lit_arm64].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_arm64].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_wasm32 = 473;

        public virtual Match Lit_wasm32(int start)
        {
            if (!Caches[Cache_Lit_wasm32].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_wasm32].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_powerpc64 = 474;

        public virtual Match Lit_powerpc64(int start)
        {
            if (!Caches[Cache_Lit_powerpc64].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_powerpc64].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_s390x = 475;

        public virtual Match Lit_s390x(int start)
        {
            if (!Caches[Cache_Lit_s390x].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_s390x].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_16_/*'.'*/ = 476;

        public virtual Match Lit_16_/*'.'*/(int start)
        {
            if (!Caches[Cache_Lit_16_/*'.'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_16_/*'.'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_simulator = 477;

        public virtual Match Lit_simulator(int start)
        {
            if (!Caches[Cache_Lit_simulator].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_simulator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_macCatalyst = 478;

        public virtual Match Lit_macCatalyst(int start)
        {
            if (!Caches[Cache_Lit_macCatalyst].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_macCatalyst].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_17_/*'#sourceLocation'*/ = 479;

        public virtual Match Lit_17_/*'#sourceLocation'*/(int start)
        {
            if (!Caches[Cache_Lit_17_/*'#sourceLocation'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_17_/*'#sourceLocation'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_18_/*'file:'*/ = 480;

        public virtual Match Lit_18_/*'file:'*/(int start)
        {
            if (!Caches[Cache_Lit_18_/*'file:'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_18_/*'file:'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_19_/*'line:'*/ = 481;

        public virtual Match Lit_19_/*'line:'*/(int start)
        {
            if (!Caches[Cache_Lit_19_/*'line:'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_19_/*'line:'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_20_/*'#error'*/ = 482;

        public virtual Match Lit_20_/*'#error'*/(int start)
        {
            if (!Caches[Cache_Lit_20_/*'#error'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_20_/*'#error'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_21_/*'#warning'*/ = 483;

        public virtual Match Lit_21_/*'#warning'*/(int start)
        {
            if (!Caches[Cache_Lit_21_/*'#warning'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_21_/*'#warning'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_let = 484;

        public virtual Match Lit_let(int start)
        {
            if (!Caches[Cache_Lit_let].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_let].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_22_/*'='*/ = 485;

        public virtual Match Lit_22_/*'='*/(int start)
        {
            if (!Caches[Cache_Lit_22_/*'='*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_22_/*'='*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_indirect = 486;

        public virtual Match Lit_indirect(int start)
        {
            if (!Caches[Cache_Lit_indirect].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_indirect].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_enum = 487;

        public virtual Match Lit_enum(int start)
        {
            if (!Caches[Cache_Lit_enum].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_enum].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_23_/*'{'*/ = 488;

        public virtual Match Lit_23_/*'{'*/(int start)
        {
            if (!Caches[Cache_Lit_23_/*'{'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_23_/*'{'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_24_/*'}'*/ = 489;

        public virtual Match Lit_24_/*'}'*/(int start)
        {
            if (!Caches[Cache_Lit_24_/*'}'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_24_/*'}'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_case = 490;

        public virtual Match Lit_case(int start)
        {
            if (!Caches[Cache_Lit_case].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_case].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_extension = 491;

        public virtual Match Lit_extension(int start)
        {
            if (!Caches[Cache_Lit_extension].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_extension].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_func = 492;

        public virtual Match Lit_func(int start)
        {
            if (!Caches[Cache_Lit_func].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_func].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_throws = 493;

        public virtual Match Lit_throws(int start)
        {
            if (!Caches[Cache_Lit_throws].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_throws].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_rethrows = 494;

        public virtual Match Lit_rethrows(int start)
        {
            if (!Caches[Cache_Lit_rethrows].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_rethrows].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_25_/*'...'*/ = 495;

        public virtual Match Lit_25_/*'...'*/(int start)
        {
            if (!Caches[Cache_Lit_25_/*'...'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_25_/*'...'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_26_/*'->'*/ = 496;

        public virtual Match Lit_26_/*'->'*/(int start)
        {
            if (!Caches[Cache_Lit_26_/*'->'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_26_/*'->'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit___owned = 497;

        public virtual Match Lit___owned(int start)
        {
            if (!Caches[Cache_Lit___owned].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit___owned].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_inout = 498;

        public virtual Match Lit_inout(int start)
        {
            if (!Caches[Cache_Lit_inout].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_inout].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_init = 499;

        public virtual Match Lit_init(int start)
        {
            if (!Caches[Cache_Lit_init].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_init].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_27_/*'?'*/ = 500;

        public virtual Match Lit_27_/*'?'*/(int start)
        {
            if (!Caches[Cache_Lit_27_/*'?'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_27_/*'?'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_operator = 501;

        public virtual Match Lit_operator(int start)
        {
            if (!Caches[Cache_Lit_operator].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_operator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_precedencegroup = 502;

        public virtual Match Lit_precedencegroup(int start)
        {
            if (!Caches[Cache_Lit_precedencegroup].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_precedencegroup].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_higherThan = 503;

        public virtual Match Lit_higherThan(int start)
        {
            if (!Caches[Cache_Lit_higherThan].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_higherThan].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_lowerThan = 504;

        public virtual Match Lit_lowerThan(int start)
        {
            if (!Caches[Cache_Lit_lowerThan].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_lowerThan].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_assignment = 505;

        public virtual Match Lit_assignment(int start)
        {
            if (!Caches[Cache_Lit_assignment].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_assignment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_associativity = 506;

        public virtual Match Lit_associativity(int start)
        {
            if (!Caches[Cache_Lit_associativity].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_associativity].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_left = 507;

        public virtual Match Lit_left(int start)
        {
            if (!Caches[Cache_Lit_left].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_left].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_right = 508;

        public virtual Match Lit_right(int start)
        {
            if (!Caches[Cache_Lit_right].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_right].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_none = 509;

        public virtual Match Lit_none(int start)
        {
            if (!Caches[Cache_Lit_none].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_none].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_protocol = 510;

        public virtual Match Lit_protocol(int start)
        {
            if (!Caches[Cache_Lit_protocol].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_protocol].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_associatedtype = 511;

        public virtual Match Lit_associatedtype(int start)
        {
            if (!Caches[Cache_Lit_associatedtype].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_associatedtype].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_struct = 512;

        public virtual Match Lit_struct(int start)
        {
            if (!Caches[Cache_Lit_struct].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_struct].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_subscript = 513;

        public virtual Match Lit_subscript(int start)
        {
            if (!Caches[Cache_Lit_subscript].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_subscript].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_typealias = 514;

        public virtual Match Lit_typealias(int start)
        {
            if (!Caches[Cache_Lit_typealias].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_typealias].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_var = 515;

        public virtual Match Lit_var(int start)
        {
            if (!Caches[Cache_Lit_var].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_var].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_get = 516;

        public virtual Match Lit_get(int start)
        {
            if (!Caches[Cache_Lit_get].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_get].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__modify = 517;

        public virtual Match Lit__modify(int start)
        {
            if (!Caches[Cache_Lit__modify].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__modify].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_willSet = 518;

        public virtual Match Lit_willSet(int start)
        {
            if (!Caches[Cache_Lit_willSet].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_willSet].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_didSet = 519;

        public virtual Match Lit_didSet(int start)
        {
            if (!Caches[Cache_Lit_didSet].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_didSet].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_import = 520;

        public virtual Match Lit_import(int start)
        {
            if (!Caches[Cache_Lit_import].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_import].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_deinit = 521;

        public virtual Match Lit_deinit(int start)
        {
            if (!Caches[Cache_Lit_deinit].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_deinit].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_28_/*'&'*/ = 522;

        public virtual Match Lit_28_/*'&'*/(int start)
        {
            if (!Caches[Cache_Lit_28_/*'&'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_28_/*'&'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_self = 523;

        public virtual Match Lit_self(int start)
        {
            if (!Caches[Cache_Lit_self].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_self].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_29_/*'['*/ = 524;

        public virtual Match Lit_29_/*'['*/(int start)
        {
            if (!Caches[Cache_Lit_29_/*'['*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_29_/*'['*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_30_/*']'*/ = 525;

        public virtual Match Lit_30_/*']'*/(int start)
        {
            if (!Caches[Cache_Lit_30_/*']'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_30_/*']'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_31_/*'#file'*/ = 526;

        public virtual Match Lit_31_/*'#file'*/(int start)
        {
            if (!Caches[Cache_Lit_31_/*'#file'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_31_/*'#file'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_32_/*'#fileID'*/ = 527;

        public virtual Match Lit_32_/*'#fileID'*/(int start)
        {
            if (!Caches[Cache_Lit_32_/*'#fileID'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_32_/*'#fileID'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_33_/*'#filePath'*/ = 528;

        public virtual Match Lit_33_/*'#filePath'*/(int start)
        {
            if (!Caches[Cache_Lit_33_/*'#filePath'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_33_/*'#filePath'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_34_/*'#line'*/ = 529;

        public virtual Match Lit_34_/*'#line'*/(int start)
        {
            if (!Caches[Cache_Lit_34_/*'#line'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_34_/*'#line'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_35_/*'#column'*/ = 530;

        public virtual Match Lit_35_/*'#column'*/(int start)
        {
            if (!Caches[Cache_Lit_35_/*'#column'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_35_/*'#column'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_36_/*'#function'*/ = 531;

        public virtual Match Lit_36_/*'#function'*/(int start)
        {
            if (!Caches[Cache_Lit_36_/*'#function'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_36_/*'#function'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_37_/*'#dsohandle'*/ = 532;

        public virtual Match Lit_37_/*'#dsohandle'*/(int start)
        {
            if (!Caches[Cache_Lit_37_/*'#dsohandle'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_37_/*'#dsohandle'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_38_/*'-'*/ = 533;

        public virtual Match Lit_38_/*'-'*/(int start)
        {
            if (!Caches[Cache_Lit_38_/*'-'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_38_/*'-'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_true = 534;

        public virtual Match Lit_true(int start)
        {
            if (!Caches[Cache_Lit_true].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_true].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_false = 535;

        public virtual Match Lit_false(int start)
        {
            if (!Caches[Cache_Lit_false].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_false].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_nil = 536;

        public virtual Match Lit_nil(int start)
        {
            if (!Caches[Cache_Lit_nil].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_nil].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_39_/*'#colorLiteral'*/ = 537;

        public virtual Match Lit_39_/*'#colorLiteral'*/(int start)
        {
            if (!Caches[Cache_Lit_39_/*'#colorLiteral'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_39_/*'#colorLiteral'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_red = 538;

        public virtual Match Lit_red(int start)
        {
            if (!Caches[Cache_Lit_red].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_red].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_green = 539;

        public virtual Match Lit_green(int start)
        {
            if (!Caches[Cache_Lit_green].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_green].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_blue = 540;

        public virtual Match Lit_blue(int start)
        {
            if (!Caches[Cache_Lit_blue].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_blue].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_alpha = 541;

        public virtual Match Lit_alpha(int start)
        {
            if (!Caches[Cache_Lit_alpha].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_alpha].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_40_/*'#fileLiteral'*/ = 542;

        public virtual Match Lit_40_/*'#fileLiteral'*/(int start)
        {
            if (!Caches[Cache_Lit_40_/*'#fileLiteral'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_40_/*'#fileLiteral'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_resourceName = 543;

        public virtual Match Lit_resourceName(int start)
        {
            if (!Caches[Cache_Lit_resourceName].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_resourceName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_41_/*'#imageLiteral'*/ = 544;

        public virtual Match Lit_41_/*'#imageLiteral'*/(int start)
        {
            if (!Caches[Cache_Lit_41_/*'#imageLiteral'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_41_/*'#imageLiteral'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_super = 545;

        public virtual Match Lit_super(int start)
        {
            if (!Caches[Cache_Lit_super].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_super].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_in = 546;

        public virtual Match Lit_in(int start)
        {
            if (!Caches[Cache_Lit_in].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_in].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit__ = 547;

        public virtual Match Lit__(int start)
        {
            if (!Caches[Cache_Lit__].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit__].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_42_/*'\\'*/ = 548;

        public virtual Match Lit_42_/*'\\'*/(int start)
        {
            if (!Caches[Cache_Lit_42_/*'\\'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_42_/*'\\'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_43_/*'#selector'*/ = 549;

        public virtual Match Lit_43_/*'#selector'*/(int start)
        {
            if (!Caches[Cache_Lit_43_/*'#selector'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_43_/*'#selector'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_44_/*'getter:'*/ = 550;

        public virtual Match Lit_44_/*'getter:'*/(int start)
        {
            if (!Caches[Cache_Lit_44_/*'getter:'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_44_/*'getter:'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_45_/*'setter:'*/ = 551;

        public virtual Match Lit_45_/*'setter:'*/(int start)
        {
            if (!Caches[Cache_Lit_45_/*'setter:'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_45_/*'setter:'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_46_/*'#keyPath'*/ = 552;

        public virtual Match Lit_46_/*'#keyPath'*/(int start)
        {
            if (!Caches[Cache_Lit_46_/*'#keyPath'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_46_/*'#keyPath'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_try = 553;

        public virtual Match Lit_try(int start)
        {
            if (!Caches[Cache_Lit_try].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_try].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_is = 554;

        public virtual Match Lit_is(int start)
        {
            if (!Caches[Cache_Lit_is].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_is].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_as = 555;

        public virtual Match Lit_as(int start)
        {
            if (!Caches[Cache_Lit_as].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_as].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_47_/*'>'*/ = 556;

        public virtual Match Lit_47_/*'>'*/(int start)
        {
            if (!Caches[Cache_Lit_47_/*'>'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_47_/*'>'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_where = 557;

        public virtual Match Lit_where(int start)
        {
            if (!Caches[Cache_Lit_where].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_where].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_48_/*'=='*/ = 558;

        public virtual Match Lit_48_/*'=='*/(int start)
        {
            if (!Caches[Cache_Lit_48_/*'=='*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_48_/*'=='*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_break = 559;

        public virtual Match Lit_break(int start)
        {
            if (!Caches[Cache_Lit_break].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_break].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_default = 560;

        public virtual Match Lit_default(int start)
        {
            if (!Caches[Cache_Lit_default].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_default].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_49_/*'#available'*/ = 561;

        public virtual Match Lit_49_/*'#available'*/(int start)
        {
            if (!Caches[Cache_Lit_49_/*'#available'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_49_/*'#available'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_iOSApplicationExtension = 562;

        public virtual Match Lit_iOSApplicationExtension(int start)
        {
            if (!Caches[Cache_Lit_iOSApplicationExtension].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_iOSApplicationExtension].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_macOSApplicationExtension = 563;

        public virtual Match Lit_macOSApplicationExtension(int start)
        {
            if (!Caches[Cache_Lit_macOSApplicationExtension].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_macOSApplicationExtension].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_macCatalystApplicationExtension = 564;

        public virtual Match Lit_macCatalystApplicationExtension(int start)
        {
            if (!Caches[Cache_Lit_macCatalystApplicationExtension].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_macCatalystApplicationExtension].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_continue = 565;

        public virtual Match Lit_continue(int start)
        {
            if (!Caches[Cache_Lit_continue].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_continue].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_defer = 566;

        public virtual Match Lit_defer(int start)
        {
            if (!Caches[Cache_Lit_defer].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_defer].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_do = 567;

        public virtual Match Lit_do(int start)
        {
            if (!Caches[Cache_Lit_do].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_do].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_catch = 568;

        public virtual Match Lit_catch(int start)
        {
            if (!Caches[Cache_Lit_catch].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_catch].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_guard = 569;

        public virtual Match Lit_guard(int start)
        {
            if (!Caches[Cache_Lit_guard].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_guard].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_else = 570;

        public virtual Match Lit_else(int start)
        {
            if (!Caches[Cache_Lit_else].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_else].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_if = 571;

        public virtual Match Lit_if(int start)
        {
            if (!Caches[Cache_Lit_if].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_if].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_return = 572;

        public virtual Match Lit_return(int start)
        {
            if (!Caches[Cache_Lit_return].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_return].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_switch = 573;

        public virtual Match Lit_switch(int start)
        {
            if (!Caches[Cache_Lit_switch].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_switch].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_while = 574;

        public virtual Match Lit_while(int start)
        {
            if (!Caches[Cache_Lit_while].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_while].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_50_/*';'*/ = 575;

        public virtual Match Lit_50_/*';'*/(int start)
        {
            if (!Caches[Cache_Lit_50_/*';'*/].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_50_/*';'*/].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_fallthrough = 576;

        public virtual Match Lit_fallthrough(int start)
        {
            if (!Caches[Cache_Lit_fallthrough].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_fallthrough].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_throw = 577;

        public virtual Match Lit_throw(int start)
        {
            if (!Caches[Cache_Lit_throw].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_throw].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_for = 578;

        public virtual Match Lit_for(int start)
        {
            if (!Caches[Cache_Lit_for].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_for].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_repeat = 579;

        public virtual Match Lit_repeat(int start)
        {
            if (!Caches[Cache_Lit_repeat].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_repeat].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Type = 580;

        public virtual Match Lit_Type(int start)
        {
            if (!Caches[Cache_Lit_Type].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_Type].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Protocol = 581;

        public virtual Match Lit_Protocol(int start)
        {
            if (!Caches[Cache_Lit_Protocol].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_Protocol].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_some = 582;

        public virtual Match Lit_some(int start)
        {
            if (!Caches[Cache_Lit_some].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_some].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Any = 583;

        public virtual Match Lit_Any(int start)
        {
            if (!Caches[Cache_Lit_Any].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_Any].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Lit_Self = 584;

        public virtual Match Lit_Self(int start)
        {
            if (!Caches[Cache_Lit_Self].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                for (;;) // ---Sequence---
                {
                    if ((match = _space_(next)) == null)
                    {
                        break;
                    }
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
                    match = Match.Success(start, matches);
                }
                Caches[Cache_Lit_Self].Cache(start, match);
            }
            return match;
        }
    }
}
#endif
