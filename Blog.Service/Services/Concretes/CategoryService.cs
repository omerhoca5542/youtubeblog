using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOS.Categories;
using Blog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly UnitOfWork unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<List<CategoryDTO>> GetAllCategoriesNonDeleted()
        {
var categories= unitOfWork     
                
                }
    }
}
