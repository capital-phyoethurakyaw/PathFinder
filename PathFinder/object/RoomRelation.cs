namespace PathFinder
{
using System;
    using System.Collections;
    using System.Collections.Generic;
using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
using System.Threading.Tasks;
    using PathFinder.util;

public class RoomRelation
{
       public Room sRoom;
        public Room eRoom;
        public ArrayList roomLists;

        public RoomRelation(Room sRoom, Room eRoom) {
            this.sRoom = sRoom;
            this.eRoom = eRoom;
            roomLists = getWayList(sRoom, eRoom);
        }
        
        public static ArrayList getWayList(Room firstSpaceObject, Room secondSpaceObject)
        {

            ArrayList returnHistoy = new ArrayList();
            ArrayList relationList = new ArrayList();
            ArrayList historyList = new ArrayList();
            ArrayList history = new ArrayList();
            history.Add(firstSpaceObject);
            historyList.Add(history);
            relationList.Add(firstSpaceObject);
            bool wh = true;
            while (wh)
            {
                wh = setDepth(historyList, returnHistoy, firstSpaceObject, secondSpaceObject);
            }
            return returnHistoy;
        }

        protected static bool setDepth(ArrayList historyList, ArrayList returnHistoy, Room firstSpaceObject, Room secondSpaceObject)
        {
            ArrayList relationReturnList = new ArrayList();
            ArrayList saveList = new ArrayList();
            ArrayList deleteList = new ArrayList();
            ArrayList historytTemp = new ArrayList();
            bool isSame = false;
            int co = 0;
            foreach (ArrayList history in historyList)
            {
                historytTemp = history;
                ArrayList tempList = new ArrayList();
                ArrayList copyList = new ArrayList();
                ArrayList removeList = new ArrayList();//지우기위한
                Room lastSpaceObject = (Room)history[history.Count - 1];
                foreach (Room relationSpaceObject in lastSpaceObject.relationRoomList)
                {
                    if (!history.Contains(relationSpaceObject) && !history.Contains(secondSpaceObject))
                    {
                        ArrayList cloneList = (ArrayList)history.Clone();
                        cloneList.Add(relationSpaceObject);
                        saveList.Add(cloneList);
                        co++;
                    }
                    else
                    {
                        continue;
                    }
                }
                deleteList.Add(history);
            }
            if (isSame)
            {
                historyList.Remove(historytTemp);
                return true;
            }
            foreach (ArrayList save in saveList) historyList.Add(save);
            foreach (ArrayList del in deleteList) historyList.Remove(del);
            foreach (ArrayList history in historyList)
            {
                if (history.Contains(firstSpaceObject) && history.Contains(secondSpaceObject))
                {
                    ArrayList history1 = new ArrayList();
                    foreach (Room spaceObject in history)
                    {
                        history1.Add(spaceObject);
                    }
                    if (history1.Count != 0) returnHistoy.Add(history1);
                }
            }
            if (co == 0) return false;
            return true;
        }



        public override string ToString()
        {
            string text = "";
            foreach (Room r in roomLists)
            {
                text += r.name + Protocol.Delimiter_Rooms;
            }
            if (text.Length > 0) text = text.Substring(0, text.Length - 1);
            return text;
        }


    }


}
