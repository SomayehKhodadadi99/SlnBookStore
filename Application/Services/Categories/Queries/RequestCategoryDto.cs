using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Queries
{
    public class RequestCategoryDto
    {
        public string search { get; set; }

        public int Page { get; set; }
    }
}
