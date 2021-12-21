using EasyProject.Model;
using System;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EasyProject.Dao
{
    public class UsersDao : CommonDBConn, IUsersDao
    {
        public List<UserModel> GetUserInfo(string auth)
        {
            List<UserModel> list = new List<UserModel>();

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

                        cmd.CommandText = "SELECT N.nurse_no, N.nurse_name, N.nurse_auth, D.dept_name " +
                                          "FROM NURSE N" +
                                          "LEFT OUTER JOIN DEPT D" +
                                          "ON N.dept_id = D.dept_id" +
                                          "WHERE N.nurse_auth in (:auth)";

                        /*if (auth.Equals("전체"))
                        {
                            auth = "'SUPER','ADMIN','NORMAL'";
                        }//if*/
                        //cmd.Parameters.Add(new OracleParameter("auth", auth));
                        cmd.Parameters.Add(new OracleParameter("auth", "ADMIN"));

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            int? nurse_no = reader.GetInt32(0);
                            string nurse_name = reader.GetString(1);
                            string nurse_auth = reader.GetString(2);
                            string dept_name = reader.GetString(3);

                            UserModel user = new UserModel()
                            {
                                Nurse_no = nurse_no,
                                Nurse_name = nurse_name,
                                Nurse_auth = nurse_auth,
                                Dept_name = dept_name
                            };

                            list.Add(user);
                        }//while

                    }//using(cmd)

                }//using(conn)

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch

            return list;

        }//GetUserInfo()


        public void UserAuthChange(string auth, List<UserModel> no)
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

                        cmd.CommandText = "UPDATE NURSE " +
                                          "SET nurse_auth = :auth " +
                                          "WHERE nurse_no IN (:no)";

                        cmd.Parameters.Add(new OracleParameter("auth", auth));

                        string user_nos = null;
                        foreach (var item in no)
                        {
                            user_nos += item.Nurse_no + ",";
                        }//foreach
                        user_nos = user_nos.Remove(user_nos.Length - 1, 1);
                        cmd.Parameters.Add(new OracleParameter("no", user_nos));
                        Console.WriteLine(user_nos);
                        cmd.ExecuteNonQuery();

                    }//using(cmd)

                }//using(conn)
            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch

        }//UserAuthChange()



        public List<UserModel> SearchUser(string auth, string searchType, string keyword)
        {
            Console.WriteLine($"SearchUser({auth}, {keyword})");
            List<UserModel> list = new List<UserModel>();

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

                        switch (searchType)
                        {
                            case "이름":
                                cmd.CommandText = "SELECT N.nurse_no, N.nurse_name, N.nurse_auth, D.dept_name " +
                                          "FROM NURSE N " +
                                          "LEFT OUTER JOIN DEPT D " +
                                          "ON N.dept_id = D.dept_id " +
                                          "WHERE N.nurse_name LIKE '%'|| :keyword ||'%' " +
                                          "AND N.nurse_auth = :auth";
                                break;

                            case "아이디":
                                cmd.CommandText = "SELECT N.nurse_no, N.nurse_name, N.nurse_auth, D.dept_name " +
                                          "FROM NURSE N " +
                                          "LEFT OUTER JOIN DEPT D " +
                                          "ON N.dept_id = D.dept_id " +
                                          "WHERE N.nurse_no LIKE '%'|| :keyword ||'%' " +
                                          "AND N.nurse_auth = :auth";
                                break;

                            case "부서":
                                cmd.CommandText = "SELECT N.nurse_no, N.nurse_name, N.nurse_auth, D.dept_name " +
                                          "FROM NURSE N " +
                                          "LEFT OUTER JOIN DEPT D " +
                                          "ON N.dept_id = D.dept_id " +
                                          "WHERE D.dept_name LIKE '%'|| :keyword ||'%' " +
                                          "AND N.nurse_auth = :auth";
                                break;
                        }

                        cmd.Parameters.Add(new OracleParameter("keyword", keyword));
                        cmd.Parameters.Add(new OracleParameter("auth", auth));

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {

                            int? nurse_no = reader.GetInt32(0);
                            string nurse_name = reader.GetString(1);
                            string nurse_auth = reader.GetString(2);
                            string dept_name = reader.GetString(3);

                            UserModel user = new UserModel()
                            {
                                Nurse_no = nurse_no,
                                Nurse_name = nurse_name,
                                Nurse_auth = nurse_auth,
                                Dept_name = dept_name,
                                IsChecked = false
                            };

                            list.Add(user);

                        }//while

                    }//using(cmd)

                }//using(conn)

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch
            Console.WriteLine(list.Count);
            return list;

        }//SearchUser()

    }//class

}//namespace
