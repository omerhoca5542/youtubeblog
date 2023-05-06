using Blog.Core.Entities;
using Blog.Entity.Entities;
using System.Reflection.Metadata.Ecma335;
namespace Blog.Entity.Entities
{
    public class Category:EntityBase
    {// aşağıdaki constructer yapısını kurduk. burada Category sınıfını newlediğimiz zaman new Category() şeklinde başka bi yerde kullanıcağımız zaman aşağıda zorunlu olarak kullanılacak alanları belirttilk.
        
        public Category()
        {

        }
        public Category( string name)
        {
           Name = name;
        }
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
        // çoka bir ilişkiden dolayı bir  categoryde birden fazla article(MAKALE) olacağından bu ilişkiyi burda böyle kurduk.örnek c# kategorisinde yazılan birden fazla makale olabilir.

    }
}
