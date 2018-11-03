﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessEntity
{
    public class Neighbourhood
    {
        [BsonId]
        public Object _id { get; set; }
        [BsonElement("neighbourhood_name")]
        public string neighbourhood_name { get; set; }
        [BsonElement("p_id")]
        public string p_id { get; set; }
    }
}
