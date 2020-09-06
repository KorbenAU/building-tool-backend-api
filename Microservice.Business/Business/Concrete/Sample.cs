using Microservice.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Business.Business.Concrete
{
    public class Sample : ISample
    {
        IRepositories _repositories;
        public Sample(IRepositories Repositories)
        {
            _repositories = Repositories;
        }

        public string WriteToDatabase()
        {
            var entity = new Database.Entities.Sample()
            {
                SampleColumn = $"Sample entry from {DateTime.Now.ToString()}"
            };
            _repositories.SampleRepository.Add(entity);
            _repositories.CommitChanges();

            return "OK";
        }

        public int AddNumbers(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }
    }
}
