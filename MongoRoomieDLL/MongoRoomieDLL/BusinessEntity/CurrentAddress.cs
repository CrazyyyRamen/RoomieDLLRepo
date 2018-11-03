using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessEntity
{
    public class CurrentAddress
    {
        public string current_address { get; set; }
        public string current_neighbourhood { get; set; }
        public string current_province { get; set; }
        public string current_country { get; set; }
    }
}
