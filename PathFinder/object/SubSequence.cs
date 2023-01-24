namespace PathFinder
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using PathFinder.util;
    using VectorDraw.Geometry;
    using VectorDraw.Professional.vdFigures;

public class SubSequence
{
        public double distance2 = 0;
        public List<Room> roomList = new List<Room>();
        
        private gPoints routeLine = new gPoints();
        public bool isRoute = true;

        public void setRouteLine(gPoints line)
        {
            line.RemoveInLinePoints();

            this.routeLine = line;
            this.distance2 = routeLine.Length();
        }
        public gPoints getRouteLine()
        {
           return this.routeLine ;
        }
        public SubSequence(List<Room> roomList) {

            this.roomList = roomList;

        }
        public SubSequence( )
        {

        }

        public double getDistance() {
            if (isRoute) return (Math.Round(distance2 / 10)) / 100.0;
            return double.MaxValue;
        }

        public double getDistanceValue()
        {
              return (Math.Round(distance2 / 10)) / 100.0;
         ;
        }


        public void reverse()
        {
            roomList.Reverse();
            routeLine.Reverse();
        }



        public override string ToString()
        {
            string text = "";
            foreach (Room r in roomList)
            {
                text += r.name + Protocol.Delimiter_Rooms;
            }
            if (text.Length > 0) text = text.Substring(0, text.Length - 1);
            return text;
        }

    }
}



