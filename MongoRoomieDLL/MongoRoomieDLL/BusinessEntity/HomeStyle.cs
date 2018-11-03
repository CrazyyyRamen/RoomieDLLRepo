using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessEntity
{
    public class HomeStyle
    {
        [BsonId]
        public Object _id { get; set; }
        [BsonElement("home_style")]
        public string home_style { get; set; }
    }
}
