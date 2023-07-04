using System;
using System.Collections.Generic;
using System.Text;

namespace BridgeProblem
{
    internal class Node
    {
        public State State { get; private set; }
        public int Depth { get; private set; }
        public Node Parent { get; private set; }
        public int OperatorIndex { get; set; }

        public Node(State state, Node parent = null)
        {
            Parent = parent;
            State = state;
            OperatorIndex = 0;
            if (Parent == null)
            {
                Depth = 0;
            }
            else
            {
                Depth = Parent.Depth + 1;
            }
        }

        public bool IsTargetNode()
        {
            return State.IsTargetState();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Parent != null)
            {
                sb.AppendLine(Parent.ToString());
                sb.AppendLine("---------------------");
            }
            sb.AppendLine("Depth: " + Depth);
            sb.Append(State.ToString());
            return sb.ToString();
        }

        public bool HasLoop()
        {
            Node temp = Parent;
            while (temp != null)
            {
                if (temp.Equals(this))
                {
                    return true;
                }
                temp = temp.Parent;
            }
            return false;
        }
    }
}


