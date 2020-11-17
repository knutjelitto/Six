using System;
using System.Linq;

namespace SixComp.Support
{
    public class RepAttribute : Attribute
    {
        public RepAttribute(string rep)
        {
            Rep = rep;
        }

        public string Rep { get; }
    }

    public static class TokenHelper
    {
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
