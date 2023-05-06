using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOS.Categories;
using Blog.Service.Services.Abstractions;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Blog.Service.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<List<CategoryDTO>> getAllCategoriesNonDeleted()
        {
            var categories= await unitOfWork.GetRepository<Category>().GetAllAsync(x=>!x.IsDeleted);
            var map = mapper.Map<List<CategoryDTO>>(categories);
            return map;
        }
    }
}
