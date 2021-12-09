using EasyProject.Model;
using EasyProject.Util;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace EasyProject.Dao
{
    public class SignupDao : CommonDBConn, ISignupDao //DB연결 Class 및 인터페이스 상속
    {

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

        public void SignUp(string sql, NurseModel nurse_dto, DeptModel dept_dto)
        {
            try
            {
                ConnectingDB();
                cmd.Connection = conn;

                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();

                // INSERT INTO NURSE VALUES(:no, :name, :auth, :pw, :dept_id)
                //파라미터 값 바인딩
                cmd.Parameters.Add(new OracleParameter("no", nurse_dto.nurse_no));
                cmd.Parameters.Add(new OracleParameter("name", nurse_dto.nurse_name));
                cmd.Parameters.Add(new OracleParameter("auth", nurse_dto.nurse_auth));
                cmd.Parameters.Add(new OracleParameter("pw", SHA256Hash.StringToHash(nurse_dto.nurse_pw))); //비밀번호 암호화
                
                switch (dept_dto.Dept_name)
                {
                    case "응급실":
                        cmd.Parameters.Add(new OracleParameter("dept_id", 2));
                        break;
                }

            }//try
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch
        }//SignUp(string sql)

    } // SignupDao
} // namespace
