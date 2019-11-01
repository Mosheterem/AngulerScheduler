using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.EF.Model
{
    public class LoginDataContext : DbContext
    {
        public LoginDataContext(DbContextOptions<LoginDataContext> options)
            : base(options)
        {
        }

        public DbSet<CaltUsers> CaltUsers { get; set; }

    }
}
