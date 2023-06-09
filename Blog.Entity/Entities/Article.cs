﻿using Blog.Core.Entities;
using Blog.Entity.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Blog.Entity.Entities
// ctrl+r ve ctrl+g yaptığımızda yukarıda using ile çağırdığımız gereksiz bağımlılıklardan da kurtulmuş oluruz.
{// not: blogcore katmanı içerisinde bulunan Entitybase clasında verilen özellikleri  article entitysininde içinde bulunduğu blog entity katmanımıza sağ tıklayıp add dedikten sonra project referance diyerek blogcore katmanını işaretliyoruz.
    public  class Article:EntityBase // entitybase clası  ve ıentitybase interfaceinden  kalıtım aldık
    {
        // aşağıdaki constructer yapısını kurduk. burada article sınıfını newlediğimiz zaman new Article() şeklinde başka bi yerde kullanıcağımız zaman aşağıda zorunlu olarak kullanılacak alanları belirttilk.
        // 
        public Article()
        {

        }
        public Article(string title, string content, Guid userId, string createdBy, Guid categoryId, Guid imageId)
        {// bu kısımda da aşağıda tanımladığımız alan adlarını yukarda tanımladığımız değişkenlere eşitleyerek yapıyı kurduk
            Title = title;
            Content = content;
            UserId = userId;
            CreatedBy = createdBy;
            CategoryId = categoryId;
            ImageId = imageId;
            CreatedBy = createdBy;
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public int WiewCount { get; set; }=0;
        // otomatik sıfır değeri verdik
        public Guid CategoryId { get; set; }
        // article (makale) category entitisine bağımlıdır.yani kategori olmadan makale olmaz
        public Category Category { get; set; }
        // bir article(makale) nin sadece bir tane kategorisi olabilir.o yüzden burda bire çok ilişki var.Örnek c# ın temelleri makalesi sadece c# kategorisi içinde yer alır.
        public Guid? ImageId { get; set; }// ımage ıd null değer de gelebilir demek için ? işareti ekledik

        public  Image Image { get; set; }
        public  Guid UserId { get; set; }
        public AppUser User { get; set; }
        // AppUser sınıfı ile ilişkilendirme yapacağımız için app userdan nesne aldık


    }
}
