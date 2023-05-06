using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Entity.DTOS.Categories;
using Blog.Entity.DTOS.Articles;
namespace Blog.Entity.DTOS.Categories
{
    public  class CategoryDTO
    {
        public Guid Id { get; set; }
        // Id değeri de kategory çağırırken kullanılır hangi kategory yi çağıracağımızı Id sinden belirleriz.Tabi karşımıza Name kısmı çıksa da sonuçta Id değerine göre çağırma işlemi yapılır
        public string ?Name { get; set; }
    }
    //sadece name kısmını aldık şimdilik.bunu Article .DTO DA ÇAĞIRICAZ
    // burada mapleme işmeinin nimetini kullandık yani category  tablosundan sadece
    //işimize yarayan name field yani sütununu aldık
}
