using Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Categories.Queries
{
    public class GetCategoryService:IGetCategoryService
    {

        private readonly IDataBaseContext context;
        public GetCategoryService(IDataBaseContext _context)
        {
            context = _context;
        }

        public ResultCategoryDto GetCategory(RequestCategoryDto req)
        {
            var Categories = context.Categories.AsQueryable();
            if (string.IsNullOrEmpty(req.search) == false)
            {
                Categories = Categories.Where(x => x.CategoryName.Contains(req.search));
            }
                var userList= Categories.Select(p=>new GetCategoryDto { CategoryName=p.CategoryName,Id=p.Id}).ToList();

                return new ResultCategoryDto { lstCategory = userList, Rows = 1 };

        }
    }
}
