using Blog.Entity.DTOS.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOS.Articles
{
    public class ArticleAddDTO
    {
        public string Title { get; set; }
        public string Content { get; set; } 
        public int CategoryId { get; set; } 
        
        public IList<CategoryDTO> Categories { get; set; }

        // bu liste şeklinde category dto dan dönen categori isimlerini almış olduk   categorydto da public string Name { get; set; } var yani sadece category ismini dödürüyor
    }
}
