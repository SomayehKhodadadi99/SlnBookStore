using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookDetailvw
    {
        public Book mybook { get; set; }
        public long BookId { get; set; }
        public BookDetail BookDetail { get; set; }
    }
}
