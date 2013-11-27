using blastic.mongodb.clases;
using blastic.pawhub.models.Core;
using blastic.pawhub.models.LostAndFound;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System.Collections.Generic;

namespace blastic.pawhub.repositories
{
    public class KindsRepository : ObjectRepositoryBase<Kind>
    {
        public KindsRepository()
            : base()
        {
        }
    }
}