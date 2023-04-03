using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Entities
{
    public abstract class EntityBase:IEntityBase//abstract class lar sanal class oluşumunu sağlar.Interface olan IEntitybaseden de kalıtım aldık
    {
        
        //    Sadece metod isimlerini belirteceğimiz bir classımız olsun, metodların ne işlev yapacağını diğer classlarda belirtecek isek Virtual metod kullanırız.
        //Birden çok Class larımızın, aynı anlama gelen metodları varsa, bu metodu Virtual olarak tanımlarız.
        //Yazılımda standartlaşmak önemlidir, eğer geliştirilen uygulamamızın farklı modüllerinde, metod isimlerimizin aynı olması isteniyorsa, yine Virtual metod kullanırız.
        public virtual Guid Id { get; set; } = Guid.NewGuid();// guid benzersiz demek benzersiz ıd  oluşturmak için kullanıyoruz
        public virtual string CreatedBy { get; set; } = "Undifined";
        // createdbye kısmını undifened yani tanımsız yaptık
        public virtual string? DeletedBy { get; set; }// koduğumuz soru işaretleri o alanların nullable yani boş bırakılabilir anlamına gelir
        public virtual string? ModifiedBy { get; set; }
       
        public virtual DateTime CreatedDate { get; set; }= DateTime.Now;
        //datetime.now  ile tarih otomatik olarak şimdiki zamanın tarihi oluyor
        public virtual DateTime? DeletedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
               
        public virtual bool IsDeleted { get; set; }= false;
        // default olarak false yani silinmemiş olarak ayarlanıyor
    }
}
