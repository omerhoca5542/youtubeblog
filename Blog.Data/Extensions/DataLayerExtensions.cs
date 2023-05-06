using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using FluentValidation;// AddValidatorsFromAssemblyContaining sınıfını kullanmak için çağırdık
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Extensions
{
  public static class DataLayerExtensions
        //extensiosn genişletilebilir demektir.
    {
//        Extension metodlar static bir class içerisinde static olarak tanımlanmalıdır.
//Extend edilecek class ilgili extension metoda metodun ilk parametresi olarak verilip önünde this keyword'ü ile tanımlanmalıdır
//Extension metod da tanımlı parametrelerden sadece 1 tanesi this keyword'ü ile tanımlanır
        // IServicecollection DependencyInjection sınıfından geliyor
        public static IServiceCollection  LoadDataExtensions(this IServiceCollection services, IConfiguration config)
        //IConfiguration config kullanabilmek için Program.cs sınıfında 
        //builder.Services.LoadDataExtensions(builder.Configuration); kodunu kullandık
        // datalayerextensions sınıfından oluşturduğumuz yardımcı sınıf olan bu sınıfta this ile Iservicecollection türünde LoadDataExtensions adında metot tanımladık  tabi burdda işleri services nesnesi yapacak
        {
            // scopun amacı ırepository sınıfınddan  nesne çağırdığımda  repository sınıfından işlevini çağıracak
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // typeof un amacı ırepository olarak hangi nesneyi çağırırsak çağıralım farketmeksizin hepsini de repository deki halini alabilmemizi sağladı.
            // addscope ırepositoryden çağırınca repositoryden nesneyi almak typeof da tek tek almak yerine hepsini alabilmemizi sağlamak amacı güder
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();// her Iunitofwork istendiğinde unitofwork verilecek
            
            return services;

        }
    }
}
