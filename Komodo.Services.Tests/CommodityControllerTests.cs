// - Required Assemblies
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;

// - Application Assemblies
using Komodo.Sevices.Controllers;
using Komodo.Sevices.DbContext;
using Komodo.Sevices.Repositories.Commodity;

namespace Komodo.Services.Tests
{
  [TestClass]
  public class CommodityControllerTests
  {
    public class Resources
    {
      #region ClassVariables

      public CommodityController Controller;
      public Mock<ICommodityRepository> Repository;

      #endregion

      #region Construct

      public Resources()
      {
        Repository = new Mock<ICommodityRepository>();
        Controller = new CommodityController(Repository.Object);
      }

      #endregion
    }

    //Notes on Testing:
    //-----------------
    // - Always test against the database in the Service layer to ensure schema is correct.
    // - Normally do not test existing data in the database as this can change over time
    //   and cause tests to fail that should pass. For this purpose each test is 
    //   wrapped in Begin-End-Transaction, test data is created on the database for that
    //   test and then after the test it is all undone after rollback transaction.
    // - Normally will run unit-test in transaction and rollback after test.
    // - There are about 3 scenarios per ContollerMethod() that should be added 
    //   as part of TDD
    // - I cannot justify the time for this small project
    // - This is a conceptual proof that I know he inner workings of TDD...

    [TestMethod]
    public async Task GetCommodities()
    {
      // - Given
      var resources = new Resources();

      string filterDescription = string.Empty;

      // - Setup of Mock for IRepository
      List<Commodity> commodities = TestSetup.BuildCommodities();
      resources.Repository.Setup(Rps => Rps.GetCommodities(String.Empty))
                          .Returns(Task.FromResult<IEnumerable<Commodity>>(commodities));

      // - When
      var result = await resources.Controller.GetCommodities("") ;

      // - Then
      Assert.IsNotNull(result);
      var okObjectResult = result.Result as OkObjectResult;
      Assert.AreEqual(200, okObjectResult.StatusCode);
      var commoditiesResult = okObjectResult.Value as List<Commodity>;
      Assert.AreEqual(commodities.Count, commoditiesResult.Count);
    }

    [TestMethod]
    public async Task GetCommodity()
    {
      // - Given
      var resources = new Resources();
      var commodityId = 2;

      // - Setup the Mock to Repository
      Commodity commodity = TestSetup.BuildCommodity(commodityId);

      resources.Repository.Setup(Rps => Rps.GetCommodity(commodityId))
                          .Returns(Task.FromResult<Commodity>(commodity));

      // - When
      var result = await resources.Controller.GetCommodity(commodityId);
      
      // - Then
      Assert.IsNotNull(result);
      var okObjectResult = result.Result as OkObjectResult;
      Assert.AreEqual(200,okObjectResult.StatusCode);
      var commodityResult = okObjectResult.Value as Commodity;
      Assert.AreEqual(commodityId,commodityResult.CommodityId);
      Assert.AreEqual(commodity.CommodityCode,commodityResult.CommodityCode);
      Assert.AreEqual(commodity.CommodityDescription, commodityResult.CommodityDescription);
    }

    [TestMethod]
    public async Task CreateCommodity()
    {
      // - Given
      var resources = new Resources();
      var commodityId = 2;

      // - Setup the Mock to Repository
      Commodity commodityIn = TestSetup.BuildCommodity(0);
      Commodity commodityOut = TestSetup.BuildCommodity(commodityId);

      // - Validation checks performed in the Controller.
      commodityIn.CommodityGroupId = commodityId;

      // - Two meethods to mock this time...
      resources.Repository.Setup(Rps => Rps.CreateCommodity(commodityIn))
                          .Returns(Task.FromResult<Commodity>(commodityOut));
      resources.Repository.Setup(Rps => Rps.CommodityGroupExists(commodityId))
                          .Returns(Task.FromResult<bool>(true));

      // - When
      var result = await resources.Controller.CreateCommodity(commodityIn);
      
      // - Then
      Assert.IsNotNull(result);
      var okObjectResult = result.Result as OkObjectResult;
      Assert.AreEqual(200,okObjectResult.StatusCode);
      var commodityResult = okObjectResult.Value as Commodity;
      Assert.AreEqual(commodityId,commodityResult.CommodityId);
      Assert.AreEqual(commodityOut.CommodityCode,commodityResult.CommodityCode);
      Assert.AreEqual(commodityOut.CommodityDescription, commodityResult.CommodityDescription);
    }

    [TestMethod]
    public async Task UpdateCommodity()
    {
      // - Given
      var resources = new Resources();
      var commodityId = 2;

      // - Setup the Mock to Repository
      Commodity commodityIn = TestSetup.BuildCommodity(0);
      Commodity commodityOut = TestSetup.BuildCommodity(commodityId);

      // - Validation in Controller method
      commodityIn.CommodityGroupId = commodityId;

      // - Two methods to mock...
      resources.Repository.Setup(Rps => Rps.UpdateCommodity(commodityIn))
                          .Returns(Task.FromResult<Commodity>(commodityOut));
      resources.Repository.Setup(Rps => Rps.CommodityGroupExists(commodityId))
                          .Returns(Task.FromResult<bool>(true));

      // - When
      var result = await resources.Controller.UpdateCommodity(commodityIn);
      
      // - Then
      Assert.IsNotNull(result);
      var okObjectResult = result.Result as OkObjectResult;
      Assert.AreEqual(200,okObjectResult.StatusCode);
      var commodityResult = okObjectResult.Value as Commodity;
      Assert.AreEqual(commodityId,commodityResult.CommodityId);
      Assert.AreEqual(commodityOut.CommodityCode,commodityResult.CommodityCode);
      Assert.AreEqual(commodityOut.CommodityDescription, commodityResult.CommodityDescription);
    }

    [TestMethod]
    public async Task DeleteCommodity()
    {
      // - Given
      var resources = new Resources();
      var commodityId = 2;

      // - Setup the Mock to Repository
      Commodity commodityOut  = TestSetup.BuildCommodity(0);

      resources.Repository.Setup(Rps => Rps.DeleteCommodity(commodityId))
                          .Returns(Task.FromResult<Commodity>(commodityOut));

      // - When
      var result = await resources.Controller.DeleteCommodity(commodityId);
      
      // - Then
      Assert.IsNotNull(result);
      var okObjectResult = result.Result as OkObjectResult;
      Assert.AreEqual(200,okObjectResult.StatusCode);
      var commodityResult = okObjectResult.Value as Commodity;
      Assert.AreEqual(0,commodityResult.CommodityId);   // - returns an undo object that is ready to be inserted again.
      Assert.AreEqual(commodityOut.CommodityCode,commodityResult.CommodityCode);
      Assert.AreEqual(commodityOut.CommodityDescription, commodityResult.CommodityDescription);
    }
  }
}