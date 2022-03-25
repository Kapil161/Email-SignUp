using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdminMaster.Models
{
    public class MSDBContext:DbContext
    {
       // public MSDBContext() : base() { }
       public DbSet<Tbl_users> Tbl_Users { get; set; }
        public DbSet<VerifyAccount> VerifyAccounts { get; set; }
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DBConnection.ConnectionStr);
        }
    }
}
