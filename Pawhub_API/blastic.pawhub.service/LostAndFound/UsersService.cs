using blastic.mongodb.interfaces;
using blastic.pawhub.models;
using blastic.pawhub.models.LostAndFound;
using blastic.pawhub.models.Register;
using blastic.pawhub.repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blastic.pawhub.service.lostAndFound
{
    public class UsersService : IService<User>
    {
        private Lazy<IObjectRepository<User>> _repository = new Lazy<IObjectRepository<User>>(() => new UsersRepository());

        public IObjectRepository<User> repository
        {
            get
            {
                return _repository.Value;
            }
        }

        public IEnumerable<User> Get(short? pageNumber, short pageSize)
        {
            pageSize = pageSize > 0 ? pageSize : (short)20;
            if (pageNumber != null && pageNumber > 0)
            {
                return repository.ListByPage((short)pageNumber, pageSize);
            }
            else
            {
                return repository.ListAll();
            }
        }

        public User GetById(ObjectId id)
        {
            return repository.LoadById(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool Save(User user)
        {
            return repository.Insert(user);
        }

        public bool Update(User user)
        {
            return repository.Update(user);
        }

        public bool Delete(ObjectId objectId)
        {
            return repository.Delete(objectId);
        }

    }
}
