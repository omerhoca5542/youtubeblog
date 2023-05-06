using Blog.Entity.DTOS.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Blog.Entity.DTOS.Articles;
namespace Blog.Entity.DTOS.Articles
{
    public class ArticleUpdateDTO
    {
        public Guid Id { get; set; }
        //Id ye göre güncelleme yapacağımızdan articleadddto ya göre fazladan burada Id kullandık
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }

        public IList<CategoryDTO> Categories { get; set; }

        // bu liste şeklinde category dto dan dönen categori isimlerini almış olduk   categorydto da public string Name { get; set; } var yani sadece category ismini dödürüyor
    }
}
