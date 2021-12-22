using EasyProject.Model;
using EasyProject.Dao;
using System;
using Microsoft.Expression.Interactivity.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;



namespace EasyProject.ViewModel
{
    public class LoginViewModel : Notifier
    {
        LoginDao dao = new LoginDao();

        private NurseModel nurse;
        public NurseModel Nurse
        {
            get { return nurse; }
            set 
            { 
                nurse = value;
                //OnPropertyChanged("Nurse");
            }
        }

        public INavigation Navigation { get; set; }

        public LoginViewModel()
        {
            Nurse = new NurseModel();
        }


        private ActionCommand command;
        public ICommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new ActionCommand(Login);
                }
                return command;
            }

        }

        private ActionCommand logout;
        public ICommand LogoutCommand
        {
            get
            {
                if(logout == null)
                {
                    logout = new ActionCommand(Logout);
                }
                return logout;
            }
        }

        public bool isLogin { get; set; }
        public void Login()
        {
            NurseModel result = dao.LoginUserInfo(Nurse);

            if (dao.IdPasswordCheck(Nurse) == true)
            {
                Console.WriteLine("id password check ok!");
                App.nurse_dto.Nurse_no = result.Nurse_no;
                App.nurse_dto.Nurse_name = result.Nurse_name;
                App.nurse_dto.Nurse_auth = result.Nurse_auth;
                App.nurse_dto.Nurse_pw = result.Nurse_pw;
                App.nurse_dto.Dept_id = result.Dept_id;

                Console.WriteLine("로그인 성공");
                Console.WriteLine("  Nurse NO : {0}", App.nurse_dto.Nurse_no);
                Console.WriteLine("  Nurse NAME : {0}", App.nurse_dto.Nurse_name);
                Console.WriteLine("  Nurse AUTH : {0}", App.nurse_dto.Nurse_auth);
                Console.WriteLine("  Nurse PW : {0}", App.nurse_dto.Nurse_pw);
                Console.WriteLine("  DEPT ID : {0}", App.nurse_dto.Dept_id);

                isLogin = true;
            }
            else
            {
                Console.WriteLine("id password check fail!");
                Console.WriteLine("로그인 실패");
                Console.WriteLine("  Nurse NO : {0}", App.nurse_dto.Nurse_no);
                Console.WriteLine("  Nurse NAME : {0}", App.nurse_dto.Nurse_name);
                Console.WriteLine("  Nurse AUTH : {0}", App.nurse_dto.Nurse_auth);
                Console.WriteLine("  Nurse PW : {0}", App.nurse_dto.Nurse_pw);
                Console.WriteLine("  DEPT ID : {0}", App.nurse_dto.Dept_id);

                isLogin = false;
            }

        }

        public void Logout()
        {
            
            if (App.nurse_dto.Nurse_no != null)
            {
                App.nurse_dto.Nurse_no = null;
                App.nurse_dto.Nurse_name = null;
                App.nurse_dto.Nurse_auth = null;
                App.nurse_dto.Nurse_pw = null;
                App.nurse_dto.Dept_id = null;

                Console.WriteLine("로그아웃 성공");
                Console.WriteLine("  Nurse NO : {0}", App.nurse_dto.Nurse_no);
                Console.WriteLine("  Nurse NAME : {0}", App.nurse_dto.Nurse_name);
                Console.WriteLine("  Nurse AUTH : {0}", App.nurse_dto.Nurse_auth);
                Console.WriteLine("  Nurse PW : {0}", App.nurse_dto.Nurse_pw);
                Console.WriteLine("  DEPT ID : {0}", App.nurse_dto.Dept_id);
            }
        }


        private string password;

        public string Password 
        { 
            get => password;
            set
            { 
                password = value;
                nurse.Nurse_pw = value;
            }
         }

    }//class

}//namespace
