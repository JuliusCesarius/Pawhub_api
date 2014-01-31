using blastic.mongodb.interfaces;
using blastic.pawhub.models;
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
            //path += "\\" + id + ".jpg";
            if (!File.Exists(path))
            {
                //TODO: No Mandar info de la imagen (solo para loggeo)
                throw new FileNotFoundException("File " + path + " not found");
            }
            fileStream = File.OpenRead(path);
            return fileStream;
        }

        public void EnsureDirectory(string path)
        {
            try
            {
                //Check if the path exists
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                //TODO: Manejar este caso
            }
        }

        public void DeleteFile(string path)
        {
            //Check if the path exists
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public string Save(PicType type, string id, string tempFile, string path)
        {
            var picture = new Picture
            {
                _referenceId = id,
                date = DateTime.UtcNow,
                type = type
            };
            var succeed = this.Save(picture);
            if (!succeed)
            {
                throw new Exception("No se pudo guardar");
            }

            var fileStream = File.OpenRead(tempFile);

            var origPath = path + "\\" + type.ToString() + "\\orig";
            var smallPath = path + "\\" + type.ToString() + "\\small";
            var midPath = path + "\\" + type.ToString() + "\\mid";
            var bigPath = path + "\\" + type.ToString() + "\\big";

            var fileName = "\\" + picture._referenceId;

            EnsureDirectory(origPath);
            EnsureDirectory(smallPath);
            EnsureDirectory(midPath);
            EnsureDirectory(bigPath);

            //Saves the file
            //using (Stream file = File.Create(origPath + fileName))
            //{
            //    FileStreamHelper.CopyStream(fileStream, file);
            //}

            var bitmap = new Bitmap(fileStream);
            var imageHandler = new ImageHandler();

            //origin
            imageHandler.Save(bitmap, 1900, 1900, 90, origPath + fileName);
            //small
            imageHandler.Save(bitmap, 50, 50, 60, smallPath + fileName);
            //mid
            imageHandler.Save(bitmap, 700, 700, 60, midPath + fileName);
            //big
            imageHandler.Save(bitmap, 1024, 1024, 60, bigPath + fileName);

            picture.path = fileName;
            this.Update(picture);
            return picture._id;

        }
    }
}
