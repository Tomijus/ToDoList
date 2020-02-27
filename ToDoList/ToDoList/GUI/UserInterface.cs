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
        START:

            while (true)
            {


                header();
                Console.WriteLine(xst + "1.New Task.\t\t5.Update Task.\n");
                Console.WriteLine(xst + "2.View All.\t\t6.Delete Task.\n");
                Console.WriteLine(xst + "3.View by ID.\t\t7.Sort by Priority.\n");
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

                    case 3: FindByID(); break;

                    case 4: FindByTask(); break;

                    case 5: UpdateTableData(); break;

                    case 6: DeleteTableData(); break;

                    case 7: SortByCriteria(); break;

                    case 8: Environment.Exit(0); break;

                    default: Message("Invalid choice!"); break;
                }
            }
        }
        private void PrintTable()
        {
            header();
            Console.WriteLine("\t\tID \tDate\t\tTask\tPriority");
            List<ToDoItem> toDoItem = toDoListRepository.GetAll();
            toDoItem.ForEach(Console.WriteLine);
            //toDoItem.ForEach(t => Console.WriteLine(t.PrittyString()));
            footer();
            Console.Write(st + "Press <any> key to continue:");
            Console.ReadKey();
        }

        private void AddTableData()
        {
            START:
            header();
            Console.Write("\t\tEnter the Date.\t[yyyy-MM-dd]\n\t\t");          
            string date = Console.ReadLine();
            string daat = date;

            DateTime cur_time = DateTime.Now;
            cur_time.ToString("yyyy-M-d");
            try
            {
                TimeSpan duration = DateTime.Parse(cur_time.ToString()) - (DateTime.Parse(date.ToString()));


                int day = (int)Math.Round(duration.TotalDays);

                int x = 0;
                if (day % 2 != 0)
                {
                    x = 2;
                }
                else
                {
                    x = 1;
                }
                if (day >= x) // if date less than todays   
                {
                    DateTime dtu = DateTime.Now;
                    string msg = "Select date from\n\t\t" + dtu.ToString("yyyy-M-d") + " onwards!";
                    Message("ERROR: " + msg);
                    goto START;
                }

            }
            catch (FormatException)
            {
                Message("ERROR: Invalid Date!");
                goto START;
            }
            if (chk_date(daat)) // check validity of date   
            {
                Console.Write("\n\t\tEnter Task.\n\t\t");
                String task = Console.ReadLine();
                priority:
                Console.Write("\n\t\tEnter Level of Priority.\t[1-5]\n\t\t");
                int priority = int.Parse(Console.ReadLine());
                if (priority >= 1 && priority <= 5)
                {
                    toDoListRepository.Add(new ToDoItem()
                    {
                        Date = date,
                        Task = task,
                        Priority = priority
                    });
                    Message("New Task created!");
                       
                }
                else
                {
                    Message("ERROR: Only between [1-5]!");
                    goto priority;
                }
            }
            else
            {
                Message("ERROR: Invalid Date!");
            }
        }

        private void UpdateTableData()
        {
            header();
            Console.Write("\t\tEnter the Task_ID.\n\t\t");
            
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\t\t------------------------------------");

                Start:
                Console.Write("\t\tEnter the Date.\t[yyyy-MM-dd]\n\t\t");
                try
                {
                    string date = Console.ReadLine();
                    string daat = date;

                    DateTime cur_time = DateTime.Now;
                    cur_time.ToString("yyyy-M-d");
                    try
                    {
                        TimeSpan duration = DateTime.Parse(cur_time.ToString()) - (DateTime.Parse(date.ToString()));


                        int day = (int)Math.Round(duration.TotalDays);

                        if (day >= 2) // if date less than todays   
                        {
                            DateTime dtu = DateTime.Now;
                            string msg = "Select date from\n\t\t" + dtu.ToString("yyyy-M-d") + " onwards!";
                            Message("ERROR: " + msg);
                            goto Start;
                        }

                    }
                    catch (FormatException)
                    {
                        Message("ERROR: Invalid Date!");
                        goto Start;

                    }



                    if (chk_date(daat)) // check validity of date   
                    {
                        Console.Write("\n\t\tEnter Task.\n\t\t");
                        string task = Console.ReadLine();

                        Console.Write("\n\t\tEnter Level of Priority.\t[1-5]\n\t\t");
                        int priority = int.Parse(Console.ReadLine());
                        if (priority >= 1 && priority <= 5)
                        {

                            toDoListRepository.Update(new ToDoItem()
                            {
                                ID = id,
                                Date = date,
                                Task = task,
                                Priority = priority
                            });

                            Console.WriteLine("\t\tTask Updated!");
  
                        }
                        else
                        {
                            Message("ERROR: Only between [1-5]!");
                        }
                    }
                    else
                    {
                        Message("ERROR: Invalid Date!");
                    }


                }
                catch (Exception)
                {
                    Message("ERROR: Enter Integer Only!!");
                }

                footer();
                Console.Write(st + "Press <any> key to continue:");
                Console.ReadKey();

            }
            catch (Exception)
            {

                Message("ERROR: Insert Only Intergers!");
            }

        }

        private void DeleteTableData()
        {
            header();
            Console.Write("\t\tEnter the Task_ID.\n\t\t");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\t\t------------------------------------");
                toDoListRepository.Delete(id);
                Console.WriteLine("\n\n\t\t\t Record Deleted!\n\n");
                footer();
                Console.Write(st + "Press <any> key to continue:");
                Console.ReadKey();

            }
            catch (Exception)
            {

                Message("ERROR: Insert Only Intergers!");
            }
        }
        private void FindByID()
        {
            header();
            Console.Write("\t\tEnter ID.\t\n\t\t");
            Int32 id = Convert.ToInt32(Console.ReadLine());
            
            header();
            Console.WriteLine("\t\tID \tDate\t\tTask\tPriority");
            ToDoItem toDoItem = toDoListRepository.Get(id);
            Console.WriteLine(toDoItem);
            footer();
            Console.Write(st + "Press <any> key to continue:");
            Console.ReadKey();
        }
        private void FindByTask()
        {
            header();
            Console.Write("\t\tEnter word.\t\n\t\t");
            string task = Console.ReadLine();

            header();
            Console.WriteLine("\t\tID \tDate\t\tTask\tPriority");
            List<ToDoItem> toDoItem = toDoListRepository.Find(task);
            toDoItem.ForEach(Console.WriteLine);
            footer();
            Console.Write(st + "Press <any> key to continue:");
            Console.ReadKey();
        }
        private void SortByCriteria()
        {
            header();
            Console.WriteLine("\t\tID \tDate\t\tTask\tPriority");
            List<ToDoItem> toDoItem = toDoListRepository.Sort();
            toDoItem.ForEach(Console.WriteLine);
            footer();
            Console.Write(st + "Press <any> key to continue:");
            Console.ReadKey();
        }
    }
}
