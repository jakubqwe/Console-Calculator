using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public static class ParseHelper
    {
        public static Decimal ReadNumber(this string s, ref int startPos)
        {
            int i;


            for (i = startPos; i != s.Length && (char.IsDigit(s[i]) || s[i] == '.' || (i==startPos && s[i]=='-')); i++) ;

            var num = s.Substring(startPos, i - startPos);
            startPos += i - startPos - 1;
            return decimal.Parse(num);
        }
        public static string ReadFunction(this string s, ref int startPos)
        {
            int i;


            for (i = startPos; i != s.Length && char.IsLetter(s[i]); i++) ;

            var func = s.Substring(startPos, i - startPos);
            startPos += i - startPos - 1;
            return func;
        }
    }
}
