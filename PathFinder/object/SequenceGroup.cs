using PathFinder.util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorDraw.Generics;
using VectorDraw.Geometry;
using VectorDraw.Professional.Constants;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;

namespace PathFinder
{
    public class SequenceGroup
    {
        Floor floor = new Floor();
       public double id = 0;  // ptk

       // public List<CombinedSequence> combinedSequenceList = new List<CombinedSequence>();
        public DefinedSequence definedSequence = new DefinedSequence();

        public vdPoint startPoint = new vdPoint();
        public vdPoint endPoint = new vdPoint();
      //  public  CombinedSequence shortestCombinedSequence; 

        public int startEndType = 1;// 0:roomCenter , 1:connectorCenter, 2:userPoint
        public List<Sequence> sequences = new List<Sequence>();
        public SequenceGroup()
        {
            this.id = DateTime.Now.TimeOfDay.TotalSeconds;
        }

        /// <summary>
        /// ////////////////////
     /*
        public void setCombinedSequence(List<RoomRelation> roomRelations, Info info)
        {
            this.combinedSequenceList.Clear();

            int combinatioinCount = 1;
            ArrayList roomLists = new ArrayList();

            foreach (RoomAndGroupObject rgo in this.definedSequence.roomList)
            {
                if (rgo is Room)
                {
                    List<Room> tempList = new List<Room>();
                    List<List<Room>> tempLists = new List<List<Room>>();
                    tempList.Add((Room)rgo);
                    tempLists.Add(tempList);
                    roomLists.Add(tempLists);

                }
                if (rgo is RoomGroup)
                {
                    RoomGroup rg = (RoomGroup)rgo;
                    combinatioinCount *= rg.combinatedRooms.Count;
                    roomLists.Add(rg.combinatedRooms);
                }     
            }
            int[] listCount = new int[roomLists.Count];

            for (int i = 0; i < roomLists.Count; i++) {
                 List < List < Room >> rs = (List<List<Room>>) roomLists[i];
                listCount[i] = rs.Count;
            }

            List<int[]>  combinations =   Combination.combination(listCount);

            List<List<Room>> roomsList = new  List<List<Room>>();
            for (int i = 0; i < combinations.Count; i++)
            {
                int[] sels = combinations[i];
              
                List<Room> roomSum = new List<Room>();
                for (int j = 0; j < sels.Length; j++)
                {
                    List<List<Room>> roomlist = (List<List<Room>>)roomLists[j];
                    

                    List<Room> rooms = roomlist[sels[j]];
                    roomSum.AddRange(rooms);
                }
                roomsList.Add(roomSum);
            }


            foreach(List<Room>  rooms in roomsList) {
                List<Sequence>  sequences = new List<Sequence>();
                for (int i = 0; i < rooms.Count - 1; i++) { 
                    Room room1 = (Room)rooms[i];
                    Room room2= (Room)rooms[i+1];
                    Sequence sequence = new Sequence(room1, room2);
                    foreach (RoomRelation rr in roomRelations)
                    {
                            if (rr.sRoom == room1 && rr.eRoom == room2)
                            {
                                foreach (ArrayList list in rr.roomLists)
                                {
                                    List<Room> roomList = new List<Room>();
                                    foreach (Room r in list) roomList.Add(r);
                                    SubSequence sequence1 = new SubSequence(roomList);
                                    sequence.subSequences.Add(sequence1);
                                }
                                break;
                            }
                        }
                    

                    sequences.Add(sequence);
                    }
                    CombinedSequence combinedSequence = new CombinedSequence();
                    combinedSequence.roomList = rooms;
                    combinedSequence.sequenceList = sequences;
                    this.combinedSequenceList.Add(combinedSequence);
                }
        }

        public void setShortestCombinedSequence()
        {
           
          //this.shortestCombinedSequence = getShortestCombinedSequence();
        }
        public int getShortestCombinedSequenceIndex(CombinedSequence shortestCombinedSequence)
        {
            int i = -1;
            if (shortestCombinedSequence != null)    i =    combinedSequenceList.IndexOf(shortestCombinedSequence);
           
            return i;
        }

        public CombinedSequence getShortestCombinedSequence()
        {
            double shortestPath = double.MaxValue;
            CombinedSequence shortestCombinedSequence = null;
            foreach (CombinedSequence combinedSequence in combinedSequenceList) {
           
                double pathLength = combinedSequence.getPathLength();
                if (shortestPath > pathLength) {
                    shortestCombinedSequence = combinedSequence;
                    shortestPath = pathLength;
                }
            
            }
            return shortestCombinedSequence;
        }

        */
        /// ////////////////



        public double getPathLength()
        {
            double pathLength = 0;
            foreach (Sequence sequence in this.sequences)
            {
                if (sequence.shortestSubSequence != null) pathLength += sequence.shortestSubSequence.getDistance();
            }
            return pathLength;
        }

        public gPoints getShortestPath()
        {
            gPoints gps = new gPoints();
            foreach (Sequence sequence in this.sequences)
            {

                if (sequence.shortestSubSequence != null) gps.AddRange(sequence.shortestSubSequence.getRouteLine());
            }
            return gps;
        }

        public List<vdPolyline> getShortestPaths(vdDocument doc)
        {
            List<vdPolyline> polylines = new List<vdPolyline>();
            foreach (Sequence sequence in this.sequences)
            {
                if (sequence.shortestSubSequence == null) continue;
                vdPolyline polyline = new vdPolyline(doc, sequence.shortestSubSequence.getRouteLine());
            //    polyline.VertexList.RemoveEqualPoints(2000);


                polyline.PenColor.Red = 0; polyline.PenColor.Green = 255; polyline.PenColor.Blue = 0;
                polyline.LineWeight = VdConstLineWeight.LW_50;

                polylines.Add(polyline);
            }
            return polylines;
        }


        public override string ToString()
        {
            return this.definedSequence.name;
        }

       

    }
}
