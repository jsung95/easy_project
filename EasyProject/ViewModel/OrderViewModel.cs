using EasyProject.Dao;
using EasyProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyProject.ViewModel
{
    public class OrderViewModel
    {

        OrderDao dao = new OrderDao();

        public List<DeptModel> Depts { get; set; }
       
        public DeptModel SelectedDept { get; set; } // 콤보박스에서 선택한 부서객체

        public OrderViewModel()
        {
            Depts = dao.GetDeptModels();
        }

    }//OrderViewModel
}//namespace
