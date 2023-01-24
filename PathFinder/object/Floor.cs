using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
   public class Floor
    {
        public string name;
        double id;
        public List<Room> roomList = new List<Room>();
        public List<Connector> connectorList = new List<Connector>();
        public List<Room> roomListU = new List<Room>();
        public List<Connector> connListU = new List<Connector>();
        public List<Obstacle> obstacles = new List<Obstacle>();

        public Floor() {
            this.id = DateTime.Now.TimeOfDay.TotalSeconds;
        }
    }
}
