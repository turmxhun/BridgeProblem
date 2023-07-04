using System.Collections.Generic;

namespace BridgeProblem
{
    internal abstract class ASolver
    {
        protected List<Operator> Operators = new List<Operator>();

        private void GenerateOperators()
        {
            List<List<int>> crossingTimes = new List<List<int>>
            {
                new List<int> { 1 },
                new List<int> { 2 },
                new List<int> { 5 },
                new List<int> { 10 }
            };

            foreach (List<int> times in crossingTimes)
            {
                Operators.Add(new Operator(times));
            }
        }

        public ASolver()
        {
            GenerateOperators();
        }

        protected abstract State GetInitialState();
        public abstract Operator SelectOperator();
        public abstract void Solve();
    }
}
