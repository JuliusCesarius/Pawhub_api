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

namespace blastic.pawhub.repositories
{
    public class PicturesRepository : ObjectRepositoryBase<Picture>
    {
        public PicturesRepository()
            : base()
        {
        }

        public string GetPath(string id)
        {
            Succeed = false;
            var query = Query<Report>.EQ(x => x._id, id);
            var cursor = Collection.Find(query).SetLimit(1).SetFields(Fields.Include("path"));

            Succeed = true;

            if (cursor.Count() <= 0)
            {
                throw new Exception("Image not found in DB");
            }

            return cursor.First().path;
        }
    }
}