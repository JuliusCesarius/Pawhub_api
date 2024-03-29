﻿using blastic.mongodb.interfaces;
using blastic.pawhub.models.LostAndFound;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace blastic.pawhub.service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService<T>:IDisposable
    {
        IObjectRepository<T> repository { get; }

        [OperationContract]
        IEnumerable<T> Get(int? pageNumber, int pageSize);

        [OperationContract]
        T GetById(string id);

        [OperationContract]
        bool Save(T obj);
    }

}
