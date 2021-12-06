using System;
using Oracle.ManagedDataAccess.Client;

namespace EasyProject
{
    public static class dbConn
    {
        private static string user = "ADMIN";
        private static string password = "Oracle12345!";
        private static string ds = "db202112031025_high";

        private static string connectionString = $"User Id={user};Password={password};Data Source={ds};";
        
        
        static OracleConnection conn = new OracleConnection(connectionString);
        static OracleCommand cmd = new OracleCommand();
        

        public static void ConnectingDB()
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

        public static EmpDTO SelectQuery(string sql, params object[] param)
        {
            try
            {
                ConnectingDB();
                cmd.Connection = conn;

                cmd.CommandText = sql;
                cmd.BindByName = false;

                if(param.Length != 0)
                {
                    cmd.Parameters.Add(new OracleParameter("num", param[0]));
                    cmd.Parameters.Add(new OracleParameter("job", param[1]));
                    /*foreach (object o in param)
                    {
                        cmd.Parameters.Add(new OracleParameter("num", o));
                        cmd.Parameters.Add(new OracleParameter("job", o));
                    }//foreach*/
                }//if
                
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    

                    string ename = reader["ENAME"] as string;
                    string empno = reader["EMPNO"] as string;
                    string job = reader["JOB"] as string;

                    EmpDTO emp = new EmpDTO()
                    {
                        ename = ename,
                        empno = empno,
                        job = job
                        
                    };
                }//while
                
                

                conn.Close();

                
            }//try
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
            }//catch
            

        }//SelectQuery(string sql)

        public static void InsertQuery(string sql, params object[] param)
        {
            try
            {
                ConnectingDB();
                cmd.Connection = conn;

                cmd.CommandText = sql;


                foreach(object o in param)
                {
                    cmd.Parameters.Add(new OracleParameter());
                }//foreach
            }//try
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
            }//catch
        }

    }//class
    
}//namespace
