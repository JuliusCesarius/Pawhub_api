using blastic.mongodb.clases;
using blastic.patterns.enums;
using blastic.pawhub.models;
using blastic.pawhub.models.LostAndFound;
using blastic.pawhub.models.Register;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Linq;
using System.Collections.Generic;
using blastic.pawhub.models.Core;

namespace blastic.pawhub.repositories
{
    public class PushDevicesRepository : ObjectRepositoryBase<PushDevice>
    {
        public PushDevicesRepository()
            : base()
        {
        }
        
        public PushDevice GetByDeviceId(string id)
        {
            var query = Query<PushDevice>.EQ(x => x.deviceId, id);
            return Collection.FindOne(query);
        }
    }
}