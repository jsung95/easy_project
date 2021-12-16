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

        //재고 목록 조회해서 담을 옵저버블컬렉션 리스트 프로퍼티
        public ObservableCollection<ProductShowModel> Products { get; set; }

        //부서 목록 콤보박스, 부서 리스트 출력
        public ObservableCollection<DeptModel> Depts { get; set; }


        public ProductShowViewModel()
        {
            List<DeptModel> dept_list = dept_dao.GetDepts();
            Depts = new ObservableCollection<DeptModel>(dept_list);

            List<ProductShowModel> product_list = product_dao.GetProducts();
            Products = new ObservableCollection<ProductShowModel>(product_list);
        }


        private ActionCommand command;
        public ICommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new ActionCommand(DoSomething);
                }
                return command;
            }//get
        }

        public void DoSomething()
        {

        }

    }//class

        

}//namespace
