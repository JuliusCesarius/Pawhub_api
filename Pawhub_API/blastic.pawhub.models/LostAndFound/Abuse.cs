using System.Runtime.Serialization;
namespace blastic.pawhub.models.LostAndFound
{
    [DataContract]
    public class Abuse : ReportDetail
    {
        [DataMember]public bool isAnonimous { get; set; }
    }
}
