using EasyProject.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyProject.Dao
{
    public class SignupDao : ISignupDao
    {

        // DB 연결 정보
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

        public List<DeptModel> GetDeptModels(string sql)
        {
            List<DeptModel> list = new List<DeptModel>();
            try
            {
                ConnectingDB();
                cmd.Connection = conn;

                cmd.CommandText = sql;
                cmd.BindByName = false;

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string dept_name = reader.GetString(0); // 선택된 데이터셋 0번 컬럼 = 부서 이름 -> 문자열 변수에 담음.

                    DeptModel dept = new DeptModel() // DeptModel 객체를 생성, 필드값에 dept_name 넣어줌
                    {
                        Dept_name = dept_name
                    };
                    list.Add(dept); // 리스트에 추가
                }//while

                conn.Close();

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
            }//catch

            return list; // DeptModel 객체들이 담긴 list 리턴

        } // GetDeptModels
    } // SignupDao
} // namespace
