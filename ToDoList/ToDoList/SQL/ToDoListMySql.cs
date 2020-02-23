using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Data;

namespace ToDoList.SQL
{
    class ToDoListMySql
    {
        private MySqlConnection connection;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        private bool OpenConnection()
        {

            string connstring = @"Source=(localdb)\MSSQLLocalDB;Initial Catalog=ToDoListDB";
            connection = new MySqlConnection(connstring);

            //TODO : check if table needs to be created.
            //InitTables();

            //TODO : check if connection is fine.

            return true;
        }
        public List<ToDoItem> GetAll()
        {
            List<ToDoItem> retVal = new List<ToDoItem>();

            using (cmd = new MySqlCommand("SELECT * FROM Students", connection))
            {
                connection.Open();
                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        retVal.Add(new ToDoItem()
                        {
                            ID = dr.GetInt32(0),
                            Date = dr.GetDateTime(1),
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
            using (cmd = new MySqlCommand("SELECT * FROM Students WHERE ID = @id", connection))
            {
                connection.Open();
                cmd.Parameters.Add(new MySqlParameter("id", id));
                using (dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        retVal = new ToDoItem()
                        {
                            ID = dr.GetInt32(0),
                            Date = dr.GetDateTime(1),
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
            using (cmd = new MySqlCommand(@"INSERT INTO Students (`Name`, `Score`, `City`) 
                                                           VALUES(@name, @score, @city);", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new MySqlParameter("date", item.Date));
                cmd.Parameters.Add(new MySqlParameter("task", item.Task));
                cmd.Parameters.Add(new MySqlParameter("priority", item.Priority));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(ToDoItem item)
        {
            using (cmd = new MySqlCommand(@"UPDATE Students 
                                               SET Name = @name, Score = @score, City = @city 
                                             WHERE Id=@id;", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new MySqlParameter("id", item.ID));
                cmd.Parameters.Add(new MySqlParameter("date", item.Date));
                cmd.Parameters.Add(new MySqlParameter("task", item.Task));
                cmd.Parameters.Add(new MySqlParameter("priority", item.Priority));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (cmd = new MySqlCommand(@"DELETE FROM Students WHERE ID = @id;", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new MySqlParameter("id", id));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
