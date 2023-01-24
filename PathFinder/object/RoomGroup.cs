using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.OrTools.ConstraintSolver;
using PathFinder.analysis;
using PathFinder.gui;
using PathFinder.util;
using VectorDraw.Generics;
using VectorDraw.Geometry;
using VectorDraw.Professional.PropertyList;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;


namespace PathFinder
{
    public class RoomGroup : RoomAndGroupObject
{
        public List<Room> roomList = new List<Room>();
        public static string Order = "Order";
        public static string Non_Order= "Non-Order";
     
        public bool isOrder = true;
        
        public bool isOrderOfFirstEndRoom = true;

        public List<Sequence> shortestSequences = new List<Sequence>();


        public class DataModel
        {
            public long[,] distanceMatrix;
            public int VehicleNumber = 1;
            public int[] start;
            public int[] end;
            public DataModel(long[,] distanceMatrix)
            {
                this.distanceMatrix = distanceMatrix;
                start = new int[1];
                start[0] = 0;
                end = new int[1];
                end[0] =   distanceMatrix.GetLength(0)-1;
            }

        };

        public Room calOrderRooms(Room sr, Room er, Info info, int type, bool isRoomCenter, vdDocument doc, bool mainRoute, bool minimumRoute)
        {
            this.shortestSequences = new List<Sequence>();
            List<Room> rooms = new List<Room>();
            if (sr != null)
            {
                if(sr != this.roomList.First() ) rooms.Add(sr);
            }
            rooms.AddRange(this.roomList);
            if (er != null)
            {
                if(sr != er) rooms.Add(er);
            }

            for (int i = 0; i < rooms.Count - 1; i++)
            {
                Room r1 = rooms[i];
                Room r2 = rooms[i + 1];
                Sequence sequence = new Sequence(r1, r2);
                shortestSequences.Add(sequence);
                foreach (RoomRelation rr in info.roomRelations)
                {
                    if (rr.sRoom == r1 && rr.eRoom == r2)
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
                foreach (SubSequence subSequence in sequence.subSequences)
                {
                    gPoints ps = null;
                    if (type == 0) ps = AnalysisShortDistance.getShortDistanceDD(subSequence.roomList, info, doc, isRoomCenter);
                    else if (type == 1) ps = AnalysisShortDistance.getShortDistanceLikeHuman(subSequence.roomList, info, doc, isRoomCenter);
                    else ps = AnalysisShortDistance.getShortDistanceLikeMachine(subSequence.roomList, info, doc);
                    subSequence.setRouteLine( ps);
                  
                }
                sequence.setShortestPath(mainRoute, minimumRoute, info.mainRoutes);
            }
            return rooms.Last();
        }

        public Room calNonOrderRooms(Room sr, Room er, Info info, int type, bool isRoomCenter, vdDocument doc, bool mainRoute, bool minimumRoute)
        {
            this.shortestSequences = new List<Sequence>();
            List<Room> rooms = new List<Room>();
            bool isSameWithFirstRoomAndLastRoom = false;
            if (sr == er) isSameWithFirstRoomAndLastRoom = true;
            rooms.Add(sr);
            rooms.AddRange(this.roomList);
            if(!isSameWithFirstRoomAndLastRoom) rooms.Add(er);

            long[,] distanceMatrix = new long[rooms.Count, rooms.Count];
            List<Sequence> sequences = new List<Sequence>();
            for (int i = 0; i < rooms.Count-1; i++) {
                Room r1= rooms[i];
                for (int j = i+1; j < rooms.Count; j++)
                {
                    Room r2 = rooms[j];
                   
                    Sequence sequence = new Sequence(r1, r2);
                    sequences.Add(sequence);
                
                    foreach (RoomRelation rr in info.roomRelations)
                    {
                        if (rr.sRoom == r1 && rr.eRoom == r2)
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


                    foreach (SubSequence subSequence in sequence.subSequences)
                    {
                            gPoints ps = null;
                            if (type == 0) ps = AnalysisShortDistance.getShortDistanceDD(subSequence.roomList, info, doc, isRoomCenter);
                            else if (type == 1) ps = AnalysisShortDistance.getShortDistanceLikeHuman(subSequence.roomList, info, doc, isRoomCenter);
                            else ps = AnalysisShortDistance.getShortDistanceLikeMachine(subSequence.roomList, info, doc);
                            subSequence.setRouteLine( ps);
                          
                        }

                    sequence.setShortestPath(mainRoute, minimumRoute, info.mainRoutes);
                    if (sequence.getShortestSubSequence() == null)
                    {
                        Console.WriteLine(" sequence.getShortestSubSequence() =null    " + sequence.ToString());

                        distanceMatrix[i, j] = (long)long.MaxValue;
                        distanceMatrix[j, i] = distanceMatrix[i, j];
                    }
                    else
                    {

                        distanceMatrix[i, j] = (long)sequence.getShortestSubSequence().distance2;
                        distanceMatrix[j, i] = distanceMatrix[i, j];
                    }
                }
            }

            DataModel data = new DataModel(distanceMatrix);

            // Create Routing Index Manager
            RoutingIndexManager manager = null;
            if (isSameWithFirstRoomAndLastRoom) manager = new RoutingIndexManager(data.distanceMatrix.GetLength(0), data.VehicleNumber, 0);
            else manager = new RoutingIndexManager(data.distanceMatrix.GetLength(0), data.VehicleNumber, data.start, data.end);

            ///
            if (isSameWithFirstRoomAndLastRoom)
            {
                int minIndex = -1;
                long minValue = long.MaxValue;
                for (int i = 1; i < distanceMatrix.GetLength(0); i++)
                {
                    if (minValue > distanceMatrix[0, i])
                    {
                        minValue = distanceMatrix[0, i];
                        minIndex = i;
                    }
                }
                distanceMatrix[0, minIndex] = (long)(minValue * 0.9);
            }
            ///


            // Create Routing Model.
            RoutingModel routing = new RoutingModel(manager);

            int transitCallbackIndex = routing.RegisterTransitCallback((long fromIndex, long toIndex) =>
            {
                // Convert from routing variable Index to
                // distance matrix NodeIndex.
                var fromNode = manager.IndexToNode(fromIndex);
                var toNode = manager.IndexToNode(toIndex);
                return data.distanceMatrix[fromNode, toNode];
            });

            // Define cost of each arc.
            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

            // Setting first solution heuristic.
            RoutingSearchParameters searchParameters =
                operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

            // Solve the problem.
            Assignment solution = routing.SolveWithParameters(searchParameters);

            int roomCount = rooms.Count;
            if (isSameWithFirstRoomAndLastRoom) roomCount++;

            int[] orders = new int[roomCount];

            long routeDistance = 0;
            var index = routing.Start(0);
            int count = 0;
            while (routing.IsEnd(index) == false)
            {
                orders[count++] = manager.IndexToNode((int)index);
                Console.WriteLine(manager.IndexToNode((int)index));
                var previousIndex = index;
                index = solution.Value(routing.NextVar(index));
                routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);
            }
            orders[count++] = manager.IndexToNode((int)index);
            Console.WriteLine(manager.IndexToNode((int)index));
            Console.WriteLine("Route distance: {0}miles", routeDistance);
            for (int i = 0; i < orders.Length-1; i++) {
                Room sr1 = rooms[orders[i]];
                Room er1 = rooms[orders[i+1]];
                foreach (Sequence sequence in sequences) {
                    if ((sequence.sRoom == sr1 && sequence.eRoom == er1))
                    {
                       this.shortestSequences.Add(sequence);
                    }else if ( sequence.sRoom == er1 && sequence.eRoom == sr1){
                        sequence.reverse();
                        this.shortestSequences.Add(sequence);
                    }
                }
            }

            Room last = rooms[orders[orders.Length - 1]];
            return last;
            //  foreach (Sequence sequence2 in shortestSequences) {
            //      Console.WriteLine(  sequence2.ToString()+" // "+ sequence2.shortestSubSequence.ToString());            
            //  }
        }



        public RoomGroup(string name, List<Room> roomList, bool isOrder):base(name) { 
           
            this.roomList = roomList;
            this.isOrder = isOrder;
 
          
           
        }

        public string getRooms() {
            string text = "";
            foreach (Room r in roomList) {
              if(r != null)  text += r.name + Protocol.Delimiter_Rooms;
            
            }
            if(text.Length > 0) text = text.Substring(0, text.Length - 1);
            return text;
        }
        public string getOrder()
        {
            string text = "";
            if (isOrder) text = Order;
            else text = Non_Order;
            return text;
        }

       

        public override string ToString()
        {
            return this.name;
        }

    }
    
}
