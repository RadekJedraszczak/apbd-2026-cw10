using apbd_2026_cw10.Configurations;
using apbd_2026_cw10.Entities;
using Microsoft.EntityFrameworkCore;

namespace apbd_2026_cw10.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<PCs> PCs { get; set; }
    public DbSet<PcComponents> PcComponents { get; set; }
    public DbSet<Components> Components { get; set; }
    public DbSet<ComponentTypes> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturers> ComponentManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //wstrzykuje wszystkie konfiguracje które utworzymy w folderze Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        //wstrzyukje dana konfiguracje
        modelBuilder.ApplyConfiguration(new PcConfigurations());
        
        
        modelBuilder.Entity<PCs>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).HasMaxLength(50);

            e.ToTable("PCs");
        });

        modelBuilder.Entity<PcComponents>(e =>
        {
            //klucz złożony 
            e.HasKey(p => new {p.PcId, p.ComponentCode});
            e.Property(p => p.ComponentCode)
                .HasColumnType("char(10)")
                .IsRequired();
            //klucz obcy, FK dla PCs
            e.HasOne(p => p.Pc)
                .WithMany(c => c.PcComponents)
                .HasForeignKey(p => p.PcId)
                .OnDelete(DeleteBehavior.Cascade);
            //klucz obyc, FK dla Components
            e.HasOne(p => p.Component)
                .WithMany(c => c.PcComponents)
                .HasForeignKey(p => p.ComponentCode)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ComponentManufacturers>().HasData(new List<ComponentManufacturers>()
    {
        new ComponentManufacturers { Id = 1, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateTime(1969, 5, 1) },
        new ComponentManufacturers { Id = 2, Abbreviation = "INTC", FullName = "Intel Corporation", FoundationDate = new DateTime(1968, 7, 18) },
        new ComponentManufacturers { Id = 3, Abbreviation = "NVDA", FullName = "NVIDIA Corporation", FoundationDate = new DateTime(1993, 4, 5) }
    });

    // 2. Seeding dla ComponentTypes
    modelBuilder.Entity<ComponentTypes>().HasData(new List<ComponentTypes>()
    {
        new ComponentTypes { Id = 1, Abbreviation = "CPU", Name = "Processor" },
        new ComponentTypes { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
        new ComponentTypes { Id = 3, Abbreviation = "RAM", Name = "Random Access Memory" }
    });

    // 3. Seeding dla Components
    modelBuilder.Entity<Components>().HasData(new List<Components>()
    {
        new Components { Code = "CPU-AMD-01", Name = "Ryzen 7 7800X3D", Description = "8-core gaming processor", ComponentManufacturesId = 1, ComponentTypesId = 1 },
        new Components { Code = "CPU-INT-01", Name = "Core i7-14700K", Description = "20-core desktop processor", ComponentManufacturesId = 2, ComponentTypesId = 1 },
        new Components { Code = "GPU-NVD-01", Name = "RTX 4080 Super", Description = "High-end gaming graphics card", ComponentManufacturesId = 3, ComponentTypesId = 2 }
    });

    // 4. Seeding dla PCs 
    modelBuilder.Entity<PCs>().HasData(new List<PCs>()
    {
        new PCs { Id = 1, Name = "Gaming Beast X", Weight = 12.5f, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5 },
        new PCs { Id = 2, Name = "Office Mini Pro", Weight = 4.2f, Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
        new PCs { Id = 3, Name = "Creator Workstation", Weight = 15.0f, Warranty = 36, CreatedAt = new DateTime(2026, 5, 10, 10, 0, 0), Stock = 2 }
    });

    // 5. Seeding dla PcComponents 
    modelBuilder.Entity<PcComponents>().HasData(new List<PcComponents>()
    {
        new PcComponents { PcId = 1, ComponentCode = "CPU-AMD-01", Amount = 1 },
        new PcComponents { PcId = 1, ComponentCode = "GPU-NVD-01", Amount = 1 },
        new PcComponents { PcId = 2, ComponentCode = "CPU-INT-01", Amount = 1 }
    });
        
        //modelBuilder.ApplyConfiguration()
        
        base.OnModelCreating(modelBuilder);
    }
}