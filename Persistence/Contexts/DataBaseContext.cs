using Application.Interfaces.Contexts;
using Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Category>().HasData(new Category { Id = 1, CategoryName=nameof(ConstCategory.Mobile) });

            //modelBuilder.Entity<Category>().HasData(new Category { Id = 2, CategoryName = nameof(ConstCategory.Food) });

            //modelBuilder.Entity<Category>().HasData(new Category { Id = 3, CategoryName = nameof(ConstCategory.Poshak) });

            base.OnModelCreating(modelBuilder);
        }
    }

   
}
