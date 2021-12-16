using EasyProject.Model;
using EasyProject.Dao;
using System;
using Microsoft.Expression.Interactivity.Core;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;

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

        public void Login()
        {
            
            NurseModel result = dao.LoginUserInfo(Nurse);
            if (result.Nurse_no != null)
            {
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
            }
            else
            {
                Console.WriteLine("로그인 실패");
                Console.WriteLine("  Nurse NO : {0}", App.nurse_dto.Nurse_no);
                Console.WriteLine("  Nurse NAME : {0}", App.nurse_dto.Nurse_name);
                Console.WriteLine("  Nurse AUTH : {0}", App.nurse_dto.Nurse_auth);
                Console.WriteLine("  Nurse PW : {0}", App.nurse_dto.Nurse_pw);
                Console.WriteLine("  DEPT ID : {0}", App.nurse_dto.Dept_id);
            }
        }

    }//class

}//namespace
