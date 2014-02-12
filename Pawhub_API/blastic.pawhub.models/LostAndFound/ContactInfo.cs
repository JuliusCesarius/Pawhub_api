using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace blastic.pawhub.models.LostAndFound
{
    [DataContract]
    public class ContactInfo
    {
        [DataMember]public string name { get; set; }
        [DataMember]public string address { get; set; }
        [DataMember]public List<string> email { get; set; }
        [DataMember]public List<string> phones { get; set; }
    }
}
