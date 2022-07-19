using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class LibraryAppContext : DbContext
    {
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public LibraryAppContext()
        {
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyLibraryDB;Trusted_Connection=True;");
        }
    }
}
