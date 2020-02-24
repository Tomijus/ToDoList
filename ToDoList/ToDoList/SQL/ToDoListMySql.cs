using MySql.Data.MySqlClient;
using SqlExamples.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Data;

namespace ToDoList.SQL
{
    class ToDoListMySql : SqlConnection, IRepository<ToDoItem>
    {
        private MySqlConnection connection;
        private MySqlCommand cmd;
        private MySqlDataReader dr;
        public override bool Connect()
        {
            return OpenConnection(
                host: "remotemysql.com",
                dbName: "RBT254jPGD",
                dbUser: "RBT254jPGD",
                dbPassword: "pidF6cSLom",
                port: 3306
            );
        }

        private bool OpenConnection(string host, string dbName, string dbUser, string dbPassword, int port)
        {

            string connstring = $"server={host}; database={dbName}; port={port}; user={dbUser}; password={dbPassword};";
            connection = new MySqlConnection(connstring);

            //TODO : check if table needs to be created.
            //InitTables();

            //TODO : check if connection is fine.

            return true;
        }

        private void InitTables()
        {
            string sql = @"CREATE TABLE `ToDoList` ( 
                            `Id` INT NOT NULL AUTO_INCREMENT UNIQUE, 
                            `Date` DateTime NOT NULL , 
                            `Task` TEXT NULL , 
                            `Priority` INT NULL , 

                            PRIMARY KEY (`Id`)
                        )";

            connection.Open();
            using (cmd = new MySqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        public List<ToDoItem> GetAll()
        {
            List<ToDoItem> retVal = new List<ToDoItem>();

            using (cmd = new MySqlCommand("SELECT * FROM ToDoList", connection))
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
            using (cmd = new MySqlCommand("SELECT * FROM ToDoList WHERE ID = @id", connection))
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
            using (cmd = new MySqlCommand(@"INSERT INTO ToDoList (`Date`, `Task`, `Priority`) 
                                                           VALUES(@date, @task, @priority);", connection))
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
            using (cmd = new MySqlCommand(@"UPDATE ToDoList 
                                               SET Date = @date, Task = @task, Priority = @priority 
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
            using (cmd = new MySqlCommand(@"DELETE FROM ToDoList WHERE ID = @id;", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new MySqlParameter("id", id));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
