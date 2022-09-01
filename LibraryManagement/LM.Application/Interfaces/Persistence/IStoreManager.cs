using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Application.Interfaces.Persistence
{
    /// <summary>
    /// This manages all the activities of the DataStore.
    /// It helps save changes to all tables in a staged context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStoreManager<T> where T : class
    {
        /// <summary>
        /// Generic repository that take any entity type.
        /// It enables us to perform all data operations on any data table in the database.
        /// </summary>
        IRepository<T> DataStore { get; }

        /// <summary>
        /// This saves all changes made to the currently staged context.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChanges();
    }
}
