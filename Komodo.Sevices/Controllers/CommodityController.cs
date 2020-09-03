// - Required Assemblies
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Komodo.Sevices.DbContext;
using Microsoft.AspNetCore.Mvc;

// - Application Assemblies
using Komodo.Sevices.Repositories.Commodity;
using Microsoft.AspNetCore.Http;

namespace Komodo.Sevices.Controllers
{
  [ApiController]
  public class CommodityController : ControllerBase
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
    [Route("api/Commodity/GetCommodities")]
    public async Task<ActionResult<IEnumerable<Commodity>>> GetCommodities()
    {
      try
      {
        var commodities = await mc_CommodityRepository.GetCommodities(string.Empty);

        return Ok(commodities);
      }
      catch (Exception exception)
      {
        string message = "Error occurred in CommodityController.GetCommodity" + Environment.NewLine;
        message += exception.Message;
        return StatusCode(StatusCodes.Status500InternalServerError, message);
      }
    }

    [HttpGet]
    [Route("api/Commodity/GetCommodities/{filterDescription}")]
    public async Task<ActionResult<IEnumerable<Commodity>>> GetCommodities(string filterDescription)
    {
      try
      {
        var commodities = await mc_CommodityRepository.GetCommodities(filterDescription);

        return Ok(commodities);
      }
      catch (Exception exception)
      {
        string message = "Error occurred in CommodityController.GetCommodity with filter" + Environment.NewLine;
        message += exception.Message;
        return StatusCode(StatusCodes.Status500InternalServerError, message);
      }
    }

    [HttpGet]
    [Route("api/Commodity/GetCommodity/{commodityId:int}")]
    public async Task<ActionResult<Commodity>> GetCommodity(int commodityId)
    {
      try
      {
        var commodity = await mc_CommodityRepository.GetCommodity(commodityId);

        if (commodity == null)
        {
          return NotFound("Commodity does not exist...");
        }

        return Ok(commodity);
      }
      catch (Exception exception)
      {
        string message = "Error occurred in CommodityController.GetCommodity" + Environment.NewLine;
        message += exception.Message;
        return StatusCode(StatusCodes.Status500InternalServerError, message);
      }
    }

    [HttpPost]
    [Route("api/Commodity/CreateCommodity")]
    public async Task<ActionResult<Commodity>> CreateCommodity(Commodity commodity)
    {
      try
      {
        // - Validate : CommodityGroup(parent) must be in order.
        if (commodity.CommodityGroupId < 1)
        {
          throw new Exception("The CommodityGroup(parent) of the Commodity to be created must be specified...");
        }

        bool commodityGroupExists = await mc_CommodityRepository.CommodityGroupExists(commodity.CommodityGroupId);

        if (!commodityGroupExists)
        {
          throw new Exception("The CommodityGroup(parent) of the Commodity to be created must exist on the system...");
        }

        // - Put it away!
        var commodityCreated = await mc_CommodityRepository.CreateCommodity(commodity);

        return Ok(commodityCreated);
      }
      catch (Exception exception)
      {
        string message = "Error occurred in CommodityController.CreateCommodity" + Environment.NewLine;
        message += exception.Message;
        return StatusCode(StatusCodes.Status500InternalServerError, message);
      }
    }

    [HttpDelete]
    [Route("api/Commodity/DeleteCommodity/{commodityId:int}")]
    public async Task<ActionResult<Commodity>> DeleteCommodity(int commodityId)
    {
      try
      {
        var commodityUndo = await mc_CommodityRepository.DeleteCommodity(commodityId);

        if (commodityUndo == null)
        {
          return NotFound();
        }

        return Ok(commodityUndo);
      }
      catch (Exception exception)
      {
        var message = "Error occured in CommodityController.DeleteCommodity" + Environment.NewLine;
        message += exception.Message;
        return StatusCode(StatusCodes.Status500InternalServerError, message);
      }
    }

    [HttpPut]
    [Route("api/Commodity/UpdateCommodity")]
    public async Task<ActionResult<Commodity>> UpdateCommodity(Commodity commodity)
    {
      try
      {
        // - Validation
        if (commodity.CommodityGroupId < 1)
        {
          throw new Exception("The CommodityGroup(parent) of the Commodity must be specified...");
        }

        var commodityGroupExists = await mc_CommodityRepository.CommodityGroupExists(commodity.CommodityGroupId);

        if (!commodityGroupExists)
        {
          throw new Exception("The CommodityGroup(parent) of the Commodity must be exist in the system(saved).");
        }

        // - Get it done...
        var commodityExtant = await mc_CommodityRepository.UpdateCommodity(commodity);

        return Ok(commodityExtant);
      }
      catch (Exception exception)
      {
        string message = "Error occurred in CommodityController.UpdateCommodity" + Environment.NewLine;
        message += exception.Message;
        return StatusCode(StatusCodes.Status500InternalServerError, message);
      }
    }

    #endregion
  }
}