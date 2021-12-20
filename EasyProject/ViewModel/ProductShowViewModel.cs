using EasyProject.Dao;
using EasyProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

namespace EasyProject.ViewModel
{
    public class ProductShowViewModel : Notifier
    {
        DeptDao dept_dao = new DeptDao();
        ProductDao product_dao = new ProductDao();
        CategoryDao category_dao = new CategoryDao();

        //재고 목록 조회해서 담을 옵저버블컬렉션 리스트 프로퍼티
        private ObservableCollection<ProductShowModel> products;
        public ObservableCollection<ProductShowModel> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged("Products");
            }
        }

        //부서 목록 콤보박스, 부서 리스트 출력
        public ObservableCollection<DeptModel> Depts { get; set; }

        //카테고리 목록 콤보박스, 카테고리 목록 출력
        public ObservableCollection<CategoryModel> Categories { get; set; }

        //선택한 부서를 담을 프로퍼티
        public DeptModel SelectedDept { get; set; }

        //선택한 카테고리명을 담을 프로퍼티
        public CategoryModel SelectedCategory { get; set; }

        //선택한 1개의 제품 정보를 담을 객체
        private static ProductShowModel selectedProduct;
        public static ProductShowModel SelectedProduct 
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                //OnPropertyChanged("SelectedProducts");
                //Message.Send(SelectedProducts);
                Console.WriteLine("==선택한 재고 정보==");
                Console.WriteLine($"  Prod_code : {SelectedProduct.Prod_code}");
                Console.WriteLine($"  Prod_name : {SelectedProduct.Prod_name}");
                Console.WriteLine($"  Category_name : {SelectedProduct.Category_name}");
                Console.WriteLine($"  Prod_price : {SelectedProduct.Prod_price}");
                Console.WriteLine($"  Imp_dept_count : {SelectedProduct.Imp_dept_count}");
                Console.WriteLine($"  Prod_expire : {SelectedProduct.Prod_expire}");
                Console.WriteLine($"  Prod_id : {SelectedProduct.Prod_id}");

            }
        }

        public ProductShowViewModel()
        {
            Depts = new ObservableCollection<DeptModel>(dept_dao.GetDepts());

            Products = new ObservableCollection<ProductShowModel>(product_dao.GetProducts());

            Categories = new ObservableCollection<CategoryModel>(category_dao.GetCategories());
        }


        private ActionCommand command;
        public ICommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new ActionCommand(GetProductsByDept);
                }
                return command;
            }//get
        }

        public void GetProductsByDept()
        {


            Products = new ObservableCollection<ProductShowModel>(product_dao.GetProductsByDept(SelectedDept));
        }

    }//class

        

}//namespace
