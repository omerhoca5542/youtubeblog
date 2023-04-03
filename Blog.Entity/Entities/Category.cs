using Blog.Core.Entities;

namespace Blog.Entity.Entities
{
    public class Category:EntityBase
    {
      
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
        // çoka bir ilişkiden dolayı bir  categoryde birden fazla article(MAKALE) olacağından bu ilişkiyi burda böyle kurduk.örnek c# kategorisinde yazılan birden fazla makale olabilir.

    }
}
