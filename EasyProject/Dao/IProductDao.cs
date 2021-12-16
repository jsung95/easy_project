using EasyProject.Model;
using System;
using System.Collections.Generic;

namespace EasyProject.Dao
{
    public interface IProductDao
    {
        // 재고조회
        List<ProductShowModel> GetProducts();

        // 재고입력 - 제품 카테고리 가져오기
        List<CategoryModel> GetCategoryModels();

        // 재고입력 - 제품 입력
        void AddProduct(ProductModel prod_dto, CategoryModel category_dto);

        // 입고테이블에 추가
        void StoredProduct(ProductModel prod_dto, NurseModel nurse_dto);

        // 입고테이블 조회
        List<ProductInOutModel> GetProductIn();

        // 출고테이블 조회
        List<ProductInOutModel> GetProductOut();

        // IMP_DEPT 테이블 추가
        void AddImpDept(ProductModel prod_dto, NurseModel nurse_dto);

        // 현재 사용자의 입고 목록을 가져옴(InsertListPage)
        List<ProductInOutModel> GetProductInByNurse(NurseModel nurse_dto);
    }//interface

}//namespace
