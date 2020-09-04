// - Required Assemblies
using System.Collections.Generic;
using System.Linq;

// - Application Assemblies
using Komodo.Ui.Models.Commodity;

namespace Komodo.Ui.ViewModels
{
  /// <summary>
  /// - Commodity Index View is not displayed per CommodityGroup!
  /// - The entire list of Commodities are available for display
  /// - They are only filtered by the CommodityGroup selected...
  /// - Need a list of all Commodities and all CommodityGroups
  /// </summary>
  public class CommodityIndexViewModel
  {
    #region Properties

    public List<CommodityViewModel> CommodityViewModels { get; set; }

    public List<CommodityGroup> CommodityGroups {get; set; }

    public string FilterDescription { get; set; }

    #endregion

    #region Construct

    public CommodityIndexViewModel(IEnumerable<CommodityViewModel> commodityViewModels,
                                   IEnumerable<CommodityGroup> commodityGroups)
    {
      CommodityViewModels = commodityViewModels.ToList();
      CommodityGroups = commodityGroups.ToList();
    }

    #endregion
  }
}
