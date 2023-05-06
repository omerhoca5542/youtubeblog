using Blog.Entity.DTOS.Articles;
using Blog.Entity.Entities;
using NPOI.OpenXmlFormats.Dml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Service.Services.Abstractions

{
    public  interface IArticleService
    { //asenkron metotlarda task kullanılır void yerine
        public Task<List<ArticleDTO>> GetAllArticlesWithCategoryNonDeletedAsync();
        Task CreateArticleAsync(ArticleAddDTO articleAddDTO);
        // bana articleadddto dan nesne ekledi ismi de CreateArticleAsync
        Task<ArticleDTO> GetArticleWithCategoryNonDeletedAsync(Guid articleId);
        Task<string> UpdateArticleAsync(ArticleUpdateDTO articleUpdateDTO);
        // update için metodu oluşturduk.içinde string değer de dönücez.Bunu şundan dolayı yaptık.Makale güncellemede başarılı güncelleme olunca verdiğimiz mesajları yazarken makale başlığını alırken lazım olacak
        Task<string> SafeDeleteArticleAsync(Guid articleId);
        // silme işlemi için metod oluşturduk buna safedelete dedik.yani şöyle silinen bilgiler bi yerde tutulacak istediğimiz zaman ulaşmak ya da superadmin yada admin bunları tamamen silmesi için gerekli bi mekanizma kurmuş olduk.içinde string değer de dönücez.Bunu şundan dolayı yaptık.Makale silmede  başarılı güncelleme olunca verdiğimiz mesajları yazarken makale başlığını alırken lazım olacak
    }
}
