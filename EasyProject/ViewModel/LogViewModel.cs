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









        //그리드버튼 누르면 접혀지는 프로퍼티
        private bool isDataGridCheckBoxChecked = true;
        public bool IsDataGridCheckBoxChecked
        {
            get { return isDataGridCheckBoxChecked; }
            set
            {
                isDataGridCheckBoxChecked = value;

                OnPropertyChanged("IsDataGridCheckBoxChecked");
            }
        }

        //그래프버튼 누르면 접혀지는 프로퍼티
        private bool isGraphCheckBoxChecked = true;
        public bool IsGraphCheckBoxChecked
        {
            get { return isGraphCheckBoxChecked; }
            set
            {
                isGraphCheckBoxChecked = value;

                OnPropertyChanged("IsGraphCheckBoxChecked");
            }
        }
    }//class

}//namespace
