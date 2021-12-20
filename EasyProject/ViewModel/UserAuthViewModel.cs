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
        // 콤보박스의 검색타입 리스트
        public string[] SearchTypeList { get; set; }
        //검색 텍스트박스로 부터 입력받은 데이터를 담을 프로퍼티
        public string Normal_Keyword { get; set; }
        public string Admin_Keyword { get; set; }

        // 콤보박스에서 선택한 권한을 담을 프로퍼티
        public string NormalSearchType { get; set; }
        public string AdminSearchType { get; set; }

        // 사용자 검색 시 나온 사용자 정보를 담을 옵저버블컬렉션 프로퍼티
        public ObservableCollection<UserModel> Normals_searched { get; set; }

        private ObservableCollection<UserModel> admins_searched;

        public ObservableCollection<UserModel> Admins_searched
        {
            get { return admins_searched; }
            set 
            { 
                admins_searched = value;
                OnPropertyChanged("Admins_searched");
            }
        }


        public UserAuthViewModel()
        {
            SearchTypeList = new[] { "이름", "아이디", "부서" };
            Normals_searched = new ObservableCollection<UserModel>();
            Admins_searched = new ObservableCollection<UserModel>();

        }//Constructor

        private ActionCommand normalSearchCommand;
        public ICommand NormalSearchCommand
        {
            get
            {
                if (normalSearchCommand == null)
                {
                    normalSearchCommand = new ActionCommand(NormalSearch);
                }
                return normalSearchCommand;
            }//get

        }//Command

        private ActionCommand adminSearchCommand;
        public ICommand AdminSearchCommand
        {
            get
            {
                if (adminSearchCommand == null)
                {
                    adminSearchCommand = new ActionCommand(AdminSearch);
                }
                return adminSearchCommand;
            }//get

        }//Command

        private ActionCommand moveRightCommand;
        public ICommand MoveRightCommand
        {
            get
            {
                if (moveRightCommand == null)
                {
                    moveRightCommand = new ActionCommand(MoveRight);
                }
                return moveRightCommand;
            }//get

        }//Command

        private ActionCommand moveLeftCommand;
        public ICommand MoveLeftCommand
        {
            get
            {
                if (moveLeftCommand == null)
                {
                    moveLeftCommand = new ActionCommand(MoveLeft);
                }
                return moveLeftCommand;
            }//get

        }//Command
        public void NormalSearch() // 좌측 리스트(NORMAL) 검색
        {
            Console.WriteLine("Normal 유저 검색");
            Normals_searched.Clear();
            List<UserModel> list = dao.SearchUser("NORMAL", NormalSearchType, Normal_Keyword);
            //Users_searched1.CollectionChanged += Users_searched1ContentCollectionChanged;
            foreach (UserModel user in list)
            {
                Normals_searched.Add(user);
            }
        }

        public void AdminSearch() // 우측 리스트(ADMIN) 검색
        {
            Console.WriteLine("Admin 유저 검색");
            Admins_searched.Clear();
            List<UserModel> list = dao.SearchUser("ADMIN", AdminSearchType, Admin_Keyword);
            foreach (UserModel user in list)
            {
                Admins_searched.Add(user);
            }
        }

        public void MoveRight()
        {
            Console.WriteLine("MoveRight");
            //Admins_searched.Clear();
            for (int i = 0; i < Normals_searched.Count; i++)
            {
                var item = Normals_searched[i];
                if (item.IsChecked)
                {
                    Admins_searched.Add(item);
                    Normals_searched.Remove(item);
                }
            }
            //foreach (var item in Normals_searched)
            //{
            //    if (item.IsChecked)
            //    {
            //        Admins_searched.Add(item);
            //        Normals_searched.Remove(item);
            //    }
            //}
        }//MoveRight

        public void MoveLeft()
        {
            Console.WriteLine("MoveLeft");
            //Normals_searched.Clear();
            for (int i = 0; i < Admins_searched.Count; i++)
            {
                var item = Admins_searched[i];
                if (item.IsChecked)
                {
                    Normals_searched.Add(item);
                    Admins_searched.Remove(item);
                }
            }
            //foreach (var item in Admins_searched)
            //{
            //    if (item.IsChecked)
            //    {
            //        Normals_searched.Add(item);
            //        Admins_searched.Remove(item);
            //    }
            //}

        }//MoveLeft
    }//class

}//namespace
