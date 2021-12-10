using System;


namespace EasyProject.Model
{
    public class NurseModel : Notifier
    {
        //
        public int? nurse_no { get; set; }
        public string nurse_name { get; set; }
        public string nurse_auth { get; set; }
        public string nurse_pw { get; set; }
        public int? dept_id { get; set; }

    }
}
