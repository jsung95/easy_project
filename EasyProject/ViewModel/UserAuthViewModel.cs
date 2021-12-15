using EasyProject.Dao;
using EasyProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

namespace EasyProject.ViewModel
{
    /*public class UserAuthViewModel : Notifier
    {
        UsersDao dao = new UsersDao();

        //검색 텍스트박스로 부터 입력받은 데이터를 담을 프로퍼티
        private string user_name;
        public string User_name
        {
            get { return user_name; }
            set 
            { 
                user_name = value;
                OnPropertyChanged("User_name");
            }
        }

        //권한별로 나온 사용자 정보를 담을 옵저버블컬렉션 리스트 프로퍼티
        public ObservableCollection<UserModel> Users_before { get; set; }

        //체크박스 체크 시에 권한변경할 사용자 정보를 담을 옵저버블컬렉션 리스트 프로퍼티
        public ObservableCollection<UserModel> Users_selected { get; set; }

        //권한변경 '후' 사용자 정보를 담을 옵저버블컬렉션 리스트 프로퍼티
        public ObservableCollection<UserModel> Users_after { get; set; }

        //콤보박스에서 권한 변경 '전', 선택한 권한 데이터를 담을 프로퍼티
        private string auth_before;
        public string Auth_before
        {
            get { return auth_before; }
            set 
            {
                auth_before = value;
                OnPropertyChanged("Auth_before");
            }
        }

        //콤보박스에서 권한 변경 '후', 선택한 권한 데이터를 담을 프로퍼티
        private string auth_after;
        public string Auth_after
        {
            get { return auth_after; }
            set
            {
                auth_before = value;
                OnPropertyChanged("Auth_after");
            }
        }

        public UserAuthViewModel()
        {
            //List<UserModel> list = dao.GetUserInfo(Auth_before);
            //Users_before = new ObservableCollection<UserModel>(list);

        }//Constructor






        private ActionCommand command;
        public ICommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new ActionCommand(DoSomething);
                }
                return command;
            }//get

        }//Command


        public void DoSomething()
        {
            //권한별 유저정보 조회
            //dao.GetUserInfo(Auth_before);

            //유저권한변경
            //dao.UserAuthChange(Auth_after, Users_selected);

            //유저정보검색
            //dao.SearchUser(User_name);
        }


    }//class*/

}//namespace
