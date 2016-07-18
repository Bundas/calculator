using System;
using System.Collections.Generic;
using Calculator.Operators.Interfaces;
using Calculator.Operators.SingleOperators;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var unaryOperators = new HashSet<IUnaryOperator>
            {
                new FactorialOperator(),
                new UnaryMinusOperator(),
                new DecadicLogarithmOperator(),
                new SinusOperator()
            };

            var binaryOperators = new HashSet<IBinaryOperator>
            {
                new AddOperator(),
                new DivideOperator(),
                new SubstractOperator(),
                new MultipleOperator(),
            };

            var calc = new BurdaCalculator(unaryOperators, binaryOperators);
            var result = calc.Calculate(Console.ReadLine());
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
