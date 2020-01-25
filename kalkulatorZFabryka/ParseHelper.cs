using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public static class ParseHelper
    {
        public static Decimal OdczytajLiczbe (this string s, ref int startPos)
        {
            int i;


            for (i = startPos; i!=s.Length &&(char.IsDigit(s[i]) || s[i] == '.') ; i++) ;

            var liczba = s.Substring(startPos, i - startPos);
            startPos += i-startPos-1;
            return decimal.Parse(liczba);
        }

        public static Operations ToOperations(this string s, int pos)
        {
            switch (s[pos])
            {
                case '+':
                    return Operations.Addition;
                case '-':
                    return Operations.Subtraction;
                case '*':
                    return Operations.Multiplication;
                case '/':
                case ':':
                    return Operations.Division;
                case '^':
                    return Operations.Power;
                case '[':
                case '{':
                case '(':
                    return Operations.LeftBracket;
                case ']':
                case '}':
                case ')':
                    return Operations.RightBracket;
                default:
                    return Operations.Unknown;
            }
        }
        
    }
}
