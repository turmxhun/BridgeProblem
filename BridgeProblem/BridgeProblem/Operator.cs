using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeProblem
{
    internal class Operator
    {

        public List<int> PeopleSpeeds { get; private set; }

        public Operator(List<int> peopleSpeeds)
        {
            PeopleSpeeds = peopleSpeeds;
        }


        public bool IsApplicable(State state)
        {
            if (state.IsLampOnLeftSide)
            {
                foreach (int speed in PeopleSpeeds)
                {
                    if (state.LeftSide.Contains(speed))
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (int speed in PeopleSpeeds)
                {
                    if (state.RightSide.Contains(speed))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private int GetMaxCrossingTime(List<int> currentlyGoing)
        {

            return currentlyGoing.Max();
        }

        public State Apply(State state)
        {
            List<int> newLeftSide = new List<int>(state.LeftSide);
            List<int> newRightSide = new List<int>(state.RightSide);
            bool newIsLampOnLeftSide = !state.IsLampOnLeftSide;

            List<int> currentlyGoing = new List<int>();



            // Az átkelő emberek számát korlátozzuk a lámpa pozíciójától függően
            int maxCrossingPeople = newIsLampOnLeftSide ? 1 : 2;

            if (newIsLampOnLeftSide)
            {
                for (int i = 0; i < maxCrossingPeople; i++)
                {
                    if (newLeftSide.Count > 0)
                    {
                        int speed = newRightSide[0];
                        newRightSide.RemoveAt(0);
                        newLeftSide.Add(speed);
                        currentlyGoing.Add(speed);
                    }
                }
            }
            else
            {
                for (int i = 0; i < maxCrossingPeople; i++)
                {
                    if (newLeftSide.Count > 0)
                    {
                        int speed = newLeftSide[0];
                        newLeftSide.RemoveAt(0);
                        newRightSide.Add(speed);
                        currentlyGoing.Add(speed);
                    }
                }
            }
            int newElapsedTime = state.ElapsedTime + GetMaxCrossingTime(currentlyGoing);

            return new State(newLeftSide, newRightSide, newIsLampOnLeftSide, newElapsedTime);
        }

    }

}

