using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using webcore_app.Core.Interfaces;


namespace webcore_app.Core
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable
        where TContext : Database.AppContext
    {

        private bool _disposed = false;
        private string _errorMessage = string.Empty;
        private IDbContextTransaction _objTran;

        private Dictionary<string, object> _repositories;
        private TContext _context;

        public TContext Context
        {
            get { return _context; }
        }

        public UnitOfWork(TContext context)
        {
            _context = context;
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void CreateTransaction()
        {
            _objTran = Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _objTran.Commit();
        }

        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }
        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                _errorMessage = dbEx.Message;
                throw new Exception(_errorMessage, dbEx);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Context.Dispose();
            _disposed = true;
        }

     
    }
}
