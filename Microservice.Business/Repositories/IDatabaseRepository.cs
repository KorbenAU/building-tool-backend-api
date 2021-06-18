using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microservice.Database.Entities;

namespace Microservice.Business.Repositories
{
    public interface IDatabaseRepository<T> where T : BaseEntity
    {
        void Create(T entity);

        T Read(int id);

        IEnumerable<T> Read();

        IEnumerable<T> Read(Expression<Func<T, bool>> predicate);

        int Count();

        int Count(Expression<Func<T, bool>> predicate);

        void Update(T entity);

        void Delete(int id);

        T ReadOne(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties);
    }
}