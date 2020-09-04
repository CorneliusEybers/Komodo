// - Required Assemblies
using System.ComponentModel.DataAnnotations;

// - Application Assemblies

namespace Komodo.Ui.Models.Commodity
{
  public class Commodity
  {
    public int CommodityId { get; set; }

    [Required]
    [Display(Name = "Commodity Code:")]
    [MaxLength(10,ErrorMessage = "Commodity Code cannot exceed 10 characters")]
    public string CommodityCode { get; set; }

    [Required]
    [Display(Name = "Description:")]
    public string CommodityDescription { get; set; }

    [Display(Name="Commodity Group:")]
    public int CommodityGroupId { get; set; }
  }
}
