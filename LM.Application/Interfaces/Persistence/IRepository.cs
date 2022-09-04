using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LM.Application.Interfaces.Persistence
{
    /// <summary>
    /// Generic repository that take any entity type.
    /// It enables us to perform all data operations on any data table in the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// This method creates an object of type T which is passed to it in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Add(T entity);

        /// <summary>
        /// This method creates multiple objects of type T which is passed to it in the database.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddRange(List<T> entities);

        /// <summary>
        /// This method updates an object of type T which is passed to it in the database.
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// This method updates an object of type T which is passed to it in the database.
        /// </summary>
        /// <param name="entity"></param>
        void UpdateRange(List<T> entity);

        /// <summary>
        /// This removes a record whose primary key 'ID' is passed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid ID);

        /// <summary>
        /// This removes a record whose primary key 'Id' is passed.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> Delete(long Id);

        /// <summary>
        /// This returns a single record whose primary key 'ID' is passed.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<T> GetById(Guid ID);

        /// <summary>
        /// This returns a single record whose primary key 'Id' is passed.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<T> GetById(long Id);

        /// <summary>
        /// This method returns all entities whose type T is passed.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// This method returns collection of entities of type T as LINQ queryable.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAllQuery();

        /// <summary>
        /// This method uses the LINQ expression to search through the database for the type T that matches the passed lambda expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
    }
}
