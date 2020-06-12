using System;
using System.Reflection;
using System.Linq;

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
    public interface IFunction : IOperator
    {
        bool isNegative { get; set; }
    }
    public interface IOneArgFunction : IFunction
    {
        decimal CalculateFunction(decimal a);
    }


    public interface IConstant : IFunction
    {
        decimal Value { get; }
    }
    
    public class Default : IOperator
    {
        public uint Weight => 0;
    }

    public class Pi : IConstant
    {
        public bool isNegative { get; set; } = false;
        public uint Weight => 4;
        public decimal Value => 3.1415926535897932384626433832m;
    }
    public class EulersNumber : IConstant
    {
        public bool isNegative { get; set; } = false;
        public uint Weight => 4;
        public decimal Value => (decimal)Math.E;
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
            return (decimal)Math.Pow((double)a, (double)b);
        }
    }

    public class Sinus : IOneArgFunction
    {
        public uint Weight => 5;

        public bool isNegative { get; set; } = false;

        public decimal CalculateFunction(decimal a)
        {
            return (decimal)Math.Sin((double)a);
        }
    }
    public class Cosinus : IOneArgFunction
    {
        public uint Weight => 5;
        public bool isNegative { get; set; } = false;
        public decimal CalculateFunction(decimal a)
        {
            return (decimal)Math.Cos((double)a);
        }
    }

    public class Tangent : IOneArgFunction
    {
        public uint Weight => 5;
        public bool isNegative { get; set; } = false;
        public decimal CalculateFunction(decimal a)
        {
            return (decimal)Math.Tan((double)a);
        }
    }

    public class Cotangent : IOneArgFunction
    {
        public uint Weight => 5;
        public bool isNegative { get; set; } = false;
        public decimal CalculateFunction(decimal a)
        {
            return 1/(decimal)Math.Tan((double)a);
        }
    }

    public class LeftBracket : IOperator
    {
        public uint Weight => 1;
    }
    public class RightBracket : IOperator
    {
        public uint Weight => 6;
    }
    public static class OperatorFactory
    {

        public static void Test()
        {
            var opTypes = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => typeof(IOperator).IsAssignableFrom(x));
            var dic = opTypes.Select(x => (IOperator)Activator.CreateInstance(x)).ToDictionary(x => x.Weight, x => x);
        }

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
        public static IOperator ToFunction(this string s)
        {
            switch (s)
            {
                case "sin":
                case "sinus":
                    return OperatorFactory.Create<Sinus>();
                case "cos":
                case "cosinus":
                    return OperatorFactory.Create<Cosinus>();
                case "tangent":
                case "tan":
                case "tg":
                    return OperatorFactory.Create<Tangent>();
                case "cotangent":
                case "ctg":
                case "cot":
                    return OperatorFactory.Create<Cotangent>();
                case "PI":
                case "pi":
                    return OperatorFactory.Create<Pi>();
                case "E":
                case "e":
                    return OperatorFactory.Create<EulersNumber>();
                default:
                    return OperatorFactory.Create<Default>();
            }
        }
    }
}


