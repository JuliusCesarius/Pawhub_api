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
        private List<string> _pics;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [DataMember]public DateTime? date { get; set; }
        [DataMember]public string address { get; set; }
        [DataMember]public string name { get; set; }
        [DataMember]public string age { get; set; }
        [DataMember]public string characteristics { get; set; }
        [DataMember]public List<string> pics
        {
            get
            {
                if (_pics == null)
                {
                    _pics = new List<string>();
                }
                return _pics;
            }
            set
            {
                _pics = value;
            }
        }
    }
}
