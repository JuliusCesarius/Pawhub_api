using blastic.mongodb.clases;
using blastic.patterns.enums;
using blastic.pawhub.models;
using blastic.pawhub.models.LostAndFound;
using blastic.pawhub.models.Register;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;

namespace blastic.pawhub.repositories
{
    public class PicturesRepository : ObjectRepositoryBase<Picture>
    {
        public PicturesRepository()
            : base()
        {
        }

        public string GetPirturePath(string id)
        {
            Succeed = false;
            var query = Query<Report>.EQ(x => x._id, id);
            var cursor = Collection.Find(query).SetLimit(1).SetFields(Fields.Include("path"));

            Succeed = LastWriteConcernResult.Ok;
            if (LastWriteConcernResult.HasLastErrorMessage)
            {
                AddValidationMessage(enumMessageType.UnhandledException, LastWriteConcernResult.ErrorMessage);
            }

            if (cursor.Count() <= 0)
            {
                throw new Exception("Image not found");
            }
                       
            return cursor.GetEnumerator().MoveNext().ToString();
        }
    }
}