using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyProject.Model
{
    public class ProductModel : Notifier
    {
        public int? Prod_id { get; set; }
        public string Prod_code { get; set; }
        public string Prod_name { get; set; }
        public int? Prod_price { get; set; }
        public int? Prod_total { get; set; }
        public DateTime Prod_expire { get; set; }
        public int? Category_id { get; set; }

    }//class

}//namespace
