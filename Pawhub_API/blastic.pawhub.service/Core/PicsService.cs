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
    public class PicturesService : IService<Picture>
    {
        private Lazy<IObjectRepository<Picture>> _repository = new Lazy<IObjectRepository<Picture>>(() => new PicsRepository());

        public IObjectRepository<Picture> repository
        {
            get
            {
                return _repository.Value;
            }
        }

        public IEnumerable<Picture> Get(int? pageNumber, int pageSize)
        {
            pageSize = pageSize > 0 ? pageSize : 20;
            if (pageNumber != null && pageNumber > 0)
            {
                return repository.ListByPage((int)pageNumber, pageSize);
            }
            else
            {
                return repository.ListAll();
            }
        }

        public Picture GetById(string id)
        {
            return repository.LoadById(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool Save(Picture picture)
        {
            return repository.Insert(picture);
        }

        public bool Update(Picture picture)
        {
            return repository.Update(picture);
        }

        public bool Delete(string objectId)
        {
            return repository.Delete(objectId);
        }

    }
}
