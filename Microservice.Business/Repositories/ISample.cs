using Microservice.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Business.Repositories
{
    public interface ISampleRepository
    {
        void Add(Database.Entities.Sample entity);
    }
}
