using EasyProject.Dao;
using EasyProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

namespace EasyProject.ViewModel
{
    public class UserAuthViewModel : Notifier
    {
        UsersDao dao = new UsersDao();
        // 콤보박스의 권한 리스트
        public string[] AuthList { get; set; }
        //검색 텍스트박스로 부터 입력받은 데이터를 담을 프로퍼티
        public string Keyword1 { get; set; }
        public string Keyword2 { get; set; }

        // 콤보박스에서 선택한 권한을 담을 프로퍼티
        public string SelectedAuth1 { get; set; }
        public string SelectedAuth2 { get; set; }

        // 사용자 검색 시 나온 사용자 정보를 담을 옵저버블컬렉션 프로퍼티
        public ObservableCollection<UserModel> Users_searched1 { get; set; }
        public ObservableCollection<UserModel> Users_searched2 { get; set; }

        public UserAuthViewModel()
        {
            AuthList = new[] { "NORMAL", "ADMIN", "SUPER"};
            Users_searched1 = new ObservableCollection<UserModel>();
            Users_searched2 = new ObservableCollection<UserModel>();

        }//Constructor

        private ActionCommand command;
        public ICommand Search1
        {
            get
            {
                if (command == null)
                {
                    command = new ActionCommand(DoSearch1);
                }
                return command;
            }//get

        }//Command

        public ICommand Search2
        {
            get
            {
                if (command == null)
                {
                    command = new ActionCommand(DoSearch2);
                }
                return command;
            }//get

        }//Command
        public void DoSearch1() // 좌측 리스트 검색
        {
            Console.WriteLine("DoSearch1()");
            List<UserModel> list = dao.SearchUser(SelectedAuth1, Keyword1);
            Users_searched1 = new ObservableCollection<UserModel>(list);
        
        }

        public void DoSearch2() // 우측 리스트 검색
        {
            List<UserModel> list = dao.SearchUser(SelectedAuth2, Keyword2);
            Users_searched2 = new ObservableCollection<UserModel>(list);
        }
    }//class

}//namespace
