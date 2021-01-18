#if true
using System.Collections.Generic;
using Six.Peg.Runtime;

namespace SixPeg.Pegger.Swift
{
    public abstract class SwiftPegger : Six.Peg.Runtime.Pegger
    {
        public SwiftPegger(Context context)
            : base(context, 375)
        {
        }

        protected const int Cache_Unit = 0;

        public virtual Match Unit(int start)
        {
            if (!Caches[Cache_Unit].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                    match = EOF(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
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
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        match = _(next);
                        matches.Add(match);
                        next = match.Next;
                        match = Not_(next, CharacterAny_(next));
                        break;
                    }
                    if (match != null)
                    {
                        match = matches[0];
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    // ERROR -->
                    new Error(Context).Report("EOF", start);
                    throw new BailOutException();
                    // <-- ERROR
                }
                // <-- Choice
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
                while (true) // Sequence -->
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
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
                // <-- Sequence
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
                while (true) // Sequence -->
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
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
                // <-- Sequence
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
                while (true) // Sequence -->
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("MutationPrefix", start, match);
                }
                Caches[Cache_MutationPrefix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Attribute = 6;

        public virtual Match Attribute(int start)
        {
            if (!Caches[Cache_Attribute].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "@inlinable", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@frozen", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@escaping", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@autoclosure", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@usableFromInline", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@discardableResult", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@nonobjc", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@unknown", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@main", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@testable", More)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "@inline", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "(", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        while (true) // Choice -->
                        {
                            if ((match = Terminal_(_(next), "never", More)) != null)
                            {
                                break;
                            }
                            match = Terminal_(_(next), "__always", More);
                            break;
                        }
                        // <-- Choice
                        if (match == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "@available", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "(", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        var oomMatches = new List<Match>();
                        var oomNext = next2;
                        while (true)
                        {
                            while (true) // Choice -->
                            {
                                if ((match = Name(oomNext)) != null)
                                {
                                    break;
                                }
                                if ((match = SwiftVersion(oomNext)) != null)
                                {
                                    break;
                                }
                                if ((match = Terminal_(_(oomNext), ",", null)) != null)
                                {
                                    break;
                                }
                                if ((match = Terminal_(_(oomNext), ":", null)) != null)
                                {
                                    break;
                                }
                                if ((match = Terminal_(_(oomNext), "*", null)) != null)
                                {
                                    break;
                                }
                                match = StaticStringLiteral(oomNext);
                                break;
                            }
                            // <-- Choice
                            if (match == null)
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
                        match = Terminal_(_(next2), ")", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "@convention", More)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Terminal_(_(next3), "(", null)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        while (true) // Choice -->
                        {
                            if ((match = Terminal_(_(next3), "block", More)) != null)
                            {
                                break;
                            }
                            if ((match = Terminal_(_(next3), "thin", More)) != null)
                            {
                                break;
                            }
                            match = Terminal_(_(next3), "c", More);
                            break;
                        }
                        // <-- Choice
                        if (match == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        match = Terminal_(_(next3), ")", null);
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next4), "@objc", More)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        var next5 = next4;
                        var matches5 = new List<Match>();
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next5), "(", null)) == null)
                            {
                                break;
                            }
                            matches5.Add(match);
                            next5 = match.Next;
                            var oomMatches2 = new List<Match>();
                            var oomNext2 = next5;
                            while (true)
                            {
                                while (true) // Choice -->
                                {
                                    if ((match = Terminal_(_(oomNext2), ":", null)) != null)
                                    {
                                        break;
                                    }
                                    match = Name(oomNext2);
                                    break;
                                }
                                // <-- Choice
                                if (match == null)
                                {
                                    break;
                                }
                                oomMatches2.Add(match);
                                oomNext2 = match.Next;
                            }
                            if (oomMatches2.Count > 0)
                            {
                                match = Match.Success("+", next5, oomMatches2);
                            }
                            if (match == null)
                            {
                                break;
                            }
                            matches5.Add(match);
                            next5 = match.Next;
                            match = Terminal_(_(next5), ")", null);
                            matches5.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next4, matches5);
                        }
                        // <-- Sequence
                        match = Match.Optional(next4, match);
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches4);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@_show_in_interface", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@_fixed_layout", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@_nonoverride", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@_borrowed", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@_transparent", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@_nonEphemeral", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@_alwaysEmitIntoClient", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@_objc_non_lazy_realization", More)) != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next6), "@_implements", More)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = Terminal_(_(next6), "(", null)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = TypeIdentifier(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        var zomMatches = new List<Match>();
                        var zomNext = next6;
                        while (true)
                        {
                            var next7 = zomNext;
                            var matches7 = new List<Match>();
                            while (true) // Sequence -->
                            {
                                if ((match = Terminal_(_(next7), ",", null)) == null)
                                {
                                    break;
                                }
                                matches7.Add(match);
                                next7 = match.Next;
                                match = TypeIdentifier(next7);
                                matches7.Add(match);
                                break;
                            }
                            if (match != null)
                            {
                                match = Match.Success("_", zomNext, matches7);
                            }
                            // <-- Sequence
                            if (match == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next6, zomMatches);
                        matches6.Add(match);
                        next6 = match.Next;
                        match = Terminal_(_(next6), ")", null);
                        matches6.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches6);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next8 = start;
                    var matches8 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next8), "@_specialize", More)) == null)
                        {
                            break;
                        }
                        matches8.Add(match);
                        next8 = match.Next;
                        if ((match = Terminal_(_(next8), "(", null)) == null)
                        {
                            break;
                        }
                        matches8.Add(match);
                        next8 = match.Next;
                        if ((match = GenericWhereClause(next8)) == null)
                        {
                            break;
                        }
                        matches8.Add(match);
                        next8 = match.Next;
                        match = Terminal_(_(next8), ")", null);
                        matches8.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches8);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next9 = start;
                    var matches9 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next9), "@_effects", More)) == null)
                        {
                            break;
                        }
                        matches9.Add(match);
                        next9 = match.Next;
                        if ((match = Terminal_(_(next9), "(", null)) == null)
                        {
                            break;
                        }
                        matches9.Add(match);
                        next9 = match.Next;
                        while (true) // Choice -->
                        {
                            if ((match = Terminal_(_(next9), "readnone", More)) != null)
                            {
                                break;
                            }
                            if ((match = Terminal_(_(next9), "readonly", More)) != null)
                            {
                                break;
                            }
                            match = Terminal_(_(next9), "releasenone", More);
                            break;
                        }
                        // <-- Choice
                        if (match == null)
                        {
                            break;
                        }
                        matches9.Add(match);
                        next9 = match.Next;
                        match = Terminal_(_(next9), ")", null);
                        matches9.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches9);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next10 = start;
                    var matches10 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next10), "@_silgen_name", More)) == null)
                        {
                            break;
                        }
                        matches10.Add(match);
                        next10 = match.Next;
                        if ((match = Terminal_(_(next10), "(", null)) == null)
                        {
                            break;
                        }
                        matches10.Add(match);
                        next10 = match.Next;
                        if ((match = StaticStringLiteral(next10)) == null)
                        {
                            break;
                        }
                        matches10.Add(match);
                        next10 = match.Next;
                        match = Terminal_(_(next10), ")", null);
                        matches10.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches10);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next11 = start;
                    var matches11 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next11), "@_semantics", More)) == null)
                        {
                            break;
                        }
                        matches11.Add(match);
                        next11 = match.Next;
                        if ((match = Terminal_(_(next11), "(", null)) == null)
                        {
                            break;
                        }
                        matches11.Add(match);
                        next11 = match.Next;
                        if ((match = StaticStringLiteral(next11)) == null)
                        {
                            break;
                        }
                        matches11.Add(match);
                        next11 = match.Next;
                        match = Terminal_(_(next11), ")", null);
                        matches11.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches11);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next12 = start;
                    var matches12 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next12), "@_objcRuntimeName", More)) == null)
                        {
                            break;
                        }
                        matches12.Add(match);
                        next12 = match.Next;
                        if ((match = Terminal_(_(next12), "(", null)) == null)
                        {
                            break;
                        }
                        matches12.Add(match);
                        next12 = match.Next;
                        if ((match = Name(next12)) == null)
                        {
                            break;
                        }
                        matches12.Add(match);
                        next12 = match.Next;
                        match = Terminal_(_(next12), ")", null);
                        matches12.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches12);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next13 = start;
                    var matches13 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next13), "@_cdecl", More)) == null)
                        {
                            break;
                        }
                        matches13.Add(match);
                        next13 = match.Next;
                        if ((match = Terminal_(_(next13), "(", null)) == null)
                        {
                            break;
                        }
                        matches13.Add(match);
                        next13 = match.Next;
                        if ((match = StaticStringLiteral(next13)) == null)
                        {
                            break;
                        }
                        matches13.Add(match);
                        next13 = match.Next;
                        match = Terminal_(_(next13), ")", null);
                        matches13.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches13);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "@unsafe_no_objc_tagged_pointer", More)) != null)
                    {
                        break;
                    }
                    var next14 = start;
                    var matches14 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next14), "@derivative", More)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        if ((match = Terminal_(_(next14), "(", null)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        if ((match = Terminal_(_(next14), "of", More)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        if ((match = Terminal_(_(next14), ":", null)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        if ((match = Terminal_(_(next14), "init", More)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        if ((match = Terminal_(_(next14), "(", null)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        if ((match = Terminal_(_(next14), "_", More)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        if ((match = Terminal_(_(next14), ":", null)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        if ((match = Terminal_(_(next14), "_", More)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        if ((match = Terminal_(_(next14), ":", null)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        if ((match = Terminal_(_(next14), ")", null)) == null)
                        {
                            break;
                        }
                        matches14.Add(match);
                        next14 = match.Next;
                        match = Terminal_(_(next14), ")", null);
                        matches14.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches14);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next15 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next15), "@", null)) == null)
                        {
                            break;
                        }
                        next15 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("attribute", next15);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("Attribute", start, match);
                }
                Caches[Cache_Attribute].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DeclarationModifiers = 7;

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

        protected const int Cache_Modifier = 8;

        public virtual Match Modifier(int start)
        {
            if (!Caches[Cache_Modifier].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    match = ModifierToken(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("Modifier", start, match);
                }
                Caches[Cache_Modifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ModifierToken = 9;

        public virtual Match ModifierToken(int start)
        {
            if (!Caches[Cache_ModifierToken].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ModifierToken", start, match);
                }
                Caches[Cache_ModifierToken].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DeclarationModifier = 10;

        public virtual Match DeclarationModifier(int start)
        {
            if (!Caches[Cache_DeclarationModifier].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "class", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "convenience", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "dynamic", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "final", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "infix", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "lazy", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "optional", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "override", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "postfix", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "prefix", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "required", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "static", More)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "unowned", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var next2 = next;
                        var matches2 = new List<Match>();
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), "(", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            while (true) // Choice -->
                            {
                                if ((match = Terminal_(_(next2), "safe", More)) != null)
                                {
                                    break;
                                }
                                match = Terminal_(_(next2), "unsafe", More);
                                break;
                            }
                            // <-- Choice
                            if (match == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = Terminal_(_(next2), ")", null);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next, matches2);
                        }
                        // <-- Sequence
                        match = Match.Optional(next, match);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "weak", More)) != null)
                    {
                        break;
                    }
                    match = Terminal_(_(start), "__consuming", More);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("DeclarationModifier", start, match);
                }
                Caches[Cache_DeclarationModifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AccessLevelModifier = 11;

        public virtual Match AccessLevelModifier(int start)
        {
            if (!Caches[Cache_AccessLevelModifier].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = AccessModifierBase(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "(", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "set", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Terminal_(_(next2), ")", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    // <-- Sequence
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("AccessLevelModifier", start, match);
                }
                Caches[Cache_AccessLevelModifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AccessModifierBase = 12;

        public virtual Match AccessModifierBase(int start)
        {
            if (!Caches[Cache_AccessModifierBase].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "private", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "fileprivate", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "internal", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "public", More)) != null)
                    {
                        break;
                    }
                    match = Terminal_(_(start), "open", More);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("AccessModifierBase", start, match);
                }
                Caches[Cache_AccessModifierBase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_MutationModifier = 13;

        public virtual Match MutationModifier(int start)
        {
            if (!Caches[Cache_MutationModifier].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "mutating", More)) != null)
                    {
                        break;
                    }
                    match = Terminal_(_(start), "nonmutating", More);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("MutationModifier", start, match);
                }
                Caches[Cache_MutationModifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CompilerControlStatement = 14;

        public virtual Match CompilerControlStatement(int start)
        {
            if (!Caches[Cache_CompilerControlStatement].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("CompilerControlStatement", start, match);
                }
                Caches[Cache_CompilerControlStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConditionalCompilationBlock = 15;

        public virtual Match ConditionalCompilationBlock(int start)
        {
            if (!Caches[Cache_ConditionalCompilationBlock].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                    match = EndifDirective(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ConditionalCompilationBlock", start, match);
                }
                Caches[Cache_ConditionalCompilationBlock].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IfDirectiveClause = 16;

        public virtual Match IfDirectiveClause(int start)
        {
            if (!Caches[Cache_IfDirectiveClause].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = IfDirective(next2)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("if-directive-clause", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("IfDirectiveClause", start, match);
                }
                Caches[Cache_IfDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseifDirectiveClauses = 17;

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

        protected const int Cache_ElseifDirectiveClause = 18;

        public virtual Match ElseifDirectiveClause(int start)
        {
            if (!Caches[Cache_ElseifDirectiveClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ElseifDirectiveClause", start, match);
                }
                Caches[Cache_ElseifDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseDirectiveClause = 19;

        public virtual Match ElseDirectiveClause(int start)
        {
            if (!Caches[Cache_ElseDirectiveClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ElseDirectiveClause", start, match);
                }
                Caches[Cache_ElseDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IfDirective = 20;

        public virtual Match IfDirective(int start)
        {
            if (!Caches[Cache_IfDirective].Already(start, out var match))
            {
                match = Terminal_(_(start), "#if", More);
                if (match != null)
                {
                    match = Match.Success("IfDirective", start, match);
                }
                Caches[Cache_IfDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseifDirective = 21;

        public virtual Match ElseifDirective(int start)
        {
            if (!Caches[Cache_ElseifDirective].Already(start, out var match))
            {
                match = Terminal_(_(start), "#elseif", More);
                if (match != null)
                {
                    match = Match.Success("ElseifDirective", start, match);
                }
                Caches[Cache_ElseifDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseDirective = 22;

        public virtual Match ElseDirective(int start)
        {
            if (!Caches[Cache_ElseDirective].Already(start, out var match))
            {
                match = Terminal_(_(start), "#else", More);
                if (match != null)
                {
                    match = Match.Success("ElseDirective", start, match);
                }
                Caches[Cache_ElseDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EndifDirective = 23;

        public virtual Match EndifDirective(int start)
        {
            if (!Caches[Cache_EndifDirective].Already(start, out var match))
            {
                match = Terminal_(_(start), "#endif", More);
                if (match != null)
                {
                    match = Match.Success("EndifDirective", start, match);
                }
                Caches[Cache_EndifDirective].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CompilationConditionPrimary = 24;

        public virtual Match CompilationConditionPrimary(int start)
        {
            if (!Caches[Cache_CompilationConditionPrimary].Already(start, out var match))
            {
                while (true) // Choice -->
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
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "(", null)) == null)
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
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "!", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = CompilationCondition(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("CompilationConditionPrimary", start, match);
                }
                Caches[Cache_CompilationConditionPrimary].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CompilationCondition = 25;

        public virtual Match CompilationCondition(int start)
        {
            if (!Caches[Cache_CompilationCondition].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            while (true) // Choice -->
                            {
                                if ((match = Terminal_(_(next2), "||", null)) != null)
                                {
                                    break;
                                }
                                match = Terminal_(_(next2), "&&", null);
                                break;
                            }
                            // <-- Choice
                            if (match == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = CompilationConditionPrimary(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("CompilationCondition", start, match);
                }
                Caches[Cache_CompilationCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PlatformCondition = 26;

        public virtual Match PlatformCondition(int start)
        {
            if (!Caches[Cache_PlatformCondition].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "os", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "(", null)) == null)
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
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "arch", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "(", null)) == null)
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
                        match = Terminal_(_(next2), ")", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "swift", More)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Terminal_(_(next3), "(", null)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        while (true) // Choice -->
                        {
                            if ((match = Terminal_(_(next3), ">=", null)) != null)
                            {
                                break;
                            }
                            match = Terminal_(_(next3), "<", null);
                            break;
                        }
                        // <-- Choice
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
                        match = Terminal_(_(next3), ")", null);
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next4), "compiler", More)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = Terminal_(_(next4), "(", null)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        while (true) // Choice -->
                        {
                            if ((match = Terminal_(_(next4), ">=", null)) != null)
                            {
                                break;
                            }
                            match = Terminal_(_(next4), "<", null);
                            break;
                        }
                        // <-- Choice
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
                        match = Terminal_(_(next4), ")", null);
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches4);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next5), "canImport", More)) == null)
                        {
                            break;
                        }
                        matches5.Add(match);
                        next5 = match.Next;
                        if ((match = Terminal_(_(next5), "(", null)) == null)
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
                        match = Terminal_(_(next5), ")", null);
                        matches5.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches5);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next6), "targetEnvironment", More)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = Terminal_(_(next6), "(", null)) == null)
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
                        match = Terminal_(_(next6), ")", null);
                        matches6.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches6);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("PlatformCondition", start, match);
                }
                Caches[Cache_PlatformCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatingSystem = 27;

        public virtual Match OperatingSystem(int start)
        {
            if (!Caches[Cache_OperatingSystem].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "macOS", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "iOS", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "watchOS", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "tvOS", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "Windows", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "Android", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "Linux", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "OpenBSD", More)) != null)
                    {
                        break;
                    }
                    // ERROR -->
                    new Error(Context).Report("unknown operating system", start);
                    throw new BailOutException();
                    // <-- ERROR
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("OperatingSystem", start, match);
                }
                Caches[Cache_OperatingSystem].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Architecture = 28;

        public virtual Match Architecture(int start)
        {
            if (!Caches[Cache_Architecture].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "i386", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "x86_64", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "arm", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "arm64", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "wasm32", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "powerpc64", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "s390x", More)) != null)
                    {
                        break;
                    }
                    // ERROR -->
                    new Error(Context).Report("unknown architecture", start);
                    throw new BailOutException();
                    // <-- ERROR
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("Architecture", start, match);
                }
                Caches[Cache_Architecture].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwiftVersion = 29;

        public virtual Match SwiftVersion(int start)
        {
            if (!Caches[Cache_SwiftVersion].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ".", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = DecimalDigits(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SwiftVersion", start, match);
                }
                Caches[Cache_SwiftVersion].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ModuleName = 30;

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

        protected const int Cache_Environment = 31;

        public virtual Match Environment(int start)
        {
            if (!Caches[Cache_Environment].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "simulator", More)) != null)
                    {
                        break;
                    }
                    match = Terminal_(_(start), "macCatalyst", More);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("Environment", start, match);
                }
                Caches[Cache_Environment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LineControlStatement = 32;

        public virtual Match LineControlStatement(int start)
        {
            if (!Caches[Cache_LineControlStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "#sourceLocation", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "(", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "file:", null)) == null)
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
                        if ((match = Terminal_(_(next2), ",", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "line:", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = LineNumber(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    // <-- Sequence
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), ")", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("LineControlStatement", start, match);
                }
                Caches[Cache_LineControlStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FilePath = 33;

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

        protected const int Cache_LineNumber = 34;

        public virtual Match LineNumber(int start)
        {
            if (!Caches[Cache_LineNumber].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    match = NonzeroDecimalLiteral(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("LineNumber", start, match);
                }
                Caches[Cache_LineNumber].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DiagnosticStatement = 35;

        public virtual Match DiagnosticStatement(int start)
        {
            if (!Caches[Cache_DiagnosticStatement].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "#error", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "(", null)) == null)
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
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "#warning", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "(", null)) == null)
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
                        match = Terminal_(_(next2), ")", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("DiagnosticStatement", start, match);
                }
                Caches[Cache_DiagnosticStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DiagnosticMessage = 36;

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

        protected const int Cache_ConstantDeclaration = 37;

        public virtual Match ConstantDeclaration(int start)
        {
            if (!Caches[Cache_ConstantDeclaration].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = FullPrefix(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "let", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = PatternInitializerList(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = FullPrefix(next2)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "let", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("constant-declaration", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ConstantDeclaration", start, match);
                }
                Caches[Cache_ConstantDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternInitializerList = 38;

        public virtual Match PatternInitializerList(int start)
        {
            if (!Caches[Cache_PatternInitializerList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = PatternInitializer(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PatternInitializerList", start, match);
                }
                Caches[Cache_PatternInitializerList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternInitializer = 39;

        public virtual Match PatternInitializer(int start)
        {
            if (!Caches[Cache_PatternInitializer].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PatternInitializer", start, match);
                }
                Caches[Cache_PatternInitializer].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Initializer = 40;

        public virtual Match Initializer(int start)
        {
            if (!Caches[Cache_Initializer].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "=", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Expression(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "=", null)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("initializer", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("Initializer", start, match);
                }
                Caches[Cache_Initializer].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EnumDeclaration = 41;

        public virtual Match EnumDeclaration(int start)
        {
            if (!Caches[Cache_EnumDeclaration].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = AccessPrefix(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = UnionStyleEnum(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = AccessPrefix(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = RawValueStyleEnum(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("EnumDeclaration", start, match);
                }
                Caches[Cache_EnumDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnum = 42;

        public virtual Match UnionStyleEnum(int start)
        {
            if (!Caches[Cache_UnionStyleEnum].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    match = Match.Optional(next, Terminal_(_(next), "indirect", More));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "enum", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
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
                    match = UnionStyleEnumBody(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnum", start, match);
                }
                Caches[Cache_UnionStyleEnum].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnumBody = 43;

        public virtual Match UnionStyleEnumBody(int start)
        {
            if (!Caches[Cache_UnionStyleEnumBody].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "{", null)) == null)
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
                    match = Terminal_(_(next), "}", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnumBody", start, match);
                }
                Caches[Cache_UnionStyleEnumBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnumMember = 44;

        public virtual Match UnionStyleEnumMember(int start)
        {
            if (!Caches[Cache_UnionStyleEnumMember].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnumMember", start, match);
                }
                Caches[Cache_UnionStyleEnumMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnumCaseClause = 45;

        public virtual Match UnionStyleEnumCaseClause(int start)
        {
            if (!Caches[Cache_UnionStyleEnumCaseClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Terminal_(_(next), "indirect", More));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "case", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    next = match.Next;
                    match = UnionStyleEnumCaseList(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnumCaseClause", start, match);
                }
                Caches[Cache_UnionStyleEnumCaseClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnumCaseList = 46;

        public virtual Match UnionStyleEnumCaseList(int start)
        {
            if (!Caches[Cache_UnionStyleEnumCaseList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = UnionStyleEnumCase(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnumCaseList", start, match);
                }
                Caches[Cache_UnionStyleEnumCaseList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_UnionStyleEnumCase = 47;

        public virtual Match UnionStyleEnumCase(int start)
        {
            if (!Caches[Cache_UnionStyleEnumCase].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("UnionStyleEnumCase", start, match);
                }
                Caches[Cache_UnionStyleEnumCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnum = 48;

        public virtual Match RawValueStyleEnum(int start)
        {
            if (!Caches[Cache_RawValueStyleEnum].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "enum", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
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
                    match = RawValueStyleEnumBody(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnum", start, match);
                }
                Caches[Cache_RawValueStyleEnum].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnumBody = 49;

        public virtual Match RawValueStyleEnumBody(int start)
        {
            if (!Caches[Cache_RawValueStyleEnumBody].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "{", null)) == null)
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
                    match = Terminal_(_(next), "}", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnumBody", start, match);
                }
                Caches[Cache_RawValueStyleEnumBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnumMember = 50;

        public virtual Match RawValueStyleEnumMember(int start)
        {
            if (!Caches[Cache_RawValueStyleEnumMember].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnumMember", start, match);
                }
                Caches[Cache_RawValueStyleEnumMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnumCaseClause = 51;

        public virtual Match RawValueStyleEnumCaseClause(int start)
        {
            if (!Caches[Cache_RawValueStyleEnumCaseClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "case", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    next = match.Next;
                    match = RawValueStyleEnumCaseList(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnumCaseClause", start, match);
                }
                Caches[Cache_RawValueStyleEnumCaseClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnumCaseList = 52;

        public virtual Match RawValueStyleEnumCaseList(int start)
        {
            if (!Caches[Cache_RawValueStyleEnumCaseList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = RawValueStyleEnumCase(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnumCaseList", start, match);
                }
                Caches[Cache_RawValueStyleEnumCaseList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueStyleEnumCase = 53;

        public virtual Match RawValueStyleEnumCase(int start)
        {
            if (!Caches[Cache_RawValueStyleEnumCase].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("RawValueStyleEnumCase", start, match);
                }
                Caches[Cache_RawValueStyleEnumCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueAssignment = 54;

        public virtual Match RawValueAssignment(int start)
        {
            if (!Caches[Cache_RawValueAssignment].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "=", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = RawValueLiteral(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("RawValueAssignment", start, match);
                }
                Caches[Cache_RawValueAssignment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RawValueLiteral = 55;

        public virtual Match RawValueLiteral(int start)
        {
            if (!Caches[Cache_RawValueLiteral].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("RawValueLiteral", start, match);
                }
                Caches[Cache_RawValueLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EnumName = 56;

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

        protected const int Cache_EnumCaseName = 57;

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

        protected const int Cache_ExtensionDeclaration = 58;

        public virtual Match ExtensionDeclaration(int start)
        {
            if (!Caches[Cache_ExtensionDeclaration].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = AccessPrefix(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "extension", More)) == null)
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
                        match = ExtensionBody(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = AccessPrefix(next2)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "extension", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = TypeIdentifier(next2)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("extension-declaration", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = AccessPrefix(next3)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        if ((match = Terminal_(_(next3), "extension", More)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("extension-declaration - type-identifier", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ExtensionDeclaration", start, match);
                }
                Caches[Cache_ExtensionDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ExtensionBody = 59;

        public virtual Match ExtensionBody(int start)
        {
            if (!Caches[Cache_ExtensionBody].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "{", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), "}", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "{", null)) == null)
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
                        match = Terminal_(_(next2), "}", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "{", null)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("extension-body", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ExtensionBody", start, match);
                }
                Caches[Cache_ExtensionBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ExtensionMember = 60;

        public virtual Match ExtensionMember(int start)
        {
            if (!Caches[Cache_ExtensionMember].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ExtensionMember", start, match);
                }
                Caches[Cache_ExtensionMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionDeclaration = 61;

        public virtual Match FunctionDeclaration(int start)
        {
            if (!Caches[Cache_FunctionDeclaration].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                        match = Match.Optional(next, FunctionBody(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = FunctionHead(next2)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("function-declaration", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("FunctionDeclaration", start, match);
                }
                Caches[Cache_FunctionDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionHead = 62;

        public virtual Match FunctionHead(int start)
        {
            if (!Caches[Cache_FunctionHead].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = FullPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), "func", More);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("FunctionHead", start, match);
                }
                Caches[Cache_FunctionHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionSignature = 63;

        public virtual Match FunctionSignature(int start)
        {
            if (!Caches[Cache_FunctionSignature].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("FunctionSignature", start, match);
                }
                Caches[Cache_FunctionSignature].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Maythrow = 64;

        public virtual Match Maythrow(int start)
        {
            if (!Caches[Cache_Maythrow].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "throws", More)) != null)
                    {
                        break;
                    }
                    match = Terminal_(_(start), "rethrows", More);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("Maythrow", start, match);
                }
                Caches[Cache_Maythrow].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionName = 65;

        public virtual Match FunctionName(int start)
        {
            if (!Caches[Cache_FunctionName].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Name(start)) != null)
                    {
                        break;
                    }
                    if ((match = OperatorName(start)) != null)
                    {
                        break;
                    }
                    // ERROR -->
                    new Error(Context).Report("function-name", start);
                    throw new BailOutException();
                    // <-- ERROR
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("FunctionName", start, match);
                }
                Caches[Cache_FunctionName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ParameterClause = 66;

        public virtual Match ParameterClause(int start)
        {
            if (!Caches[Cache_ParameterClause].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "(", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "(", null)) == null)
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
                        match = Terminal_(_(next2), ")", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    // ERROR -->
                    new Error(Context).Report("parameter-clause", start);
                    throw new BailOutException();
                    // <-- ERROR
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ParameterClause", start, match);
                }
                Caches[Cache_ParameterClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ParameterList = 67;

        public virtual Match ParameterList(int start)
        {
            if (!Caches[Cache_ParameterList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = Parameter(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ParameterList", start, match);
                }
                Caches[Cache_ParameterList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Parameter = 68;

        public virtual Match Parameter(int start)
        {
            if (!Caches[Cache_Parameter].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        while (true)
                        {
                            if ((match = Attribute(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next, zomMatches);
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
                        match = DefaultArgumentClause(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        var zomMatches2 = new List<Match>();
                        var zomNext2 = next2;
                        while (true)
                        {
                            if ((match = Attribute(zomNext2)) == null)
                            {
                                break;
                            }
                            zomMatches2.Add(match);
                            zomNext2 = match.Next;
                        }
                        match = Match.Success("*", next2, zomMatches2);
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
                        match = Terminal_(_(next2), "...", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        var zomMatches3 = new List<Match>();
                        var zomNext3 = next3;
                        while (true)
                        {
                            if ((match = Attribute(zomNext3)) == null)
                            {
                                break;
                            }
                            zomMatches3.Add(match);
                            zomNext3 = match.Next;
                        }
                        match = Match.Success("*", next3, zomMatches3);
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
                        match = TypeAnnotation(next3);
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        var zomMatches4 = new List<Match>();
                        var zomNext4 = next4;
                        while (true)
                        {
                            if ((match = Attribute(zomNext4)) == null)
                            {
                                break;
                            }
                            zomMatches4.Add(match);
                            zomNext4 = match.Next;
                        }
                        match = Match.Success("*", next4, zomMatches4);
                        matches4.Add(match);
                        next4 = match.Next;
                        if ((match = LocalName(next4)) == null)
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
                        match = DefaultArgumentClause(next4);
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches4);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        var zomMatches5 = new List<Match>();
                        var zomNext5 = next5;
                        while (true)
                        {
                            if ((match = Attribute(zomNext5)) == null)
                            {
                                break;
                            }
                            zomMatches5.Add(match);
                            zomNext5 = match.Next;
                        }
                        match = Match.Success("*", next5, zomMatches5);
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
                        match = Terminal_(_(next5), "...", null);
                        matches5.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches5);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        var zomMatches6 = new List<Match>();
                        var zomNext6 = next6;
                        while (true)
                        {
                            if ((match = Attribute(zomNext6)) == null)
                            {
                                break;
                            }
                            zomMatches6.Add(match);
                            zomNext6 = match.Next;
                        }
                        match = Match.Success("*", next6, zomMatches6);
                        matches6.Add(match);
                        next6 = match.Next;
                        if ((match = LocalName(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        match = TypeAnnotation(next6);
                        matches6.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches6);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    // ERROR -->
                    new Error(Context).Report("parameter", start);
                    throw new BailOutException();
                    // <-- ERROR
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("Parameter", start, match);
                }
                Caches[Cache_Parameter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionResult = 69;

        public virtual Match FunctionResult(int start)
        {
            if (!Caches[Cache_FunctionResult].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "->", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        while (true)
                        {
                            if ((match = Attribute(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next, zomMatches);
                        matches.Add(match);
                        next = match.Next;
                        match = Type(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), ":", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        var zomMatches2 = new List<Match>();
                        var zomNext2 = next2;
                        while (true)
                        {
                            if ((match = Attribute(zomNext2)) == null)
                            {
                                break;
                            }
                            zomMatches2.Add(match);
                            zomNext2 = match.Next;
                        }
                        match = Match.Success("*", next2, zomMatches2);
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Type(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "->", null)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("function-result", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next4), ":", null)) == null)
                        {
                            break;
                        }
                        next4 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("function-result", next4);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("FunctionResult", start, match);
                }
                Caches[Cache_FunctionResult].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ExternalName = 70;

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

        protected const int Cache_LocalName = 71;

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

        protected const int Cache_DefaultArgumentClause = 72;

        public virtual Match DefaultArgumentClause(int start)
        {
            if (!Caches[Cache_DefaultArgumentClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "=", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Expression(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("DefaultArgumentClause", start, match);
                }
                Caches[Cache_DefaultArgumentClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeAnnotation = 73;

        public virtual Match TypeAnnotation(int start)
        {
            if (!Caches[Cache_TypeAnnotation].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), ":", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        while (true)
                        {
                            if ((match = Attribute(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next, zomMatches);
                        matches.Add(match);
                        next = match.Next;
                        var next2 = next;
                        var matches2 = new List<Match>();
                        while (true) // Sequence -->
                        {
                            while (true) // Choice -->
                            {
                                if ((match = Terminal_(_(next2), "__owned", More)) != null)
                                {
                                    break;
                                }
                                match = Terminal_(_(next2), "__shared", More);
                                break;
                            }
                            // <-- Choice
                            if (match == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = Not_(next2, More(next2));
                            break;
                        }
                        if (match != null)
                        {
                            match = matches2[0];
                        }
                        // <-- Sequence
                        match = Match.Optional(next, match);
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Terminal_(_(next), "inout", More));
                        matches.Add(match);
                        next = match.Next;
                        match = Type(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), ":", null)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        var zomMatches2 = new List<Match>();
                        var zomNext2 = next3;
                        while (true)
                        {
                            if ((match = Attribute(zomNext2)) == null)
                            {
                                break;
                            }
                            zomMatches2.Add(match);
                            zomNext2 = match.Next;
                        }
                        match = Match.Success("*", next3, zomMatches2);
                        next3 = match.Next;
                        var next4 = next3;
                        var matches3 = new List<Match>();
                        while (true) // Sequence -->
                        {
                            while (true) // Choice -->
                            {
                                if ((match = Terminal_(_(next4), "__owned", More)) != null)
                                {
                                    break;
                                }
                                match = Terminal_(_(next4), "__shared", More);
                                break;
                            }
                            // <-- Choice
                            if (match == null)
                            {
                                break;
                            }
                            matches3.Add(match);
                            next4 = match.Next;
                            match = Not_(next4, More(next4));
                            break;
                        }
                        if (match != null)
                        {
                            match = matches3[0];
                        }
                        // <-- Sequence
                        match = Match.Optional(next3, match);
                        next3 = match.Next;
                        match = Match.Optional(next3, Terminal_(_(next3), "inout", More));
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("type-annotation-1", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next5), ":", null)) == null)
                        {
                            break;
                        }
                        next5 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("type-annotation-2", next5);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("TypeAnnotation", start, match);
                }
                Caches[Cache_TypeAnnotation].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionBody = 74;

        public virtual Match FunctionBody(int start)
        {
            if (!Caches[Cache_FunctionBody].Already(start, out var match))
            {
                match = CodeBlock(start);
                if (match != null)
                {
                    match = Match.Success("FunctionBody", start, match);
                }
                Caches[Cache_FunctionBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InitializerDeclaration = 75;

        public virtual Match InitializerDeclaration(int start)
        {
            if (!Caches[Cache_InitializerDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("InitializerDeclaration", start, match);
                }
                Caches[Cache_InitializerDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InitializerHead = 76;

        public virtual Match InitializerHead(int start)
        {
            if (!Caches[Cache_InitializerHead].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = FullPrefix(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "init", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Not_(next, More(next))) == null)
                        {
                            break;
                        }
                        next = match.Next;
                        while (true) // Choice -->
                        {
                            if ((match = Terminal_(_(next), "?", null)) != null)
                            {
                                break;
                            }
                            match = Terminal_(_(next), "!", null);
                            break;
                        }
                        // <-- Choice
                        match = Match.Optional(next, match);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = FullPrefix(next2)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "init", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = Not_(next2, More(next2))) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("initializer-head", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("InitializerHead", start, match);
                }
                Caches[Cache_InitializerHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InitializerBody = 77;

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

        protected const int Cache_OperatorDeclaration = 78;

        public virtual Match OperatorDeclaration(int start)
        {
            if (!Caches[Cache_OperatorDeclaration].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("OperatorDeclaration", start, match);
                }
                Caches[Cache_OperatorDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrefixOperatorDeclaration = 79;

        public virtual Match PrefixOperatorDeclaration(int start)
        {
            if (!Caches[Cache_PrefixOperatorDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "prefix", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "operator", More)) == null)
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
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), ":", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = OperatorRestrictions(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    // <-- Sequence
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PrefixOperatorDeclaration", start, match);
                }
                Caches[Cache_PrefixOperatorDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PostfixOperatorDeclaration = 80;

        public virtual Match PostfixOperatorDeclaration(int start)
        {
            if (!Caches[Cache_PostfixOperatorDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "postfix", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "operator", More)) == null)
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
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), ":", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = OperatorRestrictions(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    // <-- Sequence
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PostfixOperatorDeclaration", start, match);
                }
                Caches[Cache_PostfixOperatorDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InfixOperatorDeclaration = 81;

        public virtual Match InfixOperatorDeclaration(int start)
        {
            if (!Caches[Cache_InfixOperatorDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "infix", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "operator", More)) == null)
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("InfixOperatorDeclaration", start, match);
                }
                Caches[Cache_InfixOperatorDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InfixOperatorGroup = 82;

        public virtual Match InfixOperatorGroup(int start)
        {
            if (!Caches[Cache_InfixOperatorGroup].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), ":", null)) == null)
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
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), ",", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = OperatorRestrictions(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    // <-- Sequence
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("InfixOperatorGroup", start, match);
                }
                Caches[Cache_InfixOperatorGroup].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorRestrictions = 83;

        public virtual Match OperatorRestrictions(int start)
        {
            if (!Caches[Cache_OperatorRestrictions].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = OperatorRestriction(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("OperatorRestrictions", start, match);
                }
                Caches[Cache_OperatorRestrictions].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorRestriction = 84;

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

        protected const int Cache_PrecedenceGroupDeclaration = 85;

        public virtual Match PrecedenceGroupDeclaration(int start)
        {
            if (!Caches[Cache_PrecedenceGroupDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "precedencegroup", More)) == null)
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
                    match = PrecedenceGroupBody(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupDeclaration", start, match);
                }
                Caches[Cache_PrecedenceGroupDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupBody = 86;

        public virtual Match PrecedenceGroupBody(int start)
        {
            if (!Caches[Cache_PrecedenceGroupBody].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "{", null)) == null)
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
                    match = Terminal_(_(next), "}", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupBody", start, match);
                }
                Caches[Cache_PrecedenceGroupBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupAttribute = 87;

        public virtual Match PrecedenceGroupAttribute(int start)
        {
            if (!Caches[Cache_PrecedenceGroupAttribute].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupAttribute", start, match);
                }
                Caches[Cache_PrecedenceGroupAttribute].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupRelation = 88;

        public virtual Match PrecedenceGroupRelation(int start)
        {
            if (!Caches[Cache_PrecedenceGroupRelation].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    while (true) // Choice -->
                    {
                        if ((match = Terminal_(_(next), "higherThan", More)) != null)
                        {
                            break;
                        }
                        match = Terminal_(_(next), "lowerThan", More);
                        break;
                    }
                    // <-- Choice
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), ":", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = PrecedenceGroupNames(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupRelation", start, match);
                }
                Caches[Cache_PrecedenceGroupRelation].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupAssignment = 89;

        public virtual Match PrecedenceGroupAssignment(int start)
        {
            if (!Caches[Cache_PrecedenceGroupAssignment].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "assignment", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), ":", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = BooleanLiteral(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupAssignment", start, match);
                }
                Caches[Cache_PrecedenceGroupAssignment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupAssociativity = 90;

        public virtual Match PrecedenceGroupAssociativity(int start)
        {
            if (!Caches[Cache_PrecedenceGroupAssociativity].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "associativity", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), ":", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    while (true) // Choice -->
                    {
                        if ((match = Terminal_(_(next), "left", More)) != null)
                        {
                            break;
                        }
                        if ((match = Terminal_(_(next), "right", More)) != null)
                        {
                            break;
                        }
                        match = Terminal_(_(next), "none", More);
                        break;
                    }
                    // <-- Choice
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupAssociativity", start, match);
                }
                Caches[Cache_PrecedenceGroupAssociativity].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupNames = 91;

        public virtual Match PrecedenceGroupNames(int start)
        {
            if (!Caches[Cache_PrecedenceGroupNames].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = PrecedenceGroupName(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PrecedenceGroupNames", start, match);
                }
                Caches[Cache_PrecedenceGroupNames].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrecedenceGroupName = 92;

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

        protected const int Cache_ProtocolDeclaration = 93;

        public virtual Match ProtocolDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = AccessPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "protocol", More)) == null)
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
                    match = ProtocolBody(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ProtocolDeclaration", start, match);
                }
                Caches[Cache_ProtocolDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolName = 94;

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

        protected const int Cache_ProtocolBody = 95;

        public virtual Match ProtocolBody(int start)
        {
            if (!Caches[Cache_ProtocolBody].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "{", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, ProtocolMembers(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), "}", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "{", null)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("protocol-body", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ProtocolBody", start, match);
                }
                Caches[Cache_ProtocolBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolMembers = 96;

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

        protected const int Cache_ProtocolMember = 97;

        public virtual Match ProtocolMember(int start)
        {
            if (!Caches[Cache_ProtocolMember].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = ProtocolMemberDeclaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ProtocolMember", start, match);
                }
                Caches[Cache_ProtocolMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolMemberDeclaration = 98;

        public virtual Match ProtocolMemberDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolMemberDeclaration].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ProtocolMemberDeclaration", start, match);
                }
                Caches[Cache_ProtocolMemberDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolPropertyDeclaration = 99;

        public virtual Match ProtocolPropertyDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolPropertyDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                    match = GetterSetterKeywordBlock(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ProtocolPropertyDeclaration", start, match);
                }
                Caches[Cache_ProtocolPropertyDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolMethodDeclaration = 100;

        public virtual Match ProtocolMethodDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolMethodDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ProtocolMethodDeclaration", start, match);
                }
                Caches[Cache_ProtocolMethodDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolInitializerDeclaration = 101;

        public virtual Match ProtocolInitializerDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolInitializerDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ProtocolInitializerDeclaration", start, match);
                }
                Caches[Cache_ProtocolInitializerDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolSubscriptDeclaration = 102;

        public virtual Match ProtocolSubscriptDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolSubscriptDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                    match = GetterSetterKeywordBlock(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ProtocolSubscriptDeclaration", start, match);
                }
                Caches[Cache_ProtocolSubscriptDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolAssociatedTypeDeclaration = 103;

        public virtual Match ProtocolAssociatedTypeDeclaration(int start)
        {
            if (!Caches[Cache_ProtocolAssociatedTypeDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = AccessPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Terminal_(_(next), "override", More));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "associatedtype", More)) == null)
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ProtocolAssociatedTypeDeclaration", start, match);
                }
                Caches[Cache_ProtocolAssociatedTypeDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StructDeclaration = 104;

        public virtual Match StructDeclaration(int start)
        {
            if (!Caches[Cache_StructDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = AccessPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "struct", More)) == null)
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
                    match = StructBody(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("StructDeclaration", start, match);
                }
                Caches[Cache_StructDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StructName = 105;

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

        protected const int Cache_StructBody = 106;

        public virtual Match StructBody(int start)
        {
            if (!Caches[Cache_StructBody].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "{", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        while (true)
                        {
                            if ((match = StructMember(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next, zomMatches);
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), "}", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "{", null)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("struct-body", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("StructBody", start, match);
                }
                Caches[Cache_StructBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StructMember = 107;

        public virtual Match StructMember(int start)
        {
            if (!Caches[Cache_StructMember].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    if ((match = CompilerControlStatement(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Not_(next, Terminal_(_(next), "}", null))) == null)
                        {
                            break;
                        }
                        next = match.Next;
                        // ERROR -->
                        new Error(Context).Report("struct-member", next);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("StructMember", start, match);
                }
                Caches[Cache_StructMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SubscriptDeclaration = 108;

        public virtual Match SubscriptDeclaration(int start)
        {
            if (!Caches[Cache_SubscriptDeclaration].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                        match = GetterSetterBlock(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
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
                        match = GetterSetterKeywordBlock(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("SubscriptDeclaration", start, match);
                }
                Caches[Cache_SubscriptDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SubscriptHead = 109;

        public virtual Match SubscriptHead(int start)
        {
            if (!Caches[Cache_SubscriptHead].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = FullPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "subscript", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, GenericParameterClause(next));
                    matches.Add(match);
                    next = match.Next;
                    match = ParameterClause(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SubscriptHead", start, match);
                }
                Caches[Cache_SubscriptHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SubscriptResult = 110;

        public virtual Match SubscriptResult(int start)
        {
            if (!Caches[Cache_SubscriptResult].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "->", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    match = Type(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SubscriptResult", start, match);
                }
                Caches[Cache_SubscriptResult].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypealiasDeclaration = 111;

        public virtual Match TypealiasDeclaration(int start)
        {
            if (!Caches[Cache_TypealiasDeclaration].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = AccessPrefix(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "typealias", More)) == null)
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
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = AccessPrefix(next2)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "typealias", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("typealias-declaration", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("TypealiasDeclaration", start, match);
                }
                Caches[Cache_TypealiasDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypealiasName = 112;

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

        protected const int Cache_TypealiasAssignment = 113;

        public virtual Match TypealiasAssignment(int start)
        {
            if (!Caches[Cache_TypealiasAssignment].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "=", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Type(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("TypealiasAssignment", start, match);
                }
                Caches[Cache_TypealiasAssignment].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_VariableDeclaration = 114;

        public virtual Match VariableDeclaration(int start)
        {
            if (!Caches[Cache_VariableDeclaration].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                        match = GetterSetterBlock(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
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
                        match = GetterSetterKeywordBlock(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
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
                        match = WillSetDidSetBlock(next3);
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    while (true) // Sequence -->
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
                        match = WillSetDidSetBlock(next4);
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches4);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    var matches5 = new List<Match>();
                    while (true) // Sequence -->
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
                        match = WillSetDidSetBlock(next5);
                        matches5.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches5);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next6 = start;
                    var matches6 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = VariableDeclarationHead(next6)) == null)
                        {
                            break;
                        }
                        matches6.Add(match);
                        next6 = match.Next;
                        match = PatternInitializerList(next6);
                        matches6.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches6);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next7 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = VariableDeclarationHead(next7)) == null)
                        {
                            break;
                        }
                        next7 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("variable-declaration", next7);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("VariableDeclaration", start, match);
                }
                Caches[Cache_VariableDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_VariableDeclarationHead = 115;

        public virtual Match VariableDeclarationHead(int start)
        {
            if (!Caches[Cache_VariableDeclarationHead].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = FullPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), "var", More);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("VariableDeclarationHead", start, match);
                }
                Caches[Cache_VariableDeclarationHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_VariableName = 116;

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

        protected const int Cache_GetterSetterBlock = 117;

        public virtual Match GetterSetterBlock(int start)
        {
            if (!Caches[Cache_GetterSetterBlock].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "{", null)) == null)
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
                        match = Terminal_(_(next), "}", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "{", null)) == null)
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
                        match = Terminal_(_(next2), "}", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    match = CodeBlock(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("GetterSetterBlock", start, match);
                }
                Caches[Cache_GetterSetterBlock].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GetterClause = 118;

        public virtual Match GetterClause(int start)
        {
            if (!Caches[Cache_GetterClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = MutationPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "get", More)) == null)
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("GetterClause", start, match);
                }
                Caches[Cache_GetterClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SpecialModifyClause = 119;

        public virtual Match SpecialModifyClause(int start)
        {
            if (!Caches[Cache_SpecialModifyClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = MutationPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "_modify", More)) == null)
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SpecialModifyClause", start, match);
                }
                Caches[Cache_SpecialModifyClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SetterClause = 120;

        public virtual Match SetterClause(int start)
        {
            if (!Caches[Cache_SetterClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = MutationPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "set", More)) == null)
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SetterClause", start, match);
                }
                Caches[Cache_SetterClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SetterName = 121;

        public virtual Match SetterName(int start)
        {
            if (!Caches[Cache_SetterName].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "(", null)) == null)
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
                    match = Terminal_(_(next), ")", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SetterName", start, match);
                }
                Caches[Cache_SetterName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GetterSetterKeywordBlock = 122;

        public virtual Match GetterSetterKeywordBlock(int start)
        {
            if (!Caches[Cache_GetterSetterKeywordBlock].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "{", null)) == null)
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
                        match = Terminal_(_(next), "}", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "{", null)) == null)
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
                        match = Terminal_(_(next2), "}", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("GetterSetterKeywordBlock", start, match);
                }
                Caches[Cache_GetterSetterKeywordBlock].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GetterKeywordClause = 123;

        public virtual Match GetterKeywordClause(int start)
        {
            if (!Caches[Cache_GetterKeywordClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = MutationPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), "get", More);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("GetterKeywordClause", start, match);
                }
                Caches[Cache_GetterKeywordClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SetterKeywordClause = 124;

        public virtual Match SetterKeywordClause(int start)
        {
            if (!Caches[Cache_SetterKeywordClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = MutationPrefix(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), "set", More);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SetterKeywordClause", start, match);
                }
                Caches[Cache_SetterKeywordClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WillSetDidSetBlock = 125;

        public virtual Match WillSetDidSetBlock(int start)
        {
            if (!Caches[Cache_WillSetDidSetBlock].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "{", null)) == null)
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
                        match = Terminal_(_(next), "}", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "{", null)) == null)
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
                        match = Terminal_(_(next2), "}", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("WillSetDidSetBlock", start, match);
                }
                Caches[Cache_WillSetDidSetBlock].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WillSetClause = 126;

        public virtual Match WillSetClause(int start)
        {
            if (!Caches[Cache_WillSetClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "willSet", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, SetterName(next));
                    matches.Add(match);
                    next = match.Next;
                    match = CodeBlock(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("WillSetClause", start, match);
                }
                Caches[Cache_WillSetClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DidSetClause = 127;

        public virtual Match DidSetClause(int start)
        {
            if (!Caches[Cache_DidSetClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "didSet", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, SetterName(next));
                    matches.Add(match);
                    next = match.Next;
                    match = CodeBlock(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("DidSetClause", start, match);
                }
                Caches[Cache_DidSetClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Declaration = 128;

        public virtual Match Declaration(int start)
        {
            if (!Caches[Cache_Declaration].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                }
                Caches[Cache_Declaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ImportDeclaration = 129;

        public virtual Match ImportDeclaration(int start)
        {
            if (!Caches[Cache_ImportDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "import", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, ImportKind(next));
                    matches.Add(match);
                    next = match.Next;
                    match = ImportPath(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ImportDeclaration", start, match);
                }
                Caches[Cache_ImportDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ImportKind = 130;

        public virtual Match ImportKind(int start)
        {
            if (!Caches[Cache_ImportKind].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "typealias", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "struct", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "class", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "enum", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "protocol", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "let", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "var", More)) != null)
                    {
                        break;
                    }
                    match = Terminal_(_(start), "func", More);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ImportKind", start, match);
                }
                Caches[Cache_ImportKind].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ImportPath = 131;

        public virtual Match ImportPath(int start)
        {
            if (!Caches[Cache_ImportPath].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ".", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = ImportPathIdentifier(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ImportPath", start, match);
                }
                Caches[Cache_ImportPath].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ImportPathIdentifier = 132;

        public virtual Match ImportPathIdentifier(int start)
        {
            if (!Caches[Cache_ImportPathIdentifier].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Name(start)) != null)
                    {
                        break;
                    }
                    match = Operator(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ImportPathIdentifier", start, match);
                }
                Caches[Cache_ImportPathIdentifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClassDeclaration = 133;

        public virtual Match ClassDeclaration(int start)
        {
            if (!Caches[Cache_ClassDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                    match = ClassBody(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ClassDeclaration", start, match);
                }
                Caches[Cache_ClassDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClassHead = 134;

        public virtual Match ClassHead(int start)
        {
            if (!Caches[Cache_ClassHead].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        while (true)
                        {
                            if ((match = Attribute(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next, zomMatches);
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, AccessLevelModifier(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, Terminal_(_(next), "final", More));
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), "class", More);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        var zomMatches2 = new List<Match>();
                        var zomNext2 = next2;
                        while (true)
                        {
                            if ((match = Attribute(zomNext2)) == null)
                            {
                                break;
                            }
                            zomMatches2.Add(match);
                            zomNext2 = match.Next;
                        }
                        match = Match.Success("*", next2, zomMatches2);
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "final", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Match.Optional(next2, AccessLevelModifier(next2));
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Terminal_(_(next2), "class", More);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ClassHead", start, match);
                }
                Caches[Cache_ClassHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClassName = 135;

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

        protected const int Cache_ClassBody = 136;

        public virtual Match ClassBody(int start)
        {
            if (!Caches[Cache_ClassBody].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "{", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, ClassMembers(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), "}", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ClassBody", start, match);
                }
                Caches[Cache_ClassBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClassMembers = 137;

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

        protected const int Cache_ClassMember = 138;

        public virtual Match ClassMember(int start)
        {
            if (!Caches[Cache_ClassMember].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Declaration(start)) != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ClassMember", start, match);
                }
                Caches[Cache_ClassMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DeinitializerDeclaration = 139;

        public virtual Match DeinitializerDeclaration(int start)
        {
            if (!Caches[Cache_DeinitializerDeclaration].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "deinit", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = CodeBlock(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("DeinitializerDeclaration", start, match);
                }
                Caches[Cache_DeinitializerDeclaration].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Expression = 140;

        public virtual Match Expression(int start)
        {
            if (!Caches[Cache_Expression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("Expression", start, match);
                }
                Caches[Cache_Expression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BinaryExpressions = 141;

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

        protected const int Cache_BinaryExpression = 142;

        public virtual Match BinaryExpression(int start)
        {
            if (!Caches[Cache_BinaryExpression].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                        match = PrefixExpression(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
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
                        match = PrefixExpression(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = BinaryOperator(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        match = PrefixExpression(next3);
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    match = TypeCastingOperator(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("BinaryExpression", start, match);
                }
                Caches[Cache_BinaryExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrefixExpression = 143;

        public virtual Match PrefixExpression(int start)
        {
            if (!Caches[Cache_PrefixExpression].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        match = Match.Optional(next, PrefixOperator(next));
                        matches.Add(match);
                        next = match.Next;
                        match = PostfixExpression(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    match = InOutExpression(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("PrefixExpression", start, match);
                }
                Caches[Cache_PrefixExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InOutExpression = 144;

        public virtual Match InOutExpression(int start)
        {
            if (!Caches[Cache_InOutExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "&", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Name(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("InOutExpression", start, match);
                }
                Caches[Cache_InOutExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PostfixExpression = 145;

        public virtual Match PostfixExpression(int start)
        {
            if (!Caches[Cache_PostfixExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PostfixExpression", start, match);
                }
                Caches[Cache_PostfixExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PostfixAppendix = 146;

        public virtual Match PostfixAppendix(int start)
        {
            if (!Caches[Cache_PostfixAppendix].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                }
                Caches[Cache_PostfixAppendix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionCall = 147;

        public virtual Match FunctionCall(int start)
        {
            if (!Caches[Cache_FunctionCall].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = FunctionCallArgumentClause(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = TrailingClosures(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("FunctionCall", start, match);
                }
                Caches[Cache_FunctionCall].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionCallArgumentClause = 148;

        public virtual Match FunctionCallArgumentClause(int start)
        {
            if (!Caches[Cache_FunctionCallArgumentClause].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "(", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, FunctionCallArgumentList(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "(", null)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("function-call-argument-clause", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("FunctionCallArgumentClause", start, match);
                }
                Caches[Cache_FunctionCallArgumentClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionCallArgumentList = 149;

        public virtual Match FunctionCallArgumentList(int start)
        {
            if (!Caches[Cache_FunctionCallArgumentList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = FunctionCallArgument(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("FunctionCallArgumentList", start, match);
                }
                Caches[Cache_FunctionCallArgumentList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionCallArgument = 150;

        public virtual Match FunctionCallArgument(int start)
        {
            if (!Caches[Cache_FunctionCallArgument].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Terminal_(_(next2), ":", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    // <-- Sequence
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    while (true) // Choice -->
                    {
                        if ((match = Expression(next)) != null)
                        {
                            break;
                        }
                        match = OperatorName(next);
                        break;
                    }
                    // <-- Choice
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("FunctionCallArgument", start, match);
                }
                Caches[Cache_FunctionCallArgument].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TrailingClosures = 151;

        public virtual Match TrailingClosures(int start)
        {
            if (!Caches[Cache_TrailingClosures].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), ":", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = And_(next, ClosureExpression(next))) == null)
                        {
                            break;
                        }
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
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), ":", null)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = And_(next2, Terminal_(_(next2), "{", null))) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("trailing-closures", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("TrailingClosures", start, match);
                }
                Caches[Cache_TrailingClosures].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LabeledTrailingClosures = 152;

        public virtual Match LabeledTrailingClosures(int start)
        {
            if (!Caches[Cache_LabeledTrailingClosures].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("LabeledTrailingClosures", start, match);
                }
                Caches[Cache_LabeledTrailingClosures].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LabeledTrailingClosure = 153;

        public virtual Match LabeledTrailingClosure(int start)
        {
            if (!Caches[Cache_LabeledTrailingClosure].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Name(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), ":", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = ClosureExpression(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("LabeledTrailingClosure", start, match);
                }
                Caches[Cache_LabeledTrailingClosure].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_InitializerAppendix = 154;

        public virtual Match InitializerAppendix(int start)
        {
            if (!Caches[Cache_InitializerAppendix].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), ".", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "init", More)) == null)
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("InitializerAppendix", start, match);
                }
                Caches[Cache_InitializerAppendix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArgumentNameClause = 155;

        public virtual Match ArgumentNameClause(int start)
        {
            if (!Caches[Cache_ArgumentNameClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "(", null)) == null)
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
                    match = Terminal_(_(next), ")", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ArgumentNameClause", start, match);
                }
                Caches[Cache_ArgumentNameClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArgumentNames = 156;

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

        protected const int Cache_ArgumentName = 157;

        public virtual Match ArgumentName(int start)
        {
            if (!Caches[Cache_ArgumentName].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Name(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), ":", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ArgumentName", start, match);
                }
                Caches[Cache_ArgumentName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ExplicitMember = 158;

        public virtual Match ExplicitMember(int start)
        {
            if (!Caches[Cache_ExplicitMember].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), ".", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = DecimalDigits(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), ".", null)) == null)
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
                        match = ArgumentNameClause(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), ".", null)) == null)
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
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ExplicitMember", start, match);
                }
                Caches[Cache_ExplicitMember].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PostfixSelf = 159;

        public virtual Match PostfixSelf(int start)
        {
            if (!Caches[Cache_PostfixSelf].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), ".", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), "self", More);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PostfixSelf", start, match);
                }
                Caches[Cache_PostfixSelf].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Subscript = 160;

        public virtual Match Subscript(int start)
        {
            if (!Caches[Cache_Subscript].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "[", null)) == null)
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
                    match = Terminal_(_(next), "]", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("Subscript", start, match);
                }
                Caches[Cache_Subscript].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ForcedValue = 161;

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

        protected const int Cache_OptionalChaining = 162;

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

        protected const int Cache_PrimaryExpression = 163;

        public virtual Match PrimaryExpression(int start)
        {
            if (!Caches[Cache_PrimaryExpression].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = PrimaryName(start)) != null)
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
                    if ((match = ClampedExpression(start)) != null)
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
                // <-- Choice
                if (match != null)
                {
                }
                Caches[Cache_PrimaryExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrimaryName = 164;

        public virtual Match PrimaryName(int start)
        {
            if (!Caches[Cache_PrimaryName].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PrimaryName", start, match);
                }
                Caches[Cache_PrimaryName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LiteralExpression = 165;

        public virtual Match LiteralExpression(int start)
        {
            if (!Caches[Cache_LiteralExpression].Already(start, out var match))
            {
                while (true) // Choice -->
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
                    if ((match = Terminal_(_(start), "#file", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "#fileID", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "#filePath", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "#line", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "#column", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "#function", More)) != null)
                    {
                        break;
                    }
                    match = Terminal_(_(start), "#dsohandle", More);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                }
                Caches[Cache_LiteralExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Literal = 166;

        public virtual Match Literal(int start)
        {
            if (!Caches[Cache_Literal].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                }
                Caches[Cache_Literal].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_NumericLiteral = 167;

        public virtual Match NumericLiteral(int start)
        {
            if (!Caches[Cache_NumericLiteral].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    match = Match.Optional(next, Terminal_(_(next), "-", null));
                    matches.Add(match);
                    next = match.Next;
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    while (true) // Choice -->
                    {
                        if ((match = FloatingPointLiteral(next)) != null)
                        {
                            break;
                        }
                        match = IntegerLiteral(next);
                        break;
                    }
                    // <-- Choice
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("NumericLiteral", start, match);
                }
                Caches[Cache_NumericLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BooleanLiteral = 168;

        public virtual Match BooleanLiteral(int start)
        {
            if (!Caches[Cache_BooleanLiteral].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "true", More)) != null)
                    {
                        break;
                    }
                    match = Terminal_(_(start), "false", More);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("BooleanLiteral", start, match);
                }
                Caches[Cache_BooleanLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_NilLiteral = 169;

        public virtual Match NilLiteral(int start)
        {
            if (!Caches[Cache_NilLiteral].Already(start, out var match))
            {
                match = Terminal_(_(start), "nil", More);
                if (match != null)
                {
                    match = Match.Success("NilLiteral", start, match);
                }
                Caches[Cache_NilLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArrayLiteral = 170;

        public virtual Match ArrayLiteral(int start)
        {
            if (!Caches[Cache_ArrayLiteral].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "[", null)) == null)
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
                        match = Terminal_(_(next), "]", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "[", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Terminal_(_(next2), "]", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ArrayLiteral", start, match);
                }
                Caches[Cache_ArrayLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArrayLiteralItems = 171;

        public virtual Match ArrayLiteralItems(int start)
        {
            if (!Caches[Cache_ArrayLiteralItems].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = ArrayLiteralItem(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                    match = Match.Optional(next, Terminal_(_(next), ",", null));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ArrayLiteralItems", start, match);
                }
                Caches[Cache_ArrayLiteralItems].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArrayLiteralItem = 172;

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

        protected const int Cache_DictionaryLiteral = 173;

        public virtual Match DictionaryLiteral(int start)
        {
            if (!Caches[Cache_DictionaryLiteral].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "[", null)) == null)
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
                        match = Terminal_(_(next), "]", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "[", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), ":", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Terminal_(_(next2), "]", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("DictionaryLiteral", start, match);
                }
                Caches[Cache_DictionaryLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DictionaryLiteralItems = 174;

        public virtual Match DictionaryLiteralItems(int start)
        {
            if (!Caches[Cache_DictionaryLiteralItems].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = DictionaryLiteralItem(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                    match = Match.Optional(next, Terminal_(_(next), ",", null));
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("DictionaryLiteralItems", start, match);
                }
                Caches[Cache_DictionaryLiteralItems].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DictionaryLiteralItem = 175;

        public virtual Match DictionaryLiteralItem(int start)
        {
            if (!Caches[Cache_DictionaryLiteralItem].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), ":", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Expression(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("DictionaryLiteralItem", start, match);
                }
                Caches[Cache_DictionaryLiteralItem].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PlaygroundLiteral = 176;

        public virtual Match PlaygroundLiteral(int start)
        {
            if (!Caches[Cache_PlaygroundLiteral].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "#colorLiteral", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "(", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "red", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), ":", null)) == null)
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
                        if ((match = Terminal_(_(next), ",", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "green", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), ":", null)) == null)
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
                        if ((match = Terminal_(_(next), ",", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "blue", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), ":", null)) == null)
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
                        if ((match = Terminal_(_(next), ",", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "alpha", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), ":", null)) == null)
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
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "#fileLiteral", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "(", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "resourceName", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), ":", null)) == null)
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
                        match = Terminal_(_(next2), ")", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "#imageLiteral", More)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Terminal_(_(next3), "(", null)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Terminal_(_(next3), "resourceName", More)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Terminal_(_(next3), ":", null)) == null)
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
                        match = Terminal_(_(next3), ")", null);
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("PlaygroundLiteral", start, match);
                }
                Caches[Cache_PlaygroundLiteral].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SelfExpression = 177;

        public virtual Match SelfExpression(int start)
        {
            if (!Caches[Cache_SelfExpression].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "self", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), ".", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "init", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Not_(next, More(next));
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "self", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), ".", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Name(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "self", More)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Terminal_(_(next3), "[", null)) == null)
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
                        match = Terminal_(_(next3), "]", null);
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next4), "self", More)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        match = Not_(next4, More(next4));
                        break;
                    }
                    if (match != null)
                    {
                        match = matches4[0];
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("SelfExpression", start, match);
                }
                Caches[Cache_SelfExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SuperclassExpression = 178;

        public virtual Match SuperclassExpression(int start)
        {
            if (!Caches[Cache_SuperclassExpression].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "super", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), ".", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "init", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Not_(next, More(next));
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "super", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), ".", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Name(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "super", More)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Terminal_(_(next3), "[", null)) == null)
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
                        match = Terminal_(_(next3), "]", null);
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("SuperclassExpression", start, match);
                }
                Caches[Cache_SuperclassExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureExpression = 179;

        public virtual Match ClosureExpression(int start)
        {
            if (!Caches[Cache_ClosureExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "{", null)) == null)
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
                    match = Terminal_(_(next), "}", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ClosureExpression", start, match);
                }
                Caches[Cache_ClosureExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureSignature = 180;

        public virtual Match ClosureSignature(int start)
        {
            if (!Caches[Cache_ClosureSignature].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                        match = Match.Optional(next, Terminal_(_(next), "throws", More));
                        matches.Add(match);
                        next = match.Next;
                        match = Match.Optional(next, FunctionResult(next));
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), "in", More);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = CaptureList(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Terminal_(_(next2), "in", More);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ClosureSignature", start, match);
                }
                Caches[Cache_ClosureSignature].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureParameterClause = 181;

        public virtual Match ClosureParameterClause(int start)
        {
            if (!Caches[Cache_ClosureParameterClause].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "(", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "(", null)) == null)
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
                        match = Terminal_(_(next2), ")", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    match = IdentifierList(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ClosureParameterClause", start, match);
                }
                Caches[Cache_ClosureParameterClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureParameterList = 182;

        public virtual Match ClosureParameterList(int start)
        {
            if (!Caches[Cache_ClosureParameterList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = ClosureParameter(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ClosureParameterList", start, match);
                }
                Caches[Cache_ClosureParameterList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureParameter = 183;

        public virtual Match ClosureParameter(int start)
        {
            if (!Caches[Cache_ClosureParameter].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                        match = Terminal_(_(next), "...", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = ClosureParameterName(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = TypeAnnotation(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    match = ClosureParameterName(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ClosureParameter", start, match);
                }
                Caches[Cache_ClosureParameter].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClosureParameterName = 184;

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

        protected const int Cache_IdentifierList = 185;

        public virtual Match IdentifierList(int start)
        {
            if (!Caches[Cache_IdentifierList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = Name(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("IdentifierList", start, match);
                }
                Caches[Cache_IdentifierList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaptureList = 186;

        public virtual Match CaptureList(int start)
        {
            if (!Caches[Cache_CaptureList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "[", null)) == null)
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
                    match = Terminal_(_(next), "]", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("CaptureList", start, match);
                }
                Caches[Cache_CaptureList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaptureListItems = 187;

        public virtual Match CaptureListItems(int start)
        {
            if (!Caches[Cache_CaptureListItems].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = CaptureListItem(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("CaptureListItems", start, match);
                }
                Caches[Cache_CaptureListItems].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaptureListItem = 188;

        public virtual Match CaptureListItem(int start)
        {
            if (!Caches[Cache_CaptureListItem].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    match = Match.Optional(next, CaptureSpecifier(next));
                    matches.Add(match);
                    next = match.Next;
                    match = Expression(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("CaptureListItem", start, match);
                }
                Caches[Cache_CaptureListItem].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaptureSpecifier = 189;

        public virtual Match CaptureSpecifier(int start)
        {
            if (!Caches[Cache_CaptureSpecifier].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "weak", More)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "unowned", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        var next2 = next;
                        var matches2 = new List<Match>();
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), "(", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            while (true) // Choice -->
                            {
                                if ((match = Terminal_(_(next2), "safe", More)) != null)
                                {
                                    break;
                                }
                                match = Terminal_(_(next2), "unsafe", More);
                                break;
                            }
                            // <-- Choice
                            if (match == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = Terminal_(_(next2), ")", null);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next, matches2);
                        }
                        // <-- Sequence
                        match = Match.Optional(next, match);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("CaptureSpecifier", start, match);
                }
                Caches[Cache_CaptureSpecifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClampedExpression = 190;

        public virtual Match ClampedExpression(int start)
        {
            if (!Caches[Cache_ClampedExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "(", null)) == null)
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
                    match = Terminal_(_(next), ")", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ClampedExpression", start, match);
                }
                Caches[Cache_ClampedExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleExpression = 191;

        public virtual Match TupleExpression(int start)
        {
            if (!Caches[Cache_TupleExpression].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "(", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "(", null)) == null)
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
                        match = Terminal_(_(next2), ")", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("TupleExpression", start, match);
                }
                Caches[Cache_TupleExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleElementList = 192;

        public virtual Match TupleElementList(int start)
        {
            if (!Caches[Cache_TupleElementList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = TupleElement(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", oomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("TupleElementList", start, match);
                }
                Caches[Cache_TupleElementList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleElement = 193;

        public virtual Match TupleElement(int start)
        {
            if (!Caches[Cache_TupleElement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Terminal_(_(next2), ":", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    // <-- Sequence
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    match = Expression(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("TupleElement", start, match);
                }
                Caches[Cache_TupleElement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ImplicitMemberExpression = 194;

        public virtual Match ImplicitMemberExpression(int start)
        {
            if (!Caches[Cache_ImplicitMemberExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), ".", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Name(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ImplicitMemberExpression", start, match);
                }
                Caches[Cache_ImplicitMemberExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WildcardExpression = 195;

        public virtual Match WildcardExpression(int start)
        {
            if (!Caches[Cache_WildcardExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "_", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Not_(next, More(next));
                    break;
                }
                if (match != null)
                {
                    match = matches[0];
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("WildcardExpression", start, match);
                }
                Caches[Cache_WildcardExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathExpression = 196;

        public virtual Match KeyPathExpression(int start)
        {
            if (!Caches[Cache_KeyPathExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "\\\\", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Type(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), ".", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = KeyPathComponents(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("KeyPathExpression", start, match);
                }
                Caches[Cache_KeyPathExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathComponents = 197;

        public virtual Match KeyPathComponents(int start)
        {
            if (!Caches[Cache_KeyPathComponents].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = KeyPathComponent(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), ".", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = KeyPathComponent(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    // <-- Sequence
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("KeyPathComponents", start, match);
                }
                Caches[Cache_KeyPathComponents].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathComponent = 198;

        public virtual Match KeyPathComponent(int start)
        {
            if (!Caches[Cache_KeyPathComponent].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Name(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = KeyPathPostfixes(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("KeyPathComponent", start, match);
                }
                Caches[Cache_KeyPathComponent].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathPostfixes = 199;

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

        protected const int Cache_KeyPathPostfix = 200;

        public virtual Match KeyPathPostfix(int start)
        {
            if (!Caches[Cache_KeyPathPostfix].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "?", null)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "!", null)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "self", More)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "[", null)) == null)
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
                        match = Terminal_(_(next), "]", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("KeyPathPostfix", start, match);
                }
                Caches[Cache_KeyPathPostfix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SelectorExpression = 201;

        public virtual Match SelectorExpression(int start)
        {
            if (!Caches[Cache_SelectorExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "#selector", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "(", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    while (true) // Choice -->
                    {
                        if ((match = Terminal_(_(next), "getter:", null)) != null)
                        {
                            break;
                        }
                        match = Terminal_(_(next), "setter:", null);
                        break;
                    }
                    // <-- Choice
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), ")", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SelectorExpression", start, match);
                }
                Caches[Cache_SelectorExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_KeyPathStringExpression = 202;

        public virtual Match KeyPathStringExpression(int start)
        {
            if (!Caches[Cache_KeyPathStringExpression].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "#keyPath", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "(", null)) == null)
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
                    match = Terminal_(_(next), ")", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("KeyPathStringExpression", start, match);
                }
                Caches[Cache_KeyPathStringExpression].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TryOperator = 203;

        public virtual Match TryOperator(int start)
        {
            if (!Caches[Cache_TryOperator].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "try", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    while (true) // Choice -->
                    {
                        if ((match = Terminal_(_(next), "?", null)) != null)
                        {
                            break;
                        }
                        match = Terminal_(_(next), "!", null);
                        break;
                    }
                    // <-- Choice
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("TryOperator", start, match);
                }
                Caches[Cache_TryOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AssignmentOperator = 204;

        public virtual Match AssignmentOperator(int start)
        {
            if (!Caches[Cache_AssignmentOperator].Already(start, out var match))
            {
                match = Terminal_(_(start), "=", null);
                if (match != null)
                {
                    match = Match.Success("AssignmentOperator", start, match);
                }
                Caches[Cache_AssignmentOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeCastingOperator = 205;

        public virtual Match TypeCastingOperator(int start)
        {
            if (!Caches[Cache_TypeCastingOperator].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "is", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Not_(next, More(next))) == null)
                        {
                            break;
                        }
                        next = match.Next;
                        match = Type(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "as", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Not_(next2, More(next2))) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        while (true) // Choice -->
                        {
                            if ((match = Terminal_(_(next2), "?", null)) != null)
                            {
                                break;
                            }
                            match = Terminal_(_(next2), "!", null);
                            break;
                        }
                        // <-- Choice
                        match = Match.Optional(next2, match);
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Type(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        while (true) // Choice -->
                        {
                            if ((match = Terminal_(_(next3), "is", More)) != null)
                            {
                                break;
                            }
                            match = Terminal_(_(next3), "as", More);
                            break;
                        }
                        // <-- Choice
                        if (match == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        if ((match = Not_(next3, More(next3))) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("type-casting-operator", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("TypeCastingOperator", start, match);
                }
                Caches[Cache_TypeCastingOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConditionalOperator = 206;

        public virtual Match ConditionalOperator(int start)
        {
            if (!Caches[Cache_ConditionalOperator].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                    next = match.Next;
                    if ((match = Terminal_(_(next), "?", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = And_(next, SpaceAfter(next))) == null)
                    {
                        break;
                    }
                    next = match.Next;
                    if ((match = Expression(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), ":", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ConditionalOperator", start, match);
                }
                Caches[Cache_ConditionalOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BinaryOperator = 207;

        public virtual Match BinaryOperator(int start)
        {
            if (!Caches[Cache_BinaryOperator].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                        next = match.Next;
                        if ((match = Operator(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = And_(next, SpaceAfter(next));
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Operator(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Not_(next2, SpaceAfter(next2));
                        break;
                    }
                    if (match != null)
                    {
                        match = matches2[0];
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("BinaryOperator", start, match);
                }
                Caches[Cache_BinaryOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrefixOperator = 208;

        public virtual Match PrefixOperator(int start)
        {
            if (!Caches[Cache_PrefixOperator].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                    next = match.Next;
                    if ((match = Operator(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Not_(next, SpaceAfter(next));
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PrefixOperator", start, match);
                }
                Caches[Cache_PrefixOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PostfixOperator = 209;

        public virtual Match PostfixOperator(int start)
        {
            if (!Caches[Cache_PostfixOperator].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Operator(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = And_(next, SpaceAfter(next));
                        break;
                    }
                    if (match != null)
                    {
                        match = matches[0];
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Operator(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = And_(next2, CharacterExact_(next2, '.'));
                        break;
                    }
                    if (match != null)
                    {
                        match = matches2[0];
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("PostfixOperator", start, match);
                }
                Caches[Cache_PostfixOperator].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OperatorName = 210;

        public virtual Match OperatorName(int start)
        {
            if (!Caches[Cache_OperatorName].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    match = Operator(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("OperatorName", start, match);
                }
                Caches[Cache_OperatorName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Operator = 211;

        public virtual Match Operator(int start)
        {
            if (!Caches[Cache_Operator].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
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
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("Operator", start, match);
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
                if (match != null)
                {
                    match = Match.Success("OperatorHead", start, match);
                }
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
                if (match != null)
                {
                    match = Match.Success("OperatorCharacter", start, match);
                }
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
                if (match != null)
                {
                    match = Match.Success("DotOperatorHead", start, match);
                }
                Caches[Cache_DotOperatorHead].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DotOperatorCharacter = 215;

        public virtual Match DotOperatorCharacter(int start)
        {
            if (!Caches[Cache_DotOperatorCharacter].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = CharacterExact_(start, '.')) != null)
                    {
                        break;
                    }
                    match = OperatorCharacter(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("DotOperatorCharacter", start, match);
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
                if (match != null)
                {
                    match = Match.Success("Puncts", start, match);
                }
                Caches[Cache_Puncts].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SpaceBefore = 217;

        public virtual Match SpaceBefore(int start)
        {
            if (!Caches[Cache_SpaceBefore].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("SpaceBefore", start, match);
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
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("SpaceAfter", start, match);
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
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "<", null)) == null)
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
                        match = Terminal_(_(next), ">", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "<", null)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("generic-parameters", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("GenericParameterClause", start, match);
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
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = GenericParameter(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("GenericParameters", start, match);
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
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = TypeName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), ":", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = ProtocolCompositionType(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = TypeName(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), ":", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = TypeIdentifier(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    if ((match = TypeName(start)) != null)
                    {
                        break;
                    }
                    // ERROR -->
                    new Error(Context).Report("generic-parameter", start);
                    throw new BailOutException();
                    // <-- ERROR
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("GenericParameter", start, match);
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
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "where", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = RequirementList(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "where", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("where-clause", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = And_(next3, Terminal_(_(next3), "where", More))) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("where-clause", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("GenericWhereClause", start, match);
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
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = Requirement(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("RequirementList", start, match);
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
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = TypeIdentifier(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), ":", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = ProtocolCompositionType(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = TypeIdentifier(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), ":", null)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = TypeIdentifier(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = TypeIdentifier(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        if ((match = Terminal_(_(next3), "==", null)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        match = Type(next3);
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("Requirement", start, match);
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
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "<", null)) == null)
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
                    match = Terminal_(_(next), ">", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("GenericArgumentClause", start, match);
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
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = GenericArgument(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("GenericArguments", start, match);
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
                if (match != null)
                {
                    match = Match.Success("GenericArgument", start, match);
                }
                Caches[Cache_GenericArgument].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StringLiteral = 228;

        public virtual Match StringLiteral(int start)
        {
            if (!Caches[Cache_StringLiteral].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = InterpolatedStringLiteral(start)) != null)
                    {
                        break;
                    }
                    match = StaticStringLiteral(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("StringLiteral", start, match);
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
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                        match = MultilineStringLiteralClosingDelimiter(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
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
                        match = StringLiteralClosingDelimiter(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
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
            while (true) // Choice -->
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                    match = MultilineStringLiteralClosingDelimiter(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                var next2 = start;
                var matches2 = new List<Match>();
                while (true) // Sequence -->
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
                    match = StringLiteralClosingDelimiter(next2);
                    matches2.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches2);
                }
                // <-- Sequence
                break;
            }
            // <-- Choice
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
            while (true) // Choice -->
            {
                if ((match = EscapedCharacter(start)) != null)
                {
                    break;
                }
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    while (true) // Choice -->
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
                    // <-- Choice
                    match = Not_(next, match);
                    if (match == null)
                    {
                        break;
                    }
                    next = match.Next;
                    match = CharacterAny_(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = matches[0];
                }
                // <-- Sequence
                break;
            }
            // <-- Choice
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
            while (true) // Choice -->
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
                while (true) // Sequence -->
                {
                    while (true) // Choice -->
                    {
                        if ((match = CharacterExact_(next, '\\')) != null)
                        {
                            break;
                        }
                        match = MultilineStringLiteralClosingDelimiter(next);
                        break;
                    }
                    // <-- Choice
                    match = Not_(next, match);
                    if (match == null)
                    {
                        break;
                    }
                    next = match.Next;
                    match = CharacterAny_(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = matches[0];
                }
                // <-- Sequence
                break;
            }
            // <-- Choice
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
            while (true) // Choice -->
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                    match = Terminal_(_(next), ")", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                match = QuotedTextItem(start);
                break;
            }
            // <-- Choice
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
            while (true) // Choice -->
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                    match = Terminal_(_(next), ")", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                match = MultilineQuotedTextItem(start);
                break;
            }
            // <-- Choice
            if (match != null)
            {
                match = Match.Success("MultilineInterpolatedTextItem", start, match);
            }
            return match;
        }

        public virtual Match EscapedCharacter(int start)
        {
            Match match;
            while (true) // Choice -->
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = EscapeSequence(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = CharacterExact_(next, '0');
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                var next2 = start;
                var matches2 = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = EscapeSequence(next2)) == null)
                    {
                        break;
                    }
                    matches2.Add(match);
                    next2 = match.Next;
                    match = CharacterExact_(next2, '\\');
                    matches2.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches2);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                var next3 = start;
                var matches3 = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = EscapeSequence(next3)) == null)
                    {
                        break;
                    }
                    matches3.Add(match);
                    next3 = match.Next;
                    match = CharacterExact_(next3, 't');
                    matches3.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches3);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                var next4 = start;
                var matches4 = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = EscapeSequence(next4)) == null)
                    {
                        break;
                    }
                    matches4.Add(match);
                    next4 = match.Next;
                    match = CharacterExact_(next4, 'n');
                    matches4.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches4);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                var next5 = start;
                var matches5 = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = EscapeSequence(next5)) == null)
                    {
                        break;
                    }
                    matches5.Add(match);
                    next5 = match.Next;
                    match = CharacterExact_(next5, 'r');
                    matches5.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches5);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                var next6 = start;
                var matches6 = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = EscapeSequence(next6)) == null)
                    {
                        break;
                    }
                    matches6.Add(match);
                    next6 = match.Next;
                    match = CharacterExact_(next6, '\"');
                    matches6.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches6);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                var next7 = start;
                var matches7 = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = EscapeSequence(next7)) == null)
                    {
                        break;
                    }
                    matches7.Add(match);
                    next7 = match.Next;
                    match = CharacterExact_(next7, '\'');
                    matches7.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches7);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                var next8 = start;
                var matches8 = new List<Match>();
                while (true) // Sequence -->
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
                    match = CharacterExact_(next8, '}');
                    matches8.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches8);
                }
                // <-- Sequence
                break;
            }
            // <-- Choice
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
            while (true) // Sequence -->
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
            // <-- Sequence
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
            while (true) // Sequence -->
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
                match = LineBreak(next);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            // <-- Sequence
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
            while (true) // Sequence -->
            {
                match = _(next);
                matches.Add(match);
                next = match.Next;
                match = Match.Optional(next, ExtendedStringLiteralDelimiter(next));
                matches.Add(match);
                next = match.Next;
                match = CharacterExact_(next, '\"');
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            // <-- Sequence
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
            while (true) // Sequence -->
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
            // <-- Sequence
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
            while (true) // Sequence -->
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
                match = CharacterExact_(next, '\"');
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            // <-- Sequence
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
            while (true) // Sequence -->
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
            // <-- Sequence
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

        protected const int Cache_Name = 247;

        public virtual Match Name(int start)
        {
            if (!Caches[Cache_Name].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    match = _(next);
                    matches.Add(match);
                    next = match.Next;
                    match = Identifier(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
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
            while (true) // Choice -->
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    while (true) // Choice -->
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
                    // <-- Choice
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
                        while (true) // Choice -->
                        {
                            while (true) // Choice -->
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
                            // <-- Choice
                            if (match != null)
                            {
                                break;
                            }
                            match = CharacterRange_(zomNext, '0', '9');
                            break;
                        }
                        // <-- Choice
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
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                var next2 = start;
                var matches2 = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = CharacterExact_(next2, '`')) == null)
                    {
                        break;
                    }
                    matches2.Add(match);
                    next2 = match.Next;
                    while (true) // Choice -->
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
                    // <-- Choice
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
                        while (true) // Choice -->
                        {
                            while (true) // Choice -->
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
                            // <-- Choice
                            if (match != null)
                            {
                                break;
                            }
                            match = CharacterRange_(zomNext2, '0', '9');
                            break;
                        }
                        // <-- Choice
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
                    match = CharacterExact_(next2, '`');
                    matches2.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches2);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                var next3 = start;
                var matches3 = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = CharacterExact_(next3, '$')) == null)
                    {
                        break;
                    }
                    matches3.Add(match);
                    next3 = match.Next;
                    match = DecimalDigits(next3);
                    matches3.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches3);
                }
                // <-- Sequence
                if (match != null)
                {
                    break;
                }
                var next4 = start;
                var matches4 = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Choice -->
                        {
                            while (true) // Choice -->
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
                            // <-- Choice
                            if (match != null)
                            {
                                break;
                            }
                            match = CharacterRange_(oomNext, '0', '9');
                            break;
                        }
                        // <-- Choice
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
                // <-- Sequence
                break;
            }
            // <-- Choice
            if (match != null)
            {
                match = Match.Success("Identifier", start, match.Next);
            }
            return match;
        }

        public virtual Match IdentifierHead(int start)
        {
            Match match;
            while (true) // Choice -->
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
            // <-- Choice
            if (match != null)
            {
                match = Match.Success("IdentifierHead", start, match);
            }
            return match;
        }

        public virtual Match IdentifierCharacter(int start)
        {
            Match match;
            while (true) // Choice -->
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    break;
                }
                match = CharacterRange_(start, '0', '9');
                break;
            }
            // <-- Choice
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
            while (true) // Sequence -->
            {
                if ((match = CharacterExact_(next, '$')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                match = DecimalDigits(next);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            // <-- Sequence
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
            while (true) // Sequence -->
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
                    while (true) // Choice -->
                    {
                        while (true) // Choice -->
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
                        // <-- Choice
                        if (match != null)
                        {
                            break;
                        }
                        match = CharacterRange_(oomNext, '0', '9');
                        break;
                    }
                    // <-- Choice
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
            // <-- Sequence
            if (match != null)
            {
                match = Match.Success("PropertyWrapperProjection", start, match);
            }
            return match;
        }

        protected const int Cache_More = 253;

        public virtual Match More(int start)
        {
            if (!Caches[Cache_More].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    while (true) // Choice -->
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
                    // <-- Choice
                    if (match != null)
                    {
                        break;
                    }
                    match = CharacterRange_(start, '0', '9');
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("More", start, match);
                }
                Caches[Cache_More].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IntegerLiteral = 254;

        public virtual Match IntegerLiteral(int start)
        {
            if (!Caches[Cache_IntegerLiteral].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
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
            while (true) // Sequence -->
            {
                if ((match = CharacterSequence_(next, "0b")) == null)
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
            // <-- Sequence
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
            while (true) // Choice -->
            {
                if ((match = BinaryDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            // <-- Choice
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
            while (true) // Sequence -->
            {
                if ((match = CharacterSequence_(next, "0o")) == null)
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
            // <-- Sequence
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
            while (true) // Choice -->
            {
                if ((match = OctalDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            // <-- Choice
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
            while (true) // Sequence -->
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
            // <-- Sequence
            if (match != null)
            {
                match = Match.Success("DecimalLiteral", start, match);
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
                while (true) // Sequence -->
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
                // <-- Sequence
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
            while (true) // Choice -->
            {
                if ((match = DecimalDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            // <-- Choice
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
            while (true) // Sequence -->
            {
                if ((match = CharacterSequence_(next, "0x")) == null)
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
            // <-- Sequence
            if (match != null)
            {
                match = Match.Success("HexadecimalLiteral", start, match);
            }
            return match;
        }

        public virtual Match HexadecimalDigit(int start)
        {
            Match match;
            while (true) // Choice -->
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
            // <-- Choice
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
            while (true) // Choice -->
            {
                if ((match = HexadecimalDigit(start)) != null)
                {
                    break;
                }
                match = CharacterExact_(start, '_');
                break;
            }
            // <-- Choice
            if (match != null)
            {
                match = Match.Success("HexadecimalLiteralCharacter", start, match);
            }
            return match;
        }

        protected const int Cache_FloatingPointLiteral = 270;

        public virtual Match FloatingPointLiteral(int start)
        {
            if (!Caches[Cache_FloatingPointLiteral].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = DecimalLiteral(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        if ((match = DecimalFraction(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = DecimalExponent(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = DecimalLiteral(next3)) == null)
                        {
                            break;
                        }
                        matches3.Add(match);
                        next3 = match.Next;
                        match = DecimalExponent(next3);
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    var matches4 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = DecimalFraction(next4)) == null)
                        {
                            break;
                        }
                        matches4.Add(match);
                        next4 = match.Next;
                        match = DecimalExponent(next4);
                        matches4.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches4);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    match = DecimalFraction(start);
                    break;
                }
                // <-- Choice
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
            while (true) // Sequence -->
            {
                if ((match = CharacterExact_(next, '.')) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                match = DecimalLiteral(next);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            // <-- Sequence
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
            while (true) // Sequence -->
            {
                if ((match = CharacterSet_(next, "eE")) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                match = Match.Optional(next, CharacterSet_(next, "+-"));
                matches.Add(match);
                next = match.Next;
                match = DecimalLiteral(next);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            // <-- Sequence
            if (match != null)
            {
                match = Match.Success("DecimalExponent", start, match);
            }
            return match;
        }

        public virtual Match HexadecimalFraction(int start)
        {
            Match match;
            var next = start;
            var matches = new List<Match>();
            while (true) // Sequence -->
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
            // <-- Sequence
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
            while (true) // Sequence -->
            {
                if ((match = CharacterSet_(next, "pP")) == null)
                {
                    break;
                }
                matches.Add(match);
                next = match.Next;
                match = Match.Optional(next, CharacterSet_(next, "+-"));
                matches.Add(match);
                next = match.Next;
                match = DecimalLiteral(next);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            // <-- Sequence
            if (match != null)
            {
                match = Match.Success("HexadecimalExponent", start, match);
            }
            return match;
        }

        protected const int Cache_DecimalDigits = 275;

        public virtual Match DecimalDigits(int start)
        {
            if (!Caches[Cache_DecimalDigits].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("DecimalDigits", start, match);
                }
                Caches[Cache_DecimalDigits].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Pattern = 276;

        public virtual Match Pattern(int start)
        {
            if (!Caches[Cache_Pattern].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = PrimaryPattern(next2)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        while (true) // Choice -->
                        {
                            if ((match = Terminal_(_(next2), "?", null)) != null)
                            {
                                break;
                            }
                            if ((match = Terminal_(_(next2), ".", null)) != null)
                            {
                                break;
                            }
                            match = Terminal_(_(next2), "as", More);
                            break;
                        }
                        // <-- Choice
                        match = And_(next2, match);
                        if (match == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("pattern - pattern-postfix", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("Pattern", start, match);
                }
                Caches[Cache_Pattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrimaryPattern = 277;

        public virtual Match PrimaryPattern(int start)
        {
            if (!Caches[Cache_PrimaryPattern].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("PrimaryPattern", start, match);
                }
                Caches[Cache_PrimaryPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternPostfix = 278;

        public virtual Match PatternPostfix(int start)
        {
            if (!Caches[Cache_PatternPostfix].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("PatternPostfix", start, match);
                }
                Caches[Cache_PatternPostfix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternOptional = 279;

        public virtual Match PatternOptional(int start)
        {
            if (!Caches[Cache_PatternOptional].Already(start, out var match))
            {
                match = Terminal_(_(start), "?", null);
                if (match != null)
                {
                    match = Match.Success("PatternOptional", start, match);
                }
                Caches[Cache_PatternOptional].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternCase = 280;

        public virtual Match PatternCase(int start)
        {
            if (!Caches[Cache_PatternCase].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), ".", null)) == null)
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
                    match = TuplePattern(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PatternCase", start, match);
                }
                Caches[Cache_PatternCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PatternAs = 281;

        public virtual Match PatternAs(int start)
        {
            if (!Caches[Cache_PatternAs].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "as", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Type(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "as", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("pattern-as - type", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("PatternAs", start, match);
                }
                Caches[Cache_PatternAs].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WildcardPattern = 282;

        public virtual Match WildcardPattern(int start)
        {
            if (!Caches[Cache_WildcardPattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "_", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Not_(next, More(next));
                    break;
                }
                if (match != null)
                {
                    match = matches[0];
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("WildcardPattern", start, match);
                }
                Caches[Cache_WildcardPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ValueBindingPattern = 283;

        public virtual Match ValueBindingPattern(int start)
        {
            if (!Caches[Cache_ValueBindingPattern].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "var", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Pattern(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "let", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Pattern(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "var", More)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("value-binding-pattern - pattern", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next4 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next4), "let", More)) == null)
                        {
                            break;
                        }
                        next4 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("value-binding-pattern - pattern", next4);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ValueBindingPattern", start, match);
                }
                Caches[Cache_ValueBindingPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TuplePattern = 284;

        public virtual Match TuplePattern(int start)
        {
            if (!Caches[Cache_TuplePattern].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "(", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "(", null)) == null)
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
                        match = Terminal_(_(next2), ")", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "(", null)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("tuple-pattern", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("TuplePattern", start, match);
                }
                Caches[Cache_TuplePattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TuplePatternElementList = 285;

        public virtual Match TuplePatternElementList(int start)
        {
            if (!Caches[Cache_TuplePatternElementList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = TuplePatternElement(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("TuplePatternElementList", start, match);
                }
                Caches[Cache_TuplePatternElementList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TuplePatternElement = 286;

        public virtual Match TuplePatternElement(int start)
        {
            if (!Caches[Cache_TuplePatternElement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Name(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Terminal_(_(next2), ":", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    // <-- Sequence
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    next = match.Next;
                    match = Pattern(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("TuplePatternElement", start, match);
                }
                Caches[Cache_TuplePatternElement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_EnumCasePattern = 287;

        public virtual Match EnumCasePattern(int start)
        {
            if (!Caches[Cache_EnumCasePattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    match = Match.Optional(next, TypeIdentifier(next));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), ".", null)) == null)
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("EnumCasePattern", start, match);
                }
                Caches[Cache_EnumCasePattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IsPattern = 288;

        public virtual Match IsPattern(int start)
        {
            if (!Caches[Cache_IsPattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "is", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Type(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("IsPattern", start, match);
                }
                Caches[Cache_IsPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConditionList = 289;

        public virtual Match ConditionList(int start)
        {
            if (!Caches[Cache_ConditionList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = Condition(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ConditionList", start, match);
                }
                Caches[Cache_ConditionList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Condition = 290;

        public virtual Match Condition(int start)
        {
            if (!Caches[Cache_Condition].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("Condition", start, match);
                }
                Caches[Cache_Condition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseCondition = 291;

        public virtual Match CaseCondition(int start)
        {
            if (!Caches[Cache_CaseCondition].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "case", More)) == null)
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
                    match = Initializer(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("CaseCondition", start, match);
                }
                Caches[Cache_CaseCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OptionalBindingCondition = 292;

        public virtual Match OptionalBindingCondition(int start)
        {
            if (!Caches[Cache_OptionalBindingCondition].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "let", More)) == null)
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
                        match = Initializer(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "var", More)) == null)
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
                        match = Initializer(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("OptionalBindingCondition", start, match);
                }
                Caches[Cache_OptionalBindingCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AvailableCondition = 293;

        public virtual Match AvailableCondition(int start)
        {
            if (!Caches[Cache_AvailableCondition].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "#available", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "(", null)) == null)
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
                    match = Terminal_(_(next), ")", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("AvailableCondition", start, match);
                }
                Caches[Cache_AvailableCondition].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AvailabilityArguments = 294;

        public virtual Match AvailabilityArguments(int start)
        {
            if (!Caches[Cache_AvailabilityArguments].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = AvailabilityArgument(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("AvailabilityArguments", start, match);
                }
                Caches[Cache_AvailabilityArguments].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AvailabilityArgument = 295;

        public virtual Match AvailabilityArgument(int start)
        {
            if (!Caches[Cache_AvailabilityArgument].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = PlatformName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = PlatformVersion(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    match = Terminal_(_(start), "*", null);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("AvailabilityArgument", start, match);
                }
                Caches[Cache_AvailabilityArgument].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PlatformName = 296;

        public virtual Match PlatformName(int start)
        {
            if (!Caches[Cache_PlatformName].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = Terminal_(_(start), "iOS", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "iOSApplicationExtension", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "macOS", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "macOSApplicationExtension", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "macCatalyst", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "macCatalystApplicationExtension", More)) != null)
                    {
                        break;
                    }
                    if ((match = Terminal_(_(start), "watchOS", More)) != null)
                    {
                        break;
                    }
                    match = Terminal_(_(start), "tvOS", More);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("PlatformName", start, match);
                }
                Caches[Cache_PlatformName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PlatformVersion = 297;

        public virtual Match PlatformVersion(int start)
        {
            if (!Caches[Cache_PlatformVersion].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = DecimalDigits(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    var next2 = next;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), ".", null)) == null)
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next3), ".", null)) == null)
                            {
                                break;
                            }
                            matches3.Add(match);
                            next3 = match.Next;
                            match = DecimalDigits(next3);
                            matches3.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", next2, matches3);
                        }
                        // <-- Sequence
                        match = Match.Optional(next2, match);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", next, matches2);
                    }
                    // <-- Sequence
                    match = Match.Optional(next, match);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("PlatformVersion", start, match);
                }
                Caches[Cache_PlatformVersion].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchStatement = 298;

        public virtual Match SwitchStatement(int start)
        {
            if (!Caches[Cache_SwitchStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "switch", More)) == null)
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
                    match = SwitchBody(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SwitchStatement", start, match);
                }
                Caches[Cache_SwitchStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchBody = 299;

        public virtual Match SwitchBody(int start)
        {
            if (!Caches[Cache_SwitchBody].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "{", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), "}", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "{", null)) == null)
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
                        match = Terminal_(_(next2), "}", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "{", null)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("switch-body", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("SwitchBody", start, match);
                }
                Caches[Cache_SwitchBody].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchCases = 300;

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

        protected const int Cache_SwitchCase = 301;

        public virtual Match SwitchCase(int start)
        {
            if (!Caches[Cache_SwitchCase].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
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
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    match = ConditionalSwitchCase(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("SwitchCase", start, match);
                }
                Caches[Cache_SwitchCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseLabel = 302;

        public virtual Match CaseLabel(int start)
        {
            if (!Caches[Cache_CaseLabel].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        var zomMatches = new List<Match>();
                        var zomNext = next;
                        while (true)
                        {
                            if ((match = Attribute(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next, zomMatches);
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Terminal_(_(next), "case", More)) == null)
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
                        match = Terminal_(_(next), ":", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        var zomMatches2 = new List<Match>();
                        var zomNext2 = next2;
                        while (true)
                        {
                            if ((match = Attribute(zomNext2)) == null)
                            {
                                break;
                            }
                            zomMatches2.Add(match);
                            zomNext2 = match.Next;
                        }
                        match = Match.Success("*", next2, zomMatches2);
                        next2 = match.Next;
                        if ((match = Terminal_(_(next2), "case", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("case-label", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("CaseLabel", start, match);
                }
                Caches[Cache_CaseLabel].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseItemList = 303;

        public virtual Match CaseItemList(int start)
        {
            if (!Caches[Cache_CaseItemList].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                            while (true) // Sequence -->
                            {
                                if ((match = Terminal_(_(next2), ",", null)) == null)
                                {
                                    break;
                                }
                                matches2.Add(match);
                                next2 = match.Next;
                                match = CaseItem(next2);
                                matches2.Add(match);
                                break;
                            }
                            if (match != null)
                            {
                                match = Match.Success("_", zomNext, matches2);
                            }
                            // <-- Sequence
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
                        match = And_(next, Terminal_(_(next), ":", null));
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = CaseItem(next3)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        var oomMatches = new List<Match>();
                        var oomNext = next3;
                        while (true)
                        {
                            var next4 = oomNext;
                            var matches3 = new List<Match>();
                            while (true) // Sequence -->
                            {
                                if ((match = Terminal_(_(next4), ",", null)) == null)
                                {
                                    break;
                                }
                                matches3.Add(match);
                                next4 = match.Next;
                                match = CaseItem(next4);
                                matches3.Add(match);
                                break;
                            }
                            if (match != null)
                            {
                                match = Match.Success("_", oomNext, matches3);
                            }
                            // <-- Sequence
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
                        next3 = match.Next;
                        if ((match = Not_(next3, Terminal_(_(next3), ":", null))) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("case-item-list", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next5 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = CaseItem(next5)) == null)
                        {
                            break;
                        }
                        next5 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("case-item-list: expected `,`", next5);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("CaseItemList", start, match);
                }
                Caches[Cache_CaseItemList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CaseItem = 304;

        public virtual Match CaseItem(int start)
        {
            if (!Caches[Cache_CaseItem].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("CaseItem", start, match);
                }
                Caches[Cache_CaseItem].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DefaultLabel = 305;

        public virtual Match DefaultLabel(int start)
        {
            if (!Caches[Cache_DefaultLabel].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "default", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), ":", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("DefaultLabel", start, match);
                }
                Caches[Cache_DefaultLabel].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ConditionalSwitchCase = 306;

        public virtual Match ConditionalSwitchCase(int start)
        {
            if (!Caches[Cache_ConditionalSwitchCase].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                    match = EndifDirective(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ConditionalSwitchCase", start, match);
                }
                Caches[Cache_ConditionalSwitchCase].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchIfDirectiveClause = 307;

        public virtual Match SwitchIfDirectiveClause(int start)
        {
            if (!Caches[Cache_SwitchIfDirectiveClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SwitchIfDirectiveClause", start, match);
                }
                Caches[Cache_SwitchIfDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SwitchElseifDirectiveClauses = 308;

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

        protected const int Cache_SwitchElseDirectiveClause = 309;

        public virtual Match SwitchElseDirectiveClause(int start)
        {
            if (!Caches[Cache_SwitchElseDirectiveClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SwitchElseDirectiveClause", start, match);
                }
                Caches[Cache_SwitchElseDirectiveClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Statement = 310;

        public virtual Match Statement(int start)
        {
            if (!Caches[Cache_Statement].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                        match = Match.Optional(next, Terminal_(_(next), ";", null));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    match = CompilerControlStatement(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("Statement", start, match);
                }
                Caches[Cache_Statement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FamousStatement = 311;

        public virtual Match FamousStatement(int start)
        {
            if (!Caches[Cache_FamousStatement].Already(start, out var match))
            {
                while (true) // Choice -->
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
                    while (true) // Sequence -->
                    {
                        if ((match = Not_(next, InSwitch(next))) == null)
                        {
                            break;
                        }
                        next = match.Next;
                        match = Expression(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = matches[0];
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "{", null)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("statement", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                }
                Caches[Cache_FamousStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CodeBlock = 312;

        public virtual Match CodeBlock(int start)
        {
            if (!Caches[Cache_CodeBlock].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "{", null)) == null)
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
                        match = Terminal_(_(next), "}", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "{", null)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("code-block", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("CodeBlock", start, match);
                }
                Caches[Cache_CodeBlock].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_BreakStatement = 313;

        public virtual Match BreakStatement(int start)
        {
            if (!Caches[Cache_BreakStatement].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "break", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Not_(next, InSwitch(next))) == null)
                        {
                            break;
                        }
                        next = match.Next;
                        match = Match.Optional(next, LabelName(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "break", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = Not_(next2, InSwitch(next2))) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("break-statement", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("BreakStatement", start, match);
                }
                Caches[Cache_BreakStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ContinueStatement = 314;

        public virtual Match ContinueStatement(int start)
        {
            if (!Caches[Cache_ContinueStatement].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "continue", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Not_(next, InSwitch(next))) == null)
                        {
                            break;
                        }
                        next = match.Next;
                        match = Match.Optional(next, LabelName(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "continue", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = Not_(next2, InSwitch(next2))) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("continue-statement", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ContinueStatement", start, match);
                }
                Caches[Cache_ContinueStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FallthroughStatement = 315;

        public virtual Match FallthroughStatement(int start)
        {
            if (!Caches[Cache_FallthroughStatement].Already(start, out var match))
            {
                match = Terminal_(_(start), "fallthrough", More);
                if (match != null)
                {
                    match = Match.Success("FallthroughStatement", start, match);
                }
                Caches[Cache_FallthroughStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ReturnStatement = 316;

        public virtual Match ReturnStatement(int start)
        {
            if (!Caches[Cache_ReturnStatement].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "return", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        if ((match = Not_(next, InSwitch(next))) == null)
                        {
                            break;
                        }
                        next = match.Next;
                        match = Match.Optional(next, Expression(next));
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "return", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = Not_(next2, InSwitch(next2))) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("return-statement", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ReturnStatement", start, match);
                }
                Caches[Cache_ReturnStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ThrowStatement = 317;

        public virtual Match ThrowStatement(int start)
        {
            if (!Caches[Cache_ThrowStatement].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "throw", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Expression(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "throw", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("throw-statement", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ThrowStatement", start, match);
                }
                Caches[Cache_ThrowStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ForInStatement = 318;

        public virtual Match ForInStatement(int start)
        {
            if (!Caches[Cache_ForInStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "for", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Terminal_(_(next), "case", More));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Pattern(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "in", More)) == null)
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
                    match = CodeBlock(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ForInStatement", start, match);
                }
                Caches[Cache_ForInStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WhileStatement = 319;

        public virtual Match WhileStatement(int start)
        {
            if (!Caches[Cache_WhileStatement].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "while", More)) == null)
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
                        match = CodeBlock(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "while", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = ConditionList(next2)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("while-statement - code-block", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "while", More)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("while-statement - condition-list", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("WhileStatement", start, match);
                }
                Caches[Cache_WhileStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_RepeatWhileStatement = 320;

        public virtual Match RepeatWhileStatement(int start)
        {
            if (!Caches[Cache_RepeatWhileStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "repeat", More)) == null)
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
                    if ((match = Terminal_(_(next), "while", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Expression(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("RepeatWhileStatement", start, match);
                }
                Caches[Cache_RepeatWhileStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_IfStatement = 321;

        public virtual Match IfStatement(int start)
        {
            if (!Caches[Cache_IfStatement].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "if", More)) == null)
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
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "if", More)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        if ((match = ConditionList(next2)) == null)
                        {
                            break;
                        }
                        next2 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("if-statement - code-block", next2);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next3), "if", More)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("if-statement - condition-list", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("IfStatement", start, match);
                }
                Caches[Cache_IfStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElseClause = 322;

        public virtual Match ElseClause(int start)
        {
            if (!Caches[Cache_ElseClause].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "else", More)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = CodeBlock(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "else", More)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = IfStatement(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = And_(next3, Terminal_(_(next3), "else", More))) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("else-clause", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("ElseClause", start, match);
                }
                Caches[Cache_ElseClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_GuardStatement = 323;

        public virtual Match GuardStatement(int start)
        {
            if (!Caches[Cache_GuardStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "guard", More)) == null)
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
                    if ((match = Terminal_(_(next), "else", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = CodeBlock(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("GuardStatement", start, match);
                }
                Caches[Cache_GuardStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DoStatement = 324;

        public virtual Match DoStatement(int start)
        {
            if (!Caches[Cache_DoStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "do", More)) == null)
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("DoStatement", start, match);
                }
                Caches[Cache_DoStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CatchClause = 325;

        public virtual Match CatchClause(int start)
        {
            if (!Caches[Cache_CatchClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "catch", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, CatchPatternList(next));
                    matches.Add(match);
                    next = match.Next;
                    match = CodeBlock(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("CatchClause", start, match);
                }
                Caches[Cache_CatchClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CatchPatternList = 326;

        public virtual Match CatchPatternList(int start)
        {
            if (!Caches[Cache_CatchPatternList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = CatchPattern(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("CatchPatternList", start, match);
                }
                Caches[Cache_CatchPatternList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_CatchPattern = 327;

        public virtual Match CatchPattern(int start)
        {
            if (!Caches[Cache_CatchPattern].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("CatchPattern", start, match);
                }
                Caches[Cache_CatchPattern].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DeferStatement = 328;

        public virtual Match DeferStatement(int start)
        {
            if (!Caches[Cache_DeferStatement].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "defer", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = CodeBlock(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("DeferStatement", start, match);
                }
                Caches[Cache_DeferStatement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_StatementLabel = 329;

        public virtual Match StatementLabel(int start)
        {
            if (!Caches[Cache_StatementLabel].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = LabelName(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Terminal_(_(next), ":", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("StatementLabel", start, match);
                }
                Caches[Cache_StatementLabel].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_LabelName = 330;

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

        protected const int Cache_InSwitch = 331;

        public virtual Match InSwitch(int start)
        {
            if (!Caches[Cache_InSwitch].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    while (true) // Choice -->
                    {
                        if ((match = Terminal_(_(next), "case", More)) != null)
                        {
                            break;
                        }
                        match = Terminal_(_(next), "default", More);
                        break;
                    }
                    // <-- Choice
                    if (match == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Not_(next, More(next));
                    break;
                }
                if (match != null)
                {
                    match = matches[0];
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("InSwitch", start, match);
                }
                Caches[Cache_InSwitch].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WhereClause = 332;

        public virtual Match WhereClause(int start)
        {
            if (!Caches[Cache_WhereClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "where", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = WhereExpression(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("WhereClause", start, match);
                }
                Caches[Cache_WhereClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_WhereExpression = 333;

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

        protected const int Cache_TupleType = 334;

        public virtual Match TupleType(int start)
        {
            if (!Caches[Cache_TupleType].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "(", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "(", null)) == null)
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
                        match = Terminal_(_(next2), ")", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("TupleType", start, match);
                }
                Caches[Cache_TupleType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleTypeElementList = 335;

        public virtual Match TupleTypeElementList(int start)
        {
            if (!Caches[Cache_TupleTypeElementList].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                            while (true) // Sequence -->
                            {
                                if ((match = Terminal_(_(next2), ",", null)) == null)
                                {
                                    break;
                                }
                                matches2.Add(match);
                                next2 = match.Next;
                                match = TupleTypeElement(next2);
                                matches2.Add(match);
                                break;
                            }
                            if (match != null)
                            {
                                match = Match.Success("_", oomNext, matches2);
                            }
                            // <-- Sequence
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
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    while (true) // Sequence -->
                    {
                        if ((match = TupleTypeElement(next3)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        if ((match = Terminal_(_(next3), ",", null)) == null)
                        {
                            break;
                        }
                        next3 = match.Next;
                        // ERROR -->
                        new Error(Context).Report("tuple-type-element-list - tuple-type-element", next3);
                        throw new BailOutException();
                        // <-- ERROR
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("TupleTypeElementList", start, match);
                }
                Caches[Cache_TupleTypeElementList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TupleTypeElement = 336;

        public virtual Match TupleTypeElement(int start)
        {
            if (!Caches[Cache_TupleTypeElement].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = ElementName(next)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = TypeAnnotation(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    match = Type(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("TupleTypeElement", start, match);
                }
                Caches[Cache_TupleTypeElement].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ElementName = 337;

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

        protected const int Cache_EnumTupleType = 338;

        public virtual Match EnumTupleType(int start)
        {
            if (!Caches[Cache_EnumTupleType].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = TupleType(start)) != null)
                    {
                        break;
                    }
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "(", null)) == null)
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
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("EnumTupleType", start, match);
                }
                Caches[Cache_EnumTupleType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_Type = 339;

        public virtual Match Type(int start)
        {
            if (!Caches[Cache_Type].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("Type", start, match);
                }
                Caches[Cache_Type].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_PrimaryType = 340;

        public virtual Match PrimaryType(int start)
        {
            if (!Caches[Cache_PrimaryType].Already(start, out var match))
            {
                while (true) // Choice -->
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
                    match = ClampedType(start);
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                }
                Caches[Cache_PrimaryType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ClampedType = 341;

        public virtual Match ClampedType(int start)
        {
            if (!Caches[Cache_ClampedType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "(", null)) == null)
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
                    match = Terminal_(_(next), ")", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ClampedType", start, match);
                }
                Caches[Cache_ClampedType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypePostfix = 342;

        public virtual Match TypePostfix(int start)
        {
            if (!Caches[Cache_TypePostfix].Already(start, out var match))
            {
                while (true) // Choice -->
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
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("TypePostfix", start, match);
                }
                Caches[Cache_TypePostfix].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeOptional = 343;

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

        protected const int Cache_TypeUnwrap = 344;

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

        protected const int Cache_TypeMetatype = 345;

        public virtual Match TypeMetatype(int start)
        {
            if (!Caches[Cache_TypeMetatype].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), ".", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    while (true) // Choice -->
                    {
                        var next2 = next;
                        var matches2 = new List<Match>();
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), "Type", More)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = Not_(next2, More(next2));
                            break;
                        }
                        if (match != null)
                        {
                            match = matches2[0];
                        }
                        // <-- Sequence
                        if (match != null)
                        {
                            break;
                        }
                        var next3 = next;
                        var matches3 = new List<Match>();
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next3), "Protocol", More)) == null)
                            {
                                break;
                            }
                            matches3.Add(match);
                            next3 = match.Next;
                            match = Not_(next3, More(next3));
                            break;
                        }
                        if (match != null)
                        {
                            match = matches3[0];
                        }
                        // <-- Sequence
                        break;
                    }
                    // <-- Choice
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("TypeMetatype", start, match);
                }
                Caches[Cache_TypeMetatype].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionType = 346;

        public virtual Match FunctionType(int start)
        {
            if (!Caches[Cache_FunctionType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    var zomMatches = new List<Match>();
                    var zomNext = next;
                    while (true)
                    {
                        if ((match = Attribute(zomNext)) == null)
                        {
                            break;
                        }
                        zomMatches.Add(match);
                        zomNext = match.Next;
                    }
                    match = Match.Success("*", next, zomMatches);
                    matches.Add(match);
                    next = match.Next;
                    if ((match = FunctionTypeArgumentClause(next)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Match.Optional(next, Terminal_(_(next), "throws", More));
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Terminal_(_(next), "->", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Type(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("FunctionType", start, match);
                }
                Caches[Cache_FunctionType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionTypeArgumentClause = 347;

        public virtual Match FunctionTypeArgumentClause(int start)
        {
            if (!Caches[Cache_FunctionTypeArgumentClause].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next), "(", null)) == null)
                        {
                            break;
                        }
                        matches.Add(match);
                        next = match.Next;
                        match = Terminal_(_(next), ")", null);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = Terminal_(_(next2), "(", null)) == null)
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
                        match = Match.Optional(next2, Terminal_(_(next2), "...", null));
                        matches2.Add(match);
                        next2 = match.Next;
                        match = Terminal_(_(next2), ")", null);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("FunctionTypeArgumentClause", start, match);
                }
                Caches[Cache_FunctionTypeArgumentClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionTypeArgumentList = 348;

        public virtual Match FunctionTypeArgumentList(int start)
        {
            if (!Caches[Cache_FunctionTypeArgumentList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = FunctionTypeArgument(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("FunctionTypeArgumentList", start, match);
                }
                Caches[Cache_FunctionTypeArgumentList].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_FunctionTypeArgument = 349;

        public virtual Match FunctionTypeArgument(int start)
        {
            if (!Caches[Cache_FunctionTypeArgument].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    var next = start;
                    var matches = new List<Match>();
                    while (true) // Sequence -->
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
                        match = TypeAnnotation(next);
                        matches.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next2 = start;
                    var matches2 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        if ((match = ArgumentLabel(next2)) == null)
                        {
                            break;
                        }
                        matches2.Add(match);
                        next2 = match.Next;
                        match = TypeAnnotation(next2);
                        matches2.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches2);
                    }
                    // <-- Sequence
                    if (match != null)
                    {
                        break;
                    }
                    var next3 = start;
                    var matches3 = new List<Match>();
                    while (true) // Sequence -->
                    {
                        var zomMatches = new List<Match>();
                        var zomNext = next3;
                        while (true)
                        {
                            if ((match = Attribute(zomNext)) == null)
                            {
                                break;
                            }
                            zomMatches.Add(match);
                            zomNext = match.Next;
                        }
                        match = Match.Success("*", next3, zomMatches);
                        matches3.Add(match);
                        next3 = match.Next;
                        match = Match.Optional(next3, Terminal_(_(next3), "inout", More));
                        matches3.Add(match);
                        next3 = match.Next;
                        match = Type(next3);
                        matches3.Add(match);
                        break;
                    }
                    if (match != null)
                    {
                        match = Match.Success("_", start, matches3);
                    }
                    // <-- Sequence
                    break;
                }
                // <-- Choice
                if (match != null)
                {
                    match = Match.Success("FunctionTypeArgument", start, match);
                }
                Caches[Cache_FunctionTypeArgument].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ArgumentLabel = 350;

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

        protected const int Cache_ArrayType = 351;

        public virtual Match ArrayType(int start)
        {
            if (!Caches[Cache_ArrayType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "[", null)) == null)
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
                    match = Terminal_(_(next), "]", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ArrayType", start, match);
                }
                Caches[Cache_ArrayType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_DictionaryType = 352;

        public virtual Match DictionaryType(int start)
        {
            if (!Caches[Cache_DictionaryType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "[", null)) == null)
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
                    if ((match = Terminal_(_(next), ":", null)) == null)
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
                    match = Terminal_(_(next), "]", null);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("DictionaryType", start, match);
                }
                Caches[Cache_DictionaryType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeIdentifier = 353;

        public virtual Match TypeIdentifier(int start)
        {
            if (!Caches[Cache_TypeIdentifier].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ".", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = TypeIdentifierPart(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("TypeIdentifier", start, match);
                }
                Caches[Cache_TypeIdentifier].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeIdentifierPart = 354;

        public virtual Match TypeIdentifierPart(int start)
        {
            if (!Caches[Cache_TypeIdentifierPart].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("TypeIdentifierPart", start, match);
                }
                Caches[Cache_TypeIdentifierPart].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_ProtocolCompositionType = 355;

        public virtual Match ProtocolCompositionType(int start)
        {
            if (!Caches[Cache_ProtocolCompositionType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), "&", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            if ((match = Not_(next2, OperatorCharacter(next2))) == null)
                            {
                                break;
                            }
                            next2 = match.Next;
                            match = TypeIdentifier(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", oomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("ProtocolCompositionType", start, match);
                }
                Caches[Cache_ProtocolCompositionType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_OpaqueType = 356;

        public virtual Match OpaqueType(int start)
        {
            if (!Caches[Cache_OpaqueType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "some", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    if ((match = Not_(next, More(next))) == null)
                    {
                        break;
                    }
                    next = match.Next;
                    match = Type(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("OpaqueType", start, match);
                }
                Caches[Cache_OpaqueType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_AnyType = 357;

        public virtual Match AnyType(int start)
        {
            if (!Caches[Cache_AnyType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "Any", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Not_(next, More(next));
                    break;
                }
                if (match != null)
                {
                    match = matches[0];
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("AnyType", start, match);
                }
                Caches[Cache_AnyType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_SelfType = 358;

        public virtual Match SelfType(int start)
        {
            if (!Caches[Cache_SelfType].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), "Self", More)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = Not_(next, More(next));
                    break;
                }
                if (match != null)
                {
                    match = matches[0];
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("SelfType", start, match);
                }
                Caches[Cache_SelfType].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeName = 359;

        public virtual Match TypeName(int start)
        {
            if (!Caches[Cache_TypeName].Already(start, out var match))
            {
                match = Name(start);
                if (match != null)
                {
                }
                Caches[Cache_TypeName].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeInheritanceClause = 360;

        public virtual Match TypeInheritanceClause(int start)
        {
            if (!Caches[Cache_TypeInheritanceClause].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    if ((match = Terminal_(_(next), ":", null)) == null)
                    {
                        break;
                    }
                    matches.Add(match);
                    next = match.Next;
                    match = TypeInheritanceList(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = Match.Success("_", start, matches);
                }
                // <-- Sequence
                if (match != null)
                {
                    match = Match.Success("TypeInheritanceClause", start, match);
                }
                Caches[Cache_TypeInheritanceClause].Cache(start, match);
            }
            return match;
        }

        protected const int Cache_TypeInheritanceList = 361;

        public virtual Match TypeInheritanceList(int start)
        {
            if (!Caches[Cache_TypeInheritanceList].Already(start, out var match))
            {
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
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
                        while (true) // Sequence -->
                        {
                            if ((match = Terminal_(_(next2), ",", null)) == null)
                            {
                                break;
                            }
                            matches2.Add(match);
                            next2 = match.Next;
                            match = TypeIdentifier(next2);
                            matches2.Add(match);
                            break;
                        }
                        if (match != null)
                        {
                            match = Match.Success("_", zomNext, matches2);
                        }
                        // <-- Sequence
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
                // <-- Sequence
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

        protected const int Cache_SingleWhitespace = 363;

        public virtual Match SingleWhitespace(int start)
        {
            if (!Caches[Cache_SingleWhitespace].Already(start, out var match))
            {
                while (true) // Choice -->
                {
                    if ((match = LineBreakCharacter(start)) != null)
                    {
                        break;
                    }
                    if ((match = InlineSpace(start)) != null)
                    {
                        break;
                    }
                    if ((match = CharacterExact_(start, '\u0000')) != null)
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
                // <-- Choice
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
            while (true) // Choice -->
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
                if ((match = CharacterExact_(start, '\u0000')) != null)
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
            // <-- Choice
            if (match != null)
            {
                match = Match.Success("WhitespaceItem", start, match);
            }
            return match;
        }

        public virtual Match LineBreak(int start)
        {
            Match match;
            while (true) // Choice -->
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
            // <-- Choice
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
            while (true) // Sequence -->
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
                match = LineBreak(next);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            // <-- Sequence
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
            while (true) // Sequence -->
            {
                if ((match = Not_(next, LineBreakCharacter(next))) == null)
                {
                    break;
                }
                next = match.Next;
                match = CharacterAny_(next);
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = matches[0];
            }
            // <-- Sequence
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
            while (true) // Sequence -->
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
                match = CharacterExact_(next, '/');
                matches.Add(match);
                break;
            }
            if (match != null)
            {
                match = Match.Success("_", start, matches);
            }
            // <-- Sequence
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
            while (true) // Choice -->
            {
                if ((match = MultilineComment(start)) != null)
                {
                    break;
                }
                var next = start;
                var matches = new List<Match>();
                while (true) // Sequence -->
                {
                    while (true) // Choice -->
                    {
                        if ((match = CharacterSequence_(next, "/*")) != null)
                        {
                            break;
                        }
                        match = CharacterSequence_(next, "*/");
                        break;
                    }
                    // <-- Choice
                    match = Not_(next, match);
                    if (match == null)
                    {
                        break;
                    }
                    next = match.Next;
                    match = CharacterAny_(next);
                    matches.Add(match);
                    break;
                }
                if (match != null)
                {
                    match = matches[0];
                }
                // <-- Sequence
                break;
            }
            // <-- Choice
            if (match != null)
            {
                match = Match.Success("MultilineCommentTextItem", start, match);
            }
            return match;
        }

        protected HashSet<string> _keywords = new HashSet<string>
        {
            "never",
            "__always",
            "block",
            "thin",
            "c",
            "readnone",
            "readonly",
            "releasenone",
            "of",
            "init",
            "_",
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
            "__shared",
            "inout",
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
            "try",
            "is",
            "as",
            "where",
            "iOSApplicationExtension",
            "macOSApplicationExtension",
            "macCatalystApplicationExtension",
            "switch",
            "default",
            "break",
            "continue",
            "fallthrough",
            "return",
            "throw",
            "for",
            "while",
            "repeat",
            "if",
            "else",
            "guard",
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
