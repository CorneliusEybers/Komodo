// - Required Assemblies
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Komodo.Ui.Models.Commodity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// - Application Assemblies

namespace Komodo.Ui.Repositories.Commodity
{
  public class CommodityRepositoryToService : ICommodityRepository, IDisposable
  {
    #region ClassVariables

    private IConfiguration mc_Configuration;
    private readonly ILogger<CommodityRepositoryToService> mc_Logger;
    private HttpClient mc_CommodityHttpClient;

    #endregion

    #region ConstructDestruct

    public CommodityRepositoryToService(IConfiguration configuration,
                                        ILogger<CommodityRepositoryToService> logger)
    {
      mc_Configuration = configuration;
      mc_Logger = logger;
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
    public async Task<IEnumerable<Models.Commodity.CommodityGroup>> GetCommodityGroups(string filterDescription)
    {
      var commodityGroups = new List<Models.Commodity.CommodityGroup>();
      var serviceUrl = string.Empty;

      serviceUrl = mc_Configuration["DomainServices:Commodity:GetCommodityGroups"];

      if (!string.IsNullOrEmpty(filterDescription))
      {
        serviceUrl += "/" + filterDescription;
      }

      using (var response = await mc_CommodityHttpClient.GetAsync(serviceUrl))
      {
        if (!response.IsSuccessStatusCode)
        {
          string message = "Error occurred in CommodityRepositoryToService.GetCommodityGroups";
          message += "Response Code : " + response.StatusCode + ". ";
          message += "Response Message : " + response.ReasonPhrase + ". ";
          mc_Logger.LogError(message);
        }
        else
        {
          var commodityGroupsJson = response.Content.ReadAsStringAsync().Result;
          commodityGroups = JsonConvert.DeserializeObject<List<CommodityGroup>>(commodityGroupsJson);
        }
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
    /// - Commodity
    /// </returns>
    public async Task<Models.Commodity.CommodityGroup> GetCommodityGroup(int commodityGroupId)
    {
      var commodityGroup = new Models.Commodity.CommodityGroup();
      var serviceUrl = string.Empty;

      serviceUrl = mc_Configuration["DomainServices:Commodity:GetCommodityGroup"];
      serviceUrl += commodityGroupId.ToString();

      using(var response = await mc_CommodityHttpClient.GetAsync(serviceUrl))
      {
        if (!response.IsSuccessStatusCode)
        {
          var message = "Error occured in CommodityRepositoryToService.GetCommodityGroup. ";
          message += "Response Code : " + response.StatusCode + ". ";
          message += "Response Message : " + response.ReasonPhrase + ". ";
          mc_Logger.LogError(message);
        }
        else
        {
          var commodityGroupJson = response.Content.ReadAsStringAsync().Result;
          commodityGroup = JsonConvert.DeserializeObject<CommodityGroup>(commodityGroupJson);
        }
      }

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
    public async Task<Models.Commodity.CommodityGroup> CreateCommodityGroup(Models.Commodity.CommodityGroup commodityGroup)
    {
      var commodityGroupCreated = new CommodityGroup();
      var serviceUrl = string.Empty;

      serviceUrl = mc_Configuration["DomainServices:Commodity:CreateCommodityGroup"];

      // - Load up the Http
      var commodityGroupStringContent = new StringContent(JsonConvert.SerializeObject(commodityGroup), Encoding.UTF8, "application/json");

      using (var response = await mc_CommodityHttpClient.PostAsync(serviceUrl, commodityGroupStringContent))
      {
        if (!response.IsSuccessStatusCode)
        {
          var message = "Error occurred in CommodityRepositoryToService.CreateCommodityGroup.";
          message += "Response StatusCode : " + response.StatusCode + ". ";
          message += "Response Message : " + response.ReasonPhrase + ". ";
        }
        else
        {
          var commodityGroupCreatedJson = response.Content.ReadAsStringAsync().Result;
          commodityGroupCreated = JsonConvert.DeserializeObject<CommodityGroup>(commodityGroupCreatedJson);
        }
      }

      return commodityGroupCreated;
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
    public async Task<Models.Commodity.CommodityGroup> UpdateCommodityGroup(Models.Commodity.CommodityGroup commodityGroup)
    {
      var commodityGroupUpdated = new CommodityGroup();
      var serviceUrl = string.Empty;

      serviceUrl = mc_Configuration["DomainServices:Commodity:UpdateCommodityGroup"];

      // - Load up our trusty the Http
      var commodityGroupStringContent = new StringContent(JsonConvert.SerializeObject(commodityGroup), Encoding.UTF8,"application/json");

      using(var response = await mc_CommodityHttpClient.PutAsync(serviceUrl,commodityGroupStringContent))
      {
        if (!response.IsSuccessStatusCode)
        {
          var message = "Error occurred in CommodityRepositoryToService.UpdateCommodityGroup.";
          message += "Response Status Code : " + response.StatusCode;
          message += "Response Message : " + response.ReasonPhrase;
        }
        else
        {
          var commodityGroupUpdatedJson = response.Content.ReadAsStringAsync().Result;
          commodityGroupUpdated = JsonConvert.DeserializeObject<CommodityGroup>(commodityGroupUpdatedJson);
        }
      }

      return commodityGroupUpdated;
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
    public async Task<Models.Commodity.CommodityGroup> DeleteCommodityGroup(int commodityGroupId)
    {
      var commodityGroupUndo = new CommodityGroup();
      var serviceUrl = string.Empty;
      
      serviceUrl = mc_Configuration["DomainServices:Commodity:DeleteCommodityGroup"];
      serviceUrl += commodityGroupId.ToString();

      using (var response = await mc_CommodityHttpClient.DeleteAsync(serviceUrl))
      {
        if (!response.IsSuccessStatusCode)
        {
          string message = "Error occurred in CommodityRepositoryToService.DeleteCommodityGroup.";
          message += "Response StatusCode : " + response.StatusCode + ". ";
          message += "Response Message : " + response.ReasonPhrase + ". ";
          mc_Logger.LogError(message);
        }
        else
        {
          var commodityGroupUndoJson = response.Content.ReadAsStringAsync().Result;
          commodityGroupUndo = JsonConvert.DeserializeObject<CommodityGroup>(commodityGroupUndoJson);
        }
      }

      return commodityGroupUndo;
    }

    #endregion

    #region Commodity

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
    public async Task<IEnumerable<Models.Commodity.Commodity>> GetCommodities(string filterDescription)
    {
      var commodities = new List<Models.Commodity.Commodity>();
      string serviceUrl  = string.Empty;

      serviceUrl = mc_Configuration["DomainServices:Commodity:GetCommodities"];

      if (!string.IsNullOrEmpty(filterDescription))
      {
        serviceUrl += "/" + filterDescription;
      }

      using (var response = await mc_CommodityHttpClient.GetAsync(serviceUrl))
      {
        // - What response did we get?
        if (!response.IsSuccessStatusCode)
        {
          string message = "Message : Error occurred in CommodityRepositoryToService.GetCommodities. ";
          message += "Response Status Code : " + response.StatusCode + ". ";
          message += "Response Message : " + response.ReasonPhrase + ". ";
          mc_Logger.LogError(message);
        }
        else
        {
          string commoditiesJson = response.Content.ReadAsStringAsync().Result;
          commodities = JsonConvert.DeserializeObject<List<Models.Commodity.Commodity>>(commoditiesJson);
        }
      }

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
    public async Task<Models.Commodity.Commodity> GetCommodity(int commodityId)
    {
      var commodity = new Models.Commodity.Commodity();
      string serviceUrl = string.Empty;

      serviceUrl = mc_Configuration["DomainServices:Commodity:GetCommodity"];
      serviceUrl += commodityId.ToString();

      using (var response = await mc_CommodityHttpClient.GetAsync(serviceUrl))
      {
        if (!response.IsSuccessStatusCode)
        {
          string message = "Message : Error occurred in CommodityRepositoryToService.GetCommodity.";
          message += "Response Status Code : " + response.StatusCode + ". ";
          message += "Response Message : " + response.ReasonPhrase + ". ";
          mc_Logger.LogError(message);
        }
        else
        {
          string commodityJson = response.Content.ReadAsStringAsync().Result;
          commodity = JsonConvert.DeserializeObject<Models.Commodity.Commodity>(commodityJson);
        }
      }

      return commodity;
    }

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
    public async Task<Models.Commodity.Commodity> CreateCommodity(Models.Commodity.Commodity commodity)
    {
      var commodityCreated = new Models.Commodity.Commodity();
      string serviceUrl = string.Empty;

      serviceUrl = mc_Configuration["DomainServices:Commodity:CreateCommodity"];

      // - Load up the Http
      var commodityGroupStringContext = new StringContent(JsonConvert.SerializeObject(commodity),Encoding.UTF8, "application/json");

      using (var response = await mc_CommodityHttpClient.PostAsync(serviceUrl, commodityGroupStringContext))
      {
        if (!response.IsSuccessStatusCode)
        {
          string message = "Message : Error occurred in CommodityRepositoryToService.CreateCommodity";
          message += "Response Status Code : " + response.StatusCode;
          message += "Response Mesage : " + response.ReasonPhrase;
          mc_Logger.LogError(message);
        }
        else
        {
          string commodityJson = response.Content.ReadAsStringAsync().Result;
          commodityCreated = JsonConvert.DeserializeObject<Models.Commodity.Commodity>(commodityJson);
        }
      }

      return commodityCreated;
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
    public async Task<Models.Commodity.Commodity> UpdateCommodity(Models.Commodity.Commodity commodity)
    {
      var commodityUpdated = new Models.Commodity.Commodity();
      string serviceUrl = string.Empty;

      serviceUrl = mc_Configuration["DomainServices:Commodity:UpdateCommodity"];

      // - Load up the Http
      var commodityStringContent = new StringContent(JsonConvert.SerializeObject(commodity),Encoding.UTF8,"application/json");

      using (var response = await mc_CommodityHttpClient.PutAsync(serviceUrl, commodityStringContent))
      {
        if (!response.IsSuccessStatusCode)
        {
          string message = "Message : Error occurred in CommodityRepositoryToService.UpdateCommodity";
          message += "Response Status Code : " + response.StatusCode;
          message += "Response Message : " + response.ReasonPhrase;
          mc_Logger.LogError(message);
        }
        else
        {
          string commodityJson = response.Content.ReadAsStringAsync().Result;
          commodityUpdated = JsonConvert.DeserializeObject<Models.Commodity.Commodity>(commodityJson);
        }
      }

      return commodityUpdated;
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
    public async Task<Models.Commodity.Commodity> DeleteCommodity(int commodityId)
    {
      Models.Commodity.Commodity commodityUndo = new Models.Commodity.Commodity();
      var serviceUrl = string.Empty;

      serviceUrl = mc_Configuration["DomainServices:Commodity:DeleteCommodity"];
      serviceUrl += commodityId.ToString();

      using (var response = await mc_CommodityHttpClient.DeleteAsync(serviceUrl))
      {
        if (!response.IsSuccessStatusCode)
        {
          string message = "Error occurred in CommodityRepositoryToService.DeleteCommodity";
          message += "Response Status Code : " + response.StatusCode;
          message += "Response Message : " + response.ReasonPhrase;
          mc_Logger.LogError(message);
        }
        else
        {
          string commodityJson = response.Content.ReadAsStringAsync().Result;
          commodityUndo = JsonConvert.DeserializeObject<Models.Commodity.Commodity>(commodityJson);
        }
      }

      return commodityUndo;
    }

    #endregion

    #endregion
  }
}
