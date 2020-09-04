// - Required Assemblies
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

// - Application Assemblies
using Komodo.Ui.Models.Commodity;
using Komodo.Ui.Repositories.Commodity;
using Komodo.Ui.ViewModels;

namespace Komodo.Ui.Controllers
{
  public class CommodityController : Controller
  {
    #region ClassVariables

    private ICommodityRepository mc_CommodityRepository;

    #endregion

    #region Construct

    public CommodityController(ICommodityRepository commodityRepository)
    {
      mc_CommodityRepository = commodityRepository;
    }

    #endregion

    #region Public Methods

    [HttpGet]
    public ViewResult Index()
    {
      var getCommoditiesResult = mc_CommodityRepository.GetCommodities("");
      var commodities = getCommoditiesResult.Result as List<Commodity>;

      var retCommodityGroupsResult = mc_CommodityRepository.GetCommodityGroups("");
      var commodityGroups = retCommodityGroupsResult.Result as List<CommodityGroup>;

      var commodityViewModels = BuildCommodityViewModels(commodityGroups, commodities) as List<CommodityViewModel>;
      var commodityIndexViewModel = new CommodityIndexViewModel(commodityViewModels,commodityGroups);

      return View(commodityIndexViewModel);
    }

    [HttpGet]
    [Route("/{controller}/details/{commodityId:int}")]
    public ViewResult Details(int commodityId)
    {
      var getCommodityResult = mc_CommodityRepository.GetCommodity(commodityId);
      var commodity = getCommodityResult.Result as Commodity;

      return View(commodity);
    }

    [HttpGet]
    [Route("/{controller}/create")]
    public ViewResult Create()
    {
      var commoditySaveViewModel = new CommoditySaveViewModel();
      var commodityGroupsResult = mc_CommodityRepository.GetCommodityGroups("");
      commoditySaveViewModel.CommodityGroups = commodityGroupsResult.Result as List<CommodityGroup>;

      return View("Save",commoditySaveViewModel);
    }

    [HttpPost]
    [Route("/{controller}/create")]
    public IActionResult Create(Commodity commodity)
    {
      return View();
    }

    [HttpPut]
    [Route("/{controller}/update")]
    public ViewResult Update([FromBody]Commodity commodity)
    {
      return View();
    }

    [HttpDelete]
    [Route("/{controller}/delete/{commodityId:int}")]
    public ViewResult Delete(int commodityId)
    {
      return View();
    }

    #endregion

    #region PrivateMethods

    private List<CommodityViewModel> BuildCommodityViewModels(List<CommodityGroup> commodityGroups, List<Commodity> commodities)
    {
      List<CommodityViewModel> commodityViewModels = (from Cmd in commodities
                                                      join CmdGrp in commodityGroups on Cmd.CommodityGroupId equals CmdGrp.CommodityGroupId
                                                      select new CommodityViewModel()
                                                      {
                                                        CommodityId = Cmd.CommodityId,
                                                        CommodityCode = Cmd.CommodityCode,
                                                        CommodityDescription = Cmd.CommodityDescription,
                                                        CommodityGroupId = Cmd.CommodityGroupId,
                                                        CommodityGroupCode = CmdGrp.CommodityGroupCode,
                                                        CommodityGroupDescription = CmdGrp.CommodityGroupDescription
                                                      }).ToList();

      return commodityViewModels;
    }

    #endregion
  }
}