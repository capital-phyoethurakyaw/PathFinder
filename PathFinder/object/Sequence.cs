using VectorDraw.Professional.vdFigures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorDraw.Professional.vdFigures;
using PathFinder.util;

namespace PathFinder
{
    public class Sequence
    {
        public Room sRoom;
        public Room eRoom;

     
        public List<SubSequence> subSequences = new List<SubSequence>();

        public SubSequence shortestSubSequence = null;

        public Sequence(Room sRoom, Room eRoom) { 
             this.sRoom = sRoom;
            this.eRoom = eRoom;

        }

        public void  reverse( )
        {
            Room temp = this.sRoom;
            this.sRoom = eRoom;
            this.eRoom = temp;
            foreach (SubSequence subSequence in this.subSequences)
            {

                subSequence.reverse();
            }

        }

        public double getshortestDistance()
        {
          
            double dis = 0;
            if (shortestSubSequence == null) return dis;
            else return shortestSubSequence.getDistance();
        }
        public double getshortestDistanceValue()
        {

            double dis = 0;
            if (shortestSubSequence == null) return dis;
            else return shortestSubSequence.getDistanceValue();
        }

        public SubSequence getShortestSubSequence()
        {
            return shortestSubSequence;
        }

        public int  getShortestSubSequenceIndex()
        {
            return subSequences.IndexOf(shortestSubSequence);
        }

        public void setShortestPath(bool mainRoute, bool minimumRoute, List<MainRoute> mainRoutes)
        {
            int count = 0;
            List<SubSequence> subSequences = new List<SubSequence>();
            foreach (SubSequence subSequence in this.subSequences)
            {
                bool isIn = false;
                foreach (Room room in subSequence.roomList)
                {
                    foreach (MainRoute mr in mainRoutes)
                    {
                        isIn = mr.isInMainRoute(room);
                        if (isIn)
                        {
                            subSequences.Add(subSequence);
                            break;
                        }
                    }
                }
                if (isIn) count++;
            }

            if (mainRoute)
            {
                if (this.subSequences.Count > 2)
                {
                    foreach (SubSequence subSequence in this.subSequences)
                    {
                        subSequence.isRoute = false;

                    }
                    foreach (SubSequence subSequence in subSequences)
                    {
                        subSequence.isRoute = true;
                    }

                    if (minimumRoute)
                    {
                        int min = int.MaxValue;
                        foreach (SubSequence subSequence in subSequences)
                        {
                            if (min > subSequence.roomList.Count) min = subSequence.roomList.Count;
                        }
                        foreach (SubSequence subSequence in subSequences)
                        {
                            if (min != subSequence.roomList.Count) subSequence.isRoute = false;
                        }
                    }
                }
            }
            else
            {
                foreach (SubSequence subSequence in this.subSequences)
                {
                    subSequence.isRoute = true;

                }
            }
            if (minimumRoute)
            {
                foreach (SubSequence subSequence in this.subSequences) subSequence.isRoute = false;
                int min = int.MaxValue;
                foreach (SubSequence subSequence in subSequences)
                {
                    if (min > subSequence.roomList.Count) min = subSequence.roomList.Count;
                }
                foreach (SubSequence subSequence in subSequences)
                {
                    if (min == subSequence.roomList.Count) subSequence.isRoute = true;
                }
            }

            double minValue = double.MaxValue;
            foreach (SubSequence subSequence in this.subSequences)
            {
                if (subSequence.isRoute)
                {
                    if (minValue > subSequence.getDistance())
                    {
                        minValue = subSequence.getDistance();
                        shortestSubSequence = subSequence;
                    }
                }
                else { 
                }
            }
        }


        public override string ToString()
        {
            return sRoom.name+Protocol.Delimiter_Rooms+eRoom;
        }

    }
}