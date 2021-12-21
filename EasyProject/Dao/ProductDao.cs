﻿using EasyProject.Model;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;

namespace EasyProject.Dao
{
    public class ProductDao : CommonDBConn, IProductDao //DB연결 Class 및 인터페이스 상속
    {

        public List<ProductShowModel> GetProducts()
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

                        cmd.CommandText = "SELECT P.prod_code, P.prod_name, C.category_name, P.prod_price, I.imp_dept_count, P.prod_expire, P.prod_id, I.imp_dept_id " +
                                          "FROM PRODUCT P " +
                                          "INNER JOIN IMP_DEPT I " +
                                          "ON P.prod_id = I.prod_id " +
                                          "INNER JOIN CATEGORY C " +
                                          "ON P.category_id = C.category_id " +
                                          "INNER JOIN DEPT D " +
                                          "ON I.dept_id = D.dept_id " +
                                          "WHERE D.dept_status != '폐지' " +
                                          "AND D.dept_name = (select dept_name from dept where dept_id = :dept_id) " +
                                          "ORDER BY P.prod_expire, P.prod_name";

                        cmd.Parameters.Add(new OracleParameter("dept_id", App.nurse_dto.Dept_id));

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string prod_code = reader.GetString(0);
                            string prod_name = reader.GetString(1);
                            string category_name = reader.GetString(2);
                            int? prod_price = reader.GetInt32(3);
                            int? imp_dept_count = reader.GetInt32(4);
                            DateTime prod_expire = reader.GetDateTime(5);
                            int? prod_id = reader.GetInt32(6);
                            int? imp_dept_id = reader.GetInt32(7);


                            ProductShowModel dto = new ProductShowModel()
                            {
                                Prod_code = prod_code,
                                Prod_name = prod_name,
                                Category_name = category_name,
                                Prod_price = prod_price,
                                Imp_dept_count = imp_dept_count,
                                Prod_expire = prod_expire,
                                Prod_id = prod_id,
                                Imp_dept_id = imp_dept_id
                            };

                            list.Add(dto);

                        }// while

                    } //using(cmd)

                }//using(conn)

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch

            return list;

        }//GetProduct()


        public List<ProductShowModel> GetProductsByDept(DeptModel dept_dto)
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

                        cmd.CommandText = "SELECT P.prod_code, P.prod_name, C.category_name, P.prod_price, I.imp_dept_count, P.prod_expire, P.prod_id, I.imp_dept_id " +
                                          "FROM PRODUCT P " +
                                          "INNER JOIN IMP_DEPT I " +
                                          "ON P.prod_id = I.prod_id " +
                                          "INNER JOIN CATEGORY C " +
                                          "ON P.category_id = C.category_id " +
                                          "INNER JOIN DEPT D " +
                                          "ON I.dept_id = D.dept_id " +
                                          "WHERE D.dept_status != '폐지' " +
                                          "AND D.dept_name = :dept_name " +
                                          "ORDER BY P.prod_expire";

                        cmd.Parameters.Add(new OracleParameter("dept_name", dept_dto.Dept_name));

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string prod_code = reader.GetString(0);
                            string prod_name = reader.GetString(1);
                            string category_name = reader.GetString(2);
                            int? prod_price = reader.GetInt32(3);
                            int? imp_dept_count = reader.GetInt32(4);
                            DateTime prod_expire = reader.GetDateTime(5);
                            int? prod_id = reader.GetInt32(6);
                            int? imp_dept_id = reader.GetInt32(7);


                            ProductShowModel dto = new ProductShowModel()
                            {
                                Prod_code = prod_code,
                                Prod_name = prod_name,
                                Category_name = category_name,
                                Prod_price = prod_price,
                                Imp_dept_count = imp_dept_count,
                                Prod_expire = prod_expire,
                                Prod_id = prod_id,
                                Imp_dept_id = imp_dept_id
                            };

                            list.Add(dto);

                        }// while

                    } //using(cmd)

                }//using(conn)

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch

            return list;
        }//GetProductsByDept



        public List<CategoryModel> GetCategoryModels()
        {
            List<CategoryModel> list = new List<CategoryModel>();
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

                        cmd.CommandText = "SELECT CATEGORY_NAME FROM CATEGORY";

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string category_name = reader.GetString(0);
                            CategoryModel category = new CategoryModel()
                            {
                                Category_name = category_name
                            };
                            list.Add(category);

                        }// while

                    } //using(cmd)

                }//using(conn)

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch

            return list;

        }// GetCategoryModels(string sql)

        public void AddProduct(ProductModel prod_dto, CategoryModel category_dto)
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

                        cmd.CommandText = "INSERT INTO PRODUCT(PROD_CODE, PROD_NAME, PROD_PRICE, PROD_TOTAL, PROD_EXPIRE, CATEGORY_ID) " +
                                          "VALUES(:code, :name, :price, :total, TO_DATE(:expire, 'YYYYMMDD'), (SELECT category_id FROM CATEGORY WHERE category_name = :category_name) )";


                        //파라미터 값 바인딩
                        cmd.Parameters.Add(new OracleParameter("code", prod_dto.Prod_code));
                        cmd.Parameters.Add(new OracleParameter("name", prod_dto.Prod_name));
                        cmd.Parameters.Add(new OracleParameter("price", prod_dto.Prod_price));
                        cmd.Parameters.Add(new OracleParameter("total", prod_dto.Prod_total));

                        // 날짜형식을 -> String 타입으로 변경 후 바인딩
                        string month = prod_dto.Prod_expire.Month.ToString();
                        if (prod_dto.Prod_expire.Month < 10)
                        {
                            month = "0" + prod_dto.Prod_expire.Month.ToString();
                        }// 선택한 월이 1자리 라면 앞에 0을 붙임

                        string day = prod_dto.Prod_expire.Day.ToString();
                        if (prod_dto.Prod_expire.Day < 10)
                        {
                            day = "0" + prod_dto.Prod_expire.Day.ToString();
                        }// 선택한 일이 1자리 라면 앞에 0을 붙임

                        string expire = prod_dto.Prod_expire.Year.ToString() + month + day;
                        Console.WriteLine("Insert DATE : {0}", expire);
                        cmd.Parameters.Add(new OracleParameter("expire", expire));
                        ////////////////////////////////////////////////////////////////////////////

                        cmd.Parameters.Add(new OracleParameter("category_name", category_dto.Category_name));


                        cmd.ExecuteNonQuery();
                    }//using(cmd)

                }//using(conn)


            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch


        }//AddProduct


        public void StoredProduct(ProductModel prod_dto, NurseModel nurse_dto)
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

                        cmd.CommandText = "INSERT INTO PRODUCT_IN(PROD_IN_COUNT, PROD_ID, NURSE_NO, DEPT_ID, PROD_IN_FROM, PROD_IN_TO, PROD_IN_TYPE) " +
                                          "VALUES(:count, PROD_SEQ.CURRVAL, :nurse_no, :dept_id1, :in_from, (SELECT dept_name FROM DEPT WHERE dept_id = :dept_id2), :in_type)";

                        //파라미터 값 바인딩
                        cmd.Parameters.Add(new OracleParameter("count", prod_dto.Prod_total));
                        cmd.Parameters.Add(new OracleParameter("nurse_no", nurse_dto.Nurse_no));
                        cmd.Parameters.Add(new OracleParameter("dept_id1", nurse_dto.Dept_id));

                        cmd.Parameters.Add(new OracleParameter("in_from", "발주처"));
                        cmd.Parameters.Add(new OracleParameter("dept_id2", nurse_dto.Dept_id));
                        cmd.Parameters.Add(new OracleParameter("in_type", "발주"));

                        cmd.ExecuteNonQuery();
                    }//using(cmd)

                }//using(conn)


            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch

        }// StoredProduct()


        public List<ProductInOutModel> GetProductIn()
        {
            List<ProductInOutModel> list = new List<ProductInOutModel>();

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

                        cmd.CommandText = "SELECT P.prod_code, P.prod_name, C.category_name, P.prod_expire, P.prod_price, I.prod_in_count, N.nurse_name, I.prod_in_date, I.prod_in_from, I.prod_in_to, I.prod_in_type, D.dept_name " +
                                          "FROM PRODUCT_IN I " +
                                          "INNER JOIN PRODUCT P " +
                                          "ON I.prod_id = P.prod_id " +
                                          "INNER JOIN CATEGORY C " +
                                          "ON P.category_id = C.category_id " +
                                          "LEFT OUTER JOIN NURSE N " +
                                          "ON I.nurse_no = N.nurse_no " +
                                          "INNER JOIN DEPT D " +
                                          "ON N.dept_id = D.dept_id";

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string prod_code = reader.GetString(0);
                            string prod_name = reader.GetString(1);
                            string category_name = reader.GetString(2);
                            DateTime prod_expire = reader.GetDateTime(3);
                            int? prod_price = reader.GetInt32(4);
                            int? prod_in_count = reader.GetInt32(5);
                            string nurse_name = reader.GetString(6);
                            DateTime prod_in_date = reader.GetDateTime(7);
                            string prod_in_from = reader.GetString(8);
                            string prod_in_to = reader.GetString(9);
                            string prod_in_type = reader.GetString(10);
                            string dept_name = reader.GetString(11);

                            ProductInOutModel dto = new ProductInOutModel()
                            {
                                Prod_code = prod_code,
                                Prod_name = prod_name,
                                Category_name = category_name,
                                Prod_expire = prod_expire,
                                Prod_price = prod_price,
                                Prod_in_count = prod_in_count,
                                Nurse_name = nurse_name,
                                Prod_in_date = prod_in_date,
                                Prod_in_from = prod_in_from,
                                Prod_in_to = prod_in_to,
                                Prod_in_type = prod_in_type,
                                Dept_name = dept_name
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

        }//GetPorductIn

        public List<ProductInOutModel> GetProductOut()
        {
            List<ProductInOutModel> list = new List<ProductInOutModel>();

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

                        cmd.CommandText = "SELECT P.prod_code, P.prod_name, C.category_name, P.prod_price, O.prod_out_count, O.prod_out_date, O.prod_out_type, D.dept_name, N.nurse_name " +
                                          "FROM PRODUCT_OUT O " +
                                          "INNER JOIN PRODUCT P " +
                                          "ON O.prod_id = P.prod_id " +
                                          "INNER JOIN CATEGORY C " +
                                          "ON P.category_id = C.category_id " +
                                          "LEFT OUTER JOIN NURSE N " +
                                          "ON O.nurse_no = N.nurse_no " +
                                          "INNER JOIN DEPT D " +
                                          "ON N.dept_id = D.dept_id";

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string prod_code = reader.GetString(0);
                            string prod_name = reader.GetString(1);
                            string category_name = reader.GetString(2);
                            int? prod_price = reader.GetInt32(3);
                            int? prod_out_count = reader.GetInt32(4);
                            DateTime prod_out_date = reader.GetDateTime(5);
                            string prod_out_type = reader.GetString(6);
                            string dept_name = reader.GetString(7);
                            string nurse_name = reader.GetString(8);

                            ProductInOutModel dto = new ProductInOutModel()
                            {
                                Prod_code = prod_code,
                                Prod_name = prod_name,
                                Category_name = category_name,
                                Prod_price = prod_price,
                                Prod_out_count = prod_out_count,
                                Prod_out_date = prod_out_date,
                                Prod_out_type = prod_out_type,
                                Dept_name = dept_name,
                                Nurse_name = nurse_name
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

        }//GetProductOut

        public void AddImpDept(ProductModel prod_dto, NurseModel nurse_dto)
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

                        cmd.CommandText = "INSERT INTO IMP_DEPT(imp_dept_count, dept_id, prod_id) " +
                                          "VALUES(:count, :dept_id, PROD_SEQ.CURRVAL) ";

                        //파라미터 값 바인딩
                        cmd.Parameters.Add(new OracleParameter("count", prod_dto.Prod_total));
                        cmd.Parameters.Add(new OracleParameter("dept_id", nurse_dto.Dept_id));



                        cmd.ExecuteNonQuery();
                    }//using(cmd)

                }//using(conn)


            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch

        }//AddImpDept

        public ObservableCollection<ProductInOutModel> GetProductInByNurse(NurseModel nurse_dto)
        {
            ObservableCollection<ProductInOutModel> list = new ObservableCollection<ProductInOutModel>();
            Console.WriteLine("GetProductInByNurse 실행");
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

                        cmd.CommandText = "SELECT P.prod_code, P.prod_name, C.category_name, P.prod_expire, P.prod_price, I.prod_in_count, N.nurse_name, I.prod_in_date, I.prod_in_from, I.prod_in_to, I.prod_in_type " +
                                          "FROM PRODUCT_IN I " +
                                          "INNER JOIN PRODUCT P " +
                                          "ON I.prod_id = P.prod_id " +
                                          "INNER JOIN CATEGORY C " +
                                          "ON P.category_id = C.category_id " +
                                          "LEFT OUTER JOIN NURSE N " +
                                          "ON I.nurse_no = N.nurse_no " +
                                          "WHERE I.nurse_no = :no " +
                                          "ORDER BY I.prod_in_date DESC";

                        cmd.Parameters.Add(new OracleParameter(":no", nurse_dto.Nurse_no));
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string prod_code = reader.GetString(0);
                            string prod_name = reader.GetString(1);
                            string category_name = reader.GetString(2);
                            DateTime prod_expire = reader.GetDateTime(3);
                            int? prod_price = reader.GetInt32(4);
                            int? prod_in_count = reader.GetInt32(5);
                            string nurse_name = reader.GetString(6);
                            DateTime prod_in_date = reader.GetDateTime(7);
                            string prod_in_from = reader.GetString(8);
                            string prod_in_to = reader.GetString(9);
                            string prod_in_type = reader.GetString(10);

                            ProductInOutModel dto = new ProductInOutModel()
                            {
                                Prod_code = prod_code,
                                Prod_name = prod_name,
                                Category_name = category_name,
                                Prod_expire = prod_expire,
                                Prod_price = prod_price,
                                Prod_in_count = prod_in_count,
                                Nurse_name = nurse_name,
                                Prod_in_date = prod_in_date,
                                Prod_in_from = prod_in_from,
                                Prod_in_to = prod_in_to,
                                Prod_in_type = prod_in_type
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
        }//GetProductInByNurse


        public List<ProductShowModel> SearchProducts(DeptModel dept_dto, string search_type, string search_text)
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

                        cmd.CommandText = "SELECT P.prod_code, P.prod_name, C.category_name, P.prod_price, I.imp_dept_count, P.prod_expire, P.prod_id, I.imp_dept_id " +
                                          "FROM PRODUCT P " +
                                          "INNER JOIN IMP_DEPT I " +
                                          "ON P.prod_id = I.prod_id " +
                                          "INNER JOIN CATEGORY C " +
                                          "ON P.category_id = C.category_id " +
                                          "INNER JOIN DEPT D " +
                                          "ON I.dept_id = D.dept_id " +
                                          "WHERE D.dept_status != '폐지' " +
                                          "AND D.dept_name = :dept_name " +
                                          "AND " +
                                            "((:search_combo = '제품코드') AND (P.prod_code LIKE '%'||:search_text||'%')) " +
                                          "OR " +
                                            "((:search_combo = '제품명') AND (P.prod_name LIKE '%'||:search_text||'%')) " +
                                          "ORDER BY P.prod_expire, P.prod_name";

                        cmd.Parameters.Add(new OracleParameter("dept_name", dept_dto.Dept_name));

                        cmd.Parameters.Add(new OracleParameter("search_combo", search_type));
                        cmd.Parameters.Add(new OracleParameter("search_text", search_text));
                        Console.WriteLine("dept_name : " + dept_dto.Dept_name);
                        Console.WriteLine("search_combo : " + search_type);
                        Console.WriteLine("search_text : " + search_text);
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string prod_code = reader.GetString(0);
                            string prod_name = reader.GetString(1);
                            string category_name = reader.GetString(2);
                            int? prod_price = reader.GetInt32(3);
                            int? imp_dept_count = reader.GetInt32(4);
                            DateTime prod_expire = reader.GetDateTime(5);
                            int? prod_id = reader.GetInt32(6);
                            int? imp_dept_id = reader.GetInt32(7);


                            ProductShowModel dto = new ProductShowModel()
                            {
                                Prod_code = prod_code,
                                Prod_name = prod_name,
                                Category_name = category_name,
                                Prod_price = prod_price,
                                Imp_dept_count = imp_dept_count,
                                Prod_expire = prod_expire,
                                Prod_id = prod_id,
                                Imp_dept_id = imp_dept_id
                            };

                            list.Add(dto);
                            
                        }// while

                    } //using(cmd)

                }//using(conn)

            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch
            Console.WriteLine("success");
            return list;

        }//SearchProducts()


        public void ChangeProductInfo(ProductShowModel prod_dto)
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

                        cmd.CommandText = "UPDATE PRODUCT SET " +
                                          "prod_code = :code, " +
                                          "prod_name = :name, " +
                                          "category_id = (SELECT category_id FROM CATEGORY WHERE category_name = :category_name), " +
                                          "prod_expire = TO_DATE(:expire, 'YYYYMMDD'), " +
                                          "prod_price = :pirce, " +
                                          "prod_total = :total " +
                                          "WHERE prod_id = :id ";

                        cmd.Parameters.Add(new OracleParameter("code", prod_dto.Prod_code));
                        cmd.Parameters.Add(new OracleParameter("name", prod_dto.Prod_name));
                        cmd.Parameters.Add(new OracleParameter("category_name", prod_dto.Category_name));


                        // 날짜형식을 -> String 타입으로 변경 후 바인딩
                        string month = prod_dto.Prod_expire.Month.ToString();
                        if (prod_dto.Prod_expire.Month < 10)
                        {
                            month = "0" + prod_dto.Prod_expire.Month.ToString();
                        }// 선택한 월이 1자리 라면 앞에 0을 붙임

                        string day = prod_dto.Prod_expire.Day.ToString();
                        if (prod_dto.Prod_expire.Day < 10)
                        {
                            day = "0" + prod_dto.Prod_expire.Day.ToString();
                        }// 선택한 일이 1자리 라면 앞에 0을 붙임

                        string expire = prod_dto.Prod_expire.Year.ToString() + month + day;
                        Console.WriteLine("Insert DATE : {0}", expire);
                        cmd.Parameters.Add(new OracleParameter("expire", expire));
                        ////////////////////////////////////////////////////////////////////////////


                        cmd.Parameters.Add(new OracleParameter("price", prod_dto.Prod_price));
                        cmd.Parameters.Add(new OracleParameter("total", prod_dto.Imp_dept_count));
                        cmd.Parameters.Add(new OracleParameter("id", prod_dto.Prod_id));


                        cmd.ExecuteNonQuery();

                    }//using(cmd)

                }//using(conn)
            }//try
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch
        }//ChangeProductInfo()

        public void ChangeProductInfo_IMP_DEPT(ProductShowModel prod_dto)
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

                        cmd.CommandText = "UPDATE IMP_DEPT SET " +
                                          "imp_dept_count = :imp_total " +
                                          "WHERE imp_dept_id = :imp_id";

                        cmd.Parameters.Add(new OracleParameter("imp_total", prod_dto.Imp_dept_count));
                        cmd.Parameters.Add(new OracleParameter("imp_id", prod_dto.Imp_dept_id));



                        cmd.ExecuteNonQuery();

                    }//using(cmd)

                }//using(conn)
            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch
        }
    }//class

}//namespace
