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

namespace EasyProject.ViewModel
{
    public class ProductInOutViewModel : Notifier
    {
        ProductDao product_dao = new ProductDao();
        DeptDao dept_dao = new DeptDao();

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

        public ObservableCollection<DeptModel> Depts { get; set; }

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

        public string[] SearchTypeList { get; set; }

        //검색 텍스트 - 입고
        private string _searchKeyword_In;
        public string searchKeyword_In 
        {
            get { return _searchKeyword_In;}
            set { _searchKeyword_In = value; OnPropertyChanged("searchKeyword_In"); }
        }

        //검색 텍스트 - 출고
        private string _searchKeyword_Out;
        public string searchKeyword_Out 
        {
            get { return _searchKeyword_Out;}
            set { _searchKeyword_Out = value; OnPropertyChanged("searchKeyword_Out"); }
        }



        public string selectedSearchType_In { get; set; }
        public string selectedSearchType_Out { get; set; }


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
                if (selectedStartDate_In > selectedEndDate_In)
                {
                    SelectedStartDate_In = SelectedEndDate_In.Value.AddDays(-1);
                }
                OnPropertyChanged("SelectedEndDate_In");
            }
        }


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


        public ProductInOutViewModel()
        {
            
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


        }//Constructor




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
                searchKeyword_In = null;
            }
            else
            {
                MessageBox.Show("날짜를 모두 선택해주세요.");
                //Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept_In, selectedSearchType_In, searchKeyword_In));
            }

        }// InListSearch

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
                searchKeyword_Out = null;
            }
            else
            {
                MessageBox.Show("날짜를 모두 선택해주세요.");
            }

        }// InListSearch

        public void showInListByDept()
        {
            Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept_In));
           
        }//showInListByDept

        public void showOutListByDept()
        {
            Product_out = new ObservableCollection<ProductInOutModel>(product_dao.GetProductOut(SelectedDept_Out));
        
        }//showOutListByDept

        public void ShowProductIn_By_Date() // 입고 - 시작, 끝 날짜 지정해서 입고 데이터 조회
        {
            if (SelectedStartDate_In != null && SelectedEndDate_In != null)
            {
                Product_in = new ObservableCollection<ProductInOutModel>(product_dao.GetProductIn(SelectedDept_In, SelectedStartDate_In, SelectedEndDate_In));
            }
        }//ShowProductIn_By_Date

        public void ShowProductOut_By_Date() // 출고 - 시작, 끝 날짜 지정해서 입고 데이터 조회
        {
            if (SelectedStartDate_Out != null && SelectedEndDate_Out != null)
            {
                Product_out = new ObservableCollection<ProductInOutModel>(product_dao.GetProductOut(SelectedDept_Out, SelectedStartDate_Out, SelectedEndDate_Out));
            }
        }//ShowProductOut_By_Date


    }//class

}//namespace
