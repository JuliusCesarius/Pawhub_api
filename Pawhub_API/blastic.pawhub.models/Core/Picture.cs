using System;
using MongoDB.Bson.Serialization.Attributes;

namespace blastic.pawhub.models
{
    public class Picture
    {
        public string path { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime date { get; set; }
    }
}
