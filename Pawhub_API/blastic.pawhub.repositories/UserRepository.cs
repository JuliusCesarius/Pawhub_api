using blastic.mongodb.clases;
using blastic.pawhub.models;
using blastic.pawhub.models.LostAndFound;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System.Collections.Generic;

namespace blastic.pawhub.repositories
{
    public class UsersRepository : ObjectRepositoryBase<User>
    {
        public UsersRepository()
            : base()
        {
        }
    }
}