using System;
using blastic.mongodb.interfaces;
using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using blastic.pawhub.models.enums;

namespace blastic.pawhub.models.LostAndFound
{
    [BsonIgnoreExtraElements]
    [DataContract]
    public class Report : IBsonDocument
    {
        private List<Comment> _comments;
        private ReportKind _reportKind;

        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember]public string _id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember]public String _userId { get; set; }
        
        [DataMember][BsonIgnore]public String userName { get; set; }

        [DataMember]public int kind { get; set; }
        [DataMember]public string type
        {
            get
            {
                return _reportKind.ToString();
            }
            set
            {
                _reportKind = (ReportKind)Enum.Parse(typeof(ReportKind), value.ToLower());
            }
        }
        [DataMember]public string description { get; set; }
        [DataMember]public string reportCode { get; set; }
        [DataMember]public int sharedCount { get; set; }
        [DataMember]public bool solved { get; set; }
        [DataMember]public string picture { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [DataMember]public DateTime date { get; set; }
        
        [DataMember]public Location location { get; set; }
        
        [DataMember]public List<int> linkedTo { get; set; }
        [DataMember]public List<string> viewedBy { get; set; }

        [DataMember]public List<string> alertTo { get; set; }

        [DataMember] public List<Comment> comments { get; set; }

        [DataMember]public IReportDetail detail
        {
            get
            {
                if (resque != null)
                {
                    return resque;
                }
                else if (lost != null)
                {
                    return lost;
                }
                else if (found != null)
                {
                    return found;
                }
                else if (abuse != null)
                {
                    return abuse;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    if (value.GetType() == typeof(Resque))
                    {
                        resque = value as Resque;
                    }
                    else if (value.GetType() == typeof(Lost))
                    {
                        lost = value as Lost;
                    }
                    else if (value.GetType() == typeof(Found))
                    {
                        found = value as Found;
                    }
                    else if (value.GetType() == typeof(Abuse))
                    {
                        abuse = value as Abuse;
                    }
                }
            }
        }

        [DataMember][BsonIgnore]public Resque resque { get; set; }
        [DataMember][BsonIgnore]public Lost lost { get; set; }
        [DataMember][BsonIgnore]public Found found { get; set; }
        [DataMember][BsonIgnore]public Abuse abuse { get; set; }

        [BsonExtraElements]public BsonDocument CatchAll { get; set; }

    }
}
