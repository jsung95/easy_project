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
        ProductDao dao = new ProductDao();

        //재고 목록 조회해서 담을 옵저버블컬렉션 리스트 프로퍼티
        public ObservableCollection<ProductModel> Products { get; set; }


        public ProductShowViewModel()
        {
            List<ProductModel> list = dao.GetProduct();
            Products = new ObservableCollection<ProductModel>(list);
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
