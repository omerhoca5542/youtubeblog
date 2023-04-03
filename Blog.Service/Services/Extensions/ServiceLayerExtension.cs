using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
            services.AddAutoMapper(assembly);// Service olarak da buraya Automapperı gelen assemblye göre eklemek de gerekiyo.Assemblye göre automapper profile sınıfından türeyen tüm profile ları alıp depency ınjectioana göre kuruyor.
            return services;
        }
         //scopun amacı ırepository sınıfınddan  nesne çağırdığımda  repository sınıfından işlevini çağıracak
    }
}
