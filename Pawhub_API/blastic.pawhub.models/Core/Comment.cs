using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace blastic.pawhub.models
{
    [DataContract]
    public class Comment
    {

        [BsonId]
        [DataMember]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        
        [DataMember]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _userId { get; set; }

        [DataMember]
        public string content { get; set; }

        [DataMember]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime date { get; set; }
    }
}
