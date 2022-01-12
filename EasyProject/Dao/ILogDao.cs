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

        //전체 로그데이터 - 검색
        List<LogModel> Search_GetLogs(string search_type, string search_keyword); // - 단순 검색
        List<LogModel> Search_GetLogs(string search_type, string search_keyword, DateTime? start_date, DateTime? end_date); // - 날짜 검색


        //로그인 데이터
        List<LogModel> GetLoginLogs();

        //로그아웃 데이터
        List<LogModel> GetLogOutLogs();

        //로그인, 로그아웃 데이터
        List<LogModel> GetLogInOutLogs();

    }//interface

}//namespace
