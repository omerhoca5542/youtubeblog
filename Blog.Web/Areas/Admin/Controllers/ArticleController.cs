using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOS.Articles;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]// article controllerın hangi areada olduğunu yazmamız lazım
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;// burada da field oluşturduk
        private readonly ICategoryService categoryService;

      

        public ArticleController(IArticleService articleService, ICategoryService categoryService)// Iarticle servisi çağırdık
        {
            this.articleService = articleService;
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var articles = await articleService.GetAllArticlesWithCategoryNonDeletedAsync();
            // burda article service içindeki GetAllArticlesWithCategoryNonDeletedAsync metodunu çağırdık.o metot da aşağıda açıklama olarak yazılan kısım
            //public async Task<List<ArticleDTO>> GetAllArticleAsync()// burada bize article türünden liste olarak veriler gelecek
            //{
            //    var articles = await unitOfWork.GetRepository<Article>().GetAllAsync();
            //    var map = mapper.Map<List<ArticleDTO>>(articles);

            //    return map;
            //    // yukarıda unitofwork adında bir nesne oluşturmuştuk.şimdi o nesneyle Getrepository komutunu kullanarak article türünden verileri GettAllAsync metodunu kullanarak çağırabiliriz 
            //}//şimdi yapıları oluşturduk  bunları çağırmak için BlogWeb içinde bulunan Controller kısmına gidicez

            return View(articles);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {

            return View();

        }
    }
}
