using EasyProject.Dao;
using EasyProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

namespace EasyProject.ViewModel
{
    public class ProductViewModel : Notifier
    {
        ProductDao dao = new ProductDao();

        public ObservableCollection<CategoryModel> Categories { get; set; }

        //선택한 카테고리를 담을 프로퍼티
        public CategoryModel SelectedCategory { get; set; }
        
        //재고 입력 데이터를 담을 프로퍼티
        public ProductModel Product { get; set; }

        //로그인한 간호자(사용자) 정보를 담을 프로퍼티
        public NurseModel Nurse { get; set; }

        //입력한 재고 데이터를 담은 객체를 담아줄 옵저버블컬렉션 리스트
        public static ObservableCollection<ProductInOutModel> Add_list { get; set; }

        public ProductViewModel()
        {
            Product = new ProductModel()
            {
                Prod_expire = DateTime.Now
            };
            List<CategoryModel> list = dao.GetCategoryModels();
            Categories = new ObservableCollection<CategoryModel>(list); // List타입 객체 list를 OC 타입 Products에 넣음 

            //App.xaml.cs 에 로그인할 때 바인딩 된 로그인 정보 객체
            Nurse = App.nurse_dto;

            //현재 로그인 사용자의 입고 목록을 가져옴
            Add_list = dao.GetProductInByNurse(Nurse);
        }

        private ActionCommand command;
        public ICommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new ActionCommand(ProductInsert);   
                }
                return command;
            }//get

        }//Command

        public void ProductInsert()
        {
            
            //재고입력
            dao.AddProduct(Product, SelectedCategory);

            //입고테이블에 추가
            dao.StoredProduct(Product, Nurse);

            //IMP_DEPT 테이블에 추가
            dao.AddImpDept(Product, Nurse);

            // 현재 사용자가 추가 입고 내역을 담을 임시 객체
            ProductInOutModel dto = new ProductInOutModel();

            // 새로 입고 시 Add_list(사용자의 입고 내역 목록) 업데이트
            dto.Prod_in_date = DateTime.Now;
            dto.Prod_code = Product.Prod_code;
            dto.Prod_name = Product.Prod_name;
            dto.Category_name = SelectedCategory.Category_name;
            dto.Prod_expire = Product.Prod_expire;
            dto.Prod_price = Product.Prod_price;
            dto.Prod_in_count = Product.Prod_total;
            dto.Nurse_name = Nurse.Nurse_name;

            Add_list.Insert(0, dto);

        }// ProductInsert

    }//class

}//namespace
