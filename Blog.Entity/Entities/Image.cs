 using Blog.Core.Entities;

namespace Blog.Entity.Entities
{
    public class Image:EntityBase
    {
        // aşağıdaki constructer yapısını kurduk.burada Image sınıfını newlediğimiz zaman new Image() şeklinde başka bi yerde kullanıcağımız zaman aşağıda zorunlu olarak kullanılacak alanları belirttilk.
        public Image()
        {

        }
        public Image(string fileName, string fileType)
        {
            FilaName = fileName;
            FileType = fileType;    
        }
        public string FilaName { get; set; }
        public string FileType { get; set; }

        public ICollection<Article> Articles { get; set; }
        public ICollection<AppUser> Users { get; set; }
        //Image ile app users arasında ilişki kurduk.sonuçta bir resmi birden fazla users kullanabilir 
    }
}
