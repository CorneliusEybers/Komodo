// - Required Assemblies

// - Application Assemblies
using Komodo.Ui.Models.Commodity;

namespace Komodo.Ui.ViewModels
{
  /// <summary>
  /// - This class is used to present the Commodity.
  /// - It includes the full CommodityGroup(parent) of the
  ///   Commodity where One Commodity can only belong to
  ///   one CommodityGroup while one CommodityGroup can
  ///   have multiple Commodities
  /// </summary>
  public class CommodityViewModel : Commodity
  {
    public string CommodityGroupCode { get; set; }
    public string CommodityGroupDescription { get; set; }

    public CommodityViewModel()
    {
      ;
    }

    public CommodityViewModel(int commodityId, 
                              string commodityCode, 
                              string commodityDescription, 
                              int commodityGroupId, 
                              string commodityGroupCode,
                              string commodityGroupDescription)
    {
      CommodityId = commodityId;
      CommodityCode = commodityCode;
      CommodityDescription = commodityDescription;
      CommodityGroupId = commodityGroupId;
      CommodityGroupCode = commodityGroupCode;
      CommodityGroupDescription = commodityGroupDescription;
    }
  }
}
