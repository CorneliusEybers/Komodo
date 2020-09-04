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

    [HttpGet]
    [Route("/{controller}/update/{commodityId:int}")]
    public ViewResult Update(int commodityId)
    {
      // - ViewModel to combine all the data items required
      //   to update the Commodity
      var commoditySaveViewModel = new CommoditySaveViewModel();
      var commodityResult = mc_CommodityRepository.GetCommodity(commodityId);
      var commodity = commodityResult.Result as Commodity;
      var commodityGroupsResult = mc_CommodityRepository.GetCommodityGroups("");
      var commodityGroups = commodityGroupsResult.Result as List<CommodityGroup>;
      commoditySaveViewModel.Commodity = commodity;
      commoditySaveViewModel.CommodityGroups = commodityGroups;

      return View("Save", commoditySaveViewModel);
    }

    [HttpGet]
    [Route("/{controller}/delete/{commodityId:int}")]
    public ViewResult Delete(int commodityId)
    {
      var commoditySaveViewModel = new CommoditySaveViewModel();

      var commodityUndoResult = mc_CommodityRepository.DeleteCommodity(commodityId);
      commoditySaveViewModel.Commodity = commodityUndoResult.Result as Commodity;

      var commodityGroupsResult = mc_CommodityRepository.GetCommodityGroups("");
      commoditySaveViewModel.CommodityGroups = commodityGroupsResult.Result as List<CommodityGroup>;

      return View("Save", commoditySaveViewModel);
    }

    [HttpPost]
    [Route("/{controller}/create")]
    public IActionResult Save(Commodity commodity)
    {
      Commodity commoditySaved;

      if (ModelState.IsValid)
      {
        if (commodity.CommodityId < 1)
        {
          var commodityCreatedResult = mc_CommodityRepository.CreateCommodity(commodity);
          commoditySaved = commodityCreatedResult.Result as Commodity;
        }
        else
        {
          var commodityUpdatedResult = mc_CommodityRepository.UpdateCommodity(commodity);
          commoditySaved = commodityUpdatedResult.Result as Commodity;
        }

        return View("Details", commoditySaved);
      }

      // - Houston we have a problem...
      // - Major Tom to ground control: lets go back home.
      var commoditySaveViewModel = new CommoditySaveViewModel();
      var commodityGroupsResult  = mc_CommodityRepository.GetCommodityGroups("");
      commoditySaveViewModel.CommodityGroups = commodityGroupsResult.Result as List<CommodityGroup>;

      return View("Save",commoditySaveViewModel);
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