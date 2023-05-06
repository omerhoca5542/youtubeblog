
using Blog.Data.Context;
using Blog.Data.Extensions;
using Blog.Entity.Entities;
using Blog.Service.Services.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;// ToastrOptions s�n�f�n� kullanmak i�in ekledik bu usingi
using System.Reflection;
//Startup.cs proje ilk run edildi�inde �al��an kod sat�rlar�n�n bulundu�u s�n�ft�r. Bizde proje ilk run edildi�inde ilgili kullan�lan ba��ml�l�klar� register edece�iz. Startup.cs de bulunan ConfigureServices adl� metot register i�lemlerini yapmak i�in default gelen bir metottur.
var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly().FullName;// assemblu-y bulunan katman�n ismidir burada da fullname ile tam ismini alm���z
// Add services to the container.
builder.Services.LoadDataExtensions(builder.Configuration);
builder.Services.LoadServiceLayerExtension();
// buradaki LoadData Eztension Program.cs'de de tan�mland�
builder.Services.AddControllersWithViews()
    //Toaster bizim validasyonlar�m�z sonucu do�rulama i�lemi ger�ekle�irse ba�ar� mesajlar�n� i�eren kutucuklar� i�eren bir yap� kullan�caz bu y�zden burda bu yap�y� olu�turuyoruz. Bir de bu yap�y� kullanmak i�in blog.webden nugget olarak NToastNotify i kurduk.
    .AddNToastNotifyToastr(new ToastrOptions()    {

        PositionClass=ToastPositions.TopRight,// mesaj kutusunun konumunu sa� �st k��e yapt�k
        TimeOut=3000// mesaj kutusunun bekleme s�resini koyuyoruz   
    } )
    .AddRazorRuntimeCompilation();

builder.Services.AddSession();// oturum ekleme servisini ekledik
// .addrazorruntimecomp komutuynu �� y�zden ekledik blogweb k�sm�na nuget olarak Microsoft.AspNetCore.Mvc.Razor.Runtime nugetini ekledik burda da bu kodu aktif hale getirdik.Nedir bu nugetin amac�  �ndex sayfalar�nda yada cshtml sayfas�nda yapt���m�z de�i�ikli�i sayfada yenile dedi�imizde an�nda g�rmek i�in 

//builder.services lar program aya�a kalkarken kullan�lan k�s�mlard�r
// program�m�z bir dbcontext kullanacak ve ad�da AppDbContext olacak.Options ile sqlserver kullanac���m�z� belirttik gerekli nuget paketini y�kledik.
// buradaki DefaultConnection appsetting.json dosyas�ndaki connectionstringin ba�lant� ad�.
//apdatacontexten referans ald�k
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{// a�a��da password alan� ile ilgili baz� kurallar koyucaz
    opt.Password.RequireNonAlphanumeric = false;// password alan�n�n alfanumerik olmas�na illaki gerek olmad���n� tan�mlad�k
    opt.Password.RequireUppercase = false;// burda da illaki b�y�k harf olmas�n�n gerekli olmad���n� tan�mlad�k
    opt.Password.RequireLowercase = false;

    /*Rol bazl� yetkilendirme, yukar�daki giri� c�mlesinde de ifade etmeye �al��t���m gibi kullan�c�lar�n belli ba�l� sayfalara eri�imini belirlememizi sa�layan ve bunun i�in roller tan�mlayarak yetkilendirme yapmam�za imkan tan�yan bir stratejidir.
   
    ��yle basit bir �rnekle metafor yaparsak e�er; ila� almaya gitti�iniz eczanenin arka odas�n�n kap�s�nda �Personel Harici Giremez!� uyar�s� g�rm��s�n�zd�r. Ha i�te, o odaya eri�ebilmek i�in o eczanede �Personel� rol�ne sahip olman�z gerekmektedir.*/ 
}).AddRoleManager<RoleManager<AppRole>>()
//kimlik bilgileri depolar�n�n Entity Framework uygulamas�n� ekler.
   .AddEntityFrameworkStores<AppDbContext>()
   //Her�eyden �nce uygulamada �ifremi unuttum mekanizmas�n� in�a edebilmek i�in token provider yap�lanmas�n� servis olarak dahil etmemiz gerekmektedir.
   .AddDefaultTokenProviders();
//bu �ekilde uygulamada kullan�lacak olan Cookie yap�lanmas�n�n temel konfig�rasyonunu sa�lam�� bulunmaktay�z
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Admin/Auth/Login");
    // d��ardan yetkisiz biri giri� yapmak isterse ona yol olarak admin areas� i�inde auth controller�n�n Login actionresultuna y�nlendirecek
    config.LogoutPath = new PathString("/Admin/Auth/Logout");//birisi oturumunu sonland�rmak istedi�inde bu sayfaya y�nlendirilecek
    config.Cookie = new CookieBuilder
    {//Bir cookie, bir kullan�c�y� �zelli�tirmek i�in kullan�l�r. Bu a��dan cookie i�inde o kullan�c�ya �zel kay�tlar (kullan�c� ismi, kullan�c� t�r�, hesap ad� ve kullan�c�n�n benzersiz anahtar de�eri gibi) ta��nmal�d�r.
        Name = "blogweb",// cookinin ismini verdik
        HttpOnly = true,//sadece htttp istekleri al�cak
        SameSite = SameSiteMode.Strict,//SameSite, uygulamam�za ait Cookie bilgilerinin 3. taraflardan kaynaklanan isteklere g�nderilip g�nderilmemesi ayar�n� yapt���m�z bir �zelliktir.Strict �zelli�i 3. ki�ilere g�nderime izin vermez
        SecurePolicy = CookieSecurePolicy.SameAsRequest,// Always bu da sayfam�z�n g�venli�inde sadece https uzant�l� olmas�n� sa�lar google https sayfalara ekstra g�venlik verir.ama biz local hostta test yap�yoz �u an o y�zden http uzant�l� sayfalarda da a��ls�n dedik

    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan=TimeSpan.FromDays(7);// buras� girdi�imiz sitede  aktif olduktan sonra ne kadar s�re oturumumun a��k kalaca��n� cookies de bilgilerin tutulaca��n� g�steriyo yani ben bi kere giri� yapt�m �ifremle  7 g�n boyunca �ifresiz girebiliyorum
    config.AccessDeniedPath = new PathString("/Admin/Auth/AcesDenied");// yetkisiz biri giri� yapt�ysa ona uyar� vermek i�in bir sayfa yolu kulland�k
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//ASP.NET Core'da ara katman (Middleware) yap�s�, uygulama �al��t���nda bir istemciden (Client) gelen taleplerin (Request) istemciye geri d�nd�r�lmesi (Response) s�recindeki i�lemleri ger�ekle�tirmek ve s�rece y�n vermek i�in kullan�lmaktad�r.
app.UseNToastNotify();//Toaster bizim validasyonlar�m�z sonucu do�rulama i�lemi ger�ekle�irse ba�ar� mesajlar�n� i�eren kutucuklard�.Biz burda middleware k�sm�n� hallettik
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();// bunlar�n hepsi middleware yani ara katman
// ullan�c� iste�i ile o iste�e kar��l�k �retilen cevap aras�na girmemizi ve bu noktada hert�rl� i�i y�r�tmemizi sa�layacak olan bu kavram Middleware(Ara Katman) olarak adland�r�lmaktad�r. Middleware yap�lar� mant�k olarak request ile response aras�na girip i�lem yapmam�z� sa�lamakla birlikte, birden fazla olma durumlar�nda s�ral� ad�mlar e�li�inde i�lenmekte ve son middleware i�lemide bitti�i an ilgili response kullan�c�ya g�nderilip s�re� sona erdirilmektedir.
app.UseSession();//oturum kullan�m�n� aktifle�tirdik
app.UseAuthentication();
//UseAuthentication� metodu sayesinde uygulaman�n identity ile kimlik do�rulamas� ger�ekle�tirece�ini belirtmi� bulunmaktay�z.ilk �nce bu kod kullan�l�r ��nk� �nce kimlik do�rulan�r sonra yetkilendirme yap�l�r
app.UseAuthorization();// yetkilendirme i�lemleri i�in kullan�l�r

//app.MapControllerRoute(// ba�lang�� sayfas� i�in 
//    name: "default",//ismi default olacak
//    pattern: "{controller=Home}/{action=Index}/{id?}");// yolu ise controller ad� olarak Home  ve action olarak 
////ise Index olacak. id? demek ise id alabilir de almayabilir de soru i�arati bu anlama gelir.Bu normalde gelen ama ben bunu tamamen a��klama yap�cam
app.UseEndpoints(endpoints =>// burada yeniden hem admin paneli i�in hemde default olarak index sayfas� i�in yollar�n� belirticem
{ 
    endpoints.MapAreaControllerRoute(// buraya admin sayfam i�in yol tan�m� yap�cam
   
   name: "admin",// isim 
   areaName: "Admin",// ve burda da alan ismi verdim
   pattern: "Admin/{controller=Home }/{action=Index }/{Id?}"
    );
  endpoints.MapDefaultControllerRoute();// bu k�s�mda yukar�da a��klama olarak i�ine ald���m asl�nda program.cs nin i�inde olan k�s�m� ifade ediyor.Yani ben adres sat�r�na Admin yazmazsam default olarak yukardaki yola gidecek.
    //not:bu k�s�m�n �al��mas� i�in  Blog.Web k�sm�nda Areas ad�nda klas�r olu�turucaz ve Admin i�in de bir klas�r olu�turup onun i�inde de controller, models ve views k�s�mlar� olacak 
});

app.Run();
