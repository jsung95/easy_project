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
        //private SeriesCollection seriesCollection1;
        //public SeriesCollection SeriesCollection1
        //{
        //    get { return seriesCollection1; }

        //    set
        //    {
        //        seriesCollection1 = value;
        //        OnPropertyChanged("SeriesCollection1");
        //    }
        //}
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

        public SeriesCollection SeriesCollection2               //그래프 큰 틀 만드는거
        {
            get { return seriesCollection2; }
            set
            {
                seriesCollection2 = value;
                OnPropertyChanged("SeriesCollection2");
            }
        }

        public List<string> BarLabels1 { get; set; }       //string[]
        public Func<double, string> Formatter1 { get; set; }

        public List<string> BarLabels2 { get; set; }       //string[]
        public Func<double, string> Formatter2 { get; set; }

        //private ActionCommand command;
        //public ICommand Command
        //{
        //    get
        //    {
        //        if (command == null)
        //        {
        //            command = new ActionCommand(GetProductsByDept);
        //        }
        //        return command;
        //    }//get
        //}
        //public void GetProductsByDept()
        //{
        //    Products = new ObservableCollection<ProductShowModel>(product_dao.GetProductsByDept(SelectedDept));
        //    ComboboxChanged = true;
        //}

        public DashBoardViewModel()
        {

            Depts = new ObservableCollection<DeptModel>(dept_dao.GetDepts());   //dept_od를 가져온다
            SelectedDept = Depts[(int)App.nurse_dto.Dept_id - 1];  // 
            category = new ObservableCollection<CategoryModel>(category_dao.GetCategories());
            //SelectedCategory = category[(int)App.category_dto.Category_id - 1];
            //DashboardPrint(selectedDept);
        }
        //public void DashboardPrint()                       //대시보드 출력(x축:제품code, y축:수량) 
        //{
        //    Console.WriteLine("DashboardPrint() !!");
        //    SeriesCollection1 = new SeriesCollection();   //대시보드 틀


        //    ChartValues<int> name = new ChartValues<int> { };            //y축들어갈 임시 값

        //    if (dashboard_dao.Prodcode_Info().Count != 0)
        //    {
        //        ObservableCollection<ProductShowModel> list1 = dashboard_dao.Prodtotal_Info();      //y축출력
        //                                                                                            //foreach (var item in list1)
        //                                                                                            //{
        //                                                                                            //    name.Add((int)item.Prod_total);
        //                                                                                            //}
        //        for (int i = 0; i < 8; i++)
        //        {
        //            name.Add((int)list1[i].Prod_total);
        //            Console.WriteLine("Prod_total: " + list1[i].Prod_total);
        //        }


        //        Values = new ChartValues<int> { };
        //        SeriesCollection1.Add(new ColumnSeries
        //        {
        //            Title = "재고현황",   //+ i
        //            Values = name,

        //        });

        //        BarLabels1 = new ObservableCollection<string>() { };                           //x축출력
        //        ObservableCollection<ProductShowModel> list = dashboard_dao.Prodcode_Info();
        //        foreach (var item in list)
        //        {
        //            BarLabels1.Add(item.Prod_code);
        //            Console.WriteLine("item.Prod_code : " + item.Prod_code);
        //        }

        //        Formatter1 = value => value.ToString("N");   //문자열 10진수 변환
        //    }

        //}
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




    }//class
}//namespace
