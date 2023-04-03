using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
   public class AppUserClaim:IdentityUserClaim<Guid>
       // IdentityDbContext sınıflarında İdler otomatik olarak string türünden kuruluyor ama biz Id KISMINI Guid olarak belirtmek istedik
    {
    }
}
