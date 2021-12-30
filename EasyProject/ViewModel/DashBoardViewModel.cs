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


        // 대시보드 프로퍼티
        public ChartValues<int> Values { get; set; }
        private SeriesCollection seriesCollection1;
        private SeriesCollection seriesCollection2;

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
        public SeriesCollection SeriesCollection2               
        {
            get { return seriesCollection2; }
            set
            {
                seriesCollection2 = value;
                OnPropertyChanged("SeriesCollection2");
            }
        }

        public DateTime selectedStartDate;
        public DateTime SelectedStartDate
        {
            get { return selectedStartDate; }
            set
            {
                selectedStartDate = value;
                OnPropertyChanged("SelectedStartDate");
                DashboardPrint2();
            }
        }
        public DateTime selectedEndDate;
        public DateTime SelectedEndDate
        {
            get { return selectedEndDate; }
            set
            {
                selectedEndDate = value;
                OnPropertyChanged("SelectedEndDate");
                DashboardPrint2();
            }
        }
        public List<string> BarLabels2 { get; set; }       //string[] : 컬럼명 
        public Func<double, string> Formatter2 { get; set; }
        //부서별 출고 유형 그래프 (기간 선택 가능) 끝----------------------------------------------------------------------


        public List<string> BarLabels1 { get; set; }       //string[]
        public Func<double, string> Formatter1 { get; set; }

       
        

        public DashBoardViewModel()
        {

            Depts = new ObservableCollection<DeptModel>(dept_dao.GetDepts());   //dept_od를 가져온다
            SelectedDept = Depts[(int)App.nurse_dto.Dept_id - 1];  // 
            category = new ObservableCollection<CategoryModel>(category_dao.GetCategories());

            //부서별 출고 유형 그래프 (기간 선택 가능)
            SelectedStartDate = DateTime.Today.AddDays(-7);
            SelectedEndDate = DateTime.Today;
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
            BarLabels1 = new List<string>() { };                           //x축출력
            foreach (var item in list_xy)
            {
                BarLabels1.Add(item.Prod_code);
            }
            Formatter1 = value => value.ToString("N");   //문자열 10진수 변환
        }//dashboardprint

        // 부서별 출고 유형 그래프 (기간 선택 가능) (VIEW : 좌측하단 위치)------------------------------------------------------------------------------------------------------------
        public void DashboardPrint2()                       //대시보드 출력(x축:제품code, y축:수량) 
        {

            Console.WriteLine("DashboardPrint2");
            SeriesCollection2 = new SeriesCollection();
            Values = new ChartValues<int> { }; // 컬럼의 수치 ( y 축 )
            ChartValues<int> useCases = new ChartValues<int>(); // 사용 횟수를 담을 변수
            ChartValues<int> transferCases = new ChartValues<int>(); // 이관 횟수를 담을 변수
            ChartValues<int> discardCases = new ChartValues<int>(); // 폐기 횟수를 담을 변수
            BarLabels2 = new List<string>() { }; // 컬럼의 이름 ( x 축 )
            List<ProductInOutModel> datas = dashboard_dao.ReleaseCases_Info(SelectedStartDate, SelectedEndDate); // 부서별 출고 유형/횟수 정보
            foreach (var item in datas) // 부서명 Labels에 넣기
            {
                BarLabels2.Add(item.Dept_name);
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

            Formatter2 = value => value.ToString("N");   //문자열 10진수 변환
        }//dashboardprint2 ---------------------------------------------------------------------------------------------------


    }//class
}//namespace
