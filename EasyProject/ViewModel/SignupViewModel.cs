using EasyProject.Dao;
using EasyProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

namespace EasyProject.ViewModel
{
    public class SignupViewModel : Notifier
    {
        SignupDao dao = new SignupDao();

        public List<DeptModel> Depts { get; set; }    // Depts = DeptModel 객체가 담긴 리스트

        private DeptModel selectedDept; // 콤보박스에서 선택한 부서객체
        public DeptModel SelectedDept
        {
            get { return selectedDept; }
            set
            {
                selectedDept = value;
                OnPropertyChanged("SelectedDept");
            }
        }

        // 회원가입 시에 사용자가 입력한 데이터를 담을 프로퍼티       
        public NurseModel Nurse { get; set; }
       

        public SignupViewModel()
        {
            Nurse = new NurseModel();            
            Depts = dao.GetDeptModels(); 
        }//생성자

        private ActionCommand command;
        public ICommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new ActionCommand(SignupInsert);
                }
                return command;
            }

        }
        private ActionCommand resetCommand;
        public ICommand ResetCommand
        { 
            get
            {
                if (resetCommand == null)
                {
                    resetCommand = new ActionCommand(ResetForm);
                }
                return resetCommand;
            }
        }
        public void SignupInsert()
        {
            Nurse = dao.IdCheck(Nurse);
            if(Nurse.Nurse_no != null) // 중복없을시 가입 진행
            {
                dao.SignUp(Nurse, SelectedDept);
            }
        }

        public void ResetForm()
        {           
            Nurse.Nurse_name = null;
            SelectedDept = null;
            Nurse.Nurse_no = null;
            Nurse.Nurse_pw = null;
            Nurse.Nurse_re_pw = null;
        }
    } // SignupViewModel
} // namespace
