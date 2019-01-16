using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessEntity
{
    public class Preference
    {
        public string prefer_home_type { get; set; }
        public string prefer_room_type { get; set; }
        public string prefer_gender { get; set; }
        public string prefer_status { get; set; }
        public string prefer_furnished { get; set; }
        public string allow_has_pet { get; set; }
        public string allow_smoke { get; set; }
        public string allow_party { get; set; }
        public string need_parking { get; set; }
        public string need_locker { get; set; }
        public string need_gym { get; set; }
        public string need_swim_pool { get; set; }
    }
}
