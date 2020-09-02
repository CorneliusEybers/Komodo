// - Required Assemblies

using System;
using System.Collections.Generic;
using System.Linq;
using Komodo.Sevices.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// - Application Assemblies

namespace Komodo.Services.Tests
{
  public static class TestSetup
  {
    public static T CreateDbContext<T>(string dbContextName) where T : DbContext
    {
      var configuration = GetConfiguration();
      var builder = new DbContextOptionsBuilder<T>().UseSqlServer(configuration.GetConnectionString(dbContextName));

      return (T) Activator.CreateInstance(typeof(T), builder.Options);
    }

    public static IConfigurationRoot GetConfiguration()
    {
      return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }

    internal static List<CommodityGroup> BuildCommodityGroups()
    {
      int commodityGroupDepth = 5;
      var commodityGroups = new List<CommodityGroup>();
      var commodities = new List<Commodity>();

      for (int commodityGroupIndex = 1; commodityGroupIndex <= commodityGroupDepth; commodityGroupIndex++)
      {
        var commodityGroupId = commodityGroups.Count < 1 ? 1 :  commodityGroups.Max(CmdGrp => CmdGrp.CommodityGroupId) + 1;

        var commodityGroup = new CommodityGroup()
                             {
                               CommodityGroupId          = commodityGroupId,
                               CommodityGroupCode        = "GRP"                + commodityGroupIndex.ToString("000"),
                               CommodityGroupDescription = "Group Description " + commodityGroupIndex.ToString("000")
                             };

        // - Gather the children, Margeretha,
        //   there is a spin on the horizon,
        //   and its commin' our way!!!
        for (int commodityIndex = 1; commodityIndex <= commodityGroupDepth; commodityIndex++)
        {
          var commodity = new Commodity()
                          {
                            CommodityId          = commodities.Count < 1 ? 1 : commodities.Max(Cmd => Cmd.CommodityId) + 1,
                            CommodityCode        = "CMD"                    + commodityIndex.ToString("000"),
                            CommodityDescription = "Commodity Description " + commodityIndex.ToString("000"),
                            CommodityGroupId     = commodityGroupId
                          };

          commodities.Add(commodity);
          commodityGroup.Commodities.Add(commodity);
        }

        // - Load it up, strap it down!!!
        commodityGroups.Add(commodityGroup);
      }

      return commodityGroups;
    }

    internal static List<Commodity> BuildCommodities()
    { 
      List<Commodity> commodities = new List<Commodity>()
                                    {
                                      new Commodity()
                                      {
                                        CommodityId          = 1,
                                        CommodityCode        = "001",
                                        CommodityDescription = "Commodity001"
                                      },
                                      new Commodity()
                                      {
                                        CommodityId          = 2,
                                        CommodityCode        = "002",
                                        CommodityDescription = "Commodity002"
                                      },
                                      new Commodity()
                                      {
                                        CommodityId          = 3,
                                        CommodityCode        = "003",
                                        CommodityDescription = "Commodity003"
                                      }
                                    };

      return commodities;
    }

    internal static Commodity BuildCommodity(int commodityId)
    {
      var commodity = new Commodity()
                      {
                        CommodityId = commodityId,
                        CommodityCode = commodityId.ToString("0000"),
                        CommodityDescription = "Commodity" + commodityId.ToString("000")
                      };

      return commodity;
    }

    internal static CommodityGroup BuildCommodityGroup(int commodityGroupId)
    {
      var commodityGroup = new CommodityGroup()
                           {
                             CommodityGroupId = commodityGroupId,
                             CommodityGroupCode = "0002",
                             CommodityGroupDescription = "CommodityGroup2",
                             Commodities = new List<Commodity>()
                                           {
                                             new Commodity()
                                             {
                                               CommodityId = 1,
                                               CommodityCode = "001",
                                               CommodityDescription = "Commodity001"
                                             },
                                             new Commodity()
                                             {
                                               CommodityId = 2,
                                               CommodityCode = "002",
                                               CommodityDescription = "Commodity002"
                                             },
                                             new Commodity()
                                             {
                                               CommodityId = 3,
                                               CommodityCode = "003",
                                               CommodityDescription = "Commodity003"
                                             }
                                           }
                           };

      return commodityGroup;
    }
  }
}
