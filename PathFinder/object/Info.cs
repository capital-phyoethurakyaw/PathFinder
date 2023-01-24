namespace PathFinder
{
using System;
using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
using System.Threading.Tasks;
    using VectorDraw.Professional.vdFigures;

    public class Info
{
        public static FileInfo fileInfo;
        public  List<RoomGroup> roomGroups = new List<RoomGroup>();
        public List<SequenceGroup> sequenceGroups = new List<SequenceGroup>();
        public List<RoomRelation> roomRelations = new List<RoomRelation>();
        public Floor floor = new Floor();
        public List<MainRoute> mainRoutes = new List<MainRoute>();
        public vdPolyline route = new vdPolyline();
        public List<vdPolyline> routes = new List<vdPolyline>();
        public List<Room> selectRooms = new List<Room>();
        

        public void setsequenceGroup() {
           /*
            
            foreach (SequenceGroup sequenceGroup in sequenceGroups) {

                sequenceGroup.setCombinedSequence(roomRelations,this);
            }
           */

        }

    }

    
}
