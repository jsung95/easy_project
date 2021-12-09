using System;
using Oracle.ManagedDataAccess.Client;

namespace EasyProject.Dao
{
    public class CommonDBConn
    {
        /// /////////////////////////////////////////
        // User Connect Information//////////////////
        protected static readonly string user = "ADMIN";
        protected static readonly string password = "Oracle12345!";
        protected static readonly string ds = "db202112031025_high";

        protected static readonly string connectionString = $"User Id={user};Password={password};Data Source={ds};";
        /// /////////////////////////////////////////
        /// /////////////////////////////////////////


        protected OracleConnection conn = new OracleConnection(connectionString);
        protected OracleCommand cmd = new OracleCommand();


        public void ConnectingDB()
        {
            try
            {
                //DB 연결
                conn.Open();
            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
            }//catch


        }//ConnectingDB()
    }//class

}
