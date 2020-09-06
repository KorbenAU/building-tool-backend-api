using Microservice.Database;
using Microservice.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Business.Repositories.Concrete
{
    public class SampleRepository : ISampleRepository
    {
        private DatabaseContext _dbContext;

        public SampleRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Database.Entities.Sample entity)
        {
            _dbContext.Samples.Add(entity);
        }
    }
}
