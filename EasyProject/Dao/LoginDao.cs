using EasyProject.Model;
using EasyProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace EasyProject.Dao
{
    public class LoginDao : CommonDBConn, ILoginDao
    {
        public NurseModel LoginUserInfo(NurseModel nurse_dto)
        {
            
            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                OracleCommand cmd = new OracleCommand();

                using (conn)
                {
                    conn.Open();

                    using (cmd)
                    {
                        cmd.Connection = conn;

                        cmd.CommandText = "SELECT * FROM NURSE WHERE nurse_no = :no AND nurse_pw = :pw";

                        cmd.Parameters.Add(new OracleParameter("no", nurse_dto.Nurse_no));
                        cmd.Parameters.Add(new OracleParameter("pw", SHA256Hash.StringToHash(nurse_dto.Nurse_pw)));

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            int? nurse_no = reader.GetInt32(0);
                            string nurse_name = reader.GetString(1);
                            string nurse_auth = reader.GetString(2);
                            string nurse_pw = reader.GetString(3);
                            int? dept_id = reader.GetInt32(4);

                            nurse_dto.Nurse_no = nurse_no;
                            nurse_dto.Nurse_name = nurse_name;
                            nurse_dto.Nurse_auth = nurse_auth;
                            nurse_dto.Nurse_pw = nurse_pw;
                            nurse_dto.Dept_id = dept_id;
                        }//while

                    }//using(cmd)

                }//using(conn)

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch

            return nurse_dto;
        }///
    }//class

}//namespace
