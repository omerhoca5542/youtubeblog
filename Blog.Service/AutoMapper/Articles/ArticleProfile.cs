using AutoMapper;
using Blog.Entity.DTOS.Articles;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Articles
{// Öncelikle biz AutoMapper adında BlogService altında bir klasör oluşturduk onunda altında Articles Klasörü oluşturduk ve class olarakda ArticleProfile adında bir class tanımladık mapperlarda isimlendirme böyle yapılıyor.
    public class ArticleProfile:Profile//Direk AutoMapper sınıfından miras aldık
    {
        public ArticleProfile()
        {
            CreateMap<ArticleDTO,Article>().ReverseMap();// Createmap mapleme fonksiyonu. ArticleDTO ile Article arasında birbirlerinden mapleme yapmak içinde ReverseMap komutunu kullandık
            CreateMap<ArticleUpdateDTO,Article>().ReverseMap();
            CreateMap<ArticleUpdateDTO,ArticleDTO>().ReverseMap();
            //direkt dto lar üzerinden veri alışverişi yaptığımızdan articleupdatedto dan direk article la çeviri yapamadığımızdan birde ArticleUpdateDTO dan ArticleDTO ya çeviri yaptık
            CreateMap<ArticleAddDTO, Article>().ReverseMap();
        }
    }
}
