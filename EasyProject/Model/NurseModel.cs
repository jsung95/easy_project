using System;


namespace EasyProject.Model
{
    public class NurseModel : Notifier
    {
        //
        public Int32 nurse_no { get; set; }
        public string nurse_name { get; set; }
        public string nurse_auth { get; set; }
        public string nurse_pw { get; set; }
        public Int32 dept_id { get; set; }

    }
}
