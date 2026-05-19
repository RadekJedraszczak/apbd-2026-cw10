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

        modelBuilder.Entity<Components>().HasData(new List<Components>()
        {
            new Components() {Code = "12q3", Name = "controller", Description = "brand new", 
                ComponentManufacturers =  new ComponentManufacturers(), ComponentTypes = new ComponentTypes()}
        });
        
        //modelBuilder.ApplyConfiguration()
        
        base.OnModelCreating(modelBuilder);
    }
}