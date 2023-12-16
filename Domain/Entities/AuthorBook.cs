using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuthorBook
    {

        public long BookId { get; set; } 
        public long AuthorId { get; set; }

        public Author Author { get; set; }

        public Book Book { get; set; }
    }
}
