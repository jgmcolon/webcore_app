using System.Collections.Generic;

namespace webcore_app.Core.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        IEnumerable<TEntity> All();

        TEntity Find(int key);
        TEntity Find(TKey key);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TKey key);
    }
}