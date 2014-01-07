using System;
using System.Collections.Generic;
using blastic.mongodb.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace blastic.pawhub.models
{
    public class User : IBson
    {
        [BsonId]
        public string _id { get; set; }

        public string uname { get; set; }
        public string pass { get; set; }
        public string email { get; set; }
        public string type { get; set; }
        public short petcoins { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime date { get; set; }

        public Picture pic { get; set; }
        public List<string> friends { get; set; }
        public List<string> requests { get; set; }
        public Dictionary<Int32, string> socialNetworks { get; set; }
    }
}