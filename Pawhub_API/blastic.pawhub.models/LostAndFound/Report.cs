using System;
using blastic.mongodb.interfaces;
using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace blastic.pawhub.models.LostAndFound
{
    [BsonIgnoreExtraElements]
    [DataContract]
    public class Report : IBson
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember]public string _id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember]public String _userId { get; set; }

        [DataMember]public string description { get; set; }
        [DataMember]public string picture { get; set; }
        [DataMember]public List<int> linkedTo { get; set; }
        [DataMember]public string reportCode { get; set; }
        [DataMember]public int sharedCount { get; set; }
        [DataMember]public bool solved { get; set; }
        [DataMember]public List<int> viewBy { get; set; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [DataMember]public DateTime date { get; set; }

        [DataMember]public List<Comment> comments { get; set; }
        [DataMember]public List<string> alertTo { get; set; }
        [DataMember]public Location location { get; set; }


        [DataMember]public IReportDetail detail { get; set; }
    }
}
