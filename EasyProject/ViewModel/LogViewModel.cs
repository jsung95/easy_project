using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyProject.Model;
using EasyProject.Dao;
using log4net;
using System.Collections.ObjectModel;
using Microsoft.Expression.Interactivity.Core;
using System.Windows.Input;

namespace EasyProject.ViewModel
{
    public class LogViewModel : Notifier
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(App));

        LogDao log_dao = new LogDao();

        //검색 유형 콤보박스 목록을 담을 프로퍼티
        public string[] SearchTypeList_Event_LOG { get; set; }
        public string[] SearchTypeList_LogIn_LOG { get; set; }
        public string[] SearchTypeList_LogOut_LOG { get; set; }

        public LogViewModel()
        {
            log.Info("Constructor LogViewModel() invoked.");

            //로그 데이터 초기화
            Event_Logs = new ObservableCollection<LogModel>();

            //EVENT 검색 유형 콤보박스 목록
            SearchTypeList_Event_LOG = new[] { "사번", "사용자명", "클래스", "메소드", "내용" };
            SelectedSearchType_Event_Log = SearchTypeList_Event_LOG[0]; // index 0번 item으로 초기화

            //LOGINOUT
            SearchTypeList_LogIn_LOG = new[] { "사용자명", "부서명", "IP주소" };
            SelectedSearchType_LogIn_Log = SearchTypeList_LogIn_LOG[0];



        }//LogViewModel()





        #region EVENT_LOG

        //검색 타입
        public string SelectedSearchType_Event_Log { get; set; }

        //검색 텍스트
        private string searchKeyword_Event_Log;
        public string SearchKeyword_Event_Log
        {
            get { return searchKeyword_Event_Log; }
            set 
            {
                searchKeyword_Event_Log = value; 
                OnPropertyChanged("SearchKeyword_Event_Log"); 
            }
        }



        //EventLog 시작날짜
        private DateTime? selectedStartDate_Event_Log;
        public DateTime? SelectedStartDate_Event_Log
        {
            get { return selectedStartDate_Event_Log; }
            set 
            { 
                selectedStartDate_Event_Log = value;
                SearchKeyword_Event_Log = null;
                GetEventLogs();

                OnPropertyChanged("SelectedStartDate_Event_Log"); 
            }
        }

        //EventLog 끝날짜
        private DateTime? selectedEndDate_Event_Log;
        public DateTime? SelectedEndDate_Event_Log
        {
            get { return selectedEndDate_Event_Log; }
            set 
            {
                selectedEndDate_Event_Log = value;
                SearchKeyword_Event_Log = null;
                GetEventLogs();

                OnPropertyChanged("SelectedEndDate_Event_Log"); 
            }
        }





        //로그 데이터들을 담을 프로퍼티
        private ObservableCollection<LogModel> event_Logs;
        public ObservableCollection<LogModel> Event_Logs
        {
            get { return event_Logs; }
            set 
            {
                event_Logs = value; 
                OnPropertyChanged("Event_Logs"); 
            }
        }

        private void GetEventLogs()
        {
            log.Info("GetEventLogs() invoked.");
            try
            {
                if(SelectedStartDate_Event_Log != null && SelectedEndDate_Event_Log != null)
                {
                    Event_Logs = new ObservableCollection<LogModel>(log_dao.GetAllLogs(SelectedStartDate_Event_Log, SelectedEndDate_Event_Log));
                }
            }//try
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }//catch
        }//GetEventLogs




        private ActionCommand searchEventLogsCommand;
        public ICommand SearchEventLogsCommand
        {
            get
            {
                if (searchEventLogsCommand == null)
                {
                    searchEventLogsCommand = new ActionCommand(GetSearchEvnetLogs);
                }
                return searchEventLogsCommand;
            }//get
        }

        private void GetSearchEvnetLogs()
        {
            log.Info("GetSearchEvnetLogs() invoked.");
            try
            {
                if (SelectedStartDate_Event_Log != null && SelectedEndDate_Event_Log != null)
                {
                    Event_Logs = new ObservableCollection<LogModel>(log_dao.Search_GetLogs(SelectedSearchType_Event_Log, SearchKeyword_Event_Log, SelectedStartDate_Event_Log, SelectedEndDate_Event_Log));
                }
            }//try
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }//catch
        }//GetSearchEvnetLogs

        #endregion

        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////

        #region LOGIN

        //검색 타입
        public string SelectedSearchType_LogIn_Log { get; set; }


        //검색 텍스트
        private string searchKeyword_LogIn_Log;
        public string SearchKeyword_LogIn_Log
        {
            get { return searchKeyword_LogIn_Log; }
            set
            {
                searchKeyword_LogIn_Log = value;
                OnPropertyChanged("SearchKeyword_LogIn_Log");
            }
        }


        //Loginout 시작날짜
        private DateTime? selectedStartDate_LogIn_Log;
        public DateTime? SelectedStartDate_LogIn_Log
        {
            get { return selectedStartDate_LogIn_Log; }
            set
            {
                selectedStartDate_LogIn_Log = value;
                SearchKeyword_LogIn_Log = null;
                GetLogInLogs();

                OnPropertyChanged("SelectedStartDate_LogIn_Log");
            }
        }

        //LogInOut 끝날짜
        private DateTime? selectedEndDate_LogIn_Log;
        public DateTime? SelectedEndDate_LogIn_Log
        {
            get { return selectedEndDate_LogIn_Log; }
            set
            {
                selectedEndDate_LogIn_Log = value;
                SearchKeyword_LogIn_Log = null;
                GetLogInLogs();

                OnPropertyChanged("SelectedEndDate_LogIn_Log");
            }
        }


        //로그 데이터들을 담을 프로퍼티
        private ObservableCollection<LogModel> logIn_Logs;
        public ObservableCollection<LogModel> LogIn_Logs
        {
            get { return logIn_Logs; }
            set
            {
                logIn_Logs = value;
                OnPropertyChanged("LogIn_Logs");
            }
        }


        private void GetLogInLogs()
        {
            log.Info("GetLogInLogs() invoked.");
            try
            {
                if (SelectedStartDate_LogIn_Log != null && SelectedEndDate_LogIn_Log != null)
                {
                    LogIn_Logs = new ObservableCollection<LogModel>(log_dao.GetLoginLogs(SelectedStartDate_LogIn_Log, SelectedEndDate_LogIn_Log));
                }
            }//try
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }//catch
        }//GetLogInLogs




        private ActionCommand searchLoginLogsCommand;
        public ICommand SearchLoginLogsCommand
        {
            get
            {
                if (searchLoginLogsCommand == null)
                {
                    searchLoginLogsCommand = new ActionCommand(GetSearchLoginLogs);
                }
                return searchLoginLogsCommand;
            }//get
        }

        private void GetSearchLoginLogs()
        {
            log.Info("GetSearchLoginLogs() invoked.");
            try
            {
                if (SelectedStartDate_LogIn_Log != null && SelectedEndDate_LogIn_Log != null)
                {
                    LogIn_Logs = new ObservableCollection<LogModel>(log_dao.GetLoginLogs(SelectedSearchType_LogIn_Log, SearchKeyword_LogIn_Log, SelectedStartDate_LogIn_Log, SelectedEndDate_LogIn_Log));
                }
            }//try
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }//catch
        }//GetSearchLoginLogs


        #endregion


        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////

        #region LOGOUT

        //검색 타입
        public string SelectedSearchType_LogOut_Log { get; set; }


        //검색 텍스트
        private string searchKeyword_LogOut_Log;
        public string SearchKeyword_LogOut_Log
        {
            get { return searchKeyword_LogOut_Log; }
            set
            {
                searchKeyword_LogOut_Log = value;
                OnPropertyChanged("SearchKeyword_LogOut_Log");
            }
        }


        //Loginout 시작날짜
        private DateTime? selectedStartDate_LogOut_Log;
        public DateTime? SelectedStartDate_LogOut_Log
        {
            get { return selectedStartDate_LogOut_Log; }
            set
            {
                selectedStartDate_LogOut_Log = value;
                SearchKeyword_LogOut_Log = null;


                OnPropertyChanged("SelectedStartDate_LogOut_Log");
            }
        }

        //LogInOut 끝날짜
        private DateTime? selectedEndDate_LogOut_Log;
        public DateTime? SelectedEndDate_LogOut_Log
        {
            get { return selectedEndDate_LogOut_Log; }
            set
            {
                selectedEndDate_LogOut_Log = value;
                SearchKeyword_LogOut_Log = null;


                OnPropertyChanged("SelectedEndDate_LogOut_Log");
            }
        }


        //로그 데이터들을 담을 프로퍼티
        private ObservableCollection<LogModel> logOut_Logs;
        public ObservableCollection<LogModel> LogOut_Logs
        {
            get { return logOut_Logs; }
            set
            {
                logOut_Logs = value;
                OnPropertyChanged("LogOut_Logs");
            }
        }


        private void GetLogOutLogs()
        {
            log.Info("GetLogOutLogs() invoked.");
            try
            {
                if (SelectedStartDate_LogOut_Log != null && SelectedEndDate_LogOut_Log != null)
                {
                    LogOut_Logs = new ObservableCollection<LogModel>(log_dao.GetLogOutLogs(SelectedStartDate_LogOut_Log, SelectedEndDate_LogOut_Log));
                }
            }//try
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }//catch
        }//GetLogOutLogs




        private ActionCommand searchLogOutLogsCommand;
        public ICommand SearchLogOutLogsCommand
        {
            get
            {
                if (searchLogOutLogsCommand == null)
                {
                    searchLogOutLogsCommand = new ActionCommand(GetSearchLogOutLogs);
                }
                return searchLogOutLogsCommand;
            }//get
        }

        private void GetSearchLogOutLogs()
        {
            log.Info("GetSearchLogOutLogs() invoked.");
            try
            {
                if (SelectedStartDate_LogOut_Log != null && SelectedEndDate_LogOut_Log != null)
                {
                    LogOut_Logs = new ObservableCollection<LogModel>(log_dao.GetLogOutLogs(SelectedSearchType_LogOut_Log, SearchKeyword_LogOut_Log, SelectedStartDate_LogOut_Log, SelectedEndDate_LogOut_Log));
                }
            }//try
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }//catch
        }//GetSearchLogOutLogs

        #endregion

        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        ///
        #region For XAML

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

        #endregion


    }//class

}//namespace
