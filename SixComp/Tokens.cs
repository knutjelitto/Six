using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixComp
{
    public class Tokens : IReadOnlyList<Token>
    {
        private readonly List<Token> tokens = new List<Token>();

        public Tokens(Context context)
        {
            Context = context;
        }

        public Context Context { get; }
        public Lexer Lexer => Context.Lexer;

        public int Count => tokens.Count;

        public Token this[int index]
        {
            get
            {
                while (this.tokens.Count <= index)
                {
                    var token = Lexer.GetNext();
                    Debug.Assert(token.Index == tokens.Count);
                    this.tokens.Add(token);
                }

                Debug.Assert(this.tokens[index].Index == index);
                return this.tokens[index];
            }
        }

        public void BackupForSplit(Token token)
        {
            Debug.Assert(token.Index < Count);
            while (tokens.Count > token.Index + 1)
            {
                tokens.RemoveAt(tokens.Count - 1);
            }
            tokens[token.Index] = token;
        }

        public IEnumerator<Token> GetEnumerator() => tokens.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)tokens).GetEnumerator();
    }
}
