using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Data
{
    class ToDoItem : IComparable<ToDoItem>
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string Task { get; set; }
        public int Priority { get; set; }

        public override string ToString()
        {
            //return $"ToDoItem(ID:{ID}, Datw:{Date}, Task:{Task}, Priority:{Priority})";
            return "\t\t" + String.Format("{0,3}\t{1,10}{2,10}\t{3,1}", ID, Date, Task, Priority);
        }
        public string PrittyString()
        {

            return "\t\t" + ID + "\t" + Date + "\t" + Task + "\t\t" + Priority;
        }
        public int CompareTo(ToDoItem other)
        {
            return ID.CompareTo(other.ID);
        }
    }
}
