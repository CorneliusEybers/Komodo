// - Required Assemblies
using System.Collections.Generic;
using System.Threading.Tasks;

// - Application Assemblies
using Komodo.Sevices.DbContext;

namespace Komodo.Sevices.Repositories.Commodity
{
  public interface ICommodityRepository
  {
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
    Task<bool> CommodityGroupExists(int commodityGroupId);

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
    Task<IEnumerable<CommodityGroup>> GetCommodityGroups(string filterDescription);

    /// <summary>
    /// - Retrieve the CommodityGroup of the key specified.
    /// </summary>
    /// <param name="commodityGroupId">
    /// - Identifier of the CommodityGroup to retrieve
    /// </param>
    /// <returns>
    /// - CommodityGroup
    /// </returns>
    Task<CommodityGroup> GetCommodityGroup(int commodityGroupId);

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
    Task<CommodityGroup> CreateCommodityGroup(CommodityGroup commodityGroup);

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
    Task<CommodityGroup> UpdateCommodityGroup(CommodityGroup commodityGroup);

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
    Task<CommodityGroup> DeleteCommodityGroup(int commodityGroupId);

    #endregion

    #region Commodity

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
    Task<IEnumerable<DbContext.Commodity>> GetCommodities(string filterDescription);

    /// <summary>
    /// - Retrieve the Commodity of the key specified.
    /// </summary>
    /// <param name="commodityId">
    /// - Identifier of the Commodity to retrieve
    /// </param>
    /// <returns>
    /// - Commodity
    /// </returns>
    Task<DbContext.Commodity> GetCommodity(int commodityId);

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
    Task<DbContext.Commodity> CreateCommodity(DbContext.Commodity commodity);

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
    Task<DbContext.Commodity> UpdateCommodity(DbContext.Commodity commodity);

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
    Task<DbContext.Commodity> DeleteCommodity(int commodityId);

    #endregion
  }
}
