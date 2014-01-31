using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Runtime.Serialization;
using blastic.mongodb.interfaces;
using blastic.pawhub.models.Enums;

namespace blastic.pawhub.models
{
    public class Picture:IBsonDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string _referenceId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime date { get; set; }

        public string path { get; set; }

        public PicType type { get; set; }
    }
}
