﻿using EasyProject.Dao;
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
using LiveCharts;
using LiveCharts.Wpf;

namespace EasyProject.ViewModel
{
    public class ProductInOutViewModel : Notifier
    {
                                                                                                                                                                                                                            
        ProductDao product_dao = new ProductDao();
        DeptDao dept_dao = new DeptDao();

        public string[] SearchTypeList { get; set; }

        //대시보드 프로퍼티
        public List<string> BarLabels2 { get; set; }       //string[]
        public List<string> BarLabels3 { get; set; }       //string[]
        public ChartValues<int> Values2 { get; set; }
        public ChartValues<int> Values3 { get; set; }
        public Func<double, string> Formatter { get; set; }

        // DashboardPrint() 그래프
        // 부서별 출고 유형 그래프 (기간 선택 가능) -----------------------------------
        private SeriesCollection seriesCollection2;
        public SeriesCollection SeriesCollection2
        {
            get { return seriesCollection2; }
            set
            {
                seriesCollection2 = value;
                OnPropertyChanged("SeriesCollection2");
            }
        }

        public DateTime selectedStartDate1;
        public DateTime SelectedStartDate1
        {
            get { return selectedStartDate1; }
            set
            {
                selectedStartDate1 = value;
                OnPropertyChanged("SelectedStartDate1");
                DashboardPrint2();
            }
        }
        public DateTime selectedEndDate1;
        public DateTime SelectedEndDate1
        {
            get { return selectedEndDate1; }
            set
            {
                selectedEndDate1 = value;
                OnPropertyChanged("SelectedEndDate1");
                DashboardPrint2();
            }
        }

        //부서별 입고 유형 그래프
        private SeriesCollection seriesCollection3;
        public SeriesCollection SeriesCollection3               //그래프 큰 틀 만드는거
        {
            get { return seriesCollection3; }
            set
            {
                seriesCollection3 = value;
                OnPropertyChanged("SeriesCollection3");
            }
        }
        public DateTime selectedStartDate2;
        public DateTime SelectedStartDate2
        {
            get { return selectedStartDate2; }
            set
            {
                selectedStartDate2 = value;
                OnPropertyChanged("SelectedStartDate2");
                DashboardPrint3();
            }
        }
        public DateTime selectedEndDate2;
        public DateTime SelectedEndDate2
        {
            get { return selectedEndDate2; }
            set
            {
                selectedEndDate2 = value;
                OnPropertyChanged("SelectedEndDate2");
                DashboardPrint3();
            }
        }

        //부서 리스트를 담을 프로퍼티
        public ObservableCollection<DeptModel> Depts { get; set; }

        public ProductInOutViewModel()
        {
            messagequeue = new SnackbarMessageQueue();

            SearchTypeList = new[] { "제품코드", "제품명", "품목/종류" };
            selectedSearchType_In = SearchTypeList[0];

            Depts = new ObservableCollection<DeptModel>(dept_dao.GetDepts());
            SelectedDept_In = Depts[(int)App.nurse_dto.Dept_id - 1];

            selectedSearchType_Out = SearchTypeList[0];
            SelectedDept_Out = Depts[(int)App.nurse_dto.Dept_id - 1];

            Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept_In));
            Product_out = new ObservableCollection<ProductInOutModel>(product_dao.GetProductOut(SelectedDept_Out));


            //날짜 컨트롤 부서별 해당 최소 날짜 및 최대 날짜로 초기화
            //입고
            SelectedStartDate_In = Convert.ToDateTime(product_dao.GetProductIn_MinDate(SelectedDept_In));
            SelectedEndDate_In = Convert.ToDateTime(product_dao.GetProductIn_MaxDate(SelectedDept_In));

            //출고
            SelectedStartDate_Out = Convert.ToDateTime(product_dao.GetProductOut_MinDate(SelectedDept_Out));
            SelectedEndDate_Out = Convert.ToDateTime(product_dao.GetProductOut_MaxDate(SelectedDept_Out));

            //부서별 입고 유형별 빈도 그래프 (기간 선택 가능 * 초기 설정 : 현재날짜로부터 1주일)
            SelectedStartDate2 = DateTime.Today.AddDays(-7);
            SelectedEndDate2 = DateTime.Today;

            //부서별 출고 유형별 빈도 그래프 (기간 선택 가능 * 초기 설정 : 현재날짜로부터 1주일)
            SelectedStartDate1 = DateTime.Today.AddDays(-7);
            SelectedEndDate1 = DateTime.Today;


            //UpdateCalendarBlackoutDates();
        }//Constructor


        #region 입고

        //입고 내역을 담을 프로퍼티
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

        //입고 - 선택한 부서를 담을 프로퍼티
        private DeptModel selectedDept_In;
        public DeptModel SelectedDept_In
        {
            get { return selectedDept_In; }
            set
            {
                selectedDept_In = value;

                //부서 변경 시에
                searchKeyword_In = null; //검색 텍스트 초기화
                SelectedStartDate_In = Convert.ToDateTime(product_dao.GetProductIn_MinDate(SelectedDept_In)); //날짜 컨트롤 최대, 최소 날짜로 설정
                SelectedEndDate_In = Convert.ToDateTime(product_dao.GetProductIn_MaxDate(SelectedDept_In));

                OnPropertyChanged("SelectedDept_In");
                showInListByDept();
            }
        }

        //검색 텍스트 - 입고
        private string _searchKeyword_In;
        public string searchKeyword_In
        {
            get { return _searchKeyword_In; }
            set { _searchKeyword_In = value; OnPropertyChanged("searchKeyword_In"); }
        }


        public string selectedSearchType_In { get; set; }


        //입고
        //시작일을 담을 프로퍼티
        private DateTime? selectedStartDate_In;
        public DateTime? SelectedStartDate_In
        {
            get { return selectedStartDate_In; }
            set
            {
                selectedStartDate_In = value;

                ShowProductIn_By_Date();
                Update_BlackOutDate_In_End();
                searchKeyword_In = null;
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

                ShowProductIn_By_Date();
                Update_BlackOutDate_In_Start();
                searchKeyword_In = null;
                if (selectedStartDate_In > selectedEndDate_In)
                {
                    SelectedStartDate_In = SelectedEndDate_In.Value.AddDays(-1);
                }
                OnPropertyChanged("SelectedEndDate_In");
            }
        }


        //입고 - 검색 수행
        private ActionCommand inSearchCommand;
        public ICommand InSearchCommand
        {
            get
            {
                if (inSearchCommand == null)
                {
                    inSearchCommand = new ActionCommand(InListSearch);
                }
                return inSearchCommand;
            }//get

        }//Command

        public void InListSearch()
        {
            if (SelectedStartDate_In != null && SelectedEndDate_In != null)
            {
                Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept_In, selectedSearchType_In, searchKeyword_In, SelectedStartDate_In, SelectedEndDate_In));
            }
            else
            {
                MessageQueue.Enqueue("날짜를 모두 선택해주세요.", "닫기", (x) => { IsInOutEnable = false; }, null, false, true, TimeSpan.FromMilliseconds(3000));
                IsInOutEnable = true;
                //Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept_In, selectedSearchType_In, searchKeyword_In));
            }

        }// InListSearch


        //입고 - 부서별 입고 리스트
        public void showInListByDept()
        {
            Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept_In));

        }//showInListByDept

        // 입고 - 시작, 끝 날짜 지정해서 입고 데이터 조회
        public void ShowProductIn_By_Date()
        {
            if (SelectedStartDate_In != null && SelectedEndDate_In != null)
            {
                Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept_In, SelectedStartDate_In, SelectedEndDate_In));
            }
        }//ShowProductIn_By_Date

        #endregion

        #region 입고 pagination

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

        // 부서별 출고 유형별 빈도 그래프 (기간 선택 가능) (VIEW : 좌측하단 위치)------------------------------------------------------------------------------------------------------------
        public void DashboardPrint2()
        {

            Console.WriteLine("DashboardPrint2");
            SeriesCollection2 = new SeriesCollection();
            Values2 = new ChartValues<int> { }; // 컬럼의 수치 ( y 축 )
            ChartValues<int> useCases = new ChartValues<int>(); // 사용 횟수를 담을 변수
            ChartValues<int> transferCases = new ChartValues<int>(); // 이관 횟수를 담을 변수
            ChartValues<int> discardCases = new ChartValues<int>(); // 폐기 횟수를 담을 변수
            BarLabels2 = new List<string>() { }; // 컬럼의 이름 ( x 축 )
            List<ProductInOutModel> datas = product_dao.ReleaseCases_Info(SelectedStartDate1, SelectedEndDate1); // 부서별 출고 유형/횟수 정보
            foreach (var item in datas) // 부서명 Labels에 넣기
            {
                BarLabels2.Add(item.Dept_name);
                Console.WriteLine("dashboard2_dept_name" + item.Dept_name);
            }

            foreach (var item in datas)
            {
                useCases.Add((int)item.prod_use_cases);
                Console.WriteLine("prod_use_cases" + item.prod_use_cases);
                transferCases.Add((int)item.prod_transferOut_cases);
                Console.WriteLine("prod_tarnsferout" + item.prod_transferOut_cases);
                discardCases.Add((int)item.prod_discard_cases);
                Console.WriteLine("prod_discard_cases" + item.prod_discard_cases);
            }

            //adding series updates and animates the chart

            SeriesCollection2.Add(new StackedColumnSeries // 부서별 사용 횟수
            {
                Title = "사용 횟수",
                Values = useCases,
                StackMode = StackMode.Values
            });

            SeriesCollection2.Add(new StackedColumnSeries // 부서별 이관 횟수
            {
                Title = "이관 횟수",
                Values = transferCases,
                StackMode = StackMode.Values
            });

            SeriesCollection2.Add(new StackedColumnSeries // 부서별 출고 횟수
            {
                Title = "폐기 횟수",
                Values = discardCases,
                StackMode = StackMode.Values
            });

            Formatter = value => value.ToString("N");   //문자열 10진수 변환
        }//dashboardprint2 ---------------------------------------------------------------------------------------------------
         
        // 부서별 입고 유형별 빈도 그래프 (기간 선택 가능) (VIEW : 좌측하단 위치)---------------------
        public void DashboardPrint3()
        {

            Console.WriteLine("DashboardPrint3");
            SeriesCollection3 = new SeriesCollection();
            Values3 = new ChartValues<int> { }; // 컬럼의 수치 ( y 축 )
            ChartValues<int> transferCases = new ChartValues<int>(); // 이관 횟수를 담을 변수
            ChartValues<int> orderCases = new ChartValues<int>(); // 신규 횟수를 담을 변수
            ChartValues<int> addCases = new ChartValues<int>(); // 추가 횟수를 담을 변수
            BarLabels3 = new List<string>() { }; // 컬럼의 이름 ( x 축 )
            List<ProductInOutModel> datas = product_dao.incomingCases_Info(SelectedStartDate2, SelectedEndDate2); // 부서별 출고 유형/횟수 정보
            foreach (var item in datas) // 부서명 Labels에 넣기
            {
                BarLabels3.Add(item.Dept_name);
                Console.WriteLine("dashboard3_dept_name" + item.Dept_name);
            }

            foreach (var item in datas)
            {
                transferCases.Add((int)item.prod_transferIn_cases);
                orderCases.Add((int)item.prod_order_cases);
                addCases.Add((int)item.prod_add_cases);
            }

            //adding series updates and animates the chart

            SeriesCollection3.Add(new StackedColumnSeries // 부서별 이관 횟수
            {
                Title = "이관입고 횟수",
                Values = transferCases,
                StackMode = StackMode.Values
            });

            SeriesCollection3.Add(new StackedColumnSeries // 부서별 신규입고 횟수
            {
                Title = "신규입고 횟수",
                Values = orderCases,
                StackMode = StackMode.Values
            });

            SeriesCollection3.Add(new StackedColumnSeries // 부서별 추가입고 횟수
            {
                Title = "추가입고 횟수",
                Values = addCases,
                StackMode = StackMode.Values
            });

            Formatter = value => value.ToString("N");   //문자열 10진수 변환
        }//dashboardprint3 ---------------------------------------------------------------------------------------------------


    }//class



}//namespace
