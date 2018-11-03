using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessEntity
{
    public class Country
    {
        [BsonElement("country_name")]
        public string country_name { get; set; }
        [BsonElement("country_code")]
        public string country_code { get; set; }
    }
}
