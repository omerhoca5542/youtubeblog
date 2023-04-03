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
    public class RoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<AppRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            builder.HasData(new AppRole
            { // data seeding yani veri tabanınında ilk veri girişlerin, yapıyoruz ya da kural girişi de diyebiliriz
              // id için yukarıdaki tools kısmından new quid kısmından bir quid yani benzersiz  id kopyalayıp burda yapıştırıyoruz
                Id = Guid.Parse("{5E47C048-4307-4A74-AC97-40AA57E19CB4}"),
                Name = "Superadmin",
                NormalizedName = "SUPERADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
                //ConcurrencyStamp aynı anda sisteme girmek isteyen iki kullanıcının çakışmaması için kullanılan bir yöntem
            },
                new AppRole//SÜPER ADMİN ADMİN VE USER İÇİN AYRI AYRI YENİ DEĞERLER EKLEDİK
                {
                    Id = Guid.Parse("{FFB058AD-2AE3-456F-AC01-6357F5E30C45}"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new AppRole {
                Id=Guid.Parse("{00B3F581-D52F-46A0-A4CA-A0EEEF49491D}"),
                Name="User",
                NormalizedName="USER",
                ConcurrencyStamp=Guid.NewGuid().ToString()
                }

                  );  

            
        }
    }
}
