namespace Blog.Web.ResultMessage
{
    public static class Messages// static bir class olduğundan new ile bu sınıftan nesne türetmeye gerek yok direkt Messages classını çağırıp içinden Article sınıfı ile mesajları alabiliriz
    {
        public static class Article
        {
            public static string Add (string ArticleTitle)
            {
                return $"{ArticleTitle} başlıklı makale başarı ile Eklenmiştir";
                // $ işareti  ile ArticleTitle içine bu mesaj eklendi
            }
            public static string Update(string ArticleTitle)
            {
                return $"{ArticleTitle} başlıklı makale başarı ile güncellenmiştir";
            }
            public static string Delete(string ArticleTitle)
            {
                return $"{ArticleTitle} başlıklı makale başarı ile silinmiştir";
            }
        }
    }
}
