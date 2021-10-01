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
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Classs> Classes { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<MeasurementRange> MeasurementRanges { get; set; }
        
      

        //public DbSet<FusPack> FusPacks { get; set; }

        public virtual DbSet<FusPack> FusPack { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

          /*  builder.Entity<Pack>()
                .HasMany(c => c.Measurements)
                .WithOne(e => e.Pack);
                */

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
           
            
            builder.Entity<Measurement>()
              .Property(p => p.Measurement10)
              .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement11)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement12)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement13)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement14)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement15)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement16)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement17)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement18)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement19)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
                     .Property(p => p.Measurement20)
                     .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement21)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement22)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement23)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement24)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement25)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement26)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement27)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement28)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
      .Property(p => p.Measurement29)
      .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement30)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement31)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement32)
             .HasColumnType("decimal(6,2)");

            builder.Entity<Measurement>()
             .Property(p => p.Measurement33)
             .HasColumnType("decimal(6,2)");


            builder.Entity<MeasurementRange>()
            .Property(p => p.FormatMin)
            .HasColumnType("decimal(10,2)");

            builder.Entity<MeasurementRange>()
            .Property(p => p.FormatMax)
            .HasColumnType("decimal(10,2)");

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
