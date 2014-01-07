using blastic.pawhub.models.enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blastic.pawhub.models.Core
{
    public class ActivityLog
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string _id { get; set; }
        public DateTime date { get; set; }
        public ActivityType activity { get; set; }
        public string session { get; set; }
    }
}
