using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.util;
using VectorDraw.Geometry;

namespace PathFinder
{
    public class DefinedSequence
{
        private double id;
        public string name;
        public List<RoomAndGroupObject> roomList = new List<RoomAndGroupObject>(); //room or roomGroup
        public int frequency = 1;

        public DefinedSequence()
        {

            this.id = DateTime.Now.TimeOfDay.TotalSeconds;
        }

       override public string ToString() {
            string text = "";
            foreach (RoomAndGroupObject rgo in this.roomList)
            {
                text += rgo.name + Protocol.Delimiter_Rooms;
            }
            if (string.IsNullOrEmpty(text)) return text;
            text = text.Substring(0, text.Length - 1);
            return text;
        }

        public string getNames() {
            string text = "";
            foreach (RoomAndGroupObject rgo in this.roomList) { 
            text +=rgo.name+ Protocol.Delimiter_Rooms;
            }
            if(text.Length>0) text = text.Substring(0, text.Length - 1);
            return text;
        }

        public string getIds()
        {

            string text = "";
            foreach (RoomAndGroupObject rgo in this.roomList) text += rgo.id + Protocol.Delimiter_Rooms;
            text = text.Substring(0, text.Length - 1);
            return text;
        }


    }
}
