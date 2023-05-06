using Blog.Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.FluentValidations
{
    public class ArticleValidator:AbstractValidator<Article>// validasyon işlemleri yapıcaz yani alanlarımız için kurallar koyucaz.bu yüzden AbstractValidator sınfından kalıtım aldık.validasyon kurallarını ekleme ve güncelleme yaparken kullanıcaz.Article üzerinden de işlem yapacığımızdan bu entityi çağıdık.Aslında biz bu yapıyı ekleme ve güncelleme de kullacağımız için sonrasında ArticleService de bunun çevirimlerini yapıcaz
    {
        public ArticleValidator()
        {// title ve content alanlarına  kurallar girdik.Bunların geçerli olması için datalayerextensionsda FluentValidation.DependencyInj nuget paketini yüklememiz lazım 
            RuleFor(x => x.Title)
             .NotEmpty()
             .NotNull()
             .MinimumLength(3)
             .MaximumLength(20)
             .WithName("Başlık");// withname ile title yerine türkçeleştirmek için Başlık adını verdik
            RuleFor(x => x.Content)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(50)
            .WithName("İçerik");// withname ile title yerine türkçeleştirmek için Başlık adını verdik

        }
    }
}
