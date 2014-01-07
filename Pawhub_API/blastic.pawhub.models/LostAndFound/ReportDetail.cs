using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime.Serialization;
using MongoDB.Bson;

namespace blastic.pawhub.models.LostAndFound
{
    [DataContract]
    [KnownType(typeof(Resque))]
    [KnownType(typeof(Abuse))]
    [KnownType(typeof(Lost))]
    [KnownType(typeof(Found))]
    public class ReportDetail : blastic.pawhub.models.LostAndFound.IReportDetail
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [DataMember]public DateTime? dateEvent { get; set; }
        [DataMember]public string adress { get; set; }
        [DataMember]public string name { get; set; }
        [DataMember]public string age { get; set; }
        [DataMember]public string characteristics { get; set; }
        [DataMember]public List<Picture> pics { get; set; }
    }
}
