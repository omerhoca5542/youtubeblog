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
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
     

        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {

            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");
            builder.HasData(new AppUserRole {
           UserId= Guid.Parse("{295411B6-4B8B-455B-B108-19092EA05963}"),//burda ilişkiler tanımlamak için usermapte verdiğimiz guid yi burayada aynsını kopyalıyoruz
            RoleId= Guid.Parse("{5E47C048-4307-4A74-AC97-40AA57E19CB4}"),// bu değeri de rolemapdeki superadmindeki  guid sini kopyalayarak aldım
            },
                new AppUserRole {
                UserId= Guid.Parse("{98F208D2-64AD-4284-AA39-40DA28EDF5A4}"),//burda ilişkiler tanımlamak için usermapte verdiğimiz guid yi burayada aynsını kopyalıyoruz
                RoleId= Guid.Parse("{00B3F581-D52F-46A0-A4CA-A0EEEF49491D}")// bu değeri de rolemapdeki admindeki  guid sini kopyalayarak aldım
                }
                );
        }
    }
}
