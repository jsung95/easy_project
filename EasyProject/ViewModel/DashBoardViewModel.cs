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

namespace EasyProject.ViewModel
{
    public class DashBoardViewModel : Notifier
    {
        DeptDao dept_dao = new DeptDao();
        ProductDao product_dao = new ProductDao();
        DashBoardDao dashboard_dao = new DashBoardDao();
        CategoryDao category_dao = new CategoryDao();

        //부서 목록 콤보박스, 부서 대시보드 출력
        public ObservableCollection<DeptModel> Depts { get; set; }

        //선택한 부서를 담을 프로퍼티
        private DeptModel selectedDept;
        public DeptModel SelectedDept
        {
            get { return selectedDept; }
            set
            {
                selectedDept = value;
                DashboardPrint(selectedDept);
            }
        }

        //카테고리 목록 콤보박스, 카테고리 대시보드 출력
        public ObservableCollection<CategoryModel> category { get; set; }
        //선택할 카테고리를 담을 프로퍼티
        private CategoryModel selectedCategory;
        public CategoryModel SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                //DashboardPrint(SelectedCategory);
            }
        }

        // LiveChart 공통 프로퍼티
        public ChartValues<int> Values { get; set; }
        public List<string> BarLabels { get; set; }       //string[]
        public Func<double, string> Formatter { get; set; }

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
        //------------------------------------------------------------------------------------------------------------

        // 부서별 입고 유형 그래프 (기간 선택 가능) -----------------------------------    
        private SeriesCollection seriesCollection3;
        public SeriesCollection SeriesCollection3
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
        //------------------------------------------------------------------------------------------------------------------
           
        public DashBoardViewModel()
        {

            Depts = new ObservableCollection<DeptModel>(dept_dao.GetDepts());   //dept_od를 가져온다
            SelectedDept = Depts[(int)App.nurse_dto.Dept_id - 1];  // 
            category = new ObservableCollection<CategoryModel>(category_dao.GetCategories());

            //부서별 출고 유형별 빈도 그래프 (기간 선택 가능 * 초기 설정 : 현재날짜로부터 1주일)
            SelectedStartDate1 = DateTime.Today.AddDays(-7);
            SelectedEndDate1 = DateTime.Today;

            //부서별 입고 유형별 빈도 그래프 (기간 선택 가능 * 초기 설정 : 현재날짜로부터 1주일)
            SelectedStartDate2 = DateTime.Today.AddDays(-7);
            SelectedEndDate2 = DateTime.Today;

            //파이차트
            Depts_Pie = new ObservableCollection<DeptModel>(dept_dao.GetDepts());   //dept_od를 가져온다
            SelectedDept_Pie = Depts_Pie[(int)App.nurse_dto.Dept_id - 1];  // 
        }
        
        public void DashboardPrint(DeptModel selected)                       //대시보드 출력(x축:제품code, y축:수량) 
        {
            ChartValues<int> name = new ChartValues<int>();   //y축들어갈 임시 값
            Console.WriteLine("DashboardPrint");
            SeriesCollection1 = new SeriesCollection();   //대시보드 틀
            //Console.WriteLine(selected.Dept_id); 
            List<ProductShowModel> list_xy = dashboard_dao.Prodcodetotal_Info(selected);
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
                name.Add((int)item.Prod_total);
            }
            //for (int i = 0; i < 8; i++)
            //{
            //    name.Add((int)list_xy[i].Prod_total);
            //}
            Values = new ChartValues<int> { };

            SeriesCollection1.Add(new RowSeries
            {
                Title = "재고현황",   //+ i
                Values = name,
            });
            BarLabels = new List<string>() { };                           //x축출력
            foreach (var item in list_xy)
            {
                BarLabels.Add(item.Prod_code);
            }
            Formatter = value => value.ToString("N");   //문자열 10진수 변환
        }//dashboardprint

        // 부서별 출고 유형별 빈도 그래프 (기간 선택 가능) (VIEW : 좌측하단 위치)------------------------------------------------------------------------------------------------------------
        public void DashboardPrint2()                       
        {

            Console.WriteLine("DashboardPrint2");
            SeriesCollection2 = new SeriesCollection();
            Values = new ChartValues<int> { }; // 컬럼의 수치 ( y 축 )
            ChartValues<int> useCases = new ChartValues<int>(); // 사용 횟수를 담을 변수
            ChartValues<int> transferCases = new ChartValues<int>(); // 이관 횟수를 담을 변수
            ChartValues<int> discardCases = new ChartValues<int>(); // 폐기 횟수를 담을 변수
            BarLabels = new List<string>() { }; // 컬럼의 이름 ( x 축 )
            List<ProductInOutModel> datas = dashboard_dao.ReleaseCases_Info(SelectedStartDate1, SelectedEndDate1); // 부서별 출고 유형/횟수 정보
            foreach (var item in datas) // 부서명 Labels에 넣기
            {
                BarLabels.Add(item.Dept_name);
            }

            foreach (var item in datas)
            {
                useCases.Add((int)item.prod_use_cases);
                transferCases.Add((int)item.prod_transferOut_cases);
                discardCases.Add((int)item.prod_discard_cases);
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

        // 부서별 입고 유형별 빈도 그래프 (기간 선택 가능) (VIEW : 우측하단 위치)------------------------------------------------------------------------------------------------------------
        public void DashboardPrint3()                      
        {

            Console.WriteLine("DashboardPrint3");
            SeriesCollection3 = new SeriesCollection();
            Values = new ChartValues<int> { }; // 컬럼의 수치 ( y 축 )
            ChartValues<int> transferCases = new ChartValues<int>(); // 이관 횟수를 담을 변수
            ChartValues<int> orderCases = new ChartValues<int>(); // 발주 횟수를 담을 변수
            BarLabels = new List<string>() { }; // 컬럼의 이름 ( x 축 )
            List<ProductInOutModel> datas = dashboard_dao.incomingCases_Info(SelectedStartDate2, SelectedEndDate2); // 부서별 출고 유형/횟수 정보
            foreach (var item in datas) // 부서명 Labels에 넣기
            {
                BarLabels.Add(item.Dept_name);
            }

            foreach (var item in datas)
            {
                transferCases.Add((int)item.prod_transferIn_cases);
                orderCases.Add((int)item.prod_order_cases);
            }

            //adding series updates and animates the chart

            SeriesCollection3.Add(new StackedColumnSeries // 부서별 이관 횟수
            {
                Title = "이관 횟수",
                Values = transferCases,
                StackMode = StackMode.Values
            });

            SeriesCollection3.Add(new StackedColumnSeries // 부서별 출고 횟수
            {
                Title = "발주 횟수",
                Values = orderCases,
                StackMode = StackMode.Values
            });

            Formatter = value => value.ToString("N");   //문자열 10진수 변환
        }//dashboardprint3 ---------------------------------------------------------------------------------------------------




        #region 파이 차트
        //부서 목록 콤보박스, 부서 대시보드 출력
        public ObservableCollection<DeptModel> Depts_Pie { get; set; }

        //선택한 부서를 담을 프로퍼티
        private DeptModel selectedDept_Pie;
        public DeptModel SelectedDept_Pie
        {
            get { return selectedDept_Pie; }
            set
            {
                selectedDept_Pie = value;
                DashboardPrint_Pie();
            }
        }

        private SeriesCollection seriesCollection_Pie;
        public SeriesCollection SeriesCollection_Pie
        {
            get { return seriesCollection_Pie; }
            set
            {
                seriesCollection_Pie = value;
                OnPropertyChanged("SeriesCollection_Pie");
            }
        }

        public void DashboardPrint_Pie()
        {
            List<ProductInOutModel> list = dashboard_dao.GetDiscardTotalCount(SelectedDept_Pie);




            SeriesCollection_Pie = new SeriesCollection();


            foreach (var item in list)
            {
                //Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:C})", item.Prod_name, chartPoint.Participation);
                Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0:#,0}개 ({1:#,0}￦)", item.Prod_out_count, item.Prod_price);
                SeriesCollection_Pie.Add(new PieSeries
                {
                    Title = item.Prod_name,
                    Values = new ChartValues<int> { (int)item.Prod_out_count },
                    DataLabels = true,
                    LabelPoint = labelPoint
                });

            }//foreache



        }//DashboardPrint_Pie
        #endregion
    }//class
}//namespace
