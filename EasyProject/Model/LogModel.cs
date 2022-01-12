using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyProject.Model
{
    public class LogModel : Notifier
    {
        public int? Log_no { get; set; }
        public string User_no { get; set; }
        public string User_name { get; set; }
        public string User_auth { get; set; }
        public string User_ip { get; set; }
        public string User_nation { get; set; }
        public DateTime? Log_date { get; set; }
        public string Log_level { get; set; }
        public string Log_class { get; set; }
        public string Log_method { get; set; }
        public string Message { get; set; }

    }//class

}//namespace
