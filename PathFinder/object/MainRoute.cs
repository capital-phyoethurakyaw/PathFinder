using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.util;
using VectorDraw.Generics;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdPrimaries;


namespace PathFinder
{
    public class MainRoute
    {

        public string name;
        public  List<Room> roomList = new List<Room>();

        public MainRoute(string name)
        {
            this.name = name;
        }
        public MainRoute(string name, List<Room> roomList)
        {
            this.name = name;
            this.roomList = roomList;
        }

        public bool isInMainRoute(Room room)
        {
            foreach (Room r in this.roomList) {

                if (room == r) return true;
            }
            return false;
        }



        public  string getRooms()
        {
            string text = "";
            foreach (Room r in roomList)
            {
              if(r != null)  text += r.name + Protocol.Delimiter_Rooms;
            }
            if (text.Length > 0) text = text.Substring(0, text.Length - 1);
            return text;
        }

        public override string ToString()
        {
            return name;
        }

    }
}
