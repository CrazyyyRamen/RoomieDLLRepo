using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessEntity
{
    public class City
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("city_name")]
        public string city_name { get; set; }

        public Province province { get; set; }

        public Country country { get; set; }
    }
}
