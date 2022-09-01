using LM.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Persistence.Implementation
{
    /// <summary>
    /// Class EfCoreUnitOfWork.
    /// Implements the <see cref="NIBSSCRM.Core.DataAccess.EfCore.UnitOfWork.IUnitOfWork" />
    /// </summary>
    /// <seealso cref="NIBSSCRM.Core.DataAccess.EfCore.UnitOfWork.IUnitOfWork" />
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The context
        /// </summary>
        public readonly DbContext _context;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfCoreUnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EfCoreUnitOfWork(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public void BeginTransaction()
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            if (_context.Database.GetDbConnection().State != ConnectionState.Open)
                _context.Database.OpenConnection();

            _context.Database.BeginTransaction();
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            _context.ChangeTracker.DetectChanges();

            SaveChanges();
            _context.Database.CommitTransaction();
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing) _context.Dispose();
            _disposed = true;
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            _context.Database.CurrentTransaction?.Rollback();
        }

        /// <summary>
        /// Gets the or create database context.
        /// </summary>
        /// <typeparam name="TDbContext">The type of the t database context.</typeparam>
        /// <returns>TDbContext.</returns>
        public virtual TDbContext GetOrCreateDbContext<TDbContext>()
            where TDbContext : DbContext
        {
            return (TDbContext)_context;
        }

    }
}
