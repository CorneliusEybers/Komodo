// - Required Assemblies
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

// - Application Assemblies

namespace Komodo.Ui.Repositories.Commodity
{
  public class CommodityRepositoryToService : ICommodityRepository, IDisposable
  {
    #region ClassVariables

    private IConfiguration mc_Configuration;
    private HttpClient     mc_CommodityHttpClient;

    #endregion

    #region ConstructDestruct

    public CommodityRepositoryToService(IConfiguration configuration)
    {
      mc_Configuration = configuration;
      mc_CommodityHttpClient = new HttpClient();
      mc_CommodityHttpClient.DefaultRequestHeaders.Accept.Clear();
      mc_CommodityHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    ~CommodityRepositoryToService()
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
        // - Dispose stuff
        mc_CommodityHttpClient.Dispose();
        mc_CommodityHttpClient = null;
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
      throw new System.NotImplementedException();
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
      throw new System.NotImplementedException();
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
      throw new System.NotImplementedException();
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
      var commodities = new List<Models.Commodity.Commodity>();

      //var response = mc_CommodityHttpClient.GetAsync("");

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
    public Models.Commodity.Commodity GetCommodity(int commodityId)
    {
      throw new System.NotImplementedException();
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
  }
}
