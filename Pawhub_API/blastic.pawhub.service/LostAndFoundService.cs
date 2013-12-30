using blastic.mongodb.interfaces;
using blastic.pawhub.models.LostAndFound;
using blastic.pawhub.repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blastic.pawhub.service
{
    public class LostAndFoundService : ILostAndFoundService
    {
        private Lazy<IObjectRepository<Report>> _reportsRepository = new Lazy<IObjectRepository<Report>>(() => new ReportsRepository());
        private IObjectRepository<Report> ReportsRepository
        {
            get
            {
                return _reportsRepository.Value;
            }
        }

        public IEnumerable<Report> GetReports(string type, short? pageNumber, short pageSize)
        {
            if (pageNumber != null && pageNumber > -1)
            {
                if (!string.IsNullOrEmpty(type))
                {
                    return ((ReportsRepository)ReportsRepository).ListByPageAndType((short)pageNumber, pageSize, type);
                }
                else
                {
                    return ReportsRepository.ListByPage((short)pageNumber, pageSize);
                }
            }
            else
            {
                return ReportsRepository.ListAll();
            }
        }

        public Report GetReportById(ObjectId id)
        {
            return ReportsRepository.LoadById(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool SaveReport(Report report)
        {
            return ReportsRepository.Insert(report);
        }

        public bool UpdateReport(Report report)
        {
            return ReportsRepository.Update(report);
        }
    }
}
