using EasyProject.Model;
using System;
using System.Collections.Generic;

namespace EasyProject.Dao
{
    internal interface IProductDao
    {
        // 재고입력 - 제품 카테고리 가져오기
        List<CategoryModel> GetCategoryModels(string sql);

        // 재고입력 - 제품 입력
        void AddProduct(string sql, ProductModel prod_dto, CategoryModel category_dto);

        // 입고테이블에 추가
        void StoredProduct(string sql, ProductModel prod_dto, NurseModel nurse_dto);
    }//interface

}//namespace
