using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperatorsDLL;

namespace calculator
{
    
    public class Program
    {
        
        public static List<object> ConvertToRPN(string s)
        {
            List<object> rpn = new List<object>();
            Stack<IOperator> operatorStack = new Stack<IOperator>();
            for(int i = 0; i<s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    rpn.Add(s.ReadNumber(ref i));

                }
                else if (s[i] == '(')
                {
                    operatorStack.Push(OperatorFactory.Create<LeftBracket>());
                }
                else if (s[i] == ')')
                {
                    while (operatorStack.Peek().GetType() != typeof(LeftBracket))
                    {
                        rpn.Add(operatorStack.Pop());
                    }
                    operatorStack.Pop();
                }
                else if (char.IsLetter(s[i]))
                {
                    operatorStack.Push(OperatorParser.ToFunction(s.ReadFunction(ref i)));
                }
                else if(!char.IsWhiteSpace(s[i]) && s[i] != '.')
                {
                    while (operatorStack.Count != 0 &&
                          operatorStack.Peek().GetType() != typeof(LeftBracket) &&
                          s.ToOperator(i).ComparePriority(operatorStack.Peek()))
                    {
                        rpn.Add(operatorStack.Pop());
                    }
                    operatorStack.Push(s.ToOperator(i));
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
               
                if (token is decimal)
                {
                    numberStack.Push((decimal)token);
                }
                if(token is IExecutableOperator)
                {
                    var Operator = (IExecutableOperator)token;
                    var a = numberStack.Pop();
                    var b = numberStack.Pop();
                    numberStack.Push(Operator.CalculateOperator(b, a));
                }
                if(token is IFunction)
                {
                    var Function = (IFunction)token;
                    var a = numberStack.Pop();
                    numberStack.Push(Function.CalculateFunction(a));
                }
            }
            return numberStack.Pop();
        }
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("en-US");
            while (true) {
                string s = Console.ReadLine();
                try
                {
                    Console.WriteLine(Calculate(s));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.WriteLine(ex.GetBaseException());
                }
            }
        }
    }
}
