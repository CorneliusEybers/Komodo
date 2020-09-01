// - Required Assemblies
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Komodo.Sevices.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// - Application Assemblies
using Komodo.Sevices.Repositories.Commodity;

namespace Komodo.Sevices.Controllers
{
  [ApiController]
  public class CommodityGroupController : ControllerBase
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
    [Route("api/CommodityGroup/GetCommodityGroups/{filterDescription}")]
    public async Task<ActionResult> GetCommodityGroups(string filterDescription)
    {
      try
      {
        IEnumerable<CommodityGroup> commodityGroups = await mc_CommodityRepository.GetCommodityGroups(filterDescription);

        return Ok(commodityGroups);
      }
      catch (Exception exception)
      {
        string message = "Error occurred in CommodityController.GetCommodityGroups." + Environment.NewLine;
        message += exception.Message;
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet]
    [Route("api/CommodityGroup/GetCommodityGroup/{commodityGroupId:int}")]
    public async Task<ActionResult<CommodityGroup>> GetCommodityGroup(int commodityGroupId)
    {
      try
      {
        var commodityGroup = await mc_CommodityRepository.GetCommodityGroup(commodityGroupId);

        if (commodityGroup == null)
        {
          return NotFound();
        }

        return Ok(commodityGroup);
      }
      catch (Exception exception)
      {
        string message = "Error occurred in CommodityController.GetCommodityGroup" + Environment.NewLine;
        message += exception.Message;
        return StatusCode(StatusCodes.Status500InternalServerError, message);
      }
    }

    [HttpPost]
    [Route("api/CommodityGroup/CreateCommodityGroup")]
    public async Task<ActionResult<CommodityGroup>> CreateCommodityGroup(CommodityGroup commodityGroup)
    {
      try
      {
        var commodityGroupCreated = await mc_CommodityRepository.CreateCommodityGroup(commodityGroup);

        return Ok(commodityGroupCreated);
      }
      catch (Exception exception)
      {
        string message = "Error occurred in CommodityController.CreateCommodityGroup" + Environment.NewLine;
        message += exception.Message;
        return StatusCode(StatusCodes.Status500InternalServerError, message);
      }
    }

    [HttpDelete]
    [Route("api/CommodityGroup/DeleteCommodityGroup/{commodityGroupId:int}")]
    public async Task<ActionResult<CommodityGroup>> DeleteCommodityGroup(int commodityGroupId)
    {
      try
      {
        var commodityGroupUndo = await mc_CommodityRepository.DeleteCommodityGroup(commodityGroupId);

        if (commodityGroupUndo == null)
        {
          return NotFound();
        }

        return Ok(commodityGroupUndo);
      }
      catch (Exception exception)
      {
        string message = "Error occurred in CommodityController.DeleteCommodityGroup." + Environment.NewLine;
        message += exception.Message;
        return StatusCode(StatusCodes.Status500InternalServerError, message);
      }
    }

    [HttpPut]
    [Route("api/CommodityGroup/UpdateCommodityGroup")]
    public async Task<ActionResult<Commodity>> UpdateCommodityGroup(CommodityGroup commodityGroup)
    {
      try
      {
        var commodityGroupExtant = await mc_CommodityRepository.UpdateCommodityGroup(commodityGroup);

        if (commodityGroupExtant == null)
        {
          return NotFound();
        }

        return Ok(commodityGroupExtant);
      }
      catch (Exception exception)
      {
        string message = "Error occurred in CommodityController.UpdateCommodityGroup" + Environment.NewLine;
        message += exception.Message;
        return StatusCode(StatusCodes.Status500InternalServerError, message);
      }
    }

    #endregion

  }
}
