using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessEntity
{
    public class Property
    {
        public string home_type { get; set; }
        public string room_type { get; set; }
        public string current_address { get; set; }
        public string current_city { get; set; }
        public string current_neighbour { get; set; }
        public string current_province { get; set; }
        public string current_country { get; set; }
        public string have_parking { get; set; }
        public string have_locker { get; set; }
        public string have_swim_pool { get; set; }
        public string have_gym { get; set; }
        public string have_furnished { get; set; }
        public string orent { get; set; }
        public string start_rent_day { get; set; }
        public List<PropertyImage> property_image { get; set; }
    }
}
