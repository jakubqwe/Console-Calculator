using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace calculator
{
    public enum Dzialania
    {
        Nieznany,
        LewyNawias,
        PrawyNawias,
        Dodawanie,
        Odejmowanie,
        Dzielenie,
        Mnozenie,
        Potegowanie
    }
    public enum Funkcje
    {
        Zmienna,
        Pi
    }
    class Program
    {
        static bool ComparePriority(Dzialania operatorToCompare, Dzialania comparingOperator)
        {
            if(operatorToCompare == Dzialania.Dodawanie || operatorToCompare == Dzialania.Odejmowanie)
            {
                return true;
            }
            if (operatorToCompare == Dzialania.Mnozenie || operatorToCompare == Dzialania.Dzielenie)
            {
                if (comparingOperator >= Dzialania.Dzielenie)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (operatorToCompare == Dzialania.Potegowanie)
            {
                if (comparingOperator >= Dzialania.Potegowanie)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static List<object> KonwertujNaONP(string s)
        {
            List<object> onp = new List<object>();
            Stack<Dzialania> operatorStack = new Stack<Dzialania>();
            for(int i = 0; i<s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    onp.Add(s.OdczytajLiczbe(ref i));

                }
                else if (s[i] == '(')
                {
                    operatorStack.Push(Dzialania.LewyNawias);
                }
                else if (s[i] == ')')
                {
                    while (operatorStack.Peek() != Dzialania.LewyNawias)
                    {
                        onp.Add(operatorStack.Pop());
                    }
                    operatorStack.Pop();
                }
                else if (char.IsLetter(s[i]))
                {

                }
                else if(!char.IsWhiteSpace(s[i]) && s[i] != '.')
                {
                    while(operatorStack.Count != 0 && 
                          operatorStack.Peek() != Dzialania.LewyNawias && 
                          ComparePriority(s.ToDzialania(i), operatorStack.Peek()))
                    {
                        onp.Add(operatorStack.Pop());
                    }
                    operatorStack.Push(s.ToDzialania(i));
                }
            }
            while (operatorStack.Count != 0)
            {
                onp.Add(operatorStack.Pop());
            }
            return onp;
        }
        public static decimal Calculate(string s)
        {
            List<object> onp = KonwertujNaONP(s);
            Stack<decimal> numberStack = new Stack<decimal>();
            foreach(var token in onp)
            {
               
                if (token.GetType() == typeof(decimal))
                {
                    numberStack.Push((decimal)token);
                }
                if(token.GetType() == typeof(Dzialania))
                {
                    decimal a = numberStack.Pop();
                    decimal b = numberStack.Pop();
                    numberStack.Push(DoOperation(b, a, (Dzialania)token));
                }
            }
            return numberStack.Pop();
        }
        public static decimal DoOperation(decimal a, decimal b, Dzialania dzialanie)
        {
            //Strategia 
            switch (dzialanie)
            {
                case Dzialania.Dodawanie:
                    return a + b;
                case Dzialania.Odejmowanie:
                    return a - b;
                case Dzialania.Mnozenie:
                    return a * b;
                case Dzialania.Dzielenie:
                    return a / b;
                case Dzialania.Potegowanie:
                    return (decimal)Math.Pow((double)a, (double)b);
                default:
                    return decimal.Parse("naucz sie pisac dzialania");
            }
            
        }
        static void Main(string[] args)
        {
            while (true) {
                string s = Console.ReadLine();
                try
                {
                    Console.WriteLine(Calculate(s));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
