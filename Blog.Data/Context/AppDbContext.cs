using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Blog.Data.Context
{
    //public class AppDbContext : DbContext// DbContext sınıfından miras aldık
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    // IdentityDbContext sınıfının etkili olması gereken  entityleri girdik 
    // DbContext yerine IdentityDbContext sınıfını kullandık
    {
        public AppDbContext()
        {

        }
        // alttaki constructure(yapıcı sınıfın ) kısayolu ctor yazdıktan sonra çift tık yapmaktır
       
        // yukardaki db context sınıfı üzerinden ctrl. ile alttaki metodu oluşturduk
        public AppDbContext(DbContextOptions options) : base(options)
        {
            // burdaki constructure da dbcontext optionu kullanıcamızı anlattık. ve bunu da program cs de belirticez 
         
        }
        // dbset kullanmak için Blog.entity katmanından set ediyoruzyani veritabanını oluşturacağımız tabloları alıyoruz
        public DbSet<Article> Articles { get; set; }
        //Code First yapısında en temel nokta, veritabanındaki tablolaları temsil edecek Generic yapıdaki DbSet tipinden propertyler olarak tutmasıdır.
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Image> Images { get; set; }
       protected override void OnModelCreating(ModelBuilder builder)
        //modelbuilder alacağımız ve ismini builder koyacağımız bir lmetot tanımladık.Onmodel creating metodunu everrride ederek  veri tabanımızda ilk başta olmasını istediğimiz verileri oluşturabiliriz
        {
            base.OnModelCreating(builder);// Bu metod sayesinde veritabanı tabloları oluşturulmadan araya girecek, tablo isimlerine müdahale edebilecek veya kolonlara istediğimiz ayarları gerçekleştirebileceğiz.
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());// assembly dediğimiz yapı bulunduğumuz sınıfa ait katmandır.Şimdi map.cs dosyalarında tek tek değişiklik yapmak yerine assembly yani o katmanda tek değişiklik yaparak tüm map.cs dosyalarına tek seferde o değişikliği yapmış oluruz.şimdi bunu herhangi bir map dosyasında yapıcaz

        }
    }
    
}
