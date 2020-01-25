using System;

namespace calculator {
    public interface IOperator
    {
        uint Weight { get; }

    }
    public interface IExecutableOperator : IOperator
    {
        decimal CalculateOperator(decimal a, decimal b);

    }

    class Default : IOperator
    {
        uint IOperator.Weight => 0;
    }
    class Addition : IExecutableOperator
    {
        uint IOperator.Weight => 2;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return a + b;
        }
    }
    class Subtraction : IExecutableOperator
    {
        uint IOperator.Weight => 2;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return a - b;
        }
    }
    class Multiplication : IExecutableOperator
    {
        uint IOperator.Weight => 3;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return a * b;
        }
    }
    class Division : IExecutableOperator
    {
        uint IOperator.Weight => 3;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return a * b;
        }
    }
    class Modulo : IExecutableOperator
    {
        uint IOperator.Weight => 3;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return a % b;
        }
    }
    class Power : IExecutableOperator
    {
        uint IOperator.Weight => 4;

        public decimal CalculateOperator(decimal a, decimal b)
        {
            return (decimal)Math.Pow((double)a, (double)b); ;
        }
    }

    class LeftBracket : IOperator
    {
        public uint Weight => 1;
    }
    class RightBracket : IOperator
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

}

