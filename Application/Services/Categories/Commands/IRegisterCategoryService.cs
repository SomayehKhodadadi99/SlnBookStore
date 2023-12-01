using Common.ResultDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Commands
{
    public interface IRegisterCategoryService
    {
        ResultDto<ResultRegisterCategoryDto> RegisterCategories(RequestRegisterCategoryDto req);
    }
}
