using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Queries
{
    public  interface IGetCategoryService
    {

        ResultCategoryDto GetCategory(RequestCategoryDto req);
    }
}
