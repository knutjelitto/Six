using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SixComp.Support
{
    public class RepAttribute : Attribute
    {
        public RepAttribute(string rep, ToClass cls = ToClass.None)
        {
            Rep = rep;
            Cls = cls;
        }

        public string Rep { get; }
        public ToClass Cls { get; }
    }

    public class KwRepAttribute : RepAttribute
    {
        public KwRepAttribute(string rep) : base(rep) { }
    }

    public static class TokenHelper
    {
        public static IEnumerable<(ToKind kind, string rep)> GetKeywords()
        {
            var type = typeof(ToKind);

            foreach (var field in type.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var rep = field.GetCustomAttributes(typeof(RepAttribute), false).FirstOrDefault() as RepAttribute;
                if (rep != null && (rep.Cls & ToClass.Keyword) != 0)
                {
                    var kind = (ToKind)(field.GetValue(null) ?? ToKind.ERROR);
                    yield return (kind, rep.Rep);
                }
            }
        }

        public static IEnumerable<ToKind> GetOperators()
        {
            var type = typeof(ToKind);

            foreach (var field in type.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var rep = field.GetCustomAttributes(typeof(RepAttribute), false).FirstOrDefault() as RepAttribute;
                if (rep != null && (rep.Cls & ToClass.Operator) != 0)
                {
                    var kind = (ToKind)(field.GetValue(null) ?? ToKind.ERROR);
                    yield return kind;
                }
            }
        }


        public static string GetRep(this ToKind kind)
        {
            var def = kind.ToString();
            var type = typeof(ToKind);
            var memInfo = type.GetMember(def);
            var rep = memInfo[0].GetCustomAttributes(typeof(RepAttribute), false).FirstOrDefault() as RepAttribute;
            return rep?.Rep ?? def;
        }
    }
}
