
using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOS.Articles;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Extensions;
using Microsoft.AspNetCore.Http;
using NPOI.OpenXmlFormats.Spreadsheet;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Service.Services.Concretes
{// service classları çağırdığımız entitylerle ilgili filtreleme yapma durumlarında işimize yarayacak.örneğin herhangi bir tablodan istediğimiz ilk 5 kaydı çekmek istersek bunu burda belirtebiliriz
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;//field oluşturduk burada 
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;
        // ClaimsPrincipal tipinde bir nesne oluşturdum
        // alttaki kısmım benim constructer alanım
        public ArticleService(IUnitOfWork unitOfWork,IMapper mapper,IHttpContextAccessor httpContextAccessor)
        // unitofwork un üzerine ctrl+. yapınca buradan add field diyerek yukarıdaki  private readonly IUnitOfWork unitOfWork; kısmı ekleniyor.IHttpContextAccessor httpContextAccessor u kullanıcının login olup olmadığını kontrol etmek için kullanıcaz.Bunun için ayrıca servicelayerextensionsda scope oluşturduk.
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            // _user a giriş yapan kullanıcının  LoggedinUserExension sınıfındaki bilgileri geliyor
        }

        public async Task CreateArticleAsync(ArticleAddDTO articleAdddto)
        {
            //var userId = Guid.Parse("{295411B6-4B8B-455B-B108-19092EA05963}");
            var userId = _user.GetLoggedinUserId();
            var userEmail = _user.GetLoggedinEmail();
            // userId değerine LoggedinUserExensions sınıfında tanımladığım metottan gelen Userın id ve email değerlerini aldım    
        
       var imageId = Guid.Parse("{942E2D1F-DDCF-4578-BE0A-016F0DAC1E65}");
            // userıd olarak bir tane hazır olan user ıd lerden kullandık
 var article = new Article(articleAdddto.Title, articleAdddto.Content, userId ,userEmail, articleAdddto.CategoryId, imageId);
            // userId ve ImageId yi yukarda verdik articleAddDTO dan eklemedik.onları ArticleMap den aldık

            //   // burada biz şunu yaptık formdaki alanlara sırayla yazılan değerleri articleadddto üzerinden vveriyoruz
            //};// 

            await unitOfWork.GetRepository<Article>().AddAsync(article);
            // unitofwork yapısı ile article repositorysini çağırarak addasync methodu için  yukarıdaki articlestan gelen değerleri gönderdik
            await unitOfWork.SaveAsync();

        }
        public async Task<List<ArticleDTO>> GetAllArticlesWithCategoryNonDeletedAsync()// burada bize article türünden liste olarak veriler gelecek.Biz category alanında silinmemiş olan categoryleri getir isminde bi metottanımladık
        {
           var articles= await unitOfWork.GetRepository <Article>().GetAllAsync(x=>!x.IsDeleted, x=>x.Category);
            // x=>!x.IsDeleted bu kod ile silinen yerine ! işareti koyarak silinmemiş olan ve bu kod ile de x=>x.Category category kısmından veriler alıyoruz 
            var map = mapper.Map<List<ArticleDTO>>(articles);

            return map;
            // yukarıda unitofwork adında bir nesne oluşturmuştuk.şimdi o nesneyle Getrepository komutunu kullanarak article türünden verileri GettAllAsync metodunu kullanarak çağırabiliriz 
        }//şimdi yapıları oluşturduk  bunları çağırmak için BlogWeb içinde bulunan Controller kısmına gidicez
        public async Task<ArticleDTO> GetArticleWithCategoryNonDeletedAsync( Guid articleId)// buraya biz articledto da yazdığımız article id yi articlecontrollerda Update metodundan göndericez
        {
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id==articleId, x => x.Category);
            // yukarudaki GetAllArticlesWithCategoryNonDeletedAsync metodundan farklı olarak burda sadece update için seçilen kategori geliceğinden işlemleri değiştirdik. mesela getallasync metodu yerine getasync kullandık .LİST kullanmadık çünkü çoklu veri çekme işlemi yapmıyoruz.  x.Id==articleId bu kısımda da gelen articleId değerini Id değerine eşitledik çünkü gelecek olan update komutundan bize gönderilen articleId olacaktır
            var map = mapper.Map<ArticleDTO>(article);

            return map;

        }
        public async Task<string> UpdateArticleAsync(ArticleUpdateDTO articleUpdatedto)
        {// metot update için metot oluşturduk ve articleupdatedto almasını söyledik
            var userId = _user.GetLoggedinUserId();
            var userEmail= _user.GetLoggedinEmail();
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleUpdatedto.Id, x => x.Category);
            // şimdi unitofwork ile getrepository metodundan article sınıfına ulaşıp ordan getasync metodunu 
            article.Title = articleUpdatedto.Title;
            article.Content = articleUpdatedto.Content;
            article.CategoryId = articleUpdatedto.CategoryId;
            article.ModifiedDate = DateTime.Now;
            article.ModifiedBy = userEmail;
            // kimin tarafından güncellendiği bilgisini userEmail ile aldık UserEmail zaten userın adıyla aynı şey
            // article dan gelen bilgilerle articleupdatedtodan gelen bilgileri eşitledik ki değişiklik olduğunda article tablomda da değişikler olsun
            return article.Title;// title ı şu yüzden döndürdük .başarılı makale güncellemede messaj verirken burdaki title ismine ihtiyacımız var. 
            //mapper.Map<ArticleUpdateDTO>(article);
            // mapleme işlemini articleupdatedtodan article a göre yaptık.article değişkeninde kuralları yukarda belirttik zaten
            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            // article clasına göre updateasync yani güncelleme metodunu article değişkenine göre yaptık
            await unitOfWork.SaveAsync();
            // kayıt işlemini tamamladık

            return article.Title;
        }
        public async Task<string>  SafeDeleteArticleAsync(Guid articleId) {
            // safedelete yani güvenli silme metodu tanımladık.articlecontroller ile bir articleId gönderdik buna göre işlem yapıcaz
            var userEmail = _user.GetLoggedinEmail();
            var article= await unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);
            // şimdi silme işlemi yaparken bize Id lazım işte onu getBuquidAsync metoduna articleId yi gönderip Articledaki Id yi aldık
            
            article.IsDeleted = true;// silindi göstermek için kaydı true yaptık
            article.DeletedDate = DateTime.Now;// silinme zamanını da şimdiki zaman yaptık
           article.DeletedBy=userEmail;
            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            // silme işleminden sonra güncelleme metodunu çağırarak article a göre veri tabanında article sınıfını ya da tablosunu güncelledik
           await unitOfWork.SaveAsync();// kaydedip verileri gönderdik
            return article.Title;// article dan gelen Title değerini geri dönderiyoruz
        }
    }
}
