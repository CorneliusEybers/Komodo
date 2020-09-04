// - Required Assemblies
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Komodo.Ui.Models.Commodity
{
  public class CommodityGroup
  {
    #region Properties

    public int CommodityGroupId { get; set; }

    [Required]
    [Display(Name="Group Code: ")]
    [MaxLength(10,ErrorMessage = "Group Code cannot exceed 10 characters")]
    public string CommodityGroupCode { get; set; }

    [Required]
    [Display(Name ="Group Description: ")]
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
