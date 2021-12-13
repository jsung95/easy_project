using EasyProject.Model;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace EasyProject.Dao
{
    public class ProductDao : CommonDBConn, IProductDao //DB연결 Class 및 인터페이스 상속
    {
        
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

                        cmd.CommandText = "INSERT INTO PRODUCT(PROD_CODE, PROD_NAME, PROD_PRICE, PROD_TOTAL, PROD_EXPIRE, CATEGORY_ID) VALUES(:code, :name, :price, :total, TO_DATE(:expire, 'YYYYMMDD'), :category_id)";


                        //파라미터 값 바인딩
                        cmd.Parameters.Add(new OracleParameter("code", prod_dto.Prod_code));
                        cmd.Parameters.Add(new OracleParameter("name", prod_dto.Prod_name));
                        cmd.Parameters.Add(new OracleParameter("price", prod_dto.Prod_price));
                        cmd.Parameters.Add(new OracleParameter("total", prod_dto.Prod_total));

                        // 날짜형식을 -> String 타입으로 변경 후 바인딩
                        string expire = prod_dto.Prod_expire.Year.ToString() + prod_dto.Prod_expire.Month.ToString() + prod_dto.Prod_expire.Day.ToString();
                        cmd.Parameters.Add(new OracleParameter("expire", expire));

                        // 카테고리 이름을 카테고리 번호로 변경 후 바인딩
                        switch (category_dto.Category_name)
                        {
                            case "검사재료":
                                cmd.Parameters.Add(new OracleParameter("category_id", 1));
                                break;
                            case "심혈관재료":
                                cmd.Parameters.Add(new OracleParameter("category_id", 2));
                                break;
                            case "의료소모품":
                                cmd.Parameters.Add(new OracleParameter("category_id", 3));
                                break;
                            case "수술재료":
                                cmd.Parameters.Add(new OracleParameter("category_id", 4));
                                break;
                            case "기타진료지":
                                cmd.Parameters.Add(new OracleParameter("category_id", 5));
                                break;
                            case "치과재료":
                                cmd.Parameters.Add(new OracleParameter("category_id", 6));
                                break;
                            case "방사선재료":
                                cmd.Parameters.Add(new OracleParameter("category_id", 7));
                                break;
                            case "한방진료지":
                                cmd.Parameters.Add(new OracleParameter("category_id", 8));
                                break;
                        } //switch-case


                        cmd.ExecuteNonQuery();

                    }//using(cmd)

                }//using(conn)
                

            }//try
            catch(Exception e)
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

                        cmd.CommandText = "INSERT INTO PRODUCT_IN(PROD_COUNT, PROD_ID, NURSE_NO, DEPT_ID) VALUES(:count, :prod_id, :nurse_no, :dept_id)";

                        //파라미터 값 바인딩
                        cmd.Parameters.Add(new OracleParameter("count", prod_dto.Prod_total));
                        cmd.Parameters.Add(new OracleParameter("prod_id", prod_dto.Prod_id));
                        cmd.Parameters.Add(new OracleParameter("nurse_no", nurse_dto.Nurse_no));
                        cmd.Parameters.Add(new OracleParameter("dept_id", nurse_dto.Dept_id));

                        cmd.ExecuteNonQuery();
                    }//using(cmd)

                }//using(conn)


            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }//catch

        }// StoredProduct()


    }//class

}//namespace
