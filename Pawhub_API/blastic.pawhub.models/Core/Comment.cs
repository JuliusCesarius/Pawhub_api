using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace blastic.pawhub.models
{
    [DataContract]
    public class Comment
    {
        [DataMember]
        public string content { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember]
        public string _userId { get; set; }
        [DataMember]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime date { get; set; }
    }
}
