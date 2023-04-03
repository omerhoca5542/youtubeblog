using Blog.Entity.DTOS.Articles;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Service.Services.Abstractions

{
    public  interface IArticleService
    { //asenkron metotlarda task kullanılır void yerine
        Task<List<ArticleDTO>> GetAllArticlesWithCategoryNonDeletedAsync();
    }
}
