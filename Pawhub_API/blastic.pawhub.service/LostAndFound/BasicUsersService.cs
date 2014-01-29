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

namespace blastic.pawhub.service.core
{
    public class BasicUsersService : IService<BasicUser>
    {
        private Lazy<IObjectRepository<BasicUser>> _repository = new Lazy<IObjectRepository<BasicUser>>(() => new BasicUsersRepository());

        public IObjectRepository<BasicUser> repository
        {
            get
            {
                return _repository.Value;
            }
        }

        public IEnumerable<BasicUser> Get(int? pageNumber, int pageSize)
        {
            pageSize = pageSize > 0 ? pageSize : (int)20;
            if (pageNumber != null && pageNumber > 0)
            {
                return repository.ListByPage((int)pageNumber, pageSize);
            }
            else
            {
                return repository.ListAll();
            }
        }

        public BasicUser GetById(string id)
        {
            return repository.LoadById(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool Save(BasicUser basicUser)
        {
            return repository.Insert(basicUser);
        }

        public bool Update(BasicUser basicUser)
        {
            return repository.Update(basicUser);
        }

        public bool Delete(string objectId)
        {
            return repository.Delete(objectId);
        }

        //public bool SaveBasicUser(BasicUser basicUser)
        //{
        //    //Checo que no se repita el username o el email
        //    var exist = ((UsersRepository)UsersRepository).ExitsBasicUser(basicUser.userName, basicUser.userEmail);
        //    if (exist)
        //    {
        //        throw new Exception("El usuario ya existe");
        //    }

        //    return ((UsersRepository)UsersRepository).InsertBasicUser(basicUser);
        //}

        //public bool SaveUser(User value)
        //{
        //    return UsersRepository.Insert(value);
        //}

        //public User GetBasicUserById(ObjectId id)
        //{
        //    return UsersRepository.LoadBasicUserById(id);
        //}

        //public User GetUserById(ObjectId id)
        //{
        //    return UsersRepository.LoadById(id);
        //}

    }
}
