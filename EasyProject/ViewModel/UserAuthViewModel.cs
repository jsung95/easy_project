using EasyProject.Dao;
using EasyProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using System.Linq;
using System.Windows;
using System.Text;

namespace EasyProject.ViewModel
{
    public class UserAuthViewModel : Notifier
    {
        UsersDao dao = new UsersDao();
        // 콤보박스의 검색타입 리스트
        public string[] SearchTypeList { get; set; }
        //검색 텍스트박스로 부터 입력받은 데이터를 담을 프로퍼티
        private string normal_Keyword;
        public string Normal_Keyword
        {
            get { return normal_Keyword; }
            set
            {
                normal_Keyword = value;
                OnPropertyChanged("Normal_Keyword");
                //OnNormalKeywordChanged();
            }
        }
        //public string Admin_Keyword { get; set; }

        private string admin_Keyword;
        public string Admin_Keyword
        {
            get { return admin_Keyword; }
            set
            {
                admin_Keyword = value;
                OnPropertyChanged("Admin_Keyword");
                //OnAdminKeywordChanged();
            }
        }
        // 콤보박스에서 선택한 권한을 담을 프로퍼티
        public string NormalSearchType { get; set; }
        public string AdminSearchType { get; set; }

        // 사용자 검색 시 나온 사용자 정보를 담을 옵저버블컬렉션 프로퍼티
        public ObservableCollection<UserModel> Normals_searched { get; set; }

        public ObservableCollection<UserModel> Admins_searched { get; set; }

        public List<UserModel> Normal_users { get; set; }

        public List<UserModel> Admin_users { get; set; }

        public UserAuthViewModel()
        {
            SearchTypeList = new[] { "이름", "아이디", "부서" };
            Normals_searched = new ObservableCollection<UserModel>(dao.GetUserInfo("NORMAL")); // 화면에 보일 리스트
            Admins_searched = new ObservableCollection<UserModel>(dao.GetUserInfo("ADMIN"));  // 화면에 보일 리스트
            Normal_users = dao.GetUserInfo("NORMAL"); // 사용자들을 검색할 리스트
            Admin_users = dao.GetUserInfo("ADMIN");   // 사용자들을 검색할 리스트
            
        }//Constructor

        private bool isAuthChangeEnabled = false;          // 권한변경 X 스낵바
        public bool IsAuthChangeEnabled
        {
            get { return isAuthChangeEnabled; }
            set
            {
                isAuthChangeEnabled = value;
                OnPropertyChanged("IsAuthChangeEnabled");
            }
        }

        private string errorProductString;
        public string ErrorProductString
        {
            get { return errorProductString; }
            set
            {
                errorProductString = value;
                OnPropertyChanged("ErrorProductString");
            }
        }

        private ActionCommand snackBarCommand;
        public ICommand SnackBarCommand
        {
            get
            {
                if (snackBarCommand == null)
                {
                    snackBarCommand = new ActionCommand(CloseSnackBar);
                }
                return snackBarCommand;
            }//get

        }//SnackBarCommand

        private void CloseSnackBar()
        {
            //IsEmptyProduct = false;
            IsAuthChangeEnabled = false;
        }
   

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

        public void MoveRight()
        {
            Console.WriteLine("MoveRight");
            ObservableCollection<UserModel> tempObject = new ObservableCollection<UserModel>();
            List<UserModel> updateList = new List<UserModel>(); // 업데이트 할 객체들을 담을 임시리스트

            foreach (var item in Normals_searched)
            {
                if (item.IsChecked)// item.IsChecked = true -> 권한 변경할 객체들 
                {
                    item.IsChecked = false;
                    Admins_searched.Add(item); // 화면에 보이는 Admin_searched 목록에 있는 리스트                   
                    Admin_users.Add(item);
                    Normal_users.Remove(item);
                    item.Nurse_auth = "ADMIN";
                    updateList.Add(item);                    
                }
                else tempObject.Add(item); // 체크 박스 선택되지 않은 리스트
            }

            if(updateList.Count > 0) // 체크박스를 하나 이상 체크 했을 경우
            {
                dao.UserAuthChange("ADMIN", updateList); // 업데이트 실행
                
                if(updateList.Count == 1)
                {
                    Console.WriteLine("선택한 NORMAL 유저수 : " + updateList.Count);
                    errorProductString = $"{updateList[0].Nurse_name}의 권한을 ADMIN으로 변경하였습니다.";
                    IsAuthChangeEnabled = true;
                    updateList.Clear();

                }
                else
                {
                    Console.WriteLine("선택한 NORMAL 유저수 : " + updateList.Count);
                    errorProductString = $"{updateList[0].Nurse_name} 외 {updateList.Count - 1}명의 권한을 ADMIN으로 변경하였습니다.";
                    IsAuthChangeEnabled = true;
                    updateList.Clear();

                }
            }
            else // 체크박스 선택을 안했을 경우
            {
                updateList.Clear();
                errorProductString = "권한을 변경할 사용자를 선택해주세요.";
                IsAuthChangeEnabled = true;
            }
            

            Normals_searched.Clear(); // 기존의 검색 목록을 비움.

            foreach (var item in tempObject)
            {
                Normals_searched.Add(item); // 선택되지 않은 리스트만 검색 목록에 다시 넣어줌
            }
            Normal_users = dao.GetUserInfo("NORMAL"); // 사용자들을 검색할 리스트 갱신
            Admin_users = dao.GetUserInfo("ADMIN");   // 사용자들을 검색할 리스트 갱신

        }//MoveRight

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
        public void MoveLeft()
        {
            Console.WriteLine("MoveLeft");          
            ObservableCollection<UserModel> tempObject =new  ObservableCollection<UserModel>();
            List<UserModel> updateList = new List<UserModel>(); // 업데이트 할 객체들을 담을 임시리스트

            foreach (var item in Admins_searched)
            {
                if (item.IsChecked)
                {
                    item.IsChecked = false;
                    Normals_searched.Add(item);
                    Normal_users.Add(item);
                    Admin_users.Remove(item);
                    item.Nurse_auth = "NORMAL";
                    updateList.Add(item);
                }
                else tempObject.Add(item);
            }

            if(updateList.Count > 0) // 체크 박스 선택 1개 이상 했을 때
            {
                dao.UserAuthChange("NORMAL", updateList);
                if (updateList.Count == 1)
                {
                    Console.WriteLine("선택한 ADMIN 유저수 : " + updateList.Count);
                    errorProductString = $"{updateList[0].Nurse_name}의 권한을 NORMAL로 변경하였습니다.";
                    IsAuthChangeEnabled = true;
                    updateList.Clear();

                }
                else
                {
                    Console.WriteLine("선택한 ADMIN 유저수 : " + updateList.Count);
                    Console.WriteLine(updateList.Count);
                    errorProductString = $"{updateList[0].Nurse_name} 외 {updateList.Count - 1} 명의 권한을 NORMAL로 변경하였습니다.";
                    IsAuthChangeEnabled = true;
                    updateList.Clear();

                }
            }
            else // 안했을 때 
            {
                updateList.Clear();
                errorProductString = "권한을 변경할 사용자를 선택해주세요.";
                IsAuthChangeEnabled = true;
            }
            
            Admins_searched.Clear();

            foreach (var item in tempObject)
            {
                Admins_searched.Add(item);
            }
            Normal_users = dao.GetUserInfo("NORMAL"); // 사용자들을 검색할 리스트 갱신
            Admin_users = dao.GetUserInfo("ADMIN");   // 사용자들을 검색할 리스트 갱신
        }//MoveLeft

        private ActionCommand normalKeywordCommand;
        public ICommand NormalKeywordCommand
        {
            get
            {
                if (normalKeywordCommand == null)
                {
                    normalKeywordCommand = new ActionCommand(OnNormalKeywordChanged);
                }
                return normalKeywordCommand;
            }//get

        }//SnackBarCommand
        public void OnNormalKeywordChanged()
        {
            Console.WriteLine("OnNormalKeywordChanged() : " + Normal_Keyword);
            IEnumerable <UserModel> temp = new List<UserModel>();

            switch (NormalSearchType)
            {
                case "이름":
                    temp = Normal_users.Where(user => user.Nurse_name.Contains(Normal_Keyword));
                    break;
                case "아이디":
                    temp = Normal_users.Where(user => user.Nurse_no.ToString().Contains(Normal_Keyword));
                    break;
                case "부서":
                    temp = Normal_users.Where(user => user.Dept_name.Contains(Normal_Keyword));
                    break;
            }
        
            Normals_searched.Clear();
            foreach (var item in temp)
            {
                Normals_searched.Add(item);
            }         
        }

        public void OnAdminKeywordChanged()
        {
            Console.WriteLine("OnAdminKeywordChanged()");
            IEnumerable<UserModel> temp = new List<UserModel>();

            switch (AdminSearchType)
            {
                case "이름":
                    temp = Admin_users.Where(user => user.Nurse_name.Contains(Admin_Keyword));
                    break;
                case "아이디":
                    temp = Admin_users.Where(user => user.Nurse_no.ToString().Contains(Admin_Keyword));
                    break;
                case "부서":
                    temp = Admin_users.Where(user => user.Dept_name.Contains(Admin_Keyword));
                    break;
            }
            Admins_searched.Clear();
            foreach (var item in temp)
            {
                Admins_searched.Add(item);
            }
        }
        //public IEnumerable<UserModel> FindProducts(string searchString)
        //{
        //    return Normals_searched.Where(user => user.Nurse_name.Contains(searchString));
        //}
    }//class

}//namespace
