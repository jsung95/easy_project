
namespace EasyProject.Model
{
    public class UserModel : Notifier
    {
        public int? Nurse_no { get; set; }
        public string Nurse_name { get; set; }
        public string Nurse_auth { get; set; }
        public string Nurse_pw { get; set; }
        public int? Dept_id { get; set; }
        public string Dept_name { get; set; }
        public int? Dept_phone { get; set; }
        public string Dept_status { get; set; }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

    }//class

}//namespace
