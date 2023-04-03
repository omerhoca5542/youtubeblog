using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mapping
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    //ArticleMap sınıfımıza  IEntityTypeConfiguration interface’inden miras alarak Article sınıfında property alanlarına düzenlemeler yapıcaz.Haritalandırma işlemi de dediğimiz bir olay bu
    {
        void IEntityTypeConfiguration<Article>.Configure(EntityTypeBuilder<Article> builder)
        {
        //    Projelerimizin çoğunda, oluşturulan veritabanında bazı ilk verilere sahip olmak isteyebiliriz.Bu nedenle, veritabanını oluşturmak ve yapılandırmak için geçiş dosyalarımızı çalıştırır çalıştırmaz, otomatik olarak bazı başlangıç verileriyle doldururuz.Bu eyleme “Data Seeding” yani Veri Tohumlama denir.Aşağıda bunu yapıyoruz
            //builder.Property(x => x.Title).HasMaxLength(140);
            // burada property yani her bir sütun için en fazla 140 karakter girebilir dedik.
            builder.HasData( new Article// hasdata ile verigirişi yapıcaz.Bu kısım ilk kayıt setimiz olsun
            {
            Id=Guid.NewGuid(),// article entitysi için id değerini bir guid yaptık yani özel id değeri
                              // 
            Title=".net core",
            Content=".net core 6 uygulamalarını anlatmış olucaz ",
            WiewCount=15,
          CategoryId= Guid.Parse("{C5D8CD84-F517-438F-BAF4-E691F37088FB}"),
                ImageId= Guid.Parse("{942E2D1F-DDCF-4578-BE0A-016F0DAC1E65}"),

                CreatedBy = "deniz",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId = Guid.Parse("{295411B6-4B8B-455B-B108-19092EA05963}")// bu değeri usermap dosyasındaki id bölümünden kopyaladık
            },
            new Article// hasdata ile verigirişi yapıcaz.Bu kısım ikinci kayıt setimiz olsun
            {
                Id = Guid.Parse("{B259C97E-3557-41AC-88CF-158F574CF88E}"),// article entitysi için id değerini bir guid yaptık yani özel id değeri.şimdi guid değerini bilemiyeceğimizden üst menülerden tools-> create guid deği ordan o guid yi kopyalıyoruz
                                    // 
                Title = "visual studio",
                Content = "visual studio uygulamalarını anlatmış olucaz ",
                WiewCount = 15,
               CategoryId= Guid.Parse("{CA6D96B6-C2E1-48ED-9208-60246AC1194F}"),
                ImageId= Guid.Parse("{C1C64D2F-15CF-4DB0-AF13-B1F50B4C0670}"),

                CreatedBy = "selim",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId = Guid.Parse("{98F208D2-64AD-4284-AA39-40DA28EDF5A4}")//bu değeri usermap dosyasındaki ikinci kullanıcının id bölümünden kopyaladık


            }
            );
            



        }
    }
}
