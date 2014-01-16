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
    public class UsersRepository : ObjectRepositoryBase<User>
    {
        public UsersRepository()
            : base()
        {
        }

    }
}