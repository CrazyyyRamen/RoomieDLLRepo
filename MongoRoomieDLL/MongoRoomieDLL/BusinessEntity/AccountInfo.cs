using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessEntity
{
    public class AccountInfo
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("user_image")]
        public string user_image { get; set; }
        [BsonElement("_firstname")]
        public string _firstname { get; set; }
        [BsonElement("_lastname")]
        public string _lastname { get; set; }
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
        [BsonElement("party_habit")]
        public string party_habit { get; set; }
        [BsonElement("login_day")]
        public string login_day { get; set; }
        [BsonElement("_gender")]
        public string _gender { get; set; }
        [BsonElement("account_status")]
        public string account_status { get; set; }
        [BsonElement("_featured")]
        public string _featured { get; set; }

        public List<Property> _property { get; set; }
        [BsonElement("current_status")]
        public string current_status { get; set; }
        [BsonElement("_role")]
        public string _role { get; set; }
        [BsonElement("currently_live_in")]
        public string currently_live_in { get; set; }
        [BsonElement("_rrent")]
        public string _rrent { get; set; }
        [BsonElement("move_in_day")]
        public string move_in_day { get; set; }

        public IntendPlace interested_place { get; set; }
        public Preference _preference { get; set; }
    }
}
