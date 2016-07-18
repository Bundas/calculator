using System;
using System.Collections.Generic;
using System.Reflection;
using Calculator.Operators.Interfaces;
using Calculator.Operators.SingleOperators;
using Microsoft.Extensions.DependencyModel;
using System.Linq;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var binaryOperators = new HashSet<IBinaryOperator> { new MultipleOperator(), new AddOperator() };
            
            var types = GetReferencingAssemblies(typeof(IBinaryOperator).Name);

            var unaryOperators = new HashSet<IUnaryOperator>
            {
                new FactorialOperator(),
                new UnaryMinusOperator(),
                new DecadicLogarithmOperator()
            };

            var calc = new BurdaCalculator(unaryOperators, binaryOperators);
            while (true)
            {
                var result = calc.Calculate(Console.ReadLine());
                Console.WriteLine(result);
                Console.ReadKey();
            }
        }

        public static IEnumerable<Assembly> GetReferencingAssemblies(string assemblyName)
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                if (IsCandidateLibrary(library, assemblyName))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }
            return assemblies;
        }

        private static bool IsCandidateLibrary(RuntimeLibrary library, string assemblyName)
        {
            return library.Name == assemblyName
                || library.Dependencies.Any(d => d.Name.StartsWith(assemblyName));
        }
    }
}
