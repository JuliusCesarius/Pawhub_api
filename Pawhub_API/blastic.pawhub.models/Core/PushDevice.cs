using blastic.mongodb.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blastic.pawhub.models.Core
{
    public class PushDevice: IBsonDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string _id { get; set; }
        public string deviceId { get; set; }
        public string os { get; set; }

        public DateTime date { get; set; }
    }
}
