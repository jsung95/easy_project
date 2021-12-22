using EasyProject.Model;
using Microsoft.Expression.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyProject.ViewModel
{
    public class PasswordChangeViewModel : Notifier
    {
        public NurseModel Nurse { get; set; }

        public string NewPassword { get; set; }
        public string Re_NewPassword { get; set; }

        private string newPasswordStatement;

        public string NewPasswordStatement
        {
            get { return newPasswordStatement; }
            set
            {
                newPasswordStatement = value;
                OnPropertyChanged("NewPasswordStatement");
            }
        }
        public PasswordChangeViewModel()
        {

        }

        public ActionCommand command;

        public ICommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new ActionCommand(PasswordChange);
                }
                return command;
            }
        }

        public void PasswordChange()
        {

        }

    }
}
