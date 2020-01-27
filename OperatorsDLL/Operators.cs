using System;

namespace OperatorsDLL
{

    public interface IOperator
    {
        uint Weight { get; }
       
    }
    public interface IExecutableOperator : IOperator
    {
        decimal CalculateOperator(decimal a, decimal b);

    }
    
    public class Default : IOperator
    {
        public uint Weight => 0;
    }
    public class Addition : IExecutableOperator
    {
        public uint Weight => 2;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return a + b;
        }
    }
    public class Subtraction : IExecutableOperator
    {
        public uint Weight => 2;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return a - b;
        }
    }
    public class Multiplication : IExecutableOperator
    {
        public uint Weight => 3;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return a * b;
        }
    }
    public class Division : IExecutableOperator
    {
        public uint Weight => 3;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return a / b;
        }
    }
    public class Modulo : IExecutableOperator
    {
        public uint Weight => 3;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return a % b;
        }
    }
    public class Power : IExecutableOperator
    {
        public uint Weight => 4;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return (decimal)Math.Pow((double)a, (double)b); ;
        }
    }

    public class LeftBracket : IOperator
    {
        public uint Weight => 1;
    }
    public class RightBracket : IOperator
    {
        public uint Weight => 5;
    }
    public static class OperatorFactory
    {
        public static T Create<T>() where T : IOperator, new()
        {
            return new T();
        }
    }

    public static class OperatorParser
    {
        public static IOperator ToOperator(this string s, int pos)
        {
            switch (s[pos])
            {
                case '+':
                    return OperatorFactory.Create<Addition>();
                case '-':
                    return OperatorFactory.Create<Subtraction>();
                case '*':
                    return OperatorFactory.Create<Multiplication>();
                case '/':
                case ':':
                    return OperatorFactory.Create<Division>();
                case '%':
                    return OperatorFactory.Create<Modulo>();
                case '^':
                    return OperatorFactory.Create<Power>();
                case '[':
                case '{':
                case '(':
                    return OperatorFactory.Create<LeftBracket>();
                case ']':
                case '}':
                case ')':
                    return OperatorFactory.Create<RightBracket>();
                default:
                    return OperatorFactory.Create<Default>();
            }
        }
        public static bool ComparePriority(this IOperator operatorToCompare, IOperator comparingOperator)
        {
            if (operatorToCompare.GetType() != typeof(Power))
            {
                if (operatorToCompare.Weight <= comparingOperator.Weight)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (operatorToCompare.Weight < comparingOperator.Weight)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}


