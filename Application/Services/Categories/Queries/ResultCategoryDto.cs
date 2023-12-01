using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Queries
{
    public class ResultCategoryDto
    {
        
        public List<GetCategoryDto> lstCategory { get; set; }

        public int Rows { get; set; }
    }
}
