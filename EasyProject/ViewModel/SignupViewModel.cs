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
    public class SignupViewModel
    {
        SignupDao dao = new Dao.SignupDao();

        private ObservableCollection<DeptModel> depts; // depts = DeptModel 객체가 담긴 리스트
        public ObservableCollection<DeptModel> Depts 
        {
            get { return depts; }
            set { depts = value; }
        }

        public SignupViewModel()
        {
            List<DeptModel> list = dao.GetDeptModels("SELECT DEPT_NAME FROM DEPT");
            Depts = new ObservableCollection<DeptModel>(list); // List타입 객체 list를 OC 타입 Depts에 넣음 
        }

    } // SignupViewModel
} // namespace
