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
            return $"ToDoItem(ID:{ID}, Datw:{Date}, Task:{Task}, Priority:{Priority})";
        }
        public int CompareTo(ToDoItem other)
        {
            return ID.CompareTo(other.ID);
        }
    }
}
