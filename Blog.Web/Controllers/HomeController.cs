using Blog.Service.Services.Abstractions;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService articleService;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService)// Bu kısımda ek olarak IArticleService sınıfını referans olarak çağırdık ve articleService nesnesini oluşturduk .sonrasında onu da üzerine gelerek ctrl+. ile gelen seçeneklerden field olarak ekledik
        {
            _logger = logger;
            this.articleService = articleService;
        }

        public async Task <IActionResult> Index()// bu kısmıda hem async yaptık hem de task ekledik yani void anlamında.IActionResult uda küçük büyük işaretleri arasına aldık
        {
            var articles = await articleService.GetAllArticlesWithCategoryNonDeletedAsync();
            //tüm article ları articles adında bir değişkende topladık. articleService nesnesi ile  IArticleService içinde yazdığımız ve içeriğini ArticleService de doldurduğumuz GetAllArticleAsync(); metodunu çağırdık
            return View(articles);// articles dan gelen verileri index sayfasına gönderiyoruz
        }

        public  IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}