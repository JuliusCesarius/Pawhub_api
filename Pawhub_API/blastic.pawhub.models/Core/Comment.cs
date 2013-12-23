using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace blastic.pawhub.models
{
    public class Comment
    {
        public string content { get; set; }
        public ObjectId _userId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime date { get; set; }
    }
}
