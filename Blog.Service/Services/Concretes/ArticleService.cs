
using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOS.Articles;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Service.Services.Concretes
{// service classları çağırdığımız entitylerle ilgili filtreleme yapma durumlarında işimize yarayacak.örneğin herhangi bir tablodan istediğimiz ilk 5 kaydı çekmek istersek bunu burda belirtebiliriz
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;//field oluşturduk burada 

        public ArticleService(IUnitOfWork unitOfWork,IMapper mapper)
        // unitofwork un üzerine ctrl+. yapınca buradan add field diyerek yukarıdaki  private readonly IUnitOfWork unitOfWork; kısmı ekleniyor
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<List<ArticleDTO>> GetAllArticlesWithCategoryNonDeletedAsync()// burada bize article türünden liste olarak veriler gelecek.Biz category alanında silinmemiş olan categoryleri getir isminde bi metottanımladık
        {
           var articles= await unitOfWork.GetRepository<Article>().GetAllAsync(x=>!x.IsDeleted, x=>x.Category);
            // x=>!x.IsDeleted bu kod ile silinen yerine ! işareti koyarak silinmemiş olan ve bu kod ile de x=>x.Category category kısmından veriler alıyoruz 
            var map = mapper.Map<List<ArticleDTO>>(articles);

            return map;
            // yukarıda unitofwork adında bir nesne oluşturmuştuk.şimdi o nesneyle Getrepository komutunu kullanarak article türünden verileri GettAllAsync metodunu kullanarak çağırabiliriz 
        }//şimdi yapıları oluşturduk  bunları çağırmak için BlogWeb içinde bulunan Controller kısmına gidicez
    }
}
