using blastic.mongodb.clases;
using blastic.patterns.enums;
using blastic.pawhub.models;
using blastic.pawhub.models.LostAndFound;
using blastic.pawhub.models.Register;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;

namespace blastic.pawhub.repositories
{
    public class BasicUsersRepository : ObjectRepositoryBase<BasicUser>
    {
        public BasicUsersRepository()
            : base()
        {
        }

        public bool DoesExit(string userName, string userEmail)
        {
            var query = Query.Or (
                Query<BasicUser>.EQ(x => x.userName, userName),
                Query<BasicUser>.EQ(x => x.userEmail, userEmail)
                );
            return Collection.Count(query) > 0;
        }
    }
}