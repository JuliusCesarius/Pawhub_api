using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Runtime.Serialization;

namespace blastic.pawhub.models
{
    public class Picture
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public String _userId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime date { get; set; }
        
        public string path { get; set; }
    }
}
