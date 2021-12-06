using System;
using Oracle.ManagedDataAccess.Client;

namespace EasyProject
{
    public class EasyOracleConnection
    {
        public static void dbConnection()
        {
            // Oracle 접속 변수 설정
            var user = "ADMIN";
            var password = "Oracle12345!";
            var ds = "db202112031025_high";

            var connectionString = $"User Id={user};Password={password};Data Source={ds};";

            try
            {
                // Oracle 접속
                OracleConnection conn = new OracleConnection(connectionString);

                Console.WriteLine("DB 연결 시작");
                conn.Open();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM emp WHERE empno >= :num";
                cmd.Parameters.Add(new OracleParameter("num", 5000));

                OracleDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    string ename = rdr["ENAME"] as string;
                    string job = rdr["JOB"] as string;

                    Console.WriteLine($"{ename}");
                    Console.WriteLine($"{job}");
                }

                Console.WriteLine("DB 연결 해제");
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
