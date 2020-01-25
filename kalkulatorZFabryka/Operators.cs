using System;

namespace calculator {
    public interface IOperator
    {
        decimal CalculateOperator(decimal a, decimal b);
        int Weight { get; }

    }
    class Addition : IOperator
    {
        int IOperator.Weight => 1;

        decimal IOperator.CalculateOperator(decimal a, decimal b)
        {
            return a - b;
        }
    }
    abstract class OperatorFactory
    {
        public abstract IOperator GetOperator();
    }

    class AdditionFactory : OperatorFactory
    {
        public override IOperator GetOperator()
        {
            return new Addition();
        }
    }

}

