using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOS.Articles;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Extensions;
using Blog.Web.ResultMessage;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel.Charts;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]// article controllerın hangi areada olduğunu yazmamız lazım
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;// burada da field oluşturduk
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IValidator<Article> validator;
        private readonly IToastNotification toast;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper, IValidator<Article> validator,IToastNotification toast)// Iarticle servisi çağırdık.IVlidatorı Fluentvalidation sınıfından aldık.validasyon ayarları yani kural koyma ayarlarını burada yapmak için.ve T yani entity değeri için de Article sınıfını verdik.Toaster bizim validasyonlarımız sonucu doğrulama işlemi gerçekleşirse başarı mesajlarını içeren kutucuklar. IToastNotification toast sınıfını da burda bu yüzden kullanıcaz
        {
            this.articleService = articleService;
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.validator = validator;
            this.toast = toast;
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
            var categories = await categoryService.getAllCategoriesNonDeleted();
            // CategoryService sayfasından getAllCategoriesNonDeleted metodunu çağırdık
            
            return View(new ArticleAddDTO{Categories =categories});
            //burda da articaddledto dan Categories değişkenine categoriesten gelen değerleri atadık  
        }
        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddDTO articleAddDTO)
        {// ArticleAddDTO verdik çünkü gönderme işlemi yaparken verileri buradan çekicez
            // map işlemini şu yüzden kullandık yukarda bana ArticleDTO geliyo ama ben validasyon işlemlerimde article alıyorum IValidator<Article> validator burdaki işlemle. bu yüzden articleAddDTO nesnesiyle gelen değeri Article a mapper.Map ile  çevirip  map e attım.Maplemenin çalışabilmesi için de ArticleProfile da CreateMap<ArticleDTO, Article>().ReverseMap(); kodunu yazmamız lazım.Her çeviride çevirilen sınıflar için bunu yapmalıyız
            var map = mapper.Map<Article>(articleAddDTO);
            var result = await validator.ValidateAsync(map);// bunu da sonuç olarak burda validateasync metodu olarak kullandım
            if (result.IsValid)// gelen validasyon yani doğrulama başarılıysa
            {
                await articleService.CreateArticleAsync(articleAddDTO);
                // articleservice içindeki CreateArticleAsync metodu ile articleAddDTO dan gelen değerleri gönderdik
                toast.AddSuccessToastMessage(Messages.Article.Add(articleAddDTO.Title), new ToastrOptions {Title="İşlem Başarılı" });
                // yukarıda toastmessage ile ekleme başarılı olursa mesajı Messages.Article.Add(articleAddDTO.Title) ile Blogweb -> resultmessage klasörü içindeki Messages sınıfınfa bulunan Article sınıfına ait Add mesajına articleAdddto dan gelen Title kısmını göndererek oradaki mesajın başına makale ismini ekleyerek buraya getiriyoruz. Örnek C#  başlıklı makale başarı ile eklenmiştir yazacak 
                // alt kısım Messages clasının içi
                //public static string Add(string ArticleTitle)
                //{
                //    return $"{ArticleTitle} başlıklı makale başarı ile Eklenmiştir";
                //    // $ işareti  ile ArticleTitle içine bu mesaj eklendi
                //}
                // toastoptions ile o mesajın başlığınız yazdık
                return RedirectToAction("Index", "Article", new { Area = "Admin" });
                // kaydettikten sonra admin areası içindeki article klasörü içindeki Index sayfasına geri dönüyor


            }
            else// başarısızsa
            {
                result.AddToModelState(this.ModelState);// fluentvalidationExtensions sayfasından gelen state ti ekliyoruz. public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)  fluentvalidationExtensions deki kod bu
                
            }
            var categories = await categoryService.getAllCategoriesNonDeleted();
            //// CategoryService sayfasından getAllCategoriesNonDeleted metodunu çağırdık

            return View(new ArticleAddDTO { Categories = categories });
            ////burda da articaddledto dan Categories değişkenine categoriesten gelen değerleri atadık

        }

        [HttpGet]
        public  async Task<IActionResult> Update(Guid articleId)
        {// şimdi güncellemek istediğimiz makalenin id si gelmeli ki işlem yapabilelim
           
            var article= await articleService.GetArticleWithCategoryNonDeletedAsync(articleId);// articleId değerini aldık.
            // return ile bunları update etmek için göndermemiz lazım sayfamda kategorileri ve bilgileri  değişmek için.
            var categories=await categoryService.getAllCategoriesNonDeleted();
            // burda da kategorileri çağırmış olduk çünkü değişiklik yapmak için hepsini istedim .sonuçta arasından  bir tane kategori seçicem
            var articleUpdateDto= mapper.Map<ArticleUpdateDTO>(article);
            // yukarıda şunu yaptık.şimdi ArticleUpdateDTO bütün verileri almak için yani content ,title, Categories gibi bunları maplememiz lazım. burda da ArticleUpdateDTO den yukarda tanımladığımız article a göre mapleme yaptık
            articleUpdateDto.Categories = categories;
            // yukarıda da articleUpdateDto değişkeni ile mapleyerek aldığımız bilgilerden Categories ı yukarıdaki categories değişkeninden  gelen tüm categorilere atadık
            return View(articleUpdateDto);// sonra da bu bilgileri view sayfama gönderdim

        }
        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateDTO articleUpdatedto)
        {
            var map = mapper.Map<Article>(articleUpdatedto);
            var result = await validator.ValidateAsync(map);// bunu da sonuç olarak burda validateasync metodu olarak kullandım
            
            if (result.IsValid)
            {
               var title= await articleService.UpdateArticleAsync(articleUpdatedto);
                // articleUpdateDTO dan gelen değeri title attık bunu şunun için yaptık makale güncellense de güncellenmese de articleUpdateDTO ile gelen Title bilgisini alıcam bunu da  başarılı makale güncellemesinde mesajın başına makale title ı yani başlığı olarak eklicem. Add metodundan farkı onda buna gerek yok çünkü ekleme yaparken gelen dto değeri ile eklerken bunda ise makalenin ilk başlığı ve sonra değişen başlık aynı olmayacağından şu isimli makale başarıyla güncellendi derken yeni girdiğimiz makale ismini başa yazarsa sorun oluşurdu.o yüzden bu yöntemi kullandık
                toast.AddSuccessToastMessage(Messages.Article.Update(title), new ToastrOptions() {Title="İşlem Başarılı"});
                return RedirectToAction("Index", "Article", new { Area = "Admin" });
            }
            else
            {
                result.AddToModelState(this.ModelState);
            }
            //articleservice nesnesi ile Articleservice clasından updatearticleasync metodunu çağırdık ve içine articleUpdateDTO sunu verdik ki değişiklikleri buna göre yapalım
            var categories = await categoryService.getAllCategoriesNonDeleted();
            // category leri almam lazımdı categoriler görünsün diye herhangi bir sorunda.
            articleUpdatedto.Categories = categories;
            // articleUpdateDTO den gelen categoriesı yukarda 41. satırda categoryservice ten gelen değerlere eşitledik
            return View(articleUpdatedto);
        }
        public async Task<IActionResult> Delete( Guid articleId)
        {
           var title= await articleService.SafeDeleteArticleAsync(articleId);
            toast.AddSuccessToastMessage(Messages.Article.Delete(title), new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("Index", "Article" ,new{Area="Admin"});
            // sildikten sonra area olarak Admin içerisideki Article klasöründeki Index sayfasına gitsin
        }
    }
}
