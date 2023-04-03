using Blog.Core.Entities;
using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Data.Repositories.Concretes
{
    //repositories klasörünü cruid yani veri tabanına ekleme silme güncelleme işlemleri için kullanıcaz.Concretes somut olan (sanal olmayan) classları içinde tutacak .
    public class Repository<T> : IRepository<T> where T : class, IEntityBase, new()//T NİN BİR CLASS OLDUĞUNU BELİRTTİK.Ayrıca IEntitybase sınıfımız üzerinden etiketleme yapacağımızı yani IEntityBase sınıfından türememiş hiçbir sınıfın bun kısmı kullanamayacağını anlattık belirttik.new() ile ise gelen değerleri newleyebileceğimizi anlattık.Repository<T>:IRepository<T> yapmamaızın sebebi ise IRepositorry olan soyut sınıftan miras aldık
    {
        private readonly AppDbContext dbContext;
        private DbContext dbContext1;

        // alttaki appdbcontext nesnesi üzerindebn field oluştur diyerek readonly yani sadece okunablir AppDbContext nesnesi  yaptık
        public Repository(AppDbContext dbContext)
        //bir yapıcı metot oluşturduk ve AppDbContext sınıfımızın özelliklerini almak için  dbContext adında parametreyi AppDbContext sınıfından çağırdık
        {
            this.dbContext = dbContext;
        }

        public Repository(DbContext dbContext1)
        {
            this.dbContext1 = dbContext1;
        }

        private DbSet<T> Table { get => dbContext.Set<T>(); }
        //Table adında dbseti set ettiğimiz bir metot oluşturduk
        // burada dbset olarak t nesnesi oluşturduk.şimdi bu db set get olayında yukarda tanımladığımız dbContext nesnesinden set edilecek .yani t nesnesinin özelliklerine değer atanacak.Bu t nesnesinin bir classa ait olduğunu anlatmak içinde yukarıda tanımlama yapıcaz.
        //get metodu Değişken çağırıldığında çalışır.SET metodu: Değişkene değer //atandığında
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)

        //öncelikle  biz GetAllAsync adında yani hepsini getir manasında bir metot oluşturduk bu metot liste şeklinde değerleri alacak.bu metotun içinden expression metotuna bağlı func metotunu çağırdık içine T dediğimiz entity verimizi gönderip geriye boolean bir değer döndürücez ve gelen değerilk başta null kabul ettik. sonrasında  ise yine t ile herhangi bir entity gönderip geriye object döndürüyoruz bunu yaparken de birden fazla değer döneceğinden çoğul isim kullandık.Burda include metodu farklı olan entityler arasında yani article , ımage yada category arasındaki verilere tek bir entity üzerinden ulaşmamızı sağlıyor.Biz artık GetAllAsync metodu ile entityler arasında işlem yapabilicezyani onların değerlerini herhangi bir entity üzerinden çağırabilicez

        {
            IQueryable<T> query = Table;// ıquaryable türünde bir t nesnesine set edilmiş bir entity olan Table a eşitledik
            if (predicate != null) {// gelen değer null değilse  
                query = query.Where(predicate);// predicate den gelen değeri bana getir

            }
            if (includeProperties.Any())// any bir metod .eğer includeproperties içinde bir değer varsa
            {
                foreach (var item in includeProperties) // o değerleri bul item ile döndür
                {
                    query = query.Include(item);
                }
                // itemdan gelen değerleri query ye at
            }
            return await query.ToListAsync();// ve bu değerleri liste olarak gönder.

            //biz burada include ile farklı tablolardaki yani entitylerdeki değerleri  tek bir entity üzerinden almayı da sağladık 


        }
        public async Task AddAsync(T entity)//AddAsync adında asenkron  bi metot oluşturduk.T yi de entity olarak gönderceğimiz için yanına entity yazdık.Burda Task = void demektir. yani bu metotta geriye değer döndürme işlemi yapamazsınız
        {
            await Table.AddAsync(entity);// table metoduyla entity ekleme işlemi yapıyoruz.Burdaki AddAsync bir metot asenkron ekleme yapıypr


        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);
            if (includeProperties.Any())// any bir metod .eğer includeproperties içinde bir değer varsa
            {
                foreach (var item in includeProperties) // o değerleri bul item ile döndür
                {
                    query = query.Include(item);

                }
                
            }
            return await query.SingleAsync();// bize istediğimiz tablo neyse oradan tek bir tane değer döndürmesini singleasync komutu ile sağladık
        }
            public async Task<T> GetByGuidAsync(Guid id)
            {
                return await Table.FindAsync(id);// table için  en yukarda yazdık kuralını. Table bir Dbset olarak tanımlandı yani entitylerden değer alabiliyoruz onunla.FindAsync ile değer buluyoruz o değer de id yazan değer olacak 
            }
        //async; içerisinde asenkron işlem yapılacak metodu belirtir.Benzer ifadeyle, içerisinde asenkron işlem yapacağımız metodu async keywordü ile işaretlemeliyiz.
            public async Task<T> UpdateAsync(T entity)
            {
            //asnyc ile işaretlenmiş bir metodda asenkron çalışacak komutlar await ile işaretlenir.
               await Task.Run(()=>Table.Update(entity));
            //task.run asenkron kurulan metodu işletmek için kullanılan komuttur
            // asenkron olarak update metodu olmadığından biz önce table ile aldığımız entityleri update etme komutunu kullandık .
            return entity;// burda da gelen entity değerlerini döndürdük.
            }

            public async Task DeleteAsync(T entity)
            {
            await Task.Run(() => Table.Remove(entity));
            // burda geri değer döndürmüyoruz
            }

            public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
            {
            return await Table.AnyAsync(predicate);
            }

            public async Task<int> CountAsync(Expression<Func<T, bool>> predicate =null)
            { // metot asenkron olacak bool olarak değer gelecek ama o değer int türüne çevirilecek
            return await Table.CountAsync(predicate);
            }
        }

  

      
     
    }

