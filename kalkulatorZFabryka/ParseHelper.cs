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

        public static Dzialania ToDzialania(this string s, int pos)
        {
            switch (s[pos])
            {
                case '+':
                    return Dzialania.Dodawanie;
                case '-':
                    return Dzialania.Odejmowanie;
                case '*':
                    return Dzialania.Mnozenie;
                case '/':
                case ':':
                    return Dzialania.Dzielenie;
                case '^':
                    return Dzialania.Potegowanie;
                case '[':
                case '{':
                case '(':
                    return Dzialania.LewyNawias;
                case ']':
                case '}':
                case ')':
                    return Dzialania.PrawyNawias;
                default:
                    return Dzialania.Nieznany;
            }
        }
        
    }
}
