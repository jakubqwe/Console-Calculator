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


            for (i = startPos; i != s.Length && (char.IsDigit(s[i]) || s[i] == '.'); i++) ;

            var liczba = s.Substring(startPos, i - startPos);
            startPos += i - startPos - 1;
            return decimal.Parse(liczba);
        }

    }
}
