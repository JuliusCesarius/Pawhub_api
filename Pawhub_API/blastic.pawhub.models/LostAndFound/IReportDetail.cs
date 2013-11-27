using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace blastic.pawhub.models.LostAndFound
{
    public interface IReportDetail
    {
        string adress { get; set; }
        string age { get; set; }
        string characteristics { get; set; }
        DateTime dateEvent { get; set; }
        string name { get; set; }
        List<Picture> pics { get; set; }
    }
}
