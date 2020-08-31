// - Required Assemblies

// - Application Assemblies

using System.Collections.Generic;
using Komodo.Ui.Models.Commodity;

namespace Komodo.Ui.ViewModels
{
  /// <summary>
  /// <para> - Used when a Commodity is created or updated</para>
  /// <para> - Includes the Commodity being operated on</para>
  /// <para>   as well as the list of CommodityGroups </para>
  /// <para>   that can be selected for the Commodity.</para>
  /// </summary>
  public class CommodityChangeViewModel
  {
    #region Properties
    public Commodity Commodity { get; set; }

    public List<CommodityGroup> CommodityGroups { get; set; }

    #endregion

    #region Construct

    public CommodityChangeViewModel()
    {
      Commodity = new Commodity();
      CommodityGroups = new List<CommodityGroup>();
    }

    #endregion
  }
}
