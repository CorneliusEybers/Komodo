// - Required Assemblies
using System.Collections.Generic;

// - Application Assemblies
using Komodo.Ui.Models;

namespace Komodo.Ui.Repositories.Commodity
{
  public interface ICommodityRepository
  {
    #region CommodityGroup

    /// <summary>
    /// <para> - Retrieve list of CommodityGroups.</para>
    /// <para> - Can be filtered by the Description.</para>
    /// <para>   Retrieve only all CommodityGroups which description contains</para>
    /// <para>   the filter criteria...</para>
    /// </summary>
    /// <param name="filterDescription">
    /// - Description contains...
    /// </param>
    /// <returns>
    /// - List of CommodityGroups
    /// </returns>
    IEnumerable<Models.Commodity.CommodityGroup> GetCommodityGroups(string filterDescription);

    /// <summary>
    /// - Retrieve the CommodityGroup of the key specified.
    /// </summary>
    /// <param name="commodityGroupId">
    /// - Identifier of the CommodityGroup to retrieve
    /// </param>
    /// <returns>
    /// - CommodityGroup
    /// </returns>
    Models.Commodity.CommodityGroup GetCommodityGroup(int commodityGroupId);

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
    Models.Commodity.CommodityGroup CreateCommodityGroup(Models.Commodity.CommodityGroup commodityGroup);

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
    Models.Commodity.CommodityGroup UpdateCommodityGroup(Models.Commodity.CommodityGroup commodityGroup);

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
    Models.Commodity.CommodityGroup DeleteCommodityGroup(int commodityGroupId);

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
    IEnumerable<Models.Commodity.Commodity> GetCommodities(string filterDescription);

    /// <summary>
    /// - Retrieve the Commodity of the key specified.
    /// </summary>
    /// <param name="commodityId">
    /// - Identifier of the Commodity to retrieve
    /// </param>
    /// <returns>
    /// - Commodity
    /// </returns>
    Models.Commodity.Commodity GetCommodity(int commodityId);

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
    Models.Commodity.Commodity CreateCommodity(Models.Commodity.Commodity commodity);

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
    Models.Commodity.Commodity UpdateCommodity(Models.Commodity.Commodity commodity);

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
    Models.Commodity.Commodity DeleteCommodity(int commodityId);

    #endregion
  }
}