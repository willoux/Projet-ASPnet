using Isen.DotNet.Library.Models.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Isen.DotNet.Library.Data
{
    public class ApplicationDbContext : DbContext
    {
        // 1 - Préciser les DbSet
        public DbSet<City> CityCollection { get;set; }
        public DbSet<Person> PersonCollection { get;set; }
        public DbSet<Departement> DepartCollection { get;set; }
        public DbSet<Commune> CommuneCollection { get;set; }
        public DbSet<Address> AddressCollection { get;set; }
        public DbSet<CatPoi> CatPoiCollection { get;set; }
        public DbSet<Poi> PoiCollection { get;set; }



        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(
            ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // 2 - Configurer les mappings tables / classes
            builder.Entity<City>()
                .ToTable("City")
                .HasMany(c => c.PersonCollection)
                .WithOne(p => p.City)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Person>()
                .ToTable("Person")
                .HasOne(p => p.City)
                .WithMany(c => c.PersonCollection)
                .HasForeignKey(p => p.CityId);

             builder.Entity<Commune>()
                .ToTable("Commune")
                .HasOne(co => co.Departement)
                .WithMany(d => d.CommuneCollection)
                .HasForeignKey(co => co.DepartementId);

             builder.Entity<Address>()
                .ToTable("Address")
                .HasOne(ad => ad.Commune)
                .WithMany(co => co.AddressCollection)
                .HasForeignKey(ad => ad.CommuneId);

            builder.Entity<Poi>()
                .ToTable("Poi")
                .HasOne(po => po.Address)
                .WithMany(ad => ad.PoiCollection)
                .HasForeignKey(po => po.AddressId);

            builder.Entity<Poi>()
                .ToTable("Poi")
                .HasOne(po => po.Category)
                .WithMany(cat => cat.PoiCollection)
                .HasForeignKey(po => po.CategoryId);
        }
    }
}