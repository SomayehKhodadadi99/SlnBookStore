using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuthorBookVM
    {
        public Book Book { get; set; }

        public IEnumerable<SelectListItem> AuthorList { get; set; }

        public AuthorBook AuthorBook { get; set; }

        public IEnumerable<AuthorBook> BookAuthorList { get; set; }
    }
}
