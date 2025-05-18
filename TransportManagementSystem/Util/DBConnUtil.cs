using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TransportManagementSystem.Util
{
    internal class DBConnUtil
    {
        static readonly string connectionString = @"Server =BOOMIKA\SQLEXPRESS ; Database = TransportSystem ; Integrated Security =True ; MultipleActiveResultSets=true;";

        //method to open the connection
        public static SqlConnection GetConnection()
        {
            SqlConnection connectionObject = new SqlConnection(connectionString);

            try
            {
                connectionObject.Open();
                return connectionObject;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error opening the Connection:{e.Message}");
                return null;
            }

        }

        //method to close the connection
        public static void CloseDbConnection(SqlConnection connectionObject)
        {
            if (connectionObject != null)
            {
                try
                {
                    if (connectionObject.State != ConnectionState.Open)
                    {
                        connectionObject.Close();
                        connectionObject.Dispose();
                        Console.WriteLine("Connection Closed");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Connection is already null");
                }
            }

        }
    }
}
