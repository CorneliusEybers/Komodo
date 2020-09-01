// - Required Assemblies

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// - Application Assemblies
using Komodo.Sevices.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Komodo.Sevices.Repositories.Commodity
{
  public class CommodityRepositorySql : ICommodityRepository
  {
    #region ClassVariables

    private KomodoDbContext mc_KomodoDbContext;

    #endregion

    #region construct

    public CommodityRepositorySql(KomodoDbContext komodoDbContext)
    {
      mc_KomodoDbContext = komodoDbContext;
    }

    #endregion

    #region Methods

    #region CommodityGroup

    /// <summary>
    /// - Determine whether CommodityGroup of the Id(primary key) exsits
    /// </summary>
    /// <param name="commodityGroupId">
    /// - Identifier of the CommodityGroup to find
    /// </param>
    /// <returns>
    /// - True : Found. Does Exist.
    /// - False : NOT found. Does NOT Exist.
    /// </returns>
    public async Task<bool> CommodityGroupExists(int commodityGroupId)
    {
      var commodityGroupExists = await mc_KomodoDbContext.CommodityGroups.AnyAsync(CmdGrp => CmdGrp.CommodityGroupId == commodityGroupId);

      return commodityGroupExists;
    }

    /// <summary>
    /// <para> - Retrieve list of CommodityGroups.</para>
    /// <para> - Can be filtered by the Description.</para>
    /// <para> - Retrieve only all CommodityGroups which description contains the filter criteria...</para>
    /// </summary>
    /// <param name="filterDescription">
    /// - Description contains...
    /// </param>
    /// <returns>
    /// - List of CommodityGroups
    /// </returns>
    public async Task<IEnumerable<CommodityGroup>> GetCommodityGroups(string filterDescription)
    {
      List<CommodityGroup> commodityGroups;

      if (filterDescription == String.Empty)
      {
        commodityGroups = await mc_KomodoDbContext.CommodityGroups
                                                  .Include(CmdGrp => CmdGrp.Commodities)
                                                  .ToListAsync();
      }
      else
      {
        commodityGroups = await mc_KomodoDbContext.CommodityGroups
                                                  .Include(CmdGrp => CmdGrp.Commodities)
                                                  .Where(CmdGrp => CmdGrp.CommodityGroupDescription.Contains(filterDescription))
                                                  .ToListAsync();
      }

      return commodityGroups;
    }

    /// <summary>
    /// - Retrieve the CommodityGroup of the key specified.
    /// </summary>
    /// <param name="commodityGroupId">
    /// - Identifier of the CommodityGroup to retrieve
    /// </param>
    /// <returns>
    /// - CommodityGroup
    /// </returns>
    public async Task<CommodityGroup> GetCommodityGroup(int commodityGroupId)
    {
      var commodityGroup = await mc_KomodoDbContext.CommodityGroups
                                                   .Include(CmdGrp => CmdGrp.Commodities)
                                                   .FirstOrDefaultAsync(CmdGrp => CmdGrp.CommodityGroupId == commodityGroupId);

      return commodityGroup;
    }

    /// <summary>
    /// <para> - Add/Create the CommodityGroup</para>
    /// </summary>
    /// <param name="commodityGroup">
    /// <para> - CommodityGroup to Create. Object Fully Loaded.</para>
    /// </param>
    /// <returns>
    /// <para> - CommodityGroup Created with its assigned primary key.</para>
    /// <para> - Objects in its saved state.</para>
    /// </returns>
    public async Task<CommodityGroup> CreateCommodityGroup(CommodityGroup commodityGroup)
    {
      // - Shield creation from subscriber entirely
      var commodityGroupCreated = new CommodityGroup()
      {
        CommodityGroupCode = commodityGroup.CommodityGroupCode,
        CommodityGroupDescription = commodityGroup.CommodityGroupDescription
      };

      foreach (var commodity in commodityGroup.Commodities)
      {
        var commodityCreated = new DbContext.Commodity()
        {
          CommodityCode = commodity.CommodityCode,
          CommodityDescription = commodity.CommodityDescription
        };

        commodityGroupCreated.Commodities.Add(commodityCreated);
      }

      // - To the database
      mc_KomodoDbContext.CommodityGroups.Add(commodityGroupCreated);
      await mc_KomodoDbContext.SaveChangesAsync();

      // - BACK to the future!!!
      return commodityGroupCreated;
    }

    /// <summary>
    /// <para> CommodityGroup to be removed/deleted.</para>
    /// </summary>
    /// <param name="commodityGroupId">
    /// <para>Identifier of the CommodityGroup to be removed/deleted.</para>
    /// </param>
    /// <returns>
    /// <para> - All data of the CommodityGroup removed/deleted </para>
    /// <para>   in a ready for insert-state (keys=-1)</para>
    /// <para>   to facilitate undo by re-insert...</para>
    /// </returns>
    public async Task<CommodityGroup> DeleteCommodityGroup(int commodityGroupId)
    {
      var commodityGroupDeleted = await mc_KomodoDbContext.CommodityGroups
                                                          .Include(CmdGrp => CmdGrp.Commodities)
                                                          .FirstOrDefaultAsync(CmdGrp => CmdGrp.CommodityGroupId == commodityGroupId);
      CommodityGroup commodityGroupUndo = null;

      // - If the executed is dead do not kill him again...
      if (commodityGroupDeleted != null)
      {
        // - Build a new unrelated object that can be used for undo purposes
        commodityGroupUndo = new CommodityGroup();
        commodityGroupUndo.CommodityGroupCode = commodityGroupDeleted.CommodityGroupCode;
        commodityGroupUndo.CommodityGroupDescription = commodityGroupDeleted.CommodityGroupDescription;

        foreach (var commodityDeleted in commodityGroupDeleted.Commodities)
        {
          var commodityUndo = new DbContext.Commodity()
          {
            CommodityCode = commodityDeleted.CommodityCode,
            CommodityDescription = commodityDeleted.CommodityDescription
          };

          commodityGroupUndo.Commodities.Add(commodityUndo);
        }

        // - WHO? Gets the Chop?
        mc_KomodoDbContext.CommodityGroups.Remove(commodityGroupDeleted);
        await mc_KomodoDbContext.SaveChangesAsync();
      }

      return commodityGroupUndo;
    }

    /// <summary>
    /// <para> - Update/Edit the CommodityGroup</para>
    /// </summary>
    /// <param name="commodityGroup">
    /// <para> - CommodityGroup to be updated...</para>
    /// <para> - Object fully loaded.</para>
    /// </param>
    /// <returns>
    /// <para> - CommodityGroup updated.</para>
    /// <para> - Object in its saved state.</para>
    /// </returns>
    public async Task<CommodityGroup> UpdateCommodityGroup(CommodityGroup commodityGroup)
    {
      var commodityGroupExtant = await mc_KomodoDbContext.CommodityGroups
                                                         .Include(CmdGrp => CmdGrp.Commodities)
                                                         .FirstOrDefaultAsync(CmdGrp => CmdGrp.CommodityGroupId == commodityGroup.CommodityGroupId);

      if (commodityGroupExtant == null)
      {
        return commodityGroup;
      }

      commodityGroupExtant.CommodityGroupCode = commodityGroup.CommodityGroupCode;
      commodityGroupExtant.CommodityGroupDescription = commodityGroup.CommodityGroupDescription;

      foreach (var commodity in commodityGroup.Commodities)
      {
        var commodityExtant = await mc_KomodoDbContext.Commodities.FirstOrDefaultAsync(Cmd => Cmd.CommodityId == commodity.CommodityId);

        if (commodityExtant == null)
        {
          commodityExtant = new DbContext.Commodity()
          {
            CommodityCode = commodity.CommodityCode,
            CommodityDescription = commodity.CommodityDescription
          };

          commodityGroupExtant.Commodities.Add(commodityExtant);
        }
        else
        {
          commodityExtant.CommodityCode = commodity.CommodityCode;
          commodityExtant.CommodityDescription = commodity.CommodityDescription;
        }
      }

      await mc_KomodoDbContext.SaveChangesAsync();

      return commodityGroupExtant;
    }

    #endregion 

    #region Commodity

    /// <summary>
    /// <para> - Add/Create the Commodity</para>
    /// </summary>
    /// <param name="commodity">
    /// <para> - Commodity to Create. Object Fully Loaded.</para>
    /// </param>
    /// <returns>
    /// <para> - Commodity Created with its assigned primary key.</para>
    /// <para> - Objects in its saved state.</para>
    /// </returns>
    public async Task<DbContext.Commodity> CreateCommodity(DbContext.Commodity commodity)
    {
      var commodityCreated = new DbContext.Commodity()
      {
        CommodityCode = commodity.CommodityCode,
        CommodityDescription = commodity.CommodityDescription,
        CommodityGroupId = commodity.CommodityGroupId          // - Validation required: Must have value. Parent Must exist!
      };

      mc_KomodoDbContext.Commodities.Add(commodityCreated);
      await mc_KomodoDbContext.SaveChangesAsync();

      return commodityCreated;
    }

    /// <summary>
    /// <para> Commodity to be removed/deleted.</para>
    /// </summary>
    /// <param name="commodityId">
    /// <para>Identifier of the Commodity to be removed/deleted.</para>
    /// </param>
    /// <returns>
    /// <para> - All data of the Commodity removed/deleted </para>
    /// <para>   in a ready for insert-state (keys=-1)</para>
    /// <para>   to facilitate undo by re-insert...</para>
    /// </returns>
    public async Task<DbContext.Commodity> DeleteCommodity(int commodityId)
    {
      var commodityDeleted = await mc_KomodoDbContext.Commodities.FirstOrDefaultAsync(Cmd => Cmd.CommodityId == commodityId);
      DbContext.Commodity commodityUndo = null;

      if (commodityDeleted != null)
      {
        commodityUndo = new DbContext.Commodity();
        commodityUndo.CommodityCode = commodityDeleted.CommodityCode;
        commodityUndo.CommodityDescription = commodityDeleted.CommodityDescription;
        commodityUndo.CommodityGroupId = commodityDeleted.CommodityGroupId;

        // - To the database
        mc_KomodoDbContext.Commodities.Remove(commodityDeleted);
        await mc_KomodoDbContext.SaveChangesAsync();
      }

      // - Throw it back.
      return commodityUndo;
    }

    /// <summary>
    /// <para> - Retrieve list of Commodities.</para>
    /// <para> - Can be filtered by the Description.</para>
    /// <para>   Retrieve only all Commodities which description contains</para>
    /// <para>   the filter criteria...</para>
    /// </summary>
    /// <param name="filterDescription">
    /// - Description contains...
    /// </param>
    /// <returns>
    /// - List of Commodities
    /// </returns>
    public async Task<IEnumerable<DbContext.Commodity>> GetCommodities(string filterDescription)
    {
      var commodities = await mc_KomodoDbContext.Commodities
                                                .Where(Cmd => Cmd.CommodityDescription.Contains(filterDescription))
                                                .ToListAsync();

      return commodities;
    }

    /// <summary>
    /// - Retrieve the Commodity of the key specified.
    /// </summary>
    /// <param name="commodityId">
    /// - Identifier of the Commodity to retrieve
    /// </param>
    /// <returns>
    /// - Commodity
    /// </returns>
    public async Task<DbContext.Commodity> GetCommodity(int commodityId)
    {
      var commodity = await mc_KomodoDbContext.Commodities.FirstOrDefaultAsync(Cmd => Cmd.CommodityId == commodityId);

      return commodity;
    }

    /// <summary>
    /// <para> - Update/Edit the Commodity</para>
    /// </summary>
    /// <param name="commodity">
    /// <para> - Commodity to be updated...</para>
    /// <para> - Object fully loaded.</para>
    /// </param>
    /// <returns>
    /// <para> - Commodity updated.</para>
    /// <para> - Object in its saved state.</para>
    /// </returns>
    public async Task<DbContext.Commodity> UpdateCommodity(DbContext.Commodity commodity)
    {
      var commodityExtant = await mc_KomodoDbContext.Commodities.FirstOrDefaultAsync(Cmd => Cmd.CommodityId == commodity.CommodityId);

      if (commodityExtant != null)
      {
        commodityExtant.CommodityCode = commodity.CommodityCode;
        commodityExtant.CommodityDescription = commodity.CommodityDescription;
        commodityExtant.CommodityGroupId = commodity.CommodityGroupId;    // - Validation required: GroupId must be specified and exist

        await mc_KomodoDbContext.SaveChangesAsync();
      }

      return commodityExtant;
    }

    #endregion

    #endregion
  }
}
