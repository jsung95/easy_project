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

        private ObservableCollection<DeptModel> depts; // depts = DeptModel 객체가 담긴 리스트
        public ObservableCollection<DeptModel> Depts 
        {
            get { return depts; }
            set { depts = value; }
        }
        public DeptModel SelectedDept { get; set; } // 콤보박스에서 선택한 부서객체

        // 회원가입 시에 사용자가 입력한 데이터를 담을 프로퍼티
        private NurseModel nurse;
        public NurseModel Nurse
        {
            get { return nurse; }
            set
            {
                nurse = value;
                OnPropertyChanged("Nurse");
            }
        }

        public SignupViewModel()
        {
            Nurse = new NurseModel();
            List<DeptModel> list = dao.GetDeptModels("SELECT DEPT_NAME FROM DEPT");
            Depts = new ObservableCollection<DeptModel>(list); // List타입 객체 list를 OC 타입 Depts에 넣음 
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

        public void SignupInsert()
        {
            Nurse = dao.IdCheck("SELECT nurse_no FROM nurse WHERE nurse_no = :no", Nurse);
            if(Nurse.Nurse_no != null) // 중복없을시 가입 진행
            {
                dao.SignUp("INSERT INTO NURSE(NURSE_NO, NURSE_NAME, NURSE_PW, DEPT_ID) VALUES(:no, :name, :pw, :dept_id)", Nurse, SelectedDept);
            }
        }

    } // SignupViewModel
} // namespace
