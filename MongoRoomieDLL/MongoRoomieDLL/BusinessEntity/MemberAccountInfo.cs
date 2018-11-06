using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessEntity
{
    public class MemberAccountInfo
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("user_name")]
        public string user_name { get; set; }
        [BsonElement("_password")]
        public string _password { get; set; }
        [BsonElement("_email")]
        public string _email { get; set; }
        [BsonElement("_birthday")]
        public string _birthday { get; set; }
        [BsonElement("_cell")]
        public string _cell { get; set; }
        [BsonElement("create_day")]
        public string create_day { get; set; }
        [BsonElement("update_day")]
        public string update_day { get; set; }
        [BsonElement("smoke_habit")]
        public string smoke_habit { get; set; }
        [BsonElement("has_pet")]
        public string has_pet { get; set; }
        [BsonElement("login_day")]
        public string login_day { get; set; }
        [BsonElement("_gender")]
        public string _gender { get; set; }
        [BsonElement("account_status")]
        public string account_status { get; set; }

        public CurrentAddress current_place { get; set; }
        [BsonElement("current_status")]
        public string current_status { get; set; }
        [BsonElement("offer_place")]
        public string offer_place { get; set; }
        [BsonElement("home_type")]
        public string home_type { get; set; }
        [BsonElement("room_type")]
        public string room_type { get; set; }
        [BsonElement("have_locker")]
        public string have_locker { get; set; }
        [BsonElement("have_parking")]
        public string have_parking { get; set; }
        [BsonElement("have_gym")]
        public string have_gym { get; set; }
        [BsonElement("have_swim_pool")]
        public string have_swim_pool { get; set; }
        [BsonElement("have_furniture")]
        public string have_furniture { get; set; }
        [BsonElement("rent_fee")]
        public string rent_fee { get; set; }

        public List<InterestedPlace> interested_place { get; set; }
        [BsonElement("min_rent_fee")]
        public string min_rent_fee { get; set; }
        [BsonElement("max_rent_fee")]
        public string max_rent_fee { get; set; }
        [BsonElement("move_in_date")]
        public string move_in_date { get; set; }
    }
}
