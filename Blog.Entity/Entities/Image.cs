using Blog.Core.Entities;

namespace Blog.Entity.Entities
{
    public class Image:EntityBase
    {

       
        public string FilaName { get; set; }
        public string FileType { get; set; }

        public ICollection<Article> Articles { get; set; }
        public ICollection<AppUser> Users { get; set; }
        //Image ile app users arasında ilişki kurduk.sonuçta bir resmi birden fazla users kullanabilir 
    }
}
