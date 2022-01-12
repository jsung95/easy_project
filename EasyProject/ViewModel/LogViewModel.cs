using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyProject.Model;
using EasyProject.Dao;
using log4net;

namespace EasyProject.ViewModel
{
    public class LogViewModel : Notifier
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(App));

        LogDao dao = new LogDao();

        public LogViewModel()
        {
            log.Info("Constructor LogViewModel() invoked.");
        }//LogViewModel()



    }//class

}//namespace
