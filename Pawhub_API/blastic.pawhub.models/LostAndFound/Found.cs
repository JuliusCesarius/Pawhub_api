using System.Collections.Generic;
using System.Runtime.Serialization;

namespace blastic.pawhub.models.LostAndFound
{
    [DataContract]
    public class Found : ReportDetail
    {
        [DataMember]public List<Breed> breeds { get; set; }
        [DataMember]public string size { get; set; }
    }
}
