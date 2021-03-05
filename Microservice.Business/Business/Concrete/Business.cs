using Microservice.Business.Repositories;
using Microservice.Database.Entities;
using System;

namespace Microservice.Business.Business.Concrete
{
    public class Business : IBusiness
    {
        private readonly IDatabaseRepository<BaseEntity> _repositories;

        public Business(IDatabaseRepository<BaseEntity> repositories)
        {
            _repositories = repositories;
        }

        public string WriteToDatabase()
        {
            var entity = new Database.Entities.BaseEntity();
            _repositories.Create(entity);

            return "OK";
        }

        public int AddNumbers(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }

    }
}
