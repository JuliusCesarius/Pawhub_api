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
            var baseFilter = new BaseFilter { CurrentPage = pageNumber, ItemsPerPage = pageSize };
            return base.GetItemsByFilter(baseFilter, Query.EQ("detail.name",type.ToLowerInvariant()), SortBy.Descending("_id"));
        }
    }
}