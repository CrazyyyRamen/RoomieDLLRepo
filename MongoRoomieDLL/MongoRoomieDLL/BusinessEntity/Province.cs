using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessEntity
{
    public class Province
    {
        [BsonElement("province_name")]
        public string province_name { get; set; }
        [BsonElement("province_code")]
        public string province_code { get; set; }
    }
}
