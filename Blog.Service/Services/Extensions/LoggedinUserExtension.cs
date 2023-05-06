using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Extensions
{
    public static class LoggedinUserExtension
    {
        public static Guid GetLoggedinUserId (this ClaimsPrincipal principal) {
            //ClaimsPrincipal yapısı kontrol amaçlı kullanılan bir yapı.Burda GetLoggedinUserId metoduyla aktifolmuş kullanıcıid lerini alıcaz
            return Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
            // burada veritabanından gelen  ClaimTypes.NameIdentifier ile İd yi  FindFirstValue ile ilk değeri  döndürüyoruz

        }
        public static string GetLoggedinEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Email);
            // burada da email değerini aldık .aslında email değeri bu projemizde bizim userId değerimiz de oluyor

        }
        }
}
