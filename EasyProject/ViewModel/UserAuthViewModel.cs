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

        //권한별로 나온 사용자 정보를 담을 옵저버블컬렉션 리스트 프로퍼티
        public ObservableCollection<UserModel> Users { get; set; }

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
            List<UserModel> list = dao.GetUserInfo(Auth_before);
            Users = new ObservableCollection<UserModel>(list);
        }

    }//class

}//namespace
