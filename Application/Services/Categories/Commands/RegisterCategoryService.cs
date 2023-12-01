using Application.Interfaces.Contexts;
using Common.ResultDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Commands
{
    public class RegisterCategoryService : IRegisterCategoryService
    {
        private readonly IDataBaseContext _dbContextServices;
        public RegisterCategoryService(IDataBaseContext dbContextServices)
        {
            _dbContextServices = dbContextServices;
        }


        public ResultDto<ResultRegisterCategoryDto> RegisterCategories(RequestRegisterCategoryDto req)
        {
            try
            {
                if (string.IsNullOrEmpty(req.CategoryName))
                {
                    return new ResultDto<ResultRegisterCategoryDto>
                    {
                        IsSuccess = false
                        ,
                        Message = "لطفا نام گروه را مشخص نمایید"
                        ,
                        Data = new ResultRegisterCategoryDto { CategoryId = 0 }
                    };
                }
                Category catProxy = new Category();
                catProxy.CategoryName = req.CategoryName;
                _dbContextServices.Categories.Add(catProxy);
                _dbContextServices.SaveChanges();

                return new ResultDto<ResultRegisterCategoryDto>()
                {
                    Data = new ResultRegisterCategoryDto { CategoryId = catProxy.Id },
                    IsSuccess = true
                     ,
                    Message = "عملیات موفقیت آمیز بود"

                };
                
            }
            catch (Exception ex) 
            {
             return new ResultDto<ResultRegisterCategoryDto> { IsSuccess = false
                 , Message = ex.Message
                 , Data = new ResultRegisterCategoryDto { CategoryId = 0 } };
            }

        }
    }
}
