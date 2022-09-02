using LM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Persistence.Context
{
    public class LibraryManagementDbContext : IdentityDbContext<LMUser>
    {

        public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options) : base(options)
        {
            this.Database.Migrate();
        }

        public DbSet<LMUser> LMUsers { get; set; }

        public DbSet<Book> Bookss { get; set; }

        public DbSet<BookHistory> BookHistories { get; set; }

        public DbSet<BookGenres> BookGenres { get; set; }

        public DbSet<BookInventory> BookInventories { get; set; }
    }
}
