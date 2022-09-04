using LM.Application.Interfaces.Persistence;
using LM.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Persistence.Implementation
{
    public class StoreManager<T> : IStoreManager<T> where T : class
    {
        protected readonly LibraryManagementDbContext context;

        IRepository<T> store;

        public StoreManager(LibraryManagementDbContext lMdbContext)
        {
            this.context = lMdbContext;
        }
        public IRepository<T> DataStore => store ??= new Repository<T>(context);

        //public async Task<int> SaveChanges()
        //{
        //    return await context.SaveChangesAsync();
        //}
    }
}
