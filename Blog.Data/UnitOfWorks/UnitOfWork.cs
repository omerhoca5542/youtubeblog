using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.UnitOfWorks
{
    //    Unit Of Work tasarım deseni, yazılım uygulamamızda veritabanıyla ilgili her bir aksiyonun anlık olarak veritabanına yansıtılmasını engelleyen ve buna nazaran tüm aksiyonları biriktirip bir bütün olarak bir defada tek bir connection üzerinden gerçekleştirilmesini sağlayan ve böylece veritabanı maliyetlerini oldukça minimize eden bir tasarım desenidir.Unit Of Work, toplu veritabanı işlemlerini tek seferde bir kereye mahsus execute eden ve böylece bu toplu işlem neticesinde kaç kayıtın etkilendiğini rapor olarak sunabilen bir tasarım desenidir.
    public class UnitOfWork : IUnitOfWork
    // implementasyonunu da yapmayı unutmayalım
    {
        private readonly AppDbContext dbContext;
        public object getRepository;

        public UnitOfWork(AppDbContext dbContext)// constructure oluşturduk.Db context olarak da dbcontext imizi verdik.sol tarafta çıkan tornavida işaretine tıklayarak field oluştur dedik
        {
            this.dbContext = dbContext;
        }

       

        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }


        public int Save()
        {
            return dbContext.SaveChanges();// int bir değer olduğu için return kullandık
        }

        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWork.GetRepository<T>()
        {
            return new Repository<T>(dbContext);
            // başka bir clasta bu unitofworksü çağırıp buradan getrepository komutunu çağırmış olucaz ve new ile repository clasından t ile herhangi bir entity i dbcontext sınıfına göre çağırmış oluyoruz
        }


    }
}