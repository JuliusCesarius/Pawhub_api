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
        [DataMember]public string _userId { get; set; }
        [DataMember]public string _id
        {
            get
            {
                return _userId;
            }
            set
            {
                _id = value;
            }
        }
        [DataMember]public string reportCode { get; set; }
    }
}