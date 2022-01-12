using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyProject.Model;

namespace EasyProject.Dao
{
    public interface ILogDao
    {
        //전체 로그 데이터
        List<LogModel> GetAllLogs();
        List<LogModel> GetAllLogs(DateTime? start_date, DateTime? end_date); // - 날짜 검색

        //전체 로그데이터 - 검색
        List<LogModel> Search_GetLogs(string search_type, string search_keyword); // - 단순 검색
        List<LogModel> Search_GetLogs(string search_type, string search_keyword, DateTime? start_date, DateTime? end_date); // - 날짜 검색


        //로그인 데이터
        List<LogModel> GetLoginLogs();
        List<LogModel> GetLoginLogs(DateTime? start_date, DateTime? end_date);
        List<LogModel> GetLoginLogs(string search_type, string search_keyword, DateTime? start_date, DateTime? end_date);

        //로그아웃 데이터
        List<LogModel> GetLogOutLogs();
        List<LogModel> GetLogOutLogs(DateTime? start_date, DateTime? end_date);
        List<LogModel> GetLogOutLogs(string search_type, string search_keyword, DateTime? start_date, DateTime? end_date);


    }//interface

}//namespace
