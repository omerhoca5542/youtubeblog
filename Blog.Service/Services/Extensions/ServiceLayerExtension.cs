using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using Blog.Service.FluentValidations;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concretes;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Blog.Service.Services.Extensions
{
    public static  class ServiceLayerExtension
    {
        // alttaki metodu çalıştırmak için program.cs de tanımlamamız lazım
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();// hatırlayalım assembly yapısı  bizim hangi katmanda olduğumuz bilgisini veriyodu 
            /*Scope nedir ?
             .Faaliyet alanı
             .Kapsam
             .Değişken ve fonksiyonların erişilebilirlik alanını belirleyen kapsamdır
             . c# ta süslü parantezler bi scoptur mesela {}.Bunun dışına çıkarsan mesela if den sonra  {} gelen bu süslüler içinde sınırlıdır yapacakların
             */
            services.AddScoped<IArticleService, ArticleService>();
            //AddScopped Injection Indepence yapısının bir metodudur. Peki nasıl çalışır.Şimdi hangi Service için İnterface çağırdıysak o service için service nesne örneğini getiriyor.Burda Iarticle Service çağırmışım ozaman bana sadece Article Servicinden nesne örneği getir buna izin veriyor 
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // HttpContextAccessor ile login olan kullanıcıları bulmak için ekledik
            services.AddAutoMapper(assembly);// Service olarak da buraya Automapperı gelen assemblye göre eklemek de gerekiyo.Assemblye göre automapper profile sınıfından türeyen tüm profile ları alıp depency ınjectioana göre kuruyor.
            /*// AddControllersWithViews ile mvc proje geliştirme desenini kullanacağımızı gösterdik AddFluentValidation ile validasyon yani doğrulama işlemi açtık*/
            services.AddControllersWithViews().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
                opt.DisableDataAnnotationsValidation = true;
                // burda da  entitylerde kullandığımız [key] [required] gibi DATAANNOTATİONS ların kullanımını yasakladık çünkü bütün işlemleri fluentvalidationlardan yapıcaz
                opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
                // hata  mesajlalarını türkçeye çevirmiş olduk 
                // bu kısımda ise validasyon seçeneklerinden dilyöneticisi ile culture atadık yani dili türkçe yaptık.articlevalidatorda wiithname ile verdiğimiz türkçe isimler gerçekleşmiş olacak.Bunun içinde Fluentvalidationextension oluşturmamız lazım

            });// şimdi burada RegisterValidatorsFromAssemblyContaining ile Articlevalidator a bağlı olan  bulunduğu assemleydeki yani burada blogservice assemlysi ya da katmanındaki bütün validasyonları aktif etmiş olduk. AddValidatorsFromAssemblyContaining sınıfının da aktif olması için  FluentValidation.DependencyInj nuget paketinin yüklü olması lazım
            return services;
        }
         //scopun amacı ırepository sınıfınddan  nesne çağırdığımda  repository sınıfından işlevini çağıracak
    }
}
