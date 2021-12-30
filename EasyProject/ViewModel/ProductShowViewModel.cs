using EasyProject.Dao;
using EasyProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using LiveCharts;
using LiveCharts.Wpf;
using System.Linq;
using Xamarin.Forms;
using System.Windows.Data;
using System.Windows;
using System.ComponentModel;

namespace EasyProject.ViewModel
{
    public class ProductShowViewModel : Notifier
    {
        DeptDao dept_dao = new DeptDao();
        ProductDao product_dao = new ProductDao();
        CategoryDao category_dao = new CategoryDao();
        UsersDao user_dao = new UsersDao();

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

        private bool comboboxChanged = false;
        public bool ComboboxChanged
        {
            get { return comboboxChanged; }
            set
            {
                comboboxChanged = value;
                OnPropertyChanged("ComboboxChanged");
            }
        }

        //카테고리 목록 콤보박스, 카테고리 목록 출력
        public ObservableCollection<CategoryModel> Categories { get; set; }

        //선택한 부서를 담을 프로퍼티
        private DeptModel selectedDept;
        public DeptModel SelectedDept
        {
            get { return selectedDept; }
            set
            {
                selectedDept = value;
                OnPropertyChanged("SelectedDept");
                showListbyDept();
            }
        }

        //선택한 카테고리명을 담을 프로퍼티
        private CategoryModel selectedCategory;
        public CategoryModel SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }
        //검색 유형 프로퍼티
        public string[] SearchTypeList { get; set; }
        //선택한 검색 콤보박스를 담을 프로퍼티
        public string SelectedSearchType { get; set; }
        // 선택한 검색 유형 프로퍼티

        // 버튼 컬럼 투명도
        //private Visibility buttonColumnVisibility;
        //public Visibility ButtonColumnVisibility
        //{
        //    get { return buttonColumnVisibility; }
        //    set
        //    {
        //        buttonColumnVisibility = value;
        //        Console.WriteLine("ButtonColumnVisibility set : " + buttonColumnVisibility);
        //        OnPropertyChanged("ButtonColumnVisibility");
        //    }
        //}

        // 대시보드 프로퍼티
        public ChartValues<int> Values { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> BarLabels { get; set; }       //string[]
        public Func<double, string> Formatter { get; set; }

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
        private ProductShowModel selectedProduct;
        public ProductShowModel SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                SelectedProductList.Clear(); // 이전에 담은 SelectedProduct를 리스트에서 지운다.
                selectedProduct = value;
                //OnPropertyChanged("SelectedProducts");
                //Message.Send(SelectedProducts);
                /*                Console.WriteLine("==선택한 재고 정보==");
                                Console.WriteLine($"  Prod_code : {SelectedProduct.Prod_code}");
                                Console.WriteLine($"  Prod_name : {SelectedProduct.Prod_name}");
                                Console.WriteLine($"  Category_name : {SelectedProduct.Category_name}");
                                Console.WriteLine($"  Prod_price : {SelectedProduct.Prod_price}");
                                Console.WriteLine($"  Imp_dept_count : {SelectedProduct.Imp_dept_count}");
                                Console.WriteLine($"  Prod_expire : {SelectedProduct.Prod_expire}");
                                Console.WriteLine($"  Prod_id : {SelectedProduct.Prod_id}");
                                Console.WriteLine($"  Imp_dept_id : {SelectedProduct.Imp_dept_id}");*/
                SelectedProductList.Add(selectedProduct);
                //Console.WriteLine(SelectedProductList[0].Prod_code);
            }
        }
        public List<ProductShowModel> SelectedProductList { get; set; } // SelectedProduct를 DataGrid에서 사용하기 위한 List
        // 재고 출고 - 선택한 출고 유형 콤보박스를 담을 값
        private string selectedOutType;
        public string SelectedOutType
        {
            get { return selectedOutType; }
            set
            {
                selectedOutType = value;
                OnPropertyChanged("SelectedOutType");
            }
        }

        // 재고 출고 - 선택한 출고(이관) 부서를 담을 프로퍼티
        private DeptModel selectedOutDept;
        public DeptModel SelectedOutDept
        {
            get { return selectedOutDept; }
            set
            {
                selectedOutDept = value;
                OnPropertyChanged("SelectedOutDept");
            }
        }

        // 재고 출고 - 입력한 출고 수량을 담을 프로퍼티
        private int? inputOutCount;
        public int? InputOutCount
        {
            get { return inputOutCount; }
            set
            {
                inputOutCount = value;
                OnPropertyChanged("InputOutCount");
            }
        }

        // 발주 신청 페이지 바인딩
        public UserModel SelectedUser { get; }
        public ICollectionView EmployeeCollection { get; private set; }

        public ProductShowViewModel()
        {
            Products = new ObservableCollection<ProductShowModel>();
            SearchTypeList = new[] { "제품코드", "제품명", "품목/종류" };
            SelectedSearchType = SearchTypeList[0];

            Depts = new ObservableCollection<DeptModel>(dept_dao.GetDepts());
            SelectedDept = Depts[(int)App.nurse_dto.Dept_id - 1];

            
            Categories = new ObservableCollection<CategoryModel>(category_dao.GetCategories());


            //App.xaml.cs 에 로그인할 때 바인딩 된 로그인 정보 객체
            Nurse = App.nurse_dto;
            SelectedProductList = new List<ProductShowModel>();
            DashboardPrint();  //대시보드 프린트

            SelectedUser = user_dao.GetUserInfoWithDept(Nurse);

            //EmployeeCollection = CollectionViewSource.GetDefaultView(LstEmPloyeeDetail);
            //employeeEntities = new EmployeeEntities();
            //employeeContext = new EmployeeContext();

            //LoadEmployee();
            //UpdateRecordCount();
            //EmployeeCollection.Filter = FilterByName;
        }//Constructor

        private ActionCommand nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                {
                    nextCommand = new ActionCommand(NextPage);
                }
                return nextCommand;
            }//get
        }

        private ActionCommand firstCommand;
        public ICommand FirstCommand
        {
            get
            {
                if (firstCommand == null)
                {
                    firstCommand = new ActionCommand(FirstPage);
                }
                return firstCommand;
            }//get
        }

        private ActionCommand lastCommand;
        public ICommand LastCommand
        {
            get
            {
                if (lastCommand == null)
                {
                    lastCommand = new ActionCommand(LastPage);
                }
                return lastCommand;
            }//get
        }

        private ActionCommand previouCommand;
        public ICommand PreviousCommand
        {
            get
            {
                if (previouCommand == null)
                {
                    previouCommand = new ActionCommand(PreviousPage);
                }
                return previouCommand;
            }//get
        }


        /*private ActionCommand command;
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

            LstOfRecords = new ObservableCollection<ProductShowModel>(product_dao.GetProductsByDept(SelectedDept));

            UpdateCollection(LstOfRecords.Take(SelectedRecord));
            UpdateRecordCount();

            ComboboxChanged = true;
        }*/


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
            Products = new ObservableCollection<ProductShowModel>(product_dao.GetProducts());

            //LstOfRecords = new ObservableCollection<ProductShowModel>(product_dao.SearchProducts(SelectedDept, SelectedSearchType, TextForSearch));
            //UpdateCollection(LstOfRecords.Take(SelectedRecord));
            //UpdateRecordCount();
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
            LstOfRecords = new ObservableCollection<ProductShowModel>(product_dao.SearchProducts(SelectedDept, SelectedSearchType, TextForSearch));
            UpdateCollection(LstOfRecords.Take(SelectedRecord));
            UpdateRecordCount();
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
            product_dao.OutProduct(InputOutCount, SelectedProduct, Nurse, SelectedOutType, SelectedOutDept);

            product_dao.ChangeProductInfo_IMP_DEPT_ForOut(InputOutCount, SelectedProduct);
            product_dao.ChangeProductInfo_ForOut(InputOutCount, SelectedProduct);

            SelectedOutType = null;
            SelectedOutDept = null;
            InputOutCount = null;

            Products = new ObservableCollection<ProductShowModel>(product_dao.GetProducts());
        }


        private ActionCommand modifyProductReset;
        public ICommand ModifyProductReset
        {
            get
            {
                if (modifyProductReset == null)
                {
                    modifyProductReset = new ActionCommand(modifyProductResetClick);
                }
                return modifyProductReset;
            }//get
        }

        public void modifyProductResetClick()
        {
            SelectedCategory = null;
        }



        private ActionCommand outProductReset;
        public ICommand OutProductReset
        {
            get
            {
                if (outProductReset == null)
                {
                    outProductReset = new ActionCommand(OutProductFormReset);
                }
                return outProductReset;
            }//get
        }

        public void OutProductFormReset()
        {
            SelectedOutType = null;
            SelectedOutDept = null;
            InputOutCount = null;
        }

        private void showListbyDept()
        {
            //Products = new ObservableCollection<ProductShowModel>();
            LoadEmployee();
            //if (SelectedDept == Depts[(int)App.nurse_dto.Dept_id - 1])
            //{
            //    Console.WriteLine("소속 부서");
            //    buttonColumnVisibility = Visibility.Visible;
            //    Console.WriteLine(buttonColumnVisibility);
            //}
            //else
            //{
            //    Console.WriteLine("다른 부서");
            //    buttonColumnVisibility = Visibility.Hidden;
            //    Console.WriteLine(buttonColumnVisibility);
            //}
        }

        public void DashboardPrint()                       //대시보드 출력(x축:제품code, y축:수량) 
        {
            SeriesCollection = new SeriesCollection();   //대시보드 틀


            ChartValues<int> name = new ChartValues<int> { };            //y축들어갈 임시 값

            if (product_dao.Prodcode_Info().Count != 0)
            {
                List<ProductShowModel> list1 = product_dao.Prodtotal_Info();      //y축출력
                                                                                  //foreach (var item in list1)
                                                                                  //{
                                                                                  //    name.Add((int)item.Prod_total);
                                                                                  //}
                for (int i = 0; i < 8; i++)
                {
                    //name.Add((int)list1[i].Prod_total);
                }
                

                Values = new ChartValues<int> { };
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "재고현황",   //+ i
                    Values = name,

                });

                BarLabels = new List<string>() { };                           //x축출력
                List<ProductShowModel> list = product_dao.Prodcode_Info();
                foreach (var item in list)
                {
                    BarLabels.Add(item.Prod_code);
                }

                Formatter = value => value.ToString("N");   //문자열 10진수 변환
            }

        }//DashboardPrint
        //*****************************************************************************
        //*****************************************************************************
        //여기서부터 paginaion 추가한 코드 내용

        

        private ObservableCollection<ProductShowModel> LstOfRecords;
        public void LoadEmployee() //Read details
        {

            //LstOfRecords.Add(empDetails);
            LstOfRecords = new ObservableCollection<ProductShowModel>(product_dao.GetProductsByDept(SelectedDept));

            UpdateCollection(LstOfRecords.Take(SelectedRecord));
            UpdateRecordCount();
        }
        int RecordStartFrom = 0;
        private void PreviousPage(object obj)
        {
            CurrentPage--;
            RecordStartFrom = LstOfRecords.Count - SelectedRecord * (NumberOfPages - (CurrentPage - 1));
            var recorsToShow = LstOfRecords.Skip(RecordStartFrom).Take(SelectedRecord);
            UpdateCollection(recorsToShow);
            UpdateEnableState();

        }
        private void LastPage(object obj)
        {
            //30 1->20, 2->10
            //last page - 21 -30
            //11-30=>30-20=10 -> 11-30

            //fix
            //20*(2-1)=20
            //skip = 20
            var recordsToskip = SelectedRecord * (NumberOfPages - 1);
            UpdateCollection(LstOfRecords.Skip(recordsToskip));
            CurrentPage = NumberOfPages;
            //MessageBox.Show(CurrentPage + "페이지");
            UpdateEnableState();
        }
        private void FirstPage(object obj)
        {
            UpdateCollection(LstOfRecords.Take(SelectedRecord));
            CurrentPage = 1;
            UpdateEnableState();
        }
        private void NextPage(object obj)
        {
            RecordStartFrom = CurrentPage * SelectedRecord;
            var recordsToShow = LstOfRecords.Skip(RecordStartFrom).Take(SelectedRecord);
            UpdateCollection(recordsToShow);
            CurrentPage++;
            UpdateEnableState();
        }

        private void UpdateCollection(IEnumerable<ProductShowModel> enumerable)
        {
            if(Products != null)
            {
                Products.Clear();
            }
            
            foreach (var item in enumerable)
            {
                Products.Add(item);
            }
        }
        private int _currentPage = 1;

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                UpdateEnableState();
            }
        }
        private void UpdateEnableState()
        {
            IsFirstEnabled = CurrentPage > 1;
            IsPreviousEnabled = CurrentPage > 1;
            IsNextEnabled = CurrentPage < NumberOfPages;
            IsLastEnabled = CurrentPage < NumberOfPages;
        }

        private int _numberOfPages = 10;

        public int NumberOfPages
        {
            get { return _numberOfPages; }
            set
            {
                _numberOfPages = value;
                OnPropertyChanged(nameof(NumberOfPages));
                UpdateEnableState();
            }
        }
        private bool _isFirstEnabled;

        public bool IsFirstEnabled
        {
            get { return _isFirstEnabled; }
            set
            {
                _isFirstEnabled = value;
                OnPropertyChanged(nameof(IsFirstEnabled));
            }
        }

        private bool _isPreviousEnabled;

        public bool IsPreviousEnabled
        {
            get { return _isPreviousEnabled; }
            set
            {
                _isPreviousEnabled = value;
                OnPropertyChanged(nameof(IsPreviousEnabled));
            }
        }
        private bool _isLastEnabled;

        public bool IsLastEnabled
        {
            get { return _isLastEnabled; }
            set
            {
                _isLastEnabled = value;
                OnPropertyChanged(nameof(IsLastEnabled));
            }
        }

        private bool _isNextEnabled;

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set
            {
                _isNextEnabled = value;
                OnPropertyChanged(nameof(IsNextEnabled));
            }
        }

        private int _selectedRecord = 10;

        public int SelectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;
                OnPropertyChanged(nameof(SelectedRecord));
                UpdateRecordCount();
            }
        }
        private void UpdateRecordCount()
        {
            NumberOfPages = (int)Math.Ceiling((double)LstOfRecords.Count / SelectedRecord);
            NumberOfPages = NumberOfPages == 0 ? 1 : NumberOfPages;
            UpdateCollection(LstOfRecords.Take(SelectedRecord));
            CurrentPage = 1;
        }




    }//class



}//namespace