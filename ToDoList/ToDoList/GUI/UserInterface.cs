using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ToDoList.Data;

namespace ToDoList.GUI
{
    class UserInterface
    {
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

        public static void Start()
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
                    case 1:
                        header();
                        Console.Write("\t\tEnter the Date.\t[dd-MM-yyyy]\n\t\t");
                        try
                        {
                            string dat = Console.ReadLine();
                            string daat = dat;

                            DateTime cur_time = DateTime.Now;
                            cur_time.ToString("yyyy-M-d");
                            try
                            {
                                TimeSpan duration = DateTime.Parse(cur_time.ToString()) - (DateTime.Parse(dat.ToString()));


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
                                    string msg = "Plz select date from\n\t\t" + dtu.ToString("d-M-yyyy") + " onwards!";
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
                                string msg = Console.ReadLine();

                                Console.Write("\n\t\tEnter Level of Importance.\t[1-5]\n\t\t");
                                int lvl = int.Parse(Console.ReadLine());
                                if (lvl >= 1 && lvl <= 5)
                                {
                                    ID++;

                                    TD_Task.Add(new ToDoItem(ID, DateTime.Parse(dat), msg, lvl));
                                    Message("New Task created with Task ID = " + ID.ToString());
                                    TD_Task.Sort(); // Sort db   
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
                        break;

                    case 2:
                        header();
                        Console.WriteLine("\t\tID \tDate\tTask\t\tLevel");

                        foreach (ToDoItem x in TD_Task)
                        {
                            check = false;
                            Console.WriteLine("\t\t" + x.ID + "  " + x.Date.ToString("yyyy-MM-dd") + "\t" + x.Task + "\t\t" + x.Priority);
                        }
                        if (check)
                        {
                            Console.WriteLine("\n\n\t\t\tNo Records Found!\n\n");
                        }
                        footer();
                        Console.Write(st + "Press <any> key to continue:");
                        Console.ReadKey();

                        break;

                    case 3:

                        header();

                        string cmp_date1, mon1, day1, S_d, E_d, S_m, E_m, S_da, E_da;
                        int SD, ED, cmp_date, mon, SM, EM, dayx, SDA, EDA;

                        Console.Write("\t\tEnter starting Date.\t[dd-MM-yyyy]\n\t\t");
                        string Sdat3 = Console.ReadLine();

                        if (chk_date(Sdat3)) // check validity of date   
                        {
                            Console.Write("\n\t\tEnter ending Date.\t[dd-MM-yyyy]\n\t\t");
                            string Edat3 = Console.ReadLine();
                            Console.WriteLine("\t\t------------------------------------");
                            Console.WriteLine("\t\tID \tDate\tTask\tLevel");

                            if (chk_date(Edat3)) // check validity of date   
                            {

                                DateTime s = DateTime.Parse(Sdat3);
                                DateTime e = DateTime.Parse(Edat3);


                                for (int i = 0; i < TD_Task.Count; i++)
                                {
                                    //year  
                                    cmp_date1 = TD_Task[i].Date.ToString("yyyy");
                                    cmp_date = int.Parse(cmp_date1);
                                    S_d = s.ToString("yyyy");
                                    E_d = e.ToString("yyyy");
                                    SD = int.Parse(S_d);
                                    ED = int.Parse(E_d);

                                    //month  
                                    mon1 = TD_Task[i].Date.ToString("MM");
                                    mon = int.Parse(mon1);
                                    S_m = s.ToString("MM");
                                    E_m = e.ToString("MM");
                                    SM = int.Parse(S_m);
                                    EM = int.Parse(E_m);

                                    //day  
                                    day1 = TD_Task[i].Date.ToString("dd");
                                    dayx = int.Parse(day1);
                                    S_da = s.ToString("dd");
                                    E_da = e.ToString("dd");
                                    SDA = int.Parse(S_da);
                                    EDA = int.Parse(E_da);

                                    if (cmp_date >= SD && cmp_date <= ED)  // Range of Years   
                                    {
                                        if (mon >= SM && mon <= EM)     // Range of Months  
                                        {
                                            if (dayx >= SDA && dayx <= EDA)  // Range of Days  
                                            {
                                                check = false;
                                                Console.WriteLine("\t\t" + TD_Task[i].ID + "  " + TD_Task[i].Date.ToString("dd-MM-yyyy") + "\t " + TD_Task[i].Task + "\t " + TD_Task[i].Priority);
                                            }
                                        }
                                    }
                                }

                                if (check)
                                {
                                    Console.WriteLine("\n\n\t\t\tNo Records Found!\n\n");
                                }
                                footer();
                                Console.Write(st + "Press <any> key to continue:");
                                Console.ReadKey();
                            }
                            else
                            {
                                Message("ERROR: Invalid Ending Date!");
                            }
                        }
                        else
                        {
                            Message("ERROR: Invalid Starting Date!");
                        }

                        break;

                    case 4:
                        header();
                        Console.Write("\t\tEnter the String.\n\t\t");
                        try
                        {
                            string str1;
                            string str = Console.ReadLine();
                            str.ToLower();
                            Console.WriteLine("\t\t------------------------------------");
                            Console.WriteLine("\t\tID \tDate\tTask\tLevel");
                            for (int i = 0; i < TD_Task.Count; i++)
                            {
                                str1 = TD_Task[i].Task;
                                str1.ToLower();
                                if (str1.Contains(str))
                                {
                                    check = false;
                                    Console.WriteLine("\t\t" + TD_Task[i].ID + "  " + TD_Task[i].Date.ToString("dd-MM-yyyy") + "\t " + TD_Task[i].Task + "\t " + TD_Task[i].Priority);
                                }


                            }

                            if (check)
                            {
                                Console.WriteLine("\n\n\t\t\tNo Records Found!\n\n");
                            }
                            footer();
                            Console.Write(st + "Press <any> key to continue:");
                            Console.ReadKey();
                        }
                        catch (Exception)
                        {

                            Message("Error in Find string");
                        }


                        break;

                    case 5:
                        header();
                        Console.Write("\t\tEnter the Task_ID.\n\t\t");
                        try
                        {
                            int T_ID = int.Parse(Console.ReadLine());
                            Console.WriteLine("\t\t------------------------------------");
                            for (int i = 0; i < TD_Task.Count; i++)
                            {
                                if (TD_Task[i].ID == T_ID)
                                {
                                    check = false;
                                    Console.Write("\t\tEnter the Date.\t[dd-MM-yyyy]\n\t\t");
                                    try
                                    {
                                        string dat = Console.ReadLine();
                                        string daat = dat;

                                        DateTime cur_time = DateTime.Now;
                                        cur_time.ToString("d-M-yyyy");
                                        try
                                        {
                                            TimeSpan duration = DateTime.Parse(cur_time.ToString()) - (DateTime.Parse(dat.ToString()));


                                            int day = (int)Math.Round(duration.TotalDays);

                                            if (day >= 2) // if date less than todays   
                                            {
                                                DateTime dtu = DateTime.Now;
                                                string msg = "Plz select date from\n\t\t" + dtu.ToString("d-M-yyyy") + " onwards!";
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
                                            string msg = Console.ReadLine();

                                            Console.Write("\n\t\tEnter Level of Importance.\t[1-5]\n\t\t");
                                            int lvl = int.Parse(Console.ReadLine());
                                            if (lvl >= 1 && lvl <= 5)
                                            {


                                                TD_Task[i].Date = DateTime.Parse(dat);
                                                TD_Task[i].Task = msg;
                                                TD_Task[i].Priority = lvl;

                                                Console.WriteLine("\t\tTask Updated!");
                                                TD_Task.Sort(); // Sort db   
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

                                }
                            }
                            if (check)
                            {
                                Console.WriteLine("\n\n\t\t\t No Record Found!\n\n");
                            }
                            footer();
                            Console.Write(st + "Press <any> key to continue:");
                            Console.ReadKey();

                        }
                        catch (Exception)
                        {

                            Message("ERROR: Insert Only Intergers!");
                        }
                        break;

                    case 6:
                        header();
                        Console.Write("\t\tEnter the Task_ID.\n\t\t");
                        try
                        {
                            int T_ID = int.Parse(Console.ReadLine());
                            Console.WriteLine("\t\t------------------------------------");
                            for (int i = 0; i < TD_Task.Count; i++)
                            {
                                if (TD_Task[i].ID == T_ID)
                                {
                                    check = false;
                                    TD_Task.RemoveAll(e => e.ID == T_ID);
                                }
                            }
                            if (check)
                            {
                                Console.WriteLine("\n\n\t\t\t No Record Found!\n\n");
                            }
                            else
                            {
                                Console.WriteLine("\n\n\t\t\t Record Deleted!\n\n");

                            }
                            footer();
                            Console.Write(st + "Press <any> key to continue:");
                            Console.ReadKey();

                        }
                        catch (Exception)
                        {

                            Message("ERROR: Insert Only Intergers!");
                        }
                        break;


                    case 7:

                        while (true)
                        {
                            header();
                            Console.WriteLine("\t\t\t1.Sort By ID.");
                            Console.WriteLine("\t\t\t2.Sort By DATE.");
                            Console.WriteLine("\t\t\t3.Sort By Level Of Importance.");
                            Console.WriteLine("\t\t\t4.Exit.");
                            footer();
                            Console.Write(st + "Enter your choice: ");
                            ch = int.Parse(Console.ReadLine());

                            switch (ch)
                            {

                                case 1:

                                    header();
                                    Console.WriteLine("\t\tID \tDate\tTask\tLevel");
                                    Console.WriteLine("\t\t------------------------------------");
                                    TD_Task = TD_Task.OrderBy(x => x.ID).ToList();
                                    foreach (ToDoItem x in TD_Task)
                                    {
                                        check = false;
                                        Console.WriteLine("\t\t" + x.ID + "  " + x.Date.ToString("dd-MM-yyyy") + "\t" + x.Task + "\t" + x.Priority);
                                    }
                                    if (check)
                                    {
                                        Console.WriteLine("\n\n\t\t\tNo Records Found!\n\n");
                                    }
                                    footer();
                                    TD_Task = TD_Task.OrderBy(x => x.Date).ToList();

                                    Console.Write(st + "Press <any> key to continue:");
                                    Console.ReadKey();

                                    break;

                                case 2:

                                    header();
                                    Console.WriteLine("\t\tID \tDate\tTask\tLevel");
                                    Console.WriteLine("\t\t------------------------------------");
                                    TD_Task = TD_Task.OrderBy(x => x.Date).ToList();
                                    foreach (ToDoItem x in TD_Task)
                                    {
                                        check = false;
                                        Console.WriteLine("\t\t" + x.ID + "  " + x.Date.ToString("dd-MM-yyyy") + "\t" + x.Task + "\t" + x.Priority);
                                    }
                                    if (check)
                                    {
                                        Console.WriteLine("\n\n\t\t\tNo Records Found!\n\n");
                                    }
                                    footer();
                                    TD_Task = TD_Task.OrderBy(x => x.Date).ToList();

                                    Console.Write(st + "Press <any> key to continue:");
                                    Console.ReadKey();

                                    break;

                                case 3:

                                    header();
                                    Console.WriteLine("\t\tID \tDate\tTask\tLevel");
                                    Console.WriteLine("\t\t------------------------------------");
                                    TD_Task = TD_Task.OrderBy(x => x.Priority).ToList();
                                    TD_Task.Reverse();
                                    foreach (ToDoItem x in TD_Task)
                                    {
                                        check = false;
                                        Console.WriteLine("\t\t" + x.ID + "  " + x.Date.ToString("dd-MM-yyyy") + "\t" + x.Task + "\t" + x.Priority);
                                    }
                                    if (check)
                                    {
                                        Console.WriteLine("\n\n\t\t\tNo Records Found!\n\n");
                                    }
                                    footer();
                                    TD_Task = TD_Task.OrderBy(x => x.Date).ToList();

                                    Console.Write(st + "Press <any> key to continue:");
                                    Console.ReadKey();

                                    break;
                                case 4:
                                    goto START;


                                default:
                                    Message("Invalid choice!");
                                    break;
                            }

                        }

                    case 8:
                        Environment.Exit(0);
                        break;

                    default:
                        Message("Invalid choice!");
                        break;

                }
            }
        }
    }
}
