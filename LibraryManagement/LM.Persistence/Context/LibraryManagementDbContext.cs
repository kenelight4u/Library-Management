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
    }
}
