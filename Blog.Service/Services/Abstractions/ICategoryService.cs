using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Entity.DTOS.Categories;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Abstractions
{
    public interface ICategoryService
    {
        public Task<List<CategoryDTO>> getAllCategoriesNonDeleted(); 

    }
}
