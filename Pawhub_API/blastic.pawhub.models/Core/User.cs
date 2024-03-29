﻿using System;
using System.Collections.Generic;
using blastic.mongodb.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace blastic.pawhub.models
{
    public class User : IBsonDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string uname { get; set; }
        public string pass { get; set; }
        public string email { get; set; }
        public string type { get; set; }
        public int petcoins { get; set; }
        public short yob { get; set; }
        public string country { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime date { get; set; }

        public Picture pic { get; set; }
        public List<string> friends { get; set; }
        public List<string> requests { get; set; }
        public Dictionary<Int32, string> socialNetworks { get; set; }
    }
}