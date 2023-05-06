
using Blog.Data.Context;
using Blog.Data.Extensions;
using Blog.Entity.Entities;
using Blog.Service.Services.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;// ToastrOptions sýnýfýný kullanmak için ekledik bu usingi
using System.Reflection;
//Startup.cs proje ilk run edildiðinde çalýþan kod satýrlarýnýn bulunduðu sýnýftýr. Bizde proje ilk run edildiðinde ilgili kullanýlan baðýmlýlýklarý register edeceðiz. Startup.cs de bulunan ConfigureServices adlý metot register iþlemlerini yapmak için default gelen bir metottur.
var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly().FullName;// assemblu-y bulunan katmanýn ismidir burada da fullname ile tam ismini almýþýz
// Add services to the container.
builder.Services.LoadDataExtensions(builder.Configuration);
builder.Services.LoadServiceLayerExtension();
// buradaki LoadData Eztension Program.cs'de de tanýmlandý
builder.Services.AddControllersWithViews()
    //Toaster bizim validasyonlarýmýz sonucu doðrulama iþlemi gerçekleþirse baþarý mesajlarýný içeren kutucuklarý içeren bir yapý kullanýcaz bu yüzden burda bu yapýyý oluþturuyoruz. Bir de bu yapýyý kullanmak için blog.webden nugget olarak NToastNotify i kurduk.
    .AddNToastNotifyToastr(new ToastrOptions()    {

        PositionClass=ToastPositions.TopRight,// mesaj kutusunun konumunu sað üst köþe yaptýk
        TimeOut=3000// mesaj kutusunun bekleme süresini koyuyoruz   
    } )
    .AddRazorRuntimeCompilation();

builder.Services.AddSession();// oturum ekleme servisini ekledik
// .addrazorruntimecomp komutuynu þü yüzden ekledik blogweb kýsmýna nuget olarak Microsoft.AspNetCore.Mvc.Razor.Runtime nugetini ekledik burda da bu kodu aktif hale getirdik.Nedir bu nugetin amacý  ýndex sayfalarýnda yada cshtml sayfasýnda yaptýðýmýz deðiþikliði sayfada yenile dediðimizde anýnda görmek için 

//builder.services lar program ayaða kalkarken kullanýlan kýsýmlardýr
// programýmýz bir dbcontext kullanacak ve adýda AppDbContext olacak.Options ile sqlserver kullanacýðýmýzý belirttik gerekli nuget paketini yükledik.
// buradaki DefaultConnection appsetting.json dosyasýndaki connectionstringin baðlantý adý.
//apdatacontexten referans aldýk
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{// aþaðýda password alaný ile ilgili bazý kurallar koyucaz
    opt.Password.RequireNonAlphanumeric = false;// password alanýnýn alfanumerik olmasýna illaki gerek olmadýðýný tanýmladýk
    opt.Password.RequireUppercase = false;// burda da illaki büyük harf olmasýnýn gerekli olmadýðýný tanýmladýk
    opt.Password.RequireLowercase = false;

    /*Rol bazlý yetkilendirme, yukarýdaki giriþ cümlesinde de ifade etmeye çalýþtýðým gibi kullanýcýlarýn belli baþlý sayfalara eriþimini belirlememizi saðlayan ve bunun için roller tanýmlayarak yetkilendirme yapmamýza imkan tanýyan bir stratejidir.
   
    Þöyle basit bir örnekle metafor yaparsak eðer; ilaç almaya gittiðiniz eczanenin arka odasýnýn kapýsýnda “Personel Harici Giremez!” uyarýsý görmüþsünüzdür. Ha iþte, o odaya eriþebilmek için o eczanede “Personel” rolüne sahip olmanýz gerekmektedir.*/ 
}).AddRoleManager<RoleManager<AppRole>>()
//kimlik bilgileri depolarýnýn Entity Framework uygulamasýný ekler.
   .AddEntityFrameworkStores<AppDbContext>()
   //Herþeyden önce uygulamada þifremi unuttum mekanizmasýný inþa edebilmek için token provider yapýlanmasýný servis olarak dahil etmemiz gerekmektedir.
   .AddDefaultTokenProviders();
//bu þekilde uygulamada kullanýlacak olan Cookie yapýlanmasýnýn temel konfigürasyonunu saðlamýþ bulunmaktayýz
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Admin/Auth/Login");
    // dýþardan yetkisiz biri giriþ yapmak isterse ona yol olarak admin areasý içinde auth controllerýnýn Login actionresultuna yönlendirecek
    config.LogoutPath = new PathString("/Admin/Auth/Logout");//birisi oturumunu sonlandýrmak istediðinde bu sayfaya yönlendirilecek
    config.Cookie = new CookieBuilder
    {//Bir cookie, bir kullanýcýyý özelliþtirmek için kullanýlýr. Bu açýdan cookie içinde o kullanýcýya özel kayýtlar (kullanýcý ismi, kullanýcý türü, hesap adý ve kullanýcýnýn benzersiz anahtar deðeri gibi) taþýnmalýdýr.
        Name = "blogweb",// cookinin ismini verdik
        HttpOnly = true,//sadece htttp istekleri alýcak
        SameSite = SameSiteMode.Strict,//SameSite, uygulamamýza ait Cookie bilgilerinin 3. taraflardan kaynaklanan isteklere gönderilip gönderilmemesi ayarýný yaptýðýmýz bir özelliktir.Strict özelliði 3. kiþilere gönderime izin vermez
        SecurePolicy = CookieSecurePolicy.SameAsRequest,// Always bu da sayfamýzýn güvenliðinde sadece https uzantýlý olmasýný saðlar google https sayfalara ekstra güvenlik verir.ama biz local hostta test yapýyoz þu an o yüzden http uzantýlý sayfalarda da açýlsýn dedik

    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan=TimeSpan.FromDays(7);// burasý girdiðimiz sitede  aktif olduktan sonra ne kadar süre oturumumun açýk kalacaðýný cookies de bilgilerin tutulacaðýný gösteriyo yani ben bi kere giriþ yaptým þifremle  7 gün boyunca þifresiz girebiliyorum
    config.AccessDeniedPath = new PathString("/Admin/Auth/AcesDenied");// yetkisiz biri giriþ yaptýysa ona uyarý vermek için bir sayfa yolu kullandýk
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//ASP.NET Core'da ara katman (Middleware) yapýsý, uygulama çalýþtýðýnda bir istemciden (Client) gelen taleplerin (Request) istemciye geri döndürülmesi (Response) sürecindeki iþlemleri gerçekleþtirmek ve sürece yön vermek için kullanýlmaktadýr.
app.UseNToastNotify();//Toaster bizim validasyonlarýmýz sonucu doðrulama iþlemi gerçekleþirse baþarý mesajlarýný içeren kutucuklardý.Biz burda middleware kýsmýný hallettik
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();// bunlarýn hepsi middleware yani ara katman
// ullanýcý isteði ile o isteðe karþýlýk üretilen cevap arasýna girmemizi ve bu noktada hertürlü iþi yürütmemizi saðlayacak olan bu kavram Middleware(Ara Katman) olarak adlandýrýlmaktadýr. Middleware yapýlarý mantýk olarak request ile response arasýna girip iþlem yapmamýzý saðlamakla birlikte, birden fazla olma durumlarýnda sýralý adýmlar eþliðinde iþlenmekte ve son middleware iþlemide bittiði an ilgili response kullanýcýya gönderilip süreç sona erdirilmektedir.
app.UseSession();//oturum kullanýmýný aktifleþtirdik
app.UseAuthentication();
//UseAuthentication” metodu sayesinde uygulamanýn identity ile kimlik doðrulamasý gerçekleþtireceðini belirtmiþ bulunmaktayýz.ilk önce bu kod kullanýlýr çünkü önce kimlik doðrulanýr sonra yetkilendirme yapýlýr
app.UseAuthorization();// yetkilendirme iþlemleri için kullanýlýr

//app.MapControllerRoute(// baþlangýç sayfasý için 
//    name: "default",//ismi default olacak
//    pattern: "{controller=Home}/{action=Index}/{id?}");// yolu ise controller adý olarak Home  ve action olarak 
////ise Index olacak. id? demek ise id alabilir de almayabilir de soru iþarati bu anlama gelir.Bu normalde gelen ama ben bunu tamamen açýklama yapýcam
app.UseEndpoints(endpoints =>// burada yeniden hem admin paneli için hemde default olarak index sayfasý için yollarýný belirticem
{ 
    endpoints.MapAreaControllerRoute(// buraya admin sayfam için yol tanýmý yapýcam
   
   name: "admin",// isim 
   areaName: "Admin",// ve burda da alan ismi verdim
   pattern: "Admin/{controller=Home }/{action=Index }/{Id?}"
    );
  endpoints.MapDefaultControllerRoute();// bu kýsýmda yukarýda açýklama olarak içine aldýðým aslýnda program.cs nin içinde olan kýsýmý ifade ediyor.Yani ben adres satýrýna Admin yazmazsam default olarak yukardaki yola gidecek.
    //not:bu kýsýmýn çalýþmasý için  Blog.Web kýsmýnda Areas adýnda klasör oluþturucaz ve Admin için de bir klasör oluþturup onun içinde de controller, models ve views kýsýmlarý olacak 
});

app.Run();
