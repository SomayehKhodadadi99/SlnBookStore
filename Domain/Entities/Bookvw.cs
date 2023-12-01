using Domain.Commons;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bookvw:BaseEntity
    {
        public Book Book { get; set; }

       // public IEnumerable<selectei MyProperty { get; set; }

        public IEnumerable<SelectListItem> PublisherList { get; set; }
    }
}
