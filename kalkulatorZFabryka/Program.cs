using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace calculator
{
    public enum Operations
    {
        Unknown,
        LeftBracket,
        RightBracket,
        Addition,
        Subtraction,
        Division,
        Multiplication,
        Power
    }
    public enum Funkcje
    {
        Zmienna,
        Pi
    }
    class Program
    {
        static bool ComparePriority(Operations operatorToCompare, Operations comparingOperator)
        {
            if(operatorToCompare == Operations.Addition || operatorToCompare == Operations.Subtraction)
            {
                return true;
            }
            if (operatorToCompare == Operations.Multiplication || operatorToCompare == Operations.Division)
            {
                if (comparingOperator >= Operations.Division)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (operatorToCompare == Operations.Power)
            {
                if (comparingOperator >= Operations.Power)
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
        public static List<object> ConvertToRPN(string s)
        {
            List<object> rpn = new List<object>();
            Stack<Operations> operatorStack = new Stack<Operations>();
            for(int i = 0; i<s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    rpn.Add(s.OdczytajLiczbe(ref i));

                }
                else if (s[i] == '(')
                {
                    operatorStack.Push(Operations.LeftBracket);
                }
                else if (s[i] == ')')
                {
                    while (operatorStack.Peek() != Operations.LeftBracket)
                    {
                        rpn.Add(operatorStack.Pop());
                    }
                    operatorStack.Pop();
                }
                else if (char.IsLetter(s[i]))
                {

                }
                else if(!char.IsWhiteSpace(s[i]) && s[i] != '.')
                {
                    while(operatorStack.Count != 0 && 
                          operatorStack.Peek() != Operations.LeftBracket && 
                          ComparePriority(s.ToOperations(i), operatorStack.Peek()))
                    {
                        rpn.Add(operatorStack.Pop());
                    }
                    operatorStack.Push(s.ToOperations(i));
                }
            }
            while (operatorStack.Count != 0)
            {
                rpn.Add(operatorStack.Pop());
            }
            return rpn;
        }
        public static decimal Calculate(string s)
        {
            List<object> rpn = ConvertToRPN(s);
            Stack<decimal> numberStack = new Stack<decimal>();
            foreach(var token in rpn)
            {
               
                if (token.GetType() == typeof(decimal))
                {
                    numberStack.Push((decimal)token);
                }
                if(token.GetType() == typeof(Operations))
                {
                    decimal a = numberStack.Pop();
                    decimal b = numberStack.Pop();
                    numberStack.Push(DoOperation(b, a, (Operations)token));
                }
            }
            return numberStack.Pop();
        }
        public static decimal DoOperation(decimal a, decimal b, Operations dzialanie)
        {
            //Strategia 
            switch (dzialanie)
            {
                case Operations.Addition:
                    return a + b;
                case Operations.Subtraction:
                    return a - b;
                case Operations.Multiplication:
                    return a * b;
                case Operations.Division:
                    return a / b;
                case Operations.Power:
                    return (decimal)Math.Pow((double)a, (double)b);
                default:
                    return decimal.Parse("naucz sie pisac Operations");
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
