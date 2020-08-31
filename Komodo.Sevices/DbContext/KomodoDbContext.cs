// - Required Assemblies
using Microsoft.EntityFrameworkCore;

// - Solution Assemblies

namespace Komodo.Sevices.DbContext
{
  public class KomodoDbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    #region Properties

    public DbSet<Commodity> Commodities { get; set; }
    public DbSet<CommodityGroup> CommodityGroups { get; set; }

    #endregion

    #region ConstructDestruct

    public KomodoDbContext(DbContextOptions<KomodoDbContext> options) : base(options)
    {

    }

    #endregion

    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // - Seed Data
      base.OnModelCreating(modelBuilder);

      // - Seed CommodityGroups
      modelBuilder.Entity<CommodityGroup>().HasData(new CommodityGroup{CommodityGroupId = 1, CommodityGroupCode = "0001", CommodityGroupDescription = "CommodityGroup1"});
      modelBuilder.Entity<CommodityGroup>().HasData(new CommodityGroup{CommodityGroupId = 2, CommodityGroupCode = "0002", CommodityGroupDescription = "CommodityGroup2"});
      modelBuilder.Entity<CommodityGroup>().HasData(new CommodityGroup{CommodityGroupId = 3, CommodityGroupCode = "0003", CommodityGroupDescription = "CommodityGroup3"});
      modelBuilder.Entity<CommodityGroup>().HasData(new CommodityGroup{CommodityGroupId = 4, CommodityGroupCode = "0004", CommodityGroupDescription = "CommodityGroup4"});
      modelBuilder.Entity<CommodityGroup>().HasData(new CommodityGroup{CommodityGroupId = 5, CommodityGroupCode = "0005", CommodityGroupDescription = "CommodityGroup5"});

      // - Seed Commodities
      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 1, CommodityCode = "0001", CommodityDescription = "Commodity1", CommodityGroupId = 1});
      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 2, CommodityCode = "0001", CommodityDescription = "Commodity2", CommodityGroupId = 1});
      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 3, CommodityCode = "0001", CommodityDescription = "Commodity3", CommodityGroupId = 1});

      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 4, CommodityCode = "0001", CommodityDescription = "Commodity4", CommodityGroupId = 2});
      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 5, CommodityCode = "0001", CommodityDescription = "Commodity5", CommodityGroupId = 2});
      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 6, CommodityCode = "0001", CommodityDescription = "Commodity6", CommodityGroupId = 2});

      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 7, CommodityCode = "0001", CommodityDescription = "Commodity7", CommodityGroupId = 3});
      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 8, CommodityCode = "0001", CommodityDescription = "Commodity8", CommodityGroupId = 3});
      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 9, CommodityCode = "0001", CommodityDescription = "Commodity9", CommodityGroupId = 3});

      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 10, CommodityCode = "0001", CommodityDescription = "Commodity10", CommodityGroupId = 4});
      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 11, CommodityCode = "0001", CommodityDescription = "Commodity11", CommodityGroupId = 4});
      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 12, CommodityCode = "0001", CommodityDescription = "Commodity12", CommodityGroupId = 4});

      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 13, CommodityCode = "0001", CommodityDescription = "Commodity13", CommodityGroupId = 5});
      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 14, CommodityCode = "0001", CommodityDescription = "Commodity14", CommodityGroupId = 5});
      modelBuilder.Entity<Commodity>().HasData(new Commodity{CommodityId = 15, CommodityCode = "0001", CommodityDescription = "Commodity15", CommodityGroupId = 5});
    }

    #endregion
  }
}
