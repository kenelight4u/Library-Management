using LM.Application.Interfaces.Persistence;
using LM.Application.Interfaces.Services;
using LM.Application.Interfaces.Utilities;
using LM.Persistence.Context;
using LM.Persistence.Implementation;
using LM.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Persistence
{
    /// <summary>
    /// This class registers all services for easy and clean registration in the Startup class.
    /// Ensure that all services are registered here.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// A static method for service registration in the Startup.cs Class.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryManagementDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("LMDBConnection"),
                    b => b
                    .MigrationsAssembly(typeof(LibraryManagementDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IStoreManager<>), typeof(StoreManager<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(EfCoreUnitOfWork));

            services.AddScoped<IBookGenresService, BookGenreService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ITransactionsService, TransactionsService>();

        }
        
    }
}
