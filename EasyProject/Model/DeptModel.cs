using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyProject.Model
{
    public class DeptModel : Notifier
    {
        private string dept_name;
        public string Dept_name
        {
            get { return dept_name; }
            set
            {
                dept_name = value;
                //OnPropertyChanged("Dept_name");
            }
        } // Dept_name
    } // DeptModel
} // namespace
