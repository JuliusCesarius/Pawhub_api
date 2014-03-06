using blastic.mongodb.interfaces;
using blastic.pawhub.models;
using blastic.pawhub.models.Core;
using blastic.pawhub.models.Enums;
using blastic.pawhub.models.LostAndFound;
using blastic.pawhub.models.Register;
using blastic.pawhub.repositories;
using blastic.pawhub.service.Helpers;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blastic.pawhub.service.core
{
    public class PushDevicesService : IService<PushDevice>
    {
        private Lazy<IObjectRepository<PushDevice>> _repository = new Lazy<IObjectRepository<PushDevice>>(() => new PushDevicesRepository());

        public IObjectRepository<PushDevice> repository
        {
            get
            {
                return _repository.Value;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool Save(PushDevice picture)
        {
            return repository.Insert(picture);
        }

        public bool Update(PushDevice picture)
        {
            return repository.Update(picture);
        }

        public bool Delete(string objectId)
        {
            return repository.Delete(objectId);
        }

        public IEnumerable<Picture> Get(int? pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
        
        IEnumerable<PushDevice> IService<PushDevice>.Get(int? pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public PushDevice GetById(string id)
        {
            throw new NotImplementedException();
        }

        public PushDevice GetByDeviceId(string id)
        {
            return ((PushDevicesRepository)repository).GetByDeviceId(id);
        }
    }
}
