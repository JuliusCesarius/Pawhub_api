using blastic.mongodb.interfaces;
using blastic.pawhub.models;
using blastic.pawhub.models.LostAndFound;
using blastic.pawhub.models.Register;
using blastic.pawhub.repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blastic.pawhub.service.core
{
    public class PicturesService : IService<Picture>
    {
        private Lazy<IObjectRepository<Picture>> _repository = new Lazy<IObjectRepository<Picture>>(() => new PicturesRepository());

        public IObjectRepository<Picture> repository
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

        public IEnumerable<Picture> Get(int? pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Picture GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IDisposable GetImageStream(string size, int density, string id, out FileStream fileStream)
        {
            var path = ((PicturesRepository)repository).GetPath(id);
            //TODO: Terminar de armar la ruta según los parámetros (size, debería ser enum)
            path += "\\" + id + ".jpg";
            if (!File.Exists(path))
            {
                //TODO: No Mandar info de la imagen (solo para loggeo
                throw new FileNotFoundException("File " + path + " not found");
            }
            fileStream = File.OpenRead(path);
            return fileStream;
        }
    }
}
