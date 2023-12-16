using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        [MaxLength(20)]
        [Required]
        public string ISBN { get; set; }

        public decimal Price { get; set; }
        [NotMapped]
        public BookDetail BookDetail { get; set; }
        public long PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; set; }

    }
}
