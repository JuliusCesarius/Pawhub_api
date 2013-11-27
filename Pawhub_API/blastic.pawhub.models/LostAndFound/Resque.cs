
using System.Runtime.Serialization;
namespace blastic.pawhub.models.LostAndFound
{
    [DataContract]
    public class Resque:ReportDetail
    {
        [DataMember]public ContactInfo contactInfo { get; set; }
    }
}
