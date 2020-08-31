// - Required Assemblies
using System.Collections.Generic;

// - Application Assemblies

namespace Komodo.Sevices.DbContext
{
  public class CommodityGroup
  {
    #region Properties

    public int CommodityGroupId { get; set; }

    public string CommodityGroupCode { get; set; }

    public string CommodityGroupDescription { get; set; }

    public List<Commodity> Commodities { get; set; }

    #endregion

    #region Construct

    public CommodityGroup()
    {
      Commodities = new List<Commodity>();
    }

    #endregion

  }
}
