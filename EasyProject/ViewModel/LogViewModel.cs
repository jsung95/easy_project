using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyProject.Model;
using EasyProject.Dao;
using log4net;
using System.Collections.ObjectModel;

namespace EasyProject.ViewModel
{
    public class LogViewModel : Notifier
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(App));

        LogDao log_dao = new LogDao();

        public LogViewModel()
        {
            log.Info("Constructor LogViewModel() invoked.");

            //로그 데이터 초기화
            Event_Logs = new ObservableCollection<LogModel>(log_dao.GetAllLogs());

        }//LogViewModel()

        #region EVENT_LOG

        //로그 데이터들을 담을 프로퍼티
        private ObservableCollection<LogModel> event_Logs;
        public ObservableCollection<LogModel> Event_Logs
        {
            get { return event_Logs; }
            set { event_Logs = value; OnPropertyChanged("Event_Logs"); }
        }

        


        #endregion





    }//class

}//namespace
