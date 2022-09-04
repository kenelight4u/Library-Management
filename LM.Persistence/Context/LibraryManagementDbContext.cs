using LM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LM.Persistence.Context
{
    public class LibraryManagementDbContext : IdentityDbContext<LMUser>
    {

        public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options) : base(options)
        {
            this.Database.Migrate();
        }

        public DbSet<LMUser> LMUsers { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookHistory> BookHistories { get; set; }

        public DbSet<BookGenres> BookGenres { get; set; }

        public DbSet<BookInventory> BookInventories { get; set; }

        /// <summary>
        /// Helps to seed Data in the Database
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
