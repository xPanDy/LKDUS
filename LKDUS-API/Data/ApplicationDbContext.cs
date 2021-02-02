using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LKDUS_API.Data
{
    public class ApplicationDbContext : IdentityDbContext 
    {
        public DbSet<User> Users { get; set; }
      //  public DbSet<AspUser> AspUsers { get; set; }
     //   public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

        public DbSet<MeasurementType> MeasurementsType { get; set; }
        public DbSet<MeasurementPosition> MeasurementPositions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Pack> Packs { get; set; }
        


        //public DbSet<FusPack> FusPacks { get; set; }

        public virtual DbSet<FusPack> FusPack { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<Measurement>()
                .Property(p => p.Measurement1 )
                .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
              .Property(p => p.Measurement2)
              .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
              .Property(p => p.Measurement3)
              .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
              .Property(p => p.Measurement4)
              .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
              .Property(p => p.Measurement5)
              .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
              .Property(p => p.Measurement6)
              .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
              .Property(p => p.Measurement7)
              .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
              .Property(p => p.Measurement8)
              .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
              .Property(p => p.Measurement9)
              .HasColumnType("decimal(6,2)");

            // builder.Entity<FusPack>(entity =>
            // {
            //    //entity.HasKey(e => e.Id);
            //     entity.ToTable("FusPack");
            //     entity.HasNoKey();
            //    //entity.Property(e => e.Name);

            //});
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
