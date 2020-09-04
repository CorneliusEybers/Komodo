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
  public class CommodityGroupController : Controller
  {
    #region ClassVariables

    private ICommodityRepository mc_CommodityRepository;

    #endregion

    #region Construct

    public CommodityGroupController(ICommodityRepository commodityRepository)
    {
      mc_CommodityRepository = commodityRepository;
    }

    #endregion

    #region PublicMethods

    [HttpGet]
    public ViewResult Index()
    {
      var commodityGroupIndexViewModel = new CommodityGroupIndexViewModel();

      var getCommodityGroupsResult = mc_CommodityRepository.GetCommodityGroups("");
      commodityGroupIndexViewModel.CommodityGroups = getCommodityGroupsResult.Result as List<CommodityGroup>;

      return View(commodityGroupIndexViewModel);
    }

    [HttpGet]
    [Route("/{controller}/get/{commoditygroupid:int}")]
    public ViewResult Details(int commodityGroupId)
    {
      var getCommidityGroupResult = mc_CommodityRepository.GetCommodityGroup(commodityGroupId);
      var commodityGroup = getCommidityGroupResult.Result as CommodityGroup;

      return View(commodityGroup);
    }

    [HttpGet]
    public ViewResult Create()
    {
      var commodityGroup = new CommodityGroup();

      return View("Save",commodityGroup);
    }

    [HttpPost]
    public IActionResult Create([FromBody]CommodityGroup commodityGroup)
    {
      if (ModelState.IsValid)
      {
        var createCommodityGroupResult = mc_CommodityRepository.CreateCommodityGroup(commodityGroup);
        var commodityGroupNew = createCommodityGroupResult.Result as CommodityGroup;

        return RedirectToAction("Details", new {commodityGroupId = commodityGroupNew.CommodityGroupId});
      }

      return View();
    }

    [HttpPut]
    public ViewResult Update([FromBody]CommodityGroup commodityGroup)
    {
      return View();
    }

    [HttpDelete]
    [Route("/{controller}/delete/commoditygroupid:int")]
    public ViewResult Delete(int commodityGroupId)
    {
      return View();
    }

    #endregion
  }
}