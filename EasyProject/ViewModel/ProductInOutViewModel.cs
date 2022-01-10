using EasyProject.Dao;
using EasyProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using System.Linq;

namespace EasyProject.ViewModel
{
    public class ProductInOutViewModel : Notifier
    {
        ProductDao product_dao = new ProductDao();
        DeptDao dept_dao = new DeptDao();

        public string[] SearchTypeList { get; set; }


        //부서 리스트를 담을 프로퍼티
        public ObservableCollection<DeptModel> Depts { get; set; }

        private DeptModel selectedDept;
        public DeptModel SelectedDept
        {
            get { return selectedDept; }
            set
            {
                selectedDept = value;

                //부서 변경 시에
                //SearchKeyword_In = null; //검색 텍스트 초기화
                //SelectedStartDate_In = Convert.ToDateTime(product_dao.GetProductIn_MinDate(SelectedDept)); //날짜 컨트롤 최대, 최소 날짜로 설정
                //SelectedEndDate_In = Convert.ToDateTime(product_dao.GetProductIn_MaxDate(SelectedDept));
                
                OnPropertyChanged("SelectedDept");
                //getInListByDept();
            }
        }
        private ActionCommand deptChangedCommand;
        public ICommand DeptChangedCommand
        {
            get
            {
                if (deptChangedCommand == null)
                {
                    deptChangedCommand = new ActionCommand(DeptChanged);
                }
                return deptChangedCommand;
            }//get

        }//Command

        private void DeptChanged()
        {
            Console.WriteLine("DeptChanged!--------------------------------------------------------------------");
            SearchKeyword_In = null; //검색 텍스트 초기화
            searchKeyword_Out = null; //검색 텍스트 초기화
            SelectedStartDate_In = Convert.ToDateTime(product_dao.GetProductIn_MinDate(SelectedDept)); //날짜 컨트롤 최대, 최소 날짜로 설정
            SelectedEndDate_In = Convert.ToDateTime(product_dao.GetProductIn_MaxDate(SelectedDept));
            SelectedStartDate_Out = Convert.ToDateTime(product_dao.GetProductOut_MinDate(SelectedDept_Out));
            SelectedEndDate_Out = Convert.ToDateTime(product_dao.GetProductOut_MaxDate(SelectedDept_Out));

            getProductIn_By_Date();
            showOutListByDept();
        }

        public ProductInOutViewModel()
        {
            messagequeue = new SnackbarMessageQueue();

            SearchTypeList = new[] { "제품코드", "제품명", "품목/종류" };
            SelectedSearchType_In = SearchTypeList[0];

            Depts = new ObservableCollection<DeptModel>(dept_dao.GetDepts());
            SelectedDept = Depts[(int)App.nurse_dto.Dept_id - 1];

            selectedSearchType_Out = SearchTypeList[0];
            SelectedDept_Out = Depts[(int)App.nurse_dto.Dept_id - 1];

            //Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept));
            Product_in = new ObservableCollection<ProductInOutModel>();
            Product_out = new ObservableCollection<ProductInOutModel>(product_dao.GetProductOut(SelectedDept_Out));


            //날짜 컨트롤 부서별 해당 최소 날짜 및 최대 날짜로 초기화
            //입고
            SelectedStartDate_In = Convert.ToDateTime(product_dao.GetProductIn_MinDate(SelectedDept));
            SelectedEndDate_In = Convert.ToDateTime(product_dao.GetProductIn_MaxDate(SelectedDept));

            //출고
            SelectedStartDate_Out = Convert.ToDateTime(product_dao.GetProductOut_MinDate(SelectedDept_Out));
            SelectedEndDate_Out = Convert.ToDateTime(product_dao.GetProductOut_MaxDate(SelectedDept_Out));

            //UpdateCalendarBlackoutDates();
        }//Constructor


        #region 입고      
        ////입고 - 선택한 부서를 담을 프로퍼티
        //private DeptModel selectedDept_In;
        //public DeptModel SelectedDept_In
        //{
        //    get { return selectedDept_In; }
        //    set
        //    {
        //        selectedDept_In = value;

        //        //부서 변경 시에
        //        searchKeyword_In = null; //검색 텍스트 초기화
        //        SelectedStartDate_In = Convert.ToDateTime(product_dao.GetProductIn_MinDate(SelectedDept_In)); //날짜 컨트롤 최대, 최소 날짜로 설정
        //        SelectedEndDate_In = Convert.ToDateTime(product_dao.GetProductIn_MaxDate(SelectedDept_In));

        //        OnPropertyChanged("SelectedDept_In");
        //        showInListByDept();
        //    }
        //}

        //검색 텍스트 - 입고
        private string searchKeyword_In;
        public string SearchKeyword_In
        {
            get { return searchKeyword_In; }
            set { searchKeyword_In = value; OnPropertyChanged("SearchKeyword_In"); }
        }


        public string SelectedSearchType_In { get; set; }

        //입고 시작일을 담을 프로퍼티
        private DateTime? selectedStartDate_In;
        public DateTime? SelectedStartDate_In
        {
            get { return selectedStartDate_In; }
            set
            {
                selectedStartDate_In = value;

                //ShowProductIn_By_Date();
                
                Update_BlackOutDate_In_End();
                SearchKeyword_In = null;
                getProductIn_By_Date();
                if (selectedStartDate_In > selectedEndDate_In)
                {
                    SelectedStartDate_In = SelectedEndDate_In.Value.AddDays(-1);
                }
                OnPropertyChanged("SelectedStartDate_In");
            }
        }
        //종료일을 담을 프로퍼티
        private DateTime? selectedEndDate_In;
        public DateTime? SelectedEndDate_In
        {
            get { return selectedEndDate_In; }
            set
            {
                selectedEndDate_In = value;

                //ShowProductIn_By_Date();                
                Update_BlackOutDate_In_Start();
                SearchKeyword_In = null;
                getProductIn_By_Date();
                if (selectedStartDate_In > selectedEndDate_In)
                {
                    SelectedStartDate_In = SelectedEndDate_In.Value.AddDays(-1);
                }
                OnPropertyChanged("SelectedEndDate_In");                
            }
        }


        ////입고 - 검색 수행
        //private ActionCommand inSearchCommand;
        //public ICommand InSearchCommand
        //{
        //    get
        //    {
        //        if (inSearchCommand == null)
        //        {
        //            inSearchCommand = new ActionCommand(InListSearch);
        //        }
        //        return inSearchCommand;
        //    }//get

        //}//Command

        //public void InListSearch()
        //{
        //    if (SelectedStartDate_In != null && SelectedEndDate_In != null)
        //    {
        //        Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept_In, selectedSearchType_In, searchKeyword_In, SelectedStartDate_In, SelectedEndDate_In));
        //    }
        //    else
        //    {
        //        MessageQueue.Enqueue("날짜를 모두 선택해주세요.", "닫기", (x) => { IsInOutEnable = false; }, null, false, true, TimeSpan.FromMilliseconds(3000));
        //        IsInOutEnable = true;
        //        //Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept_In, selectedSearchType_In, searchKeyword_In));
        //    }

        //}// InListSearch


        //입고 - 부서별 입고 리스트
        //public void showInListByDept()
        //{
        //    Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept));

        //}//showInListByDept

        //// 입고 - 시작, 끝 날짜 지정해서 입고 데이터 조회
        //public void ShowProductIn_By_Date()
        //{
        //    if (SelectedStartDate_In != null && SelectedEndDate_In != null)
        //    {
        //        Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept, SelectedStartDate_In, SelectedEndDate_In));
        //    }
        //}//ShowProductIn_By_Date

        #endregion

        #region 입고 pagination
        //화면에 보여줄 리스트 입고 내역 담을 프로퍼티
        private ObservableCollection<ProductInOutModel> product_in;
        public ObservableCollection<ProductInOutModel> Product_in
        {
            get { return product_in; }
            set
            {
                product_in = value;
                OnPropertyChanged("Product_in");
            }
        }
        public IEnumerable<ProductInOutModel> searchedInProducts { get; set; } // 검색결과에 해당하는 객체들을 임시로 담아놓을 프로퍼티

        private ActionCommand inSearchKeywordCommand;
        public ICommand InSearchKeywordCommand
        {
            get
            {
                if (inSearchKeywordCommand == null)
                {
                    inSearchKeywordCommand = new ActionCommand(updateInSearchedProducts);
                }
                return inSearchKeywordCommand;
            }//get
        }

        public void updateInSearchedProducts()
        {

            Console.WriteLine("updateInSearchedProducts() 검색어 : " + SearchKeyword_In);

            InCurrentPage = 1; // 검색어 바뀔 때마다 1페이지로 이동


            if (SearchKeyword_In != null) // 키워드 있을 때
            {
                if (SelectedSearchType_In == "제품명")
                {
                    searchedInProducts = InLstOfRecords.Where(model => model.Prod_name.Contains(SearchKeyword_In) || model.Prod_name.Contains(SearchKeyword_In.ToUpper()) || model.Prod_name.Contains(SearchKeyword_In.ToLower()));
                    Console.WriteLine("제품명 : " + searchedInProducts.Count() + SearchKeyword_In.ToUpper());

                    UpdateInRecordCount();

                }
                else if (SelectedSearchType_In == "제품코드")
                {
                    searchedInProducts = InLstOfRecords.Where(model => model.Prod_code.Contains(SearchKeyword_In) || model.Prod_code.Contains(SearchKeyword_In.ToUpper()) || model.Prod_code.Contains(SearchKeyword_In.ToLower()));
                    Console.WriteLine("제품코드 : " + searchedInProducts.Count());

                    UpdateInRecordCount();
                }
                else // 품목/종류
                {
                    searchedInProducts = InLstOfRecords.Where(model => model.Category_name.Contains(SearchKeyword_In) || model.Category_name.Contains(SearchKeyword_In.ToUpper()) || model.Category_name.Contains(SearchKeyword_In.ToLower()));
                    Console.WriteLine("품목/종류 : " + searchedInProducts.Count());

                    UpdateInRecordCount();
                }

            }//if
            else // 키워드 없을 때
            {
                searchedInProducts = InLstOfRecords;
                Console.WriteLine("검색어없음: " + searchedInProducts.Count());

                UpdateInRecordCount();

            }//else

        }//updateInSearchedProducts

        public ObservableCollection<ProductInOutModel> InLstOfRecords { get; set; }

        public void getProductIn_By_Date()
        {
            if (SelectedStartDate_In != null && SelectedEndDate_In != null)
            {
                InLstOfRecords = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept, SelectedStartDate_In, SelectedEndDate_In));
                updateInSearchedProducts();
                UpdateInRecordCount();
            }
        }//getProductIn_By_Date

        public void getInListByDept()
        {
            InLstOfRecords = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept));
            updateInSearchedProducts();
            UpdateInRecordCount();

        }//getInListByDept

        int InRecordStartFrom = 0;
        private void InPreviousPage(object obj)
        {
            InCurrentPage--;
            InRecordStartFrom = searchedInProducts.Count() - InSelectedRecord * (InNumberOfPages - (InCurrentPage - 1));
            var recorsToShow = searchedInProducts.Skip(InRecordStartFrom).Take(InSelectedRecord);
            UpdateInCollection(recorsToShow);
            UpdateInEnableState();

        }

        private ActionCommand inNextCommand;
        public ICommand InNextCommand
        {
            get
            {
                if (inNextCommand == null)
                {
                    inNextCommand = new ActionCommand(InNextPage);
                }
                return inNextCommand;
            }//get
        }



        private ActionCommand infirstCommand;
        public ICommand InFirstCommand
        {
            get
            {
                if (infirstCommand == null)
                {
                    infirstCommand = new ActionCommand(InFirstPage);
                }
                return infirstCommand;
            }//get
        }

        private ActionCommand inLastCommand;
        public ICommand InLastCommand
        {
            get
            {
                if (inLastCommand == null)
                {
                    inLastCommand = new ActionCommand(InLastPage);
                }
                return inLastCommand;
            }//get
        }

        private ActionCommand inPreviouCommand;
        public ICommand InPreviousCommand
        {
            get
            {
                if (inPreviouCommand == null)
                {
                    inPreviouCommand = new ActionCommand(InPreviousPage);
                }
                return inPreviouCommand;
            }//get
        }
        private void InLastPage(object obj)
        {
            //30 1->20, 2->10
            //last page - 21 -30
            //11-30=>30-20=10 -> 11-30

            //fix
            //20*(2-1)=20
            //skip = 20
            var recordsToskip = InSelectedRecord * (InNumberOfPages - 1);
            UpdateInCollection(searchedInProducts.Skip(recordsToskip));
            InCurrentPage = InNumberOfPages;
            //MessageBox.Show(CurrentPage + "페이지");
            UpdateInEnableState();
        }
        private void InFirstPage(object obj)
        {
            UpdateInCollection(searchedInProducts.Take(InSelectedRecord));
            InCurrentPage = 1;
            UpdateInEnableState();
        }
        private void InNextPage(object obj)
        {
            InRecordStartFrom = InCurrentPage * InSelectedRecord;
            var recordsToShow = searchedInProducts.Skip(InRecordStartFrom).Take(InSelectedRecord);
            UpdateInCollection(recordsToShow);
            InCurrentPage++;
            UpdateInEnableState();
        }

        private void UpdateInCollection(IEnumerable<ProductInOutModel> enumerable)
        {
            if (Product_in != null)
            {
                Product_in.Clear();
            }
            else
            {
                Product_in = new ObservableCollection<ProductInOutModel>();
            }

            foreach (var item in enumerable)
            {
                Product_in.Add(item);
            }
        }
        private int inCurrentPage = 1;

        public int InCurrentPage
        {
            get { return inCurrentPage; }
            set
            {
                inCurrentPage = value;
                OnPropertyChanged(nameof(InCurrentPage));
                UpdateInEnableState();
            }
        }
        private void UpdateInEnableState()
        {
            IsInFirstEnabled = InCurrentPage > 1;
            IsInPreviousEnabled = InCurrentPage > 1;
            IsInNextEnabled = InCurrentPage < InNumberOfPages;
            IsInLastEnabled = InCurrentPage < InNumberOfPages;
        }

        private int inNumberOfPages = 10;

        public int InNumberOfPages
        {
            get { return inNumberOfPages; }
            set
            {
                inNumberOfPages = value;
                OnPropertyChanged(nameof(InNumberOfPages));
                UpdateInEnableState();
            }
        }
        private bool isInFirstEnabled;

        public bool IsInFirstEnabled
        {
            get { return isInFirstEnabled; }
            set
            {
                isInFirstEnabled = value;
                OnPropertyChanged(nameof(IsInFirstEnabled));
            }
        }

        private bool isInPreviousEnabled;

        public bool IsInPreviousEnabled
        {
            get { return isInPreviousEnabled; }
            set
            {
                isInPreviousEnabled = value;
                OnPropertyChanged(nameof(isInPreviousEnabled));
            }
        }
        private bool isInLastEnabled;

        public bool IsInLastEnabled
        {
            get { return isInLastEnabled; }
            set
            {
                isInLastEnabled = value;
                OnPropertyChanged(nameof(IsInLastEnabled));
            }
        }

        private bool isInNextEnabled;

        public bool IsInNextEnabled
        {
            get { return isInNextEnabled; }
            set
            {
                isInNextEnabled = value;
                OnPropertyChanged(nameof(IsInNextEnabled));
            }
        }

        private int inSelectedRecord = 10;

        public int InSelectedRecord
        {
            get { return inSelectedRecord; }
            set
            {
                inSelectedRecord = value;
                OnPropertyChanged(nameof(InSelectedRecord));
                UpdateInRecordCount();
            }
        }
        private void UpdateInRecordCount()
        {
            InNumberOfPages = (int)Math.Ceiling((double)searchedInProducts.Count() / InSelectedRecord);
            InNumberOfPages = InNumberOfPages == 0 ? 1 : InNumberOfPages;

            InRecordStartFrom = (InCurrentPage - 1) * InSelectedRecord;
            var recordsToShow = searchedInProducts.Skip(InRecordStartFrom).Take(InSelectedRecord);

            UpdateInCollection(recordsToShow);
        }
        #endregion

        #region 출고

        private ObservableCollection<ProductInOutModel> product_out;
        //출고 내역을 담을 프로퍼티
        public ObservableCollection<ProductInOutModel> Product_out
        {
            get { return product_out; }
            set
            {
                product_out = value;
                OnPropertyChanged("Product_out");
            }
        }

        //출고 - 선택한 부서를 담을 프로퍼티
        private DeptModel selectedDept_Out;
        public DeptModel SelectedDept_Out
        {
            get { return selectedDept_Out; }
            set
            {
                selectedDept_Out = value;

                //부서 변경 시에 
                searchKeyword_Out = null; // 검색 텍스트 초기화
                SelectedStartDate_Out = Convert.ToDateTime(product_dao.GetProductOut_MinDate(SelectedDept_Out)); //날짜 컨트롤 최대, 최소 날짜로 설정
                SelectedEndDate_Out = Convert.ToDateTime(product_dao.GetProductOut_MaxDate(SelectedDept_Out));

                OnPropertyChanged("SelectedDept_Out");
                showOutListByDept();
            }
        }

        //검색 텍스트 - 출고
        private string _searchKeyword_Out;
        public string searchKeyword_Out
        {
            get { return _searchKeyword_Out; }
            set { _searchKeyword_Out = value; OnPropertyChanged("searchKeyword_Out"); }
        }

        public string selectedSearchType_Out { get; set; }


        //출고
        //시작일을 담을 프로퍼티
        private DateTime? selectedStartDate_Out;
        public DateTime? SelectedStartDate_Out
        {
            get { return selectedStartDate_Out; }
            set
            {
                selectedStartDate_Out = value;

                ShowProductOut_By_Date();
                if (selectedStartDate_Out > selectedEndDate_Out)
                {
                    SelectedStartDate_Out = SelectedEndDate_Out.Value.AddDays(-1);
                }
                OnPropertyChanged("SelectedStartDate_Out");
            }
        }
        //종료일을 담을 프로퍼티
        private DateTime? selectedEndDate_Out;
        public DateTime? SelectedEndDate_Out
        {
            get { return selectedEndDate_Out; }
            set
            {
                selectedEndDate_Out = value;

                ShowProductOut_By_Date();
                if (selectedStartDate_Out > selectedEndDate_Out)
                {
                    SelectedStartDate_Out = SelectedEndDate_Out.Value.AddDays(-1);
                }
                OnPropertyChanged("SelectedEndDate_Out");
            }
        }

        //출고- 검색 수행
        private ActionCommand outSearchCommand;
        public ICommand OutSearchCommand
        {
            get
            {
                if (outSearchCommand == null)
                {
                    outSearchCommand = new ActionCommand(OutListSearch);
                }
                return outSearchCommand;
            }//get

        }//Command

        public void OutListSearch()
        {
            if (SelectedStartDate_Out != null && SelectedEndDate_Out != null)
            {
                Product_out = new ObservableCollection<ProductInOutModel>(product_dao.GetProductOut(SelectedDept_Out, selectedSearchType_Out, searchKeyword_Out, SelectedStartDate_Out, SelectedEndDate_Out));
            }
            else
            {
                MessageQueue.Enqueue("날짜를 모두 선택해주세요.", "닫기", (x) => { IsInOutEnable = false; }, null, false, true, TimeSpan.FromMilliseconds(3000));
                IsInOutEnable = true;
            }

        }// OutListSearch


        //출고 - 부서별 출고 리스트
        public void showOutListByDept()
        {
            Product_out = new ObservableCollection<ProductInOutModel>(product_dao.GetProductOut(SelectedDept_Out));

        }//showOutListByDept

        // 출고 - 시작, 끝 날짜 지정해서 입고 데이터 조회
        public void ShowProductOut_By_Date()
        {
            if (SelectedStartDate_Out != null && SelectedEndDate_Out != null)
            {
                Product_out = new ObservableCollection<ProductInOutModel>(product_dao.GetProductOut(SelectedDept_Out, SelectedStartDate_Out, SelectedEndDate_Out));
            }
        }//ShowProductOut_By_Date


        #endregion

        #region 스넥바
        //스넥바 
        private bool isInOutEnable = false;
        public bool IsInOutEnable
        {
            get { return isInOutEnable; }
            set
            {
                isInOutEnable = value;
                OnPropertyChanged("IsInOutEnable");
            }
        }

        //스넥바 메세지큐
        private SnackbarMessageQueue messagequeue;
        public SnackbarMessageQueue MessageQueue
        {
            get { return messagequeue; }
            set
            {
                messagequeue = value;
                OnPropertyChanged("MessageQueue");
            }
        }
        #endregion

        



        private CalendarBlackoutDatesCollection blackOutDates_In_Start;
        public CalendarBlackoutDatesCollection BlackOutDates_In_Start
        {
            get { return blackOutDates_In_Start; }
            set
            {
                blackOutDates_In_Start = value;
                OnPropertyChanged("BlackOutDates_In_Start");
            }
        }

        private CalendarBlackoutDatesCollection blackOutDates_In_End;
        public CalendarBlackoutDatesCollection BlackOutDates_In_End
        {
            get { return blackOutDates_In_End; }
            set
            {
                blackOutDates_In_End = value;
                OnPropertyChanged("BlackOutDates_In_End");
            }
        }


        private void Update_BlackOutDate_In_Start()
        {
            
            CalendarDateRange start = new CalendarDateRange(new DateTime(SelectedEndDate_In.Value.Year, SelectedEndDate_In.Value.Month, SelectedEndDate_In.Value.Day+1), new DateTime(9999, 12, 31));


            // Because we can't reach the real calendar from the viewmodel, and we can't create a
            // new CalendarBlackoutDatesCollection without specifying a Calendar to
            // the constructor, we provide a "Dummy calendar", only to satisfy
            // the CalendarBlackoutDatesCollection...
            // because you can't do: BlackoutDates = new CalendarBlackoutDatesCollection().


            Calendar dummyCal = new Calendar();
            BlackOutDates_In_Start = new CalendarBlackoutDatesCollection(dummyCal);

            // Add the dateranges to the BlackOutDates property

            if(BlackOutDates_In_Start != null)
            {
                BlackOutDates_In_Start.Clear();
            }
            BlackOutDates_In_Start.Add(start);
        }

        private void Update_BlackOutDate_In_End()
        {

            CalendarDateRange end = new CalendarDateRange(new DateTime(0001, 01, 01), new DateTime(SelectedStartDate_In.Value.Year, SelectedStartDate_In.Value.Month, SelectedStartDate_In.Value.Day-1));

            Calendar dummyCal = new Calendar();
            BlackOutDates_In_End = new CalendarBlackoutDatesCollection(dummyCal);

            if(blackOutDates_In_End != null)
            {
                blackOutDates_In_End.Clear();
            }
            BlackOutDates_In_End.Add(end);
        }

    }//class

}//namespace
