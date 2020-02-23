using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Data
{
    class ToDoItem : IComparable<ToDoItem>
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Task { get; set; }
        public int Priority { get; set; }

        public ToDoItem(int id, DateTime date, string task, int priority)
        {
            ID = id;
            Date = date;
            Task = task;
            Priority = priority;
        }
        public int CompareTo(ToDoItem other)
        {
            return ID.CompareTo(other.ID);
        }
    }
}
