using Blog.Entity.DTOS.Users;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]// admin sayfamız area alanı içinde olduğundan belirtmemiz gerekir
   
    public class AuthController : Controller
    {// bu kısımlar  constructerda usermanager  ve signIn Manager için oluşturulan field lar
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        // ctor yazıp iki kere tab tuşuna basınca constructure  yani yapıcı metod oluşur
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]// sayfa ilk açıldığında gelen kısım

        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]//[AllowAnonymous] attribute’ü kullanarak ise public erişim’e izin vermiş oluruz. Bu güvenlikli olan yaklaşımı seçtim, böylece controller’a eklenen yeni action metodlarına sadece güvenli bir biçimde erişim sağlanabilecektir.
        [HttpPost]// sayfa açıldıktan sonra giriş yap butonuna basıp işlem yaptığımızda gelen kısımda burası.yani veri gönderme alma işlemleri gibi asıl işlemler burada yapılacak

        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)// parametre verdik çünkü userlogindto da özellikleri belirttik login sayfamıza dışarıdan gelen bilgileri burdaki parametre ile alıcaz burdaki login için dto yani data transfer object oluşturmamız lazım .Blog Entity altında DTOS kısmından yapıyoruz.aslında bunu karşılığı views model ama  biz bu şekilde  yapıyoruz.
           //UserLoginDTO SAYFASI OLUŞTURDUK VE İÇİNDE 3 alan oluşturduk Login sayfasına uygun olarak email şifre ve beni hatırla kısımları için
        {
            if (ModelState.IsValid)// eğer modelimiz doğruysa
            {
                var user = await userManager.FindByEmailAsync(userLoginDTO.Email);// burda usermanager ile email alanımızı buluyoruz ve dto muzdan gelen email değerini buraya veriyoruz.
                if (user != null)//userdan gelen değer boş değilse. aşağıda password kısmını da kontrol ediyoruz
                {
                    var result = await signInManager.PasswordSignInAsync(user, userLoginDTO.Password, userLoginDTO.Remeemberme, false);//signInManager Kullanıcının giriş ve çıkışlarını kontrol eden bir sınıftır. user için userlogindto dan gelen passwordu alıyor ve remember me alanını alıyor 
                    if (result.Succeeded)// eğer gelen değerler doğruysa 
                    {
                       return  RedirectToAction("Index", "Home", new { Area = "Admin" });
                        // beni direkt olarak admin areasının içindeki home controllera bağlı ındex sayfasına göderiyor.Yani sayfamızın ana sayfasına gidiyoruz 
                    }
                    else
                    {
                        ModelState.AddModelError("", "Eposta adresiniz  ya da parola yanlış"); // ModelState sınıfıyla hata mesajı eklememizi sağlıyor.aslında burada sadece parola kontrol ediyoruz ama başka birinin kullanıcı adını bilen biri şifreyi tahmin etmesin diye ikisinden biri yanlış diyerek onu oyalamış oluyoruz kırmızı çizgiyle belirterek ve peşine 
                        return View("Index");// bizi ilk baştaki login sayfasına geri gönderiyor.tekrar giriş yapmamız için
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eposta boş yanlış");
                    return View();//yine ilk 
                }
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout(UserLoginDTO userLoginDTO)
        {
            await signInManager.SignOutAsync();// signin manager ile signoutasync ile çıkış yapıldığına dair kontrol yapılıyor
            return RedirectToAction("Index", "Home", new { Area = "" });



        }
    }
}