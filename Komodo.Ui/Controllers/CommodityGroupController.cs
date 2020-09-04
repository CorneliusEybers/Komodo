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

    [HttpGet]
    public ViewResult Update(int commodityGroupId)
    {
      var commodityGroupResult = mc_CommodityRepository.GetCommodityGroup(commodityGroupId);
      var commodityGroup       = commodityGroupResult.Result as CommodityGroup;

      return View("Save", commodityGroup);
    }

    [HttpGet]
    [Route("/{controller}/delete/commoditygroupid:int")]
    public ViewResult Delete(int commodityGroupId)
    {
      CommodityGroup commodityGroupUndo;

      var commodityGroupUndoResult = mc_CommodityRepository.DeleteCommodityGroup(commodityGroupId);
      commodityGroupUndo = commodityGroupUndoResult.Result as CommodityGroup;

      return View("Save",commodityGroupUndo);
    }

    [HttpPost]
    public IActionResult Save(CommodityGroup commodityGroup)
    {
      CommodityGroup commodityGroupSaved;

      if (ModelState.IsValid)
      {
        if (commodityGroup.CommodityGroupId < 1)
        {
          var commodityGroupCreatedResult = mc_CommodityRepository.CreateCommodityGroup(commodityGroup);
          commodityGroupSaved = commodityGroupCreatedResult.Result as CommodityGroup;
        }
        else
        {
          var commodityGroupUpdatedResult = mc_CommodityRepository.UpdateCommodityGroup(commodityGroup);
          commodityGroupSaved = commodityGroupUpdatedResult.Result as CommodityGroup;
        }

        return View("Details", commodityGroupSaved);
      }

      // - Someting went wrong. Try again!
      return View("Save",commodityGroup);
    }

    #endregion
  }
}