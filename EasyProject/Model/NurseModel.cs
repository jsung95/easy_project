using System;


namespace EasyProject.Model
{
    public class NurseModel : Notifier
    {
        //
        public int? Nurse_no { get; set; }
        public string Nurse_name { get; set; }
        public string Nurse_auth { get; set; }
        public string Nurse_pw { get; set; }
        public int? Dept_id { get; set; }

    }
}
