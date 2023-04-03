using AutoMapper;
using Blog.Entity.DTOS.Categories;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Categories
{
    public class CategoryProfile: Profile//Direk AutoMapper sınıfından miras aldık
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            //Createmap mapleme fonksiyonu.categoryDTO ile Category arasında birbirlerinden mapleme yapmak içinde ReverseMap komutunu 
        }
    }
}
