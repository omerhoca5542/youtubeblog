using Blog.Entity.DTOS.Categories;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOS.Articles//DTO yapısını kullanmamızın sebebi şu aslında biz bu işlemi katman olarak Blog.Web katmanında Models içerisinde yapmamız lazım ama Blog Service ile Blog.Web katmanları arasında ilişki yok  ama Blog Service ile Blog.Entity katmanları arasında referans ilişkisi var bu yüzden Models yerine bLOG.Entity katmanı içinde dto Klasör ve clas yapılanmasını bu isimle  DTO ismiyle kullanıyoruz.Aslında DTO isimlendirilmesi API projelerde kullanılır ama biz burda da  öğreticimiz  buna alışkın diye :) bu isimlendirmeyi kullandık.         NOT:DTO clası aslında bakarsak article sınıfındakiyle aynı alanları kullanıyo ya da ortak kullanım olan Base sınıfından alanları ortak kullanıyo.Burdaki amaç aslında sadece "işimize yarayanları almak"  Data transfer object” ler DTO lar içlerinde business kod bulundurmazlar görevleri sadece verileri taşımak ve geçici olarak saklamaktır.Dto” katmanımızı oluşturacağız ve AutoMapper ‘ı projemize dahil edeceğiz. Entity lerimiz ve dto larımız arasında veri transferini ise AutoMapper yardımı ile yapacağız.Aşağıdaki propertylerin adları organization entity içindeki propertyle ile aynı olmalı, yoksa mapper içerisinde profiller oluştururken özel kurallar yazmamız gerekir.
/// İsimler aynı olursa AutoMapper otomatik olarak hangi property nin Dto daki hangi propertye eşit olduğunu anlayacak ve atamaları yapacak.
{
    public class ArticleDTO
    {
        public Guid Id{ get; set; }
        public string Title { get; set; }
        
        public   CategoryDTO Category  { get; set; }
        // categoryDTO  sınıfında şuan  sadece Name kısmı olduğundan ben bu kısmı kullanmak istiyorum ve direkt category dto sınıfından Category ismiyle tanımlama yapıyorum 
      
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
      

    }
}
