namespace PathFinder.util
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using VectorDraw.Professional.vdFigures;

    internal class FindUtil
    {
        public static RoomRelation fineRoomRelation(List<RoomRelation> roomRelations, Room room1, Room room2) {
            foreach (RoomRelation rr in roomRelations)
            {
                if (rr.sRoom == room1 && rr.eRoom == room2)
                {
                    return rr;
                }
                else if (rr.sRoom == room2 && rr.eRoom == room1)
                {
                    return rr;
                }
                else return null;
            }
            return null;
        } 
        public static Room findRoom(Floor floor,  string name)
        {
            foreach (Room room in floor.roomList)
            {
                if (room.name == name) return room;
            }
            return null;
        }

      

        public static RoomGroup findGroup(List<RoomGroup> roomGroups,string name)
        {
            foreach (RoomGroup rg in roomGroups)
            {
                if (rg.name == name) return rg;
            }
            return null;
        }

        public static Connector findConnector(Floor floor, Room room1, Room room2)
        {
            foreach (Connector conn in floor.connectorList)
            {
                if (conn.roomList.Contains(room1) && conn.roomList.Contains(room2)) return conn;
            }
            return null;
        }

        public static bool fineSequence(Info info, Sequence seq ) {

            foreach (SequenceGroup sequenceGroup in info.sequenceGroups)
            {
              
                    foreach (Sequence sequence in sequenceGroup.sequences)
                    {
                        if (sequence == seq) return false;
                        else if (sequence.sRoom.name == seq.sRoom.name && sequence.eRoom.name == seq.eRoom.name)
                        {
                            if (sequence.shortestSubSequence != null)
                            {
                                seq.subSequences = sequence.subSequences;
                                return true;
                            }
                        }
                    }
                
            }
            return false;
        }


    }
}
