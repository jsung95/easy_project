using EasyProject.Model;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;

namespace EasyProject.Dao
{
    public class DashBoardDao : CommonDBConn
    {
        public ObservableCollection<ProductShowModel> Prodcode_Info()     //prodcode 
        {
            ObservableCollection<ProductShowModel> list = new ObservableCollection<ProductShowModel>();
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
                        cmd.CommandText = "select distinct(prod_code) from product";
                        //cmd.CommandText = "SELECT * FROM NURSE WHERE nurse_no = :no AND nurse_pw = :pw";

                        //cmd.Parameters.Add(new OracleParameter("p_code", prod_dto.Prod_code));
                        //cmd.Parameters.Add(new OracleParameter("total", prod_dto.Prod_total));

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string Prod_code = reader.GetString(0);
                            //int? Prod_total = reader.GetInt32(1);
                            ProductShowModel dto = new ProductShowModel()
                            {
                                Prod_code = Prod_code
                            };
                            list.Add(dto);
                        }//while

                    }//using(cmd)

                }//using(conn)

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch
            return list;
        }///product_info
        public ObservableCollection<ProductShowModel> Prodtotal_Info()               //total
        {
            ObservableCollection<ProductShowModel> list = new ObservableCollection<ProductShowModel>();
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
                        cmd.CommandText = "select sum(prod_total) from product group by prod_code";
                        //cmd.CommandText = "SELECT * FROM NURSE WHERE nurse_no = :no AND nurse_pw = :pw";

                        //cmd.Parameters.Add(new OracleParameter("p_code", prod_dto.Prod_code));
                        //cmd.Parameters.Add(new OracleParameter("total", prod_dto.Prod_total));

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            int? Prod_total = reader.GetInt32(0);
                            ProductShowModel dto = new ProductShowModel()
                            {
                                Prod_total = Prod_total
                            };
                            list.Add(dto);
                        }//while

                    }//using(cmd)

                }//using(conn)

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch
            return list;
        }///product_info
        public List<ProductShowModel> Prodcodetotal_Info(DeptModel SelectedDept)               //code total 리스트
        {
            List<ProductShowModel> list = new List<ProductShowModel>();
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
                        cmd.CommandText = "SELECT P.prod_code, sum(I.imp_dept_count) " +
                            "FROM PRODUCT P " +
                            "INNER JOIN IMP_DEPT I " +
                            "ON P.prod_id = I.prod_id " +
                            "WHERE I.dept_id = :dept_id " +
                            "GROUP BY(P.prod_code) ";
                        //cmd.CommandText = "SELECT * FROM NURSE WHERE nurse_no = :no AND nurse_pw = :pw";

                        cmd.Parameters.Add(new OracleParameter("dept_id", SelectedDept.Dept_id));
                        //cmd.Parameters.Add(new OracleParameter("total", prod_dto.Prod_total));

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string Prod_code = reader.GetString(0);
                            int? Prod_total = reader.GetInt32(1);
                            ProductShowModel dto = new ProductShowModel()
                            {
                                Prod_code = Prod_code,
                                Prod_total = Prod_total
                            };
                            list.Add(dto);
                        }//while

                    }//using(cmd)

                }//using(conn)

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch
            return list;
        }///Prodcodetotal_info

        public List<ProductShowModel> Prodexpiretotal_Info(DeptModel SelectedDept, CategoryModel SelectedCategory)               //code total 리스트
        {
            List<ProductShowModel> list = new List<ProductShowModel>();
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
                        cmd.CommandText = "SELECT TO_DATE(TO_CHAR(prod_expire, 'YYYYMMDD')) - TO_DATE(TO_CHAR(CURRENT_DATE, 'YYYYMMDD')), PROD_TOTAL"
                                         + "FROM PRODUCT";
                        //cmd.CommandText = "SELECT * FROM NURSE WHERE nurse_no = :no AND nurse_pw = :pw";

                        cmd.Parameters.Add(new OracleParameter("dept_id", SelectedDept.Dept_id));
                        //cmd.Parameters.Add(new OracleParameter("total", prod_dto.Prod_total));

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string Prod_code = reader.GetString(0);
                            int? Prod_total = reader.GetInt32(1);
                            ProductShowModel dto = new ProductShowModel()
                            {
                                Prod_code = Prod_code,
                                Prod_total = Prod_total
                            };
                            list.Add(dto);
                        }//while

                    }//using(cmd)

                }//using(conn)

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch
            return list;
        }///Prodexpiretotal_info
    }//class
}//namespace
