// - Required Assemblies
using System.Collections.Generic;
using Komodo.Ui.Models.Commodity;

// - Application Assemblies

namespace Komodo.Ui.ViewModels
{
  public class CommodityGroupIndexViewModel
  {
    #region Properties

    public List<CommodityGroup> CommodityGroups { get; set; }
    public string FilterDescription { get; set; }

    #endregion

    #region Construct

    public CommodityGroupIndexViewModel()
    {
      CommodityGroups = new List<CommodityGroup>();
    }

    #endregion
  }
}
