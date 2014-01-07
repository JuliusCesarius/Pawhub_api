using blastic.mongodb.clases;
using blastic.pawhub.models.LostAndFound;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using Pawhub_API.Models.MongoUtilities;
using System.Collections.Generic;

namespace blastic.pawhub.repositories
{
    public class ReportsRepository : ObjectRepositoryBase<Report>
    {
        public ReportsRepository()
            : base()
        {

        }
        public IEnumerable<Report> ListByPageAndType(short pageNumber, short pageSize, string type)
        {
            type = type.Substring(0, 1).ToUpper() + type.Substring(1, type.Length - 1).ToLower();
            var baseFilter = new BaseFilter { CurrentPage = pageNumber, ItemsPerPage = pageSize };
            return base.GetItemsByFilter(baseFilter, type == null ? null : Query.EQ("detail._t", type), SortBy.Descending("_id"));
        }
    }
}