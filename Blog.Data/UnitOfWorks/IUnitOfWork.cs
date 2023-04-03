using Blog.Core.Entities;
using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.UnitOfWorks
{
   public interface IUnitOfWork: IAsyncDisposable// Idısposible sınıfından türeyen bir sınıf
    {
        IRepository<T> GetRepository<T> () where T:class,IEntityBase,new();
        ////T NİN BİR CLASS OLDUĞUNU BELİRTTİK.Ayrıca IEntitybase sınıfımız üzerinden etiketleme yapacağımızı yani IEntityBase sınıfından türememiş hiçbir sınıfın bu kısmı kullanamayacağını anlattık belirttik.new() ile ise gelen değerleri newleyebileceğimizi anlattık
        Task<int> SaveAsync();//int değer döndüren bir asenkron metot tanımladık
       int Save();// asenkron olarak kullanamayacağımız durumlarda save adında başka metot tanımladık

    }
}
