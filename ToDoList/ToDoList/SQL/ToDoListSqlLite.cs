using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Data;
using System.Data.SQLite;
using System.IO;

namespace ToDoList.SQL
{
    class ToDoListSqlLite : SqlConnection, IRepository<ToDoItem>
    {
        private SQLiteConnection connection;
        private SQLiteCommand cmd;
        private SQLiteDataReader dr;

        public override bool Connect()
        {
            return OpenConnection("MyDatabase.sqlite");
        }

        private bool OpenConnection(string dbFalieName)
        {
            if (!File.Exists(dbFalieName))
            {
                SQLiteConnection.CreateFile(dbFalieName);
                connection = new SQLiteConnection("Data Source=" + dbFalieName + ";Version=3;");
                InitTables();
            }
            else
            {
                connection = new SQLiteConnection("Data Source=" + dbFalieName + ";Version=3;");
            }

            return true;
        }
        protected void InitTables()
        {
            string sql = @"CREATE TABLE ToDoList (
	                        'Id'	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	                        'Date'	TEXT NOT NULL,
	                        'Task'	TEXT,
	                        'Priority'	INTEGER
                         );";

            connection.Open();
            using (cmd = new SQLiteCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        public List<ToDoItem> GetAll()
        {
            List<ToDoItem> retVal = new List<ToDoItem>();

            using (cmd = new SQLiteCommand("SELECT * FROM ToDoList", connection))
            {
                connection.Open();
                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        retVal.Add(new ToDoItem()
                        {
                            ID = dr.GetInt32(0),
                            Date = dr.GetString(1),
                            Task = dr.GetString(2),
                            Priority = dr.GetInt32(3)
                        });

                    }
                }
                connection.Close();
            }

            return retVal;
        }

        public ToDoItem Get(int id)
        {
            ToDoItem retVal = null;
            using (cmd = new SQLiteCommand("SELECT * FROM ToDoList WHERE ID = @id", connection))
            {
                connection.Open();
                cmd.Parameters.Add(new SQLiteParameter("id", id));
                using (dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        retVal = new ToDoItem()
                        {
                            ID = dr.GetInt32(0),
                            Date = dr.GetString(1),
                            Task = dr.GetString(2),
                            Priority = dr.GetInt32(3)
                        };
                    }
                }
                connection.Close();
            }
            return retVal;
        }

        public void Add(ToDoItem item)
        {
            // MySql nemegsta ' kabutes. ` tinka, arba nieko nedet tinka.. :\
            using (cmd = new SQLiteCommand(@"INSERT INTO ToDoList ('Date', 'Task', 'Priority') 
                                                           VALUES(@date, @task, @priority);", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new SQLiteParameter("date", item.Date));
                cmd.Parameters.Add(new SQLiteParameter("task", item.Task));
                cmd.Parameters.Add(new SQLiteParameter("priority", item.Priority));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        internal void Add()
        {
            throw new NotImplementedException();
        }

        public void Update(ToDoItem item)
        {
            using (cmd = new SQLiteCommand(@"UPDATE ToDoList 
                                               SET Date = @date, Task = @task, Priority = @priority 
                                             WHERE Id=@id;", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new SQLiteParameter("id", item.ID));
                cmd.Parameters.Add(new SQLiteParameter("date", item.Date));
                cmd.Parameters.Add(new SQLiteParameter("task", item.Task));
                cmd.Parameters.Add(new SQLiteParameter("priority", item.Priority));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (cmd = new SQLiteCommand(@"DELETE FROM ToDoList WHERE ID = @id;", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new SQLiteParameter("id", id));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
