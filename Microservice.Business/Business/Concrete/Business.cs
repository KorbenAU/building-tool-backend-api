using Microservice.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Business.Business.Concrete
{
    public class Business : IBusiness
    {
        public ISample Sample { get; }


        public Business(IRepositories repositories)
        {

            Sample = new Sample(repositories);
        }

       
    }
}
