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

        //대시보드 목록 콤보박스
        public ObservableCollection<CategoryModel> Category1 { get; set; }

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
                DashboardPrint2(selectedDept);
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
        //대시보드 목록 카테고리 프로퍼티
        private CategoryModel selectedCategory1;
        public CategoryModel SelectedCategory1
        {
            get { return selectedCategory1; }
            set
            {
                selectedCategory1 = value;
                OnPropertyChanged("SelectedCategory1");
                //DashboardPrint11(selectedDept11, selectedCategory11);

            }
        }
        // DashboardPrint() 그래프
        private SeriesCollection seriesCollection1;
        public SeriesCollection SeriesCollection1               //그래프 큰 틀 만드는거
        {
            get { return seriesCollection1; }
            set
            {
                seriesCollection1 = value;
                OnPropertyChanged("SeriesCollection1");
            }
        }

        private SeriesCollection seriesCollection2;
        public SeriesCollection SeriesCollection2               //부서별 카테고리//제품총수량 그래프 큰 틀 
        {
            get { return seriesCollection2; }
            set
            {
                seriesCollection2 = value;
                OnPropertyChanged("SeriesCollection2");
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
        public Func<double, string> Formatter1 { get; set; }

        
        //대시보드 동적 프로퍼티
        private ChartValues<int> values1;
        public ChartValues<int> Values1
        {
            get { return values1; }
            set
            {
                values1 = value;
                OnPropertyChanged("values1");

            }
        }
        private List<string> barLabels1;
        public List<string> BarLabels1
        {
            get { return barLabels1; }
            set
            {
                barLabels1 = value;
                OnPropertyChanged("barLabels1");

            }
        }
        public List<string> barLabels2 { get; set; }
        public List<string> BarLabels2
        {
            get { return barLabels2; }
            set
            {
                barLabels2 = value;
                OnPropertyChanged("barLabels2");

            }
        }

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
         

            SelectedUser = user_dao.GetUserInfoWithDept(Nurse);

            //EmployeeCollection = CollectionViewSource.GetDefaultView(LstEmPloyeeDetail);
            //employeeEntities = new EmployeeEntities();
            //employeeContext = new EmployeeContext();

            //LoadEmployee();
            //UpdateRecordCount();
            //EmployeeCollection.Filter = FilterByName;


            //11대시보드
            Category1 = new ObservableCollection<CategoryModel>(category_dao.GetCategoriesvalues());
            SelectedCategory1 = Category1[1];
            DashboardPrint1(SelectedDept, SelectedCategory1);
        }//Constructor
        public void DashboardPrint1(DeptModel selected_dept, CategoryModel selected_category)                       //대시보드 출력(x축:제품code, y축:수량) 
        {
            ChartValues<int> name = new ChartValues<int>();   //y축들어갈 임시 값
            Console.WriteLine("DashboardPrint11");
            SeriesCollection1 = new SeriesCollection();   //대시보드 틀
            //Console.WriteLine(selected.Dept_id); 
            List<ProductShowModel> list_xyz = product_dao.Get_Dept_Category_RemainExpire(selected_dept, selected_category);
            Console.WriteLine(selected_dept.Dept_name);
            Console.WriteLine(selected_category.Category_name);
            foreach (var item in list_xyz)
            {
                name.Add((int)item.Prod_remainexpire);
                Console.WriteLine("PROD_REMAINEXPIRE" + (int)item.Prod_remainexpire);
            }

            Values = new ChartValues<int> { };

            SeriesCollection1.Add(new RowSeries
            {
                Title = "총 수량",   //+ i
                Values = name,
                DataLabels = true,
                LabelPoint = point => point.X + "일 "
            });
            BarLabels1 = new List<string>() { };                           //x축출력
            foreach (var item in list_xyz)
            {
                BarLabels1.Add(item.Prod_code);
                Console.WriteLine("Prod_code" + item.Prod_code);
            }
            Formatter1 = value => value.ToString("N");   //문자열 10진수 변환
        }//dashboardprint4
        private ActionCommand command;
        public ICommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new ActionCommand(Dashprint);
                }
                return command;
            }//get

        }//Command

        public void Dashprint()
        {
            DashboardPrint1(SelectedDept, SelectedCategory1);
        }

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



        /*        private ActionCommand changeProductCommand;
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
                }*/

        public void ChangeProductInfo() //재고수정 확인 버튼 클릭 시에 동작하는 메소드
        {
            product_dao.ChangeProductInfo(SelectedProduct);
            product_dao.ChangeProductInfo_IMP_DEPT(SelectedProduct);
            Products = new ObservableCollection<ProductShowModel>(product_dao.GetProducts());
            //LstOfRecords = new ObservableCollection<ProductShowModel>(product_dao.SearchProducts(SelectedDept, SelectedSearchType, TextForSearch));
            //UpdateCollection(LstOfRecords.Take(SelectedRecord));
            //UpdateRecordCount();
        }


        // ==================== 스넥바 snackbar =======================
        //============================================================
        private bool isEmptyProduct = false;
        public bool IsEmptyProduct
        {
            get { return isEmptyProduct; }
            set
            {
                isEmptyProduct = value;
                OnPropertyChanged("IsEmptyProduct");
            }
        }

        private string errorProductString;
        public string ErrorProductString
        {
            get { return errorProductString; }
            set
            {
                errorProductString = value;
                OnPropertyChanged("ErrorProductString");
            }
        }

        private ActionCommand snackBarCommand;
        public ICommand SnackBarCommand
        {
            get
            {
                if (snackBarCommand == null)
                {
                    snackBarCommand = new ActionCommand(CloseSnackBar);
                }
                return snackBarCommand;
            }//get

        }//SnackBarCommand

        private void CloseSnackBar()
        {
            IsEmptyProduct = false;
        }
        //============================================================
        //============================================================


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


        /*        private ActionCommand outProductCommand;
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
                }*/

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
            if (Products != null)
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


        //부서별   카테고리//제품총수량 그래프
         public void DashboardPrint2(DeptModel selected)                       //대시보드 출력(x축:제품code, y축:수량) 
        {
            ChartValues<int> mount = new ChartValues<int>();   //y축들어갈 임시 값
            Console.WriteLine("DashboardPrint2");
            SeriesCollection2 = new SeriesCollection();   //대시보드 틀
            //Console.WriteLine(selected.Dept_id); 
            List<ImpDeptModel> list_xy = product_dao.Dept_Category_Mount(selected);
            Console.WriteLine(selected);
            //부서id별 제품code와 수량리스트
            //List<string> list_x = new List<string>();                                    //x축리스트
            //ChartValues<int> list_y = new ChartValues<int>();                          //y축리스트
            //foreach (var item in list_xy)
            //{
            //    list_x.Add((string)item.Prod_code);
            //    list_y.Add((int)item.Prod_total);
            //}
            //name을 2개선언 리스트

            //List<ProductShowModel> list1 = list_y;      //y축출력
            //List<ProductShowModel> list1 = product_dao.Prodtotal_Info();     
            foreach (var item in list_xy)
            {
                mount.Add((int)item.Imp_dept_count);
            }
            //for (int i = 0; i < 8; i++)
            //{
            //    name.Add((int)list_xy[i].Prod_total);
            //}
            Values = new ChartValues<int> { };

            SeriesCollection2.Add(new LineSeries
            {
                Title = "총 수량",   //+ i
                Values = mount,
            });
            BarLabels2 = new List<string>() { };                           //x축출력
            foreach (var item in list_xy)
            {
                BarLabels2.Add(item.Category_name);
            }
            Formatter = value => value.ToString("N");   //문자열 10진수 변환
        }//dashboardprint2


    }//class



}//namespace