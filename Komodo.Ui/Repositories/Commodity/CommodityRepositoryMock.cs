// - Required Assemblies
using System;
using System.Collections.Generic;
using System.Linq;

// - Application Assemblies
using Komodo.Ui.Models;
using Komodo.Ui.Models.Commodity;

namespace Komodo.Ui.Repositories.Commodity
{
  public class CommodityRepositoryMock : ICommodityRepository, IDisposable
  {
    #region ClassVariables

    /// <summary>
    /// - The Mock Data for Commodities...
    /// </summary>
    private List<Models.Commodity.CommodityGroup> mc_CommodityGroups;

    private List<Models.Commodity.Commodity> mc_Commodities;

    #endregion

    #region Construct

    public CommodityRepositoryMock()
    {
      mc_CommodityGroups = new List<CommodityGroup>();
      mc_Commodities = new List<Models.Commodity.Commodity>();
       
      BuildCommodityGroupsMock(commodityGroupDepth:5);
    }

    ~CommodityRepositoryMock()
    {
      try
      {
        this.Dispose();
      }
      catch (Exception)
      {
        // - Error on Dispose? do nothing
        ;
      }
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private void Dispose(Boolean disposing)
    {
      if (disposing)
      {
        // - Dispose stuff here.
        mc_CommodityGroups.Clear();
        mc_CommodityGroups = null;
      }
    }

    #endregion

    #region Public Methods

    #region CommodityGroup
    
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
    public Models.Commodity.CommodityGroup CreateCommodityGroup(Models.Commodity.CommodityGroup commodityGroup)
    {
      return new CommodityGroup();
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
    public Models.Commodity.CommodityGroup DeleteCommodityGroup(int commodityGroupId)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>
    /// <para> - Retrieve List of CommodityGroups.</para>
    /// <para> - Can be filtered by the Description.</para>
    /// <para>   Retrieve only all CommodityGroups which description contains</para>
    /// <para>   the filter criteria...</para>
    /// </summary>
    /// <param name="filterDescription">
    /// - Description contains...
    /// </param>
    /// <returns>
    /// - List of Commodities
    /// </returns>
    public IEnumerable<Models.Commodity.CommodityGroup> GetCommodityGroups(string filterDescription)
    {
      if (filterDescription == string.Empty)
      {
        return mc_CommodityGroups.OrderBy(CmdGrp => CmdGrp.CommodityGroupCode);
      }
      else
      {
        return mc_CommodityGroups.Where(CmdGrp => CmdGrp.CommodityGroupDescription.Contains(filterDescription)).OrderBy(CmdGrp => CmdGrp.CommodityGroupCode);
      }
    }

    /// <summary>
    /// - Retrieve the CommodityGroup of the key specified.
    /// </summary>
    /// <param name="commodityGroupId">
    /// - Identifier of the CommodityGroup to retrieve
    /// </param>
    /// <returns>
    /// - Commodity
    /// </returns>
    public Models.Commodity.CommodityGroup GetCommodityGroup(int commodityGroupId)
    {
      var commodityGroup = mc_CommodityGroups.FirstOrDefault(CmdGrp => CmdGrp.CommodityGroupId == commodityGroupId);

      return commodityGroup;
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
    public Models.Commodity.CommodityGroup UpdateCommodityGroup(Models.Commodity.CommodityGroup commodityGroup)
    {
      throw new System.NotImplementedException();
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
    public Models.Commodity.Commodity CreateCommodity(Models.Commodity.Commodity commodity)
    {
      throw new System.NotImplementedException();
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
    public Models.Commodity.Commodity DeleteCommodity(int commodityId)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>
    /// <para> - Retrieve List of Commodities.</para>
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
    public IEnumerable<Models.Commodity.Commodity> GetCommodities(string filterDescription)
    {
      if (filterDescription == string.Empty)
      {
        return mc_Commodities.OrderBy(Cmd => Cmd.CommodityCode);
      }
      else
      {
        return mc_Commodities.Where(Cmd => Cmd.CommodityDescription.Contains(filterDescription)).OrderBy(Cmd => Cmd.CommodityCode);
      }
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
    public Models.Commodity.Commodity GetCommodity(int commodityId)
    {
      var commodity = mc_Commodities.FirstOrDefault(Cmd => Cmd.CommodityId == commodityId);

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
    public Models.Commodity.Commodity UpdateCommodity(Models.Commodity.Commodity commodity)
    {
      throw new System.NotImplementedException();
    }

    #endregion

    #endregion

    #region PrivateMethods

    /// <summary>
    /// - Build up the list of CommodityGroups with child Commodities
    /// </summary>
    private void BuildCommodityGroupsMock(int commodityGroupDepth)
    {
      for (int commodityGroupIndex = 1; commodityGroupIndex <= commodityGroupDepth; commodityGroupIndex++)
      {
        var commodityGroupId = mc_CommodityGroups.Count < 1 ? 1 :  mc_CommodityGroups.Max(CmdGrp => CmdGrp.CommodityGroupId) + 1;

        var commodityGroup = new CommodityGroup()
                             {
                               CommodityGroupId = commodityGroupId,
                               CommodityGroupCode = "GRP" + commodityGroupIndex.ToString("000"),
                               CommodityGroupDescription = "Group Description " + commodityGroupIndex.ToString("000")
                             };

        // - Gather the children, Margeretha,
        //   there is a spin on the horizon,
        //   and its commin' our way!!!
        for (int commodityIndex = 1; commodityIndex <= commodityGroupDepth; commodityIndex++)
        {
          var commodity = new Models.Commodity.Commodity()
                          {
                            CommodityId = mc_Commodities.Count < 1 ? 1 : mc_Commodities.Max(Cmd => Cmd.CommodityId) + 1,
                            CommodityCode = "CMD" + commodityIndex.ToString("000"),
                            CommodityDescription = "Commodity Description " + commodityIndex.ToString("000"),
                            CommodityGroupId = commodityGroupId
                          };

          mc_Commodities.Add(commodity);
          commodityGroup.Commodities.Add(commodity);
        }

        // - Load it up, strap it down!!!
        mc_CommodityGroups.Add(commodityGroup);
      }
    }

    #endregion
  }
}
