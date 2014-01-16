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
    public class ReportsService : IService<Report>
    {
        private Lazy<IObjectRepository<Report>> _repository = new Lazy<IObjectRepository<Report>>(() => new ReportsRepository());

        public IObjectRepository<Report> repository
        {
            get
            {
                return _repository.Value;
            }
        }

        public IEnumerable<Report> Get(string type, short? pageNumber, short pageSize)
        {
            pageSize = pageSize > 0 ? pageSize : (short) 20;
            if (pageNumber != null && pageNumber > 0)
            {
                if (!string.IsNullOrEmpty(type))
                {
                    return ((ReportsRepository)repository).ListByPageAndType((short)pageNumber, pageSize, type);
                }
                else
                {
                    return repository.ListByPage((short)pageNumber, pageSize);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(type))
                {
                    return repository.ListAll();
                }
                else
                {
                    return ((ReportsRepository)repository).ListByPageAndType(1, pageSize, type);
                }
            }
        }

        public Report GetById(ObjectId id)
        {
            return repository.LoadById(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool Save(Report report)
        {
            return repository.Insert(report);
        }

        public bool Update(Report report)
        {
            return repository.Update(report);
        }

        public bool Delete(ObjectId objectId)
        {
            return repository.Delete(objectId);
        }

        public IEnumerable<Report> Get(short? pageNumber, short pageSize)
        {
            return this.Get(null, pageNumber, pageSize);
        }
    }
}
