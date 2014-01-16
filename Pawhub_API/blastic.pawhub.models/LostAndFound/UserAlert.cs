using blastic.mongodb.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace blastic.pawhub.models.LostAndFound
{
    [DataContract]
    public class UserAlert:IBson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember]
        public string _id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember]public string _userId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember]public string _reportId { get; set; }
    }
}