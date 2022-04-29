using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace webcore_app.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
        T GetById(Guid id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        
    }
}