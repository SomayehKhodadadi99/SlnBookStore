using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {
         DbSet<Book> Books { get; set; }
         DbSet<Category> Categories { get; set; }
         DbSet<Author> Authors { get; set; }
         DbSet<SubCategory> SubCategories { get; set; }
         DbSet<Publisher> Publishers { get; set; }
         DbSet<BookDetail> BookDetails { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

    }
}
