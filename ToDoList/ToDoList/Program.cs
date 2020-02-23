using System;
using ToDoList.GUI;
using ToDoList.SQL;

namespace ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            //UserInterface.Start();
            ToDoListMySql toDoListMySql = new ToDoListMySql();
            toDoListMySql.GetAll();
        }
    }
}
