using blastic.mongodb.clases;
using blastic.patterns.enums;
using blastic.pawhub.models;
using blastic.pawhub.models.LostAndFound;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using Pawhub_API.Models.MongoUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace blastic.pawhub.repositories
{
    public class ReportsRepository : ObjectRepositoryBase<Report>
    {
        public ReportsRepository()
            : base()
        {

        }
        public IEnumerable<Report> ListByPageAndType(int pageNumber, int pageSize, string type)
        {
            type = type.Substring(0, 1).ToUpper() + type.Substring(1, type.Length - 1).ToLower();
            var baseFilter = new BaseFilter { CurrentPage = pageNumber, ItemsPerPage = pageSize };
            return base.GetItemsByFilter(baseFilter, type == null ? null : Query.EQ("detail._t", type), SortBy.Descending("date"));
        }

        public IEnumerable<Report> GetByUserId(string userId, int pageNumber, int pageSize)
        {
            var baseFilter = new BaseFilter { CurrentPage = pageNumber, ItemsPerPage = pageSize };
            var query = Query<Report>.EQ(x => x._userId, userId);
            return base.GetItemsByFilter(baseFilter, query, SortBy.Descending("date"));
        }

        public models.Comment AddComment(string id, Comment comment)
        {
            var query = Query<Report>.EQ(x => x._id, id);
            
            var updateBuilder = new UpdateBuilder<Report>();
            updateBuilder.AddToSet(x => x.comments, comment);
                        
            LastWriteConcernResult = Collection.Update(query, updateBuilder);
            Succeed = LastWriteConcernResult.Ok;
            if (LastWriteConcernResult.HasLastErrorMessage)
            {
                AddValidationMessage(enumMessageType.UnhandledException, LastWriteConcernResult.ErrorMessage);
            }
            return comment;
        }

        public UserAlert SetAlert(string id, UserAlert userAlert)
        {
            var updateBuilder = new UpdateBuilder<Report>();
            if (userAlert.alert)
            {
                updateBuilder.AddToSet(x => x.alertTo, userAlert._userId);
            }
            else
            {
                updateBuilder.Pull(x=>x.alertTo, userAlert._userId);
            }

            var query = Query<Report>.EQ(x => x._id, id);
            LastWriteConcernResult = Collection.Update(query, updateBuilder);
            Succeed = LastWriteConcernResult.Ok;
            if (LastWriteConcernResult.HasLastErrorMessage)
            {
                AddValidationMessage(enumMessageType.UnhandledException, LastWriteConcernResult.ErrorMessage);
            }
            return userAlert;
        }

        public bool SetView(string id, string userId)
        {
            var updateBuilder = new UpdateBuilder<Report>();
            updateBuilder.AddToSet(x => x.viewedBy,userId);

            var query = Query<Report>.EQ(x => x._id, id);

            return Update(updateBuilder, query);
        }
    }
}