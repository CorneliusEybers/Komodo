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
      commodityGroupIndexViewModel.CommodityGroups = mc_CommodityRepository.GetCommodityGroups("").ToList();

      return View(commodityGroupIndexViewModel);
    }

    [HttpGet]
    [Route("/{controller}/get/{commoditygroupid:int}")]
    public ViewResult Details(int commodityGroupId)
    {
      var commodityGroup = mc_CommodityRepository.GetCommodityGroup(commodityGroupId);

      return View(commodityGroup);
    }

    [HttpGet]
    public ViewResult Create()
    {
      var commodityGroup = new CommodityGroup();

      return View(commodityGroup);
    }

    [HttpPost]
    public IActionResult Create([FromBody]CommodityGroup commodityGroup)
    {
      if (ModelState.IsValid)
      {
        var commodityGroupNew = mc_CommodityRepository.CreateCommodityGroup(commodityGroup);

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