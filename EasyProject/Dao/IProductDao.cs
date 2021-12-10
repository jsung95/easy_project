using EasyProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyProject.Dao
{
    internal interface IProductDao
    {
        // 재고입력 - 제품 카테고리 가져오기
        List<CategoryModel> GetCategoryModels(string sql);

        // 재고입력 - 제품 입력
        void AddProduct(string sql, ProductModel prod_dto, CategoryModel category_dto);
    }//interface

}//namespace
