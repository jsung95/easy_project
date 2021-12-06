using System;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;

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

        public static List<EmpDTO> SelectQuery(string sql, params object[] param)
        {
            List<EmpDTO> list = new List<EmpDTO>();
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

                    //Console.WriteLine(reader.GetInt32(0) + " + " + reader.GetString(1) + " + " + reader.GetString(2) );
                    /*string ename = reader["ENAME"] as string;
                    string empno = reader["SAL"] as string;
                    string job = reader["JOB"] as string;
                    Console.WriteLine(empno);*/

                    Int32 empno = reader.GetInt32(0);
                    string ename = reader.GetString(1);
                    string job = reader.GetString(2);

                    EmpDTO emp = new EmpDTO()
                    {
                        ename = ename,
                        empno = empno,
                        job = job
                    };
                    list.Add(emp);
                }//while
                
                

                conn.Close();

                
            }//try
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
            }//catch

            return list;
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
