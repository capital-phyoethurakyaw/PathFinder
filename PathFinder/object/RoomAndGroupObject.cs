namespace PathFinder
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RoomAndGroupObject
{

        public double id;
        public string name;

        public RoomAndGroupObject(string name) {
            this.name = name;
            this.id = DateTime.Now.TimeOfDay.TotalSeconds;

        }
        public RoomAndGroupObject()
        {
            this.id = DateTime.Now.TimeOfDay.TotalSeconds;

        }
    }
}
