// - Required Assemblies
using System.Collections.Generic;

// - Application Assemblies
using Komodo.Ui.Models.Commodity;

namespace Komodo.Ui.ViewModels
{
  /// <summary>
  /// <para> - Used when a Commodity is created or updated</para>
  /// <para> - Includes the Commodity being operated on</para>
  /// <para>   as well as the list of CommodityGroups </para>
  /// <para>   that can be selected for the Commodity.</para>
  /// </summary>
  public class CommoditySaveViewModel
  {
    #region Properties
    /// <summary>
    /// The Commodity being Created or Updated.
    /// </summary>
    public Commodity Commodity { get; set; }

    /// <summary>
    /// - List of CommodityGroups selectable as the parent
    ///   of the Commodity being worked...
    /// </summary>
    public List<CommodityGroup> CommodityGroups { get; set; }

    #endregion

    #region Construct

    public CommoditySaveViewModel()
    {
      Commodity = new Commodity();
      CommodityGroups = new List<CommodityGroup>();
    }

    #endregion
  }
}
