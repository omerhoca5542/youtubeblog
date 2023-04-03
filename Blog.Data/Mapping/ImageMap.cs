using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mapping
{
    public class ImageMap : IEntityTypeConfiguration<Image>//IEntityTypeConfiguration sınıfını aktif etmek için  addımplement diyoruz

    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(new Image// bu değerlerde ımage entitysi için yada tablosuna girilecek ilk değerleri verdiğimiz bölüm
            {
                Id = Guid.Parse("{942E2D1F-DDCF-4578-BE0A-016F0DAC1E65}"),
                FilaName = "dosyaadı/yolu",
                FileType = "jpg",
                CreatedBy = "hasan",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            },
            new Image// bu değerlerde ımage entitysi için yada tablosuna girilecek ilk değerleri verdiğimiz bölüm
            {
                Id = Guid.Parse("{C1C64D2F-15CF-4DB0-AF13-B1F50B4C0670}"),
                FilaName = "dosyaadı/yolu",
                FileType = "jpg",
                CreatedBy = "hasan",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            }


            );
        }
    }
}
