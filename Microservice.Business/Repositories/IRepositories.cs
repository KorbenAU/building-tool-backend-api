using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Business.Repositories
{
    public interface IRepositories
    {
        ISampleRepository SampleRepository { get; set; }
        void CommitChanges();
    }
}
