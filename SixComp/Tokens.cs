using System.Collections;
using System.Collections.Generic;

namespace SixComp
{
    public class Tokens : IReadOnlyList<Token>
    {
        private readonly List<Token> tokens = new List<Token>();
        private readonly Lexer lexer;

        public Tokens(Context context)
        {
            Context = context;
            lexer = Context.Lexer;
        }

        public Context Context { get; }

        public Token this[int index]
        {
            get
            {
                while (this.tokens.Count <= index)
                {
                    this.tokens.Add(lexer.GetNext());
                }

                return this.tokens[index];
            }
        }

        public int Count => tokens.Count;
        public IEnumerator<Token> GetEnumerator() => tokens.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)tokens).GetEnumerator();
    }
}
