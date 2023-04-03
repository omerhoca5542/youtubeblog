using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repositories.Abstractions
{//repositories klasörünü cruid yani veri tabanına ekleme silme güncelleme işlemleri için kullanıcaz.Abstractions klasörü sanal (soyut) olan interfaceleri içinde tutacak .Bu kısımda imzalar atılır yani asıl işi burda yapılmaz 
    public interface IRepository<T> where T : class,IEntityBase, new()
    {//T NİN BİR CLASS OLDUĞUNU BELİRTTİK.Ayrıca IEntitybase sınıfımız üzerinden etiketleme yapacağımızı yani IEntityBase sınıfından türememiş hiçbir sınıfın bu kısmı kullanamayacağını anlattık belirttik.new() ile ise gelen değerleri newleyebileceğimizi anlattık
        Task AddAsync(T entity);// burda Repostroy.cs deki metotun aynısını başında async olmadan yazdık.aslında Repository de somut olan nesnenin burda soyut haline çevirme işlemini yapmış olduk.repositoryde içini doldurduk yani yapması gereken işlemleri yaptırdık burada da çağırdık.BURADA TANIMLADIK REPOSİTORY DE IMPLEMENT ETTİK
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);//Aynı işlemi Repository de yapmıştık.Burda birden fazla değer dönecek ki onu List<T> ile aldık liste kullandık.dönen değeri boolen değer olarak aldık
        Task<T> GetAsync(Expression<Func<T, bool>> predicate , params Expression<Func<T, object>>[] includeProperties);// bu kısımda ise sadece bir değer döndürmek için kullandığımız bi metot oluşturduk.Örnek sadece bi tane veriyi arayabilir yada silebiliriz o yüzden tek değer istememiz durumunda kullanılacak metot.predicate kısmına birşey yazmadık illaki bir tane değer döndürmesi lazım
        Task<T> GetByGuidAsync(Guid id);// burada da dışardan verilen id değerine göre işlem yapıcağımız metodu tanımladık
        Task<T> UpdateAsync (T entity);// update işlemleri için Task<T> ile T değeri yani entity  alacak ve (T entity) ile entity döndürecek
        Task DeleteAsync (T entity);// burda taskın içine T yazmadık çünkü herhangi bir değer almıcak silme işleminde ama geri yine entity döndürecek
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);//burda entitymizde yani tablomuzda veri var mı atıyorum category tablosuna veri girilmiş mi kontrol ettiğimiz metot bu olacak.geriye tabiki boolean değer yani true false döndürecek.
        Task<int> CountAsync(Expression<Func<T, bool>> predicate=null);// Burda da herhangi bir tabloda misal ımage tablosunda kaçtane veri olduğunu int olarak geri döndüren CountAsync adlı metodu yazdık ve predicate=null ile geri boş değer de döndürebilir dedik.illaki bi değer gelmesini şart koşmadık
    }
   
}
