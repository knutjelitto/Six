using Six.Support;
using SixPeg.Matchers;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Writing
{
    public class Namer
    {
        private readonly Dictionary<string, int> locals = new Dictionary<string, int>();
        private readonly Dictionary<string, string> ruleNames = new Dictionary<string, string>();
        private int lit_counter = 0;

        private bool IsNameable(string text)
        {
            return text.All(t => IsNameable(t));
        }

        private bool IsNameable(char text)
        {
            return char.IsLetterOrDigit(text) ||
                   text == '@' ||
                   text == '.'
                   ;
        }

        private string Name(string text)
        {
            return text.Replace("@", "at_").Replace(".", "dot_");
        }

        public string NameFor(MatchRule rule)
        {
            var text = rule.Name.Text;
            if (ruleNames.TryGetValue(text, out var name))
            {
                return name;
            }
            if (text.StartsWith('\''))
            {
                text = text[1..^1];
                if (IsNameable(text))
                {
                    text = $"Lit_{Name(text)}";
                }
                else
                {
                    lit_counter += 1;
                    text = $"Lit_{lit_counter}_/*{rule.Name.Text}*/";
                }
            }
            else
            {
                if (text != "_")
                {
                    string[] parts = text.Split('_', '-');
                    text = string.Join(string.Empty, parts.Select(p => p.Capitalize()));
                }
                else
                {
                    text = "_";
                }
            }
            ruleNames.Add(rule.Name.Text, text);
            return text;
        }

        public string Local(string name)
        {
            if (locals.TryGetValue(name, out var count))
            {
                count += 1;
                locals[name] = count;
                return $"{name}{count}";
            }

            locals.Add(name, 1);
            return name;
        }

        public void Reset()
        {
            locals.Clear();
        }
    }
}
