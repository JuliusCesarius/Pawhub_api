using System.Collections.Generic;
using System.Runtime.Serialization;

namespace blastic.pawhub.models.LostAndFound
{
    [DataContract]
    public class Lost : ReportDetail
    {
        [DataMember]public bool reward { get; set; }
        [DataMember]public string size { get; set; }

        [DataMember]public ContactInfo contactInfo { get; set; }
        [DataMember]public List<int> breeds { get; set; }
    }
}
