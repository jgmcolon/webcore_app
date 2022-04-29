using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using webcore_app.Core;
using webcore_app.Core.Database;


namespace webcore_app.Core.Interfaces
{
    public interface IUnitOfWork<out TContext>
        where TContext : DbContext
    {
        
           

        TContext Context { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();

    }
}
