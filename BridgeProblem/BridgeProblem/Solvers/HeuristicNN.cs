using System;
using System.Collections.Generic;

namespace BridgeProblem.Solvers
{
    internal class HeuristicNN : ASolver
    {
        private State currentState;

        protected override State GetInitialState()
        {
            List<int> LeftSide = new List<int>();
            LeftSide.Add(1);
            LeftSide.Add(2);
            LeftSide.Add(5);
            LeftSide.Add(10);
            List<int> RightSide = new List<int>();
            bool IsLampOnLeftSide = true;
            int ElapsedTime = 0;

            return new State(LeftSide, RightSide, IsLampOnLeftSide, ElapsedTime);
        }

        public override Operator SelectOperator()
        {
            Operator best = null;
            int bestTime = int.MaxValue;

            foreach (Operator op in Operators)
            {
                if (op.IsApplicable(currentState))
                {
                    State newState = op.Apply(currentState);
                    Node newNode = new Node(newState, currentState.ParentNode);
                    if (!newNode.HasLoop() && newState.ElapsedTime < bestTime)
                    {
                        best = op;
                        bestTime = newState.ElapsedTime;
                    }
                }
            }

            return best;
        }


        public override void Solve()
        {
            currentState = GetInitialState();
            Node rootNode = new Node(currentState);
            Stack<Node> nodeStack = new Stack<Node>();
            nodeStack.Push(rootNode);

            while (nodeStack.Count > 0)
            {
                Node currentNode = nodeStack.Pop();
                currentState = currentNode.State;

                if (currentNode.IsTargetNode() && currentState.ElapsedTime <= 17)
                {
                    Console.WriteLine(currentNode);
                    break;
                }

                Operator selectedOperator = SelectOperator();
                if (selectedOperator != null)
                {
                    currentState = selectedOperator.Apply(currentState);
                    Node newNode = new Node(currentState, currentNode);
                    if (!newNode.HasLoop())
                    {
                        nodeStack.Push(newNode);
                    }
                }
            }
        }
    }
}
