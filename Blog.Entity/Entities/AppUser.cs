using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class AppUser:IdentityUser<Guid>// IdentityDbContext sınıflarında İdler otomatik olarak string türünden kuruluyor ama biz Id KISMINI Guid olarak belirtmek istedik
    {
        public string FirstName { get; set; }
        public string  LastName { get; set; }

        public Guid ImageId { get; set; }
        public Image Image { get; set; }// Image tablosunu buraya bağlamış olduk
        public ICollection<Article> Articles { get; set; }
        // ilişkilendirme yapıyoruz aslında article  entitysiyle her User yani kullanıcı için birden fazla article yani makale yazabileceğini düşündüğünmüzden bire çok ilişki kullandık
    }
}
