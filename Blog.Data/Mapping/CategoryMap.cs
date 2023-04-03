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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData( new Category
            { // BURADA DA category entitysi için ilk verileri giriyoruz
                Id = Guid.Parse("{C5D8CD84-F517-438F-BAF4-E691F37088FB}"),
                Name = ".net core anoptations",
                CreatedBy = "Omer",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            },
               new Category
               { // BURADA DA category entitysi için ilk verileri giriyoruz
                   Id = Guid.Parse("{CA6D96B6-C2E1-48ED-9208-60246AC1194F}"),
                   Name = ".net core anoptations",
                   CreatedBy = "Omer",
                   CreatedDate = DateTime.Now,
                   IsDeleted = false,
               }


                );

            
           
        }
    }
}
