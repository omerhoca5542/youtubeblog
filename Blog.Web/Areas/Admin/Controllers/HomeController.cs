using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]//[Authorize] özelliği sayesinde kullanıcı giriş yaptıysa tüm listeyi görebilir.
    // sonuçta bu da bir home controller olduğu için diğer Controllers klasörünün altında da aynı isimle var bu yüzden bu kodla Area olarak Adminin altında olduğunu belirtmiş ollduk
    public class HomeController : Controller
    {
        private readonly IArticleService articleService;

        public HomeController(IArticleService articleService)// burdaki constructure yani yapıcı metod ile  IArticle Service den articleservice adında nesne oluşturduk onu da solda çıkan tornavida işaretinden :)  field yaptık.
        {
            this.articleService = articleService;
        }
        public async Task <IActionResult> Index()
        {
            var articles = await articleService.GetAllArticlesWithCategoryNonDeletedAsync();// articleservice nesnesi ile GetAllArticleAsync() metodunu çağırdık.o metodda bütün article yani türkçesi makaleleri getirmiş olduk
            return View(articles);// return ile gelen bütün değerleri articles değişkeniyle gelen tüm değerleri gösterebilmek adına gönderdik. kaldı sırada model kısmı 
        }
    }
}
