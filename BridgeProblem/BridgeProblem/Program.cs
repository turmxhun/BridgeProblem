using BridgeProblem.Solvers;
using System;

namespace BridgeProblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ASolver solver = new HeuristicNN();
            solver.Solve();
            Console.ReadLine();
        }
    }
}
