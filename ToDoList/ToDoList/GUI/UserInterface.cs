using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ToDoList.Data;
using ToDoList.SQL;

namespace ToDoList.GUI
{
    
    public class UserInterface
    {
        private IRepository<ToDoItem> toDoListRepository;
        
        public void StartSqlLite()
        {

            ToDoListSqlLite repo = new ToDoListSqlLite();

            if (repo.Connect())
            {
                Console.WriteLine("ToDoList SQLite DB app started. What do you want to do?");
                toDoListRepository = repo;

                Start();
            }
            else
            {
                Console.WriteLine("Connection to DB failed. Sorry.. Bye!");
                Console.ReadKey();
            }
        }

        public static string st = "\t\t ";
        public static string xst = "\t\t";        
        
        public static void header()
        {
            Console.Clear();
            DateTime todaysDate = DateTime.Now;

            Console.WriteLine("\n\n\n\t\t\t\t\t" + todaysDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("\t\t====================================");
            Console.WriteLine("\t\t\t ToDo List");
            Console.WriteLine("\t\t====================================");
        }

        public static void footer()
        {
            Console.WriteLine("\t\t====================================");
        }



        public static void Message(String msg)
        {
            header();
            Console.WriteLine("\n\n" + st + msg + "\n\n");
            footer();
            Console.Write(st + "Press <any> key to continue:");
            Console.ReadKey();
        }
        public static bool chk_date(String daaat)
        {
            string[] formats = { "yyyy-M-d" };
            DateTime parsedDateTime;
            if (DateTime.TryParseExact(daaat, formats, new CultureInfo("lt-LT"),
                    DateTimeStyles.None, out parsedDateTime))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Start()
        {
            Random rnd = new Random();
            int ID = rnd.Next(89);

            List<ToDoItem> TD_Task = new List<ToDoItem>();
            bool check = true; ;

        START:

            while (true)
            {


                header();
                Console.WriteLine(xst + "1.New Task.\t\t5.Update Task.\n");
                Console.WriteLine(xst + "2.View Al.\t\t6.Delete Task.\n");
                Console.WriteLine(xst + "3.View b/w Dates.\t7.Sort.\n");
                Console.WriteLine(xst + "4.Find Task.\t\t8.Exit\n");
                footer();

                Console.Write(st + "Enter your choice: ");
                int ch = 0;
                try
                {
                    ch = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {

                    Message("ERROR: Insert Only Intergers!");
                }

                
                switch (ch)
                {
                    case 1: AddTableData(); break;

                    case 2: PrintTable(); break;

                    case 3:

                    case 4:

                    case 5:

                    case 6:

                    case 7:

                    case 8:
                        Environment.Exit(0);
                        break;

                    default:
                        Message("Invalid choice!");
                        break;

                }
            }
        }
        private void PrintTable()
        {
            Console.WriteLine("Table content : ");
            List<ToDoItem> toDoItem = toDoListRepository.GetAll();
            toDoItem.ForEach(Console.WriteLine);
        }

        private void AddTableData()
        {
            Console.WriteLine("Adding new task: ");
            Console.Write("Date:");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Task:");
            String task = Console.ReadLine();            
            Console.Write("Priority:");
            Int32 priority = Convert.ToInt32(Console.ReadLine());

            toDoListRepository.Add(new ToDoItem()
            {
                Date = date,
                Task = task,
                Priority = priority
            });

        }

        private void UpdateTableData()
        {

            Console.WriteLine("Which task you want to update: ");
            Console.Write("Task id:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Please enter integer number. Task id:");
            }

            ToDoItem toDoItem = toDoListRepository.Get(id);

            if (toDoItem != null)
            {

                //Console.WriteLine("Updating student - " + student);
                Console.Write("New date:");
                DateTime date = Convert.ToDateTime(Console.ReadLine());
                Console.Write("New task:");
                String task = Console.ReadLine();
                Console.Write("New priority:");
                Int32 priority = Convert.ToInt32(Console.ReadLine());

                toDoListRepository.Update(new ToDoItem()
                {
                    ID = id,
                    Date = date,
                    Task = task,
                    Priority = priority
                });


            }
            else
            {
                Console.WriteLine("Task with this id does not exist.");
            }

        }

        private void DeleteTableData()
        {
            Console.WriteLine("Which task you want to delete: ");
            Console.Write("Task id:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Please enter integer number. Task id:");
            }

            toDoListRepository.Delete(id);
        }
    }
}
