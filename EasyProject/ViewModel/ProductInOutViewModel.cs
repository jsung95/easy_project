using EasyProject.Dao;
using EasyProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

namespace EasyProject.ViewModel
{
    public class ProductInOutViewModel : Notifier
    {
        ProductDao dao = new ProductDao();

        //입고 내역을 담을 프로퍼티
        public ObservableCollection<ProductInOutModel> Product_in { get; set; }

        //출고 내역을 담을 프로퍼티
        public ObservableCollection<ProductInOutModel> Product_out { get; set; }




        public ProductInOutViewModel()
        {
            Product_in = new ObservableCollection<ProductInOutModel>(dao.GetProductIn());
            Product_out = new ObservableCollection<ProductInOutModel>(dao.GetProductOut());

        }//Constructor


        private ActionCommand command;
        public ICommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new ActionCommand(DoSomeThing);
                }
                return command;
            }//get

        }//Command

        public void DoSomeThing()
        {
            

        }// 


    }//class

}//namespace
