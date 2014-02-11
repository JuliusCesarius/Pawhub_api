using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace blastic.pawhub.models.LostAndFound
{
    public interface IReportDetail
    {
        string address { get; set; }
        string age { get; set; }
        string characteristics { get; set; }
        DateTime? date { get; set; }
        string name { get; set; }
        List<string> pics { get; set; }
    }
}
