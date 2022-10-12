using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AutoWPF.DB
{
    public class MyContext  : DbContext
    {
        private string connectString = "server=85.236.170.148,444;database=DbTest2349; user id=stud; password=stud;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectString);

        }

        public DbSet<User> Users { get; set; }
            
    }
}
