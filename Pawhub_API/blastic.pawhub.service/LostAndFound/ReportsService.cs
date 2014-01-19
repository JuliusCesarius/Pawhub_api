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

        public IEnumerable<Report> Get(string type, int? pageNumber, int pageSize)
        {
            pageSize = pageSize > 0 ? pageSize : 20;
            if (pageNumber != null && pageNumber > 0)
            {
                if (!string.IsNullOrEmpty(type))
                {
                    return ((ReportsRepository)repository).ListByPageAndType((int)pageNumber, pageSize, type);
                }
                else
                {
                    return repository.ListByPage((int)pageNumber, pageSize);
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

        public Report GetById(string id)
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

        public bool Delete(string objectId)
        {
            return repository.Delete(objectId);
        }

        public IEnumerable<Report> Get(int? pageNumber, int pageSize)
        {
            return this.Get(null, pageNumber, pageSize);
        }

        public IEnumerable<Report> GetByUserId(string id, int pageNumber = 1)
        {
            pageNumber = pageNumber > 0 ? pageNumber : 20;
            //TODO: Obtener este valor de una configuración
            var pageSize = 20;
            return ((ReportsRepository)repository).GetByUserId(id, pageNumber, pageSize);
        }

        public Comment Comment(string id, Comment comment)
        {
            //Verifica si existe el reporte y si está inicializado el arreglo de comentarios
            Report report = ((ReportsRepository)repository).GetFields(id,new string[] { "comments" }).FirstOrDefault();
            comment._id = ObjectId.GenerateNewId().ToString();
            comment.date = DateTime.UtcNow;

            if (report == null)
            {
                throw new Exception("Report not found");
            }
            if (report.comments == null)
            {
                report.comments = new List<Comment>();
                report.comments.Add(comment);
                repository.Update(report);
                return comment;
            }

            return ((ReportsRepository)repository).AddComment(id, comment);
        }

        public UserAlert SetAlert(string id, UserAlert userAlert)
        {
            userAlert._reportId = id;
            return ((ReportsRepository)repository).SetAlert(id, userAlert);
        }

        public long SetView(string id, string userId)
        {
            var success = ((ReportsRepository)repository).SetView(id, userId);
            if (success)
            {
                var report = repository.LoadById(id);
                if (report == null)
                {
                    throw new Exception("Report not found");
                }
                return report.viewedBy.Count;
            }
            else
            {
                return -1;
            }
        }
    }
}
