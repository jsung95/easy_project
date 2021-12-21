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

        //로그인한 간호자(사용자) 정보를 담을 프로퍼티
        public NurseModel Nurse { get; set; }

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

        //선택한 검색 콤보박스를 담을 프로퍼티
        public string SelectedSearchType { get; set; }

        //입력한 검색내용을 담을 프로퍼티
        private string textForSearch;
        public string TextForSearch 
        {
            get { return textForSearch; }
            set
            {
                textForSearch = value; 
                OnPropertyChanged("TextForSearch"); 
            }
        }

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
                Console.WriteLine($"  Imp_dept_id : {SelectedProduct.Imp_dept_id}");

            }
        }

        // 재고 출고 - 선택한 출고 유형 콤보박스를 담을 값
        public string SelectedOutType { get; set; }

        // 재고 출고 - 선택한 부서를 담을 프로퍼티
        private string selectedDeptForOut;
        public string SelectedDeptForOut
        {
            get { return selectedDeptForOut; }
            set 
            { 
                selectedDeptForOut = value;
                OnPropertyChanged("SelectedDeptForOut");
            }
        }

        public ProductShowViewModel()
        {
            Depts = new ObservableCollection<DeptModel>(dept_dao.GetDepts());

            Products = new ObservableCollection<ProductShowModel>(product_dao.GetProducts());

            Categories = new ObservableCollection<CategoryModel>(category_dao.GetCategories());

            //App.xaml.cs 에 로그인할 때 바인딩 된 로그인 정보 객체
            Nurse = App.nurse_dto;
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


        private ActionCommand changeProductCommand;
        public ICommand ChangeProductCommand
        {
            get
            {
                if (changeProductCommand == null)
                {
                    changeProductCommand = new ActionCommand(ChangeProductInfo);
                }
                return changeProductCommand;
            }//get
        }

        public void ChangeProductInfo()
        {
            product_dao.ChangeProductInfo(SelectedProduct);
            product_dao.ChangeProductInfo_IMP_DEPT(SelectedProduct);
        }


        private ActionCommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new ActionCommand(SearchProducts);
                }
                return searchCommand;
            }//get
        }

        public void SearchProducts()
        {
            Products = new ObservableCollection<ProductShowModel>(product_dao.SearchProducts(SelectedDept, SelectedSearchType, TextForSearch));
            
        }


        private ActionCommand outProductCommand;
        public ICommand OutProductCommand
        {
            get
            {
                if (outProductCommand == null)
                {
                    outProductCommand = new ActionCommand(OutProduct);
                }
                return outProductCommand;
            }//get
        }

        public void OutProduct()
        {
            product_dao.OutProduct(SelectedProduct, Nurse, SelectedOutType, SelectedDept);
        }


        private ActionCommand clearType;
        public ICommand ClearType
        {
            get
            {
                if (clearType == null)
                {
                    clearType = new ActionCommand(ClearComboboxType);
                }
                return clearType;
            }//get
        }

        public void ClearComboboxType()
        {
            
            if (SelectedOutType.Equals("System.Windows.Controls.ComboBoxItem: 사용") || SelectedOutType.Equals("System.Windows.Controls.ComboBoxItem: 폐기"))
            {
                Console.WriteLine("!!!!");
                SelectedDeptForOut = null;
            } 
            else
            {
                OnPropertyChanged("SelectedDeptForOut");
            }
        }



    }//class



}//namespace
