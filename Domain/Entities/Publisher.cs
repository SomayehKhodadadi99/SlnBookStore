using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Publisher : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<Author> Authors { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
