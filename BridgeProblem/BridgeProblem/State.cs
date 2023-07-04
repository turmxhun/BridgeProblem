using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeProblem
{
    internal class State : ICloneable
    {
        public List<int> LeftSide { get; private set; }
        public List<int> RightSide { get; private set; }
        public bool IsLampOnLeftSide { get; private set; }
        public int ElapsedTime { get; private set; }

        public Node ParentNode { get; private set; }


        public State(List<int> leftSide, List<int> rightSide, bool isLampOnLeftSide, int elapsedTime)
        {
            LeftSide = leftSide;
            RightSide = rightSide;
            IsLampOnLeftSide = isLampOnLeftSide;
            ElapsedTime = elapsedTime;
        }

        public bool IsTargetState()
        {
            return ElapsedTime <= 17 && RightSide.Count == 4;
        }

        public List<Node> GetNextPossibleStates()
        {
            List<Node> nextStates = new List<Node>();

            List<int> currentSide = IsLampOnLeftSide ? LeftSide : RightSide;
            List<int> oppositeSide = IsLampOnLeftSide ? RightSide: LeftSide;

            for (int i = 0; i < currentSide.Count; i++)
            {
                int person = currentSide[i];
                currentSide.Remove(person);
                oppositeSide.Add(person);

                int crossingTime = CalculateCrossingTime(currentSide.Concat(oppositeSide).ToArray());
                int newElapsed = ElapsedTime + crossingTime;
                bool newLampPosition = !IsLampOnLeftSide;

                State nextState = new State(new List<int>(LeftSide), new List<int>(RightSide), newLampPosition, newElapsed);

                Node nextNode = new Node(nextState);
                nextStates.Add(nextNode);

                oppositeSide.Remove(person);
                currentSide.Insert(i, person);
            }

            return nextStates;
        }

        private int CalculateCrossingTime(int[] speeds)
        {
            return speeds.Max();
        }

        public object Clone()
        {
            return new State(new List<int>(LeftSide), new List<int>(RightSide), IsLampOnLeftSide, ElapsedTime);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Current state:");
            sb.AppendLine("Bal oldal: " + string.Join(", ", LeftSide));
            sb.AppendLine("Jobb oldal: " + string.Join(", ", RightSide));
            sb.AppendLine("Elapsed: " + ElapsedTime);
            sb.AppendLine("Lamp position: " + (IsLampOnLeftSide ? "Left" : "Right"));
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is State))
                return false;

            State other = (State)obj;
            return LeftSide.SequenceEqual(other.LeftSide) &&
                   RightSide.SequenceEqual(other.RightSide) &&
                   ElapsedTime == other.ElapsedTime &&
                   IsLampOnLeftSide == other.IsLampOnLeftSide;
        }
    }

}
