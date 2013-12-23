using System.Collections.Generic;
using blastic.mongodb.interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace blastic.pawhub.models.Core
{
    public class Kind:IBson
    {        
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string _id { get; set; }
        public Picture pic { get; set; }
        public List<Breed> breeds { get; set; }
        public MultilingualContent name { get; set; }
        public MultilingualContent description { get; set; }

    }
}