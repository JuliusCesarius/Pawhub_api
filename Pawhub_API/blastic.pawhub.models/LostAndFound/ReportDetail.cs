using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime.Serialization;

namespace blastic.pawhub.models.LostAndFound
{
    [BsonKnownTypes(typeof(Abuse), typeof(Found), typeof(Lost), typeof(Resque))]
    [KnownType(typeof(Abuse))] 
    [KnownType(typeof(Found))]
    [KnownType(typeof(Lost))]
    [KnownType(typeof(Resque))]
    [DataContract]
    public class ReportDetail : blastic.pawhub.models.LostAndFound.IReportDetail
    {
        [BsonId]
        [DataMember]public string _id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [DataMember]public DateTime dateEvent { get; set; }
        [DataMember]public string adress { get; set; }
        [DataMember]public string name { get; set; }
        [DataMember]public string age { get; set; }
        [DataMember]public string characteristics { get; set; }
        [DataMember]public List<Picture> pics { get; set; }
    }
}
