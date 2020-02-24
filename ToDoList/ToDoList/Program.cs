using System;
using ToDoList.GUI;
using ToDoList.SQL;

namespace ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterface app = new UserInterface();
            app.StartSqlLite();
            
        }
    }
}
