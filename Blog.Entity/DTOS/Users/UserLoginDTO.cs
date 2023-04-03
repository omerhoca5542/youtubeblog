using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOS.Users
{
    public class UserLoginDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Remeemberme { get; set; }//3 alan oluşturduk Login sayfasına uygun olarak email şifre ve beni hatırla kısımları için
    }
}
