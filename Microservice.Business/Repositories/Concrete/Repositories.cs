using Microservice.Database;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Business.Repositories.Concrete
{
    public class Repositories : IRepositories
    {
        private DatabaseContext _dbContext { get; set; }

        public ISampleRepository SampleRepository { get; set; }

        public Repositories(DatabaseContext DbContext)
        {
            _dbContext = DbContext;

            SampleRepository = new SampleRepository(_dbContext);

        }

        public void CommitChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error writing to database");
            }
        }
    }
}
