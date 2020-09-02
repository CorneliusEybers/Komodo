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
  public class CommodityGroupControllerTests
  {
    public class Resources
    {
      #region ClassVariables

      public CommodityGroupController Controller;
      public Mock<ICommodityRepository> Repository;

      #endregion

      #region Construct

      public Resources()
      {
        Repository = new Mock<ICommodityRepository>();
        Controller = new CommodityGroupController(Repository.Object);
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
    public async Task GetCommodityGroups()
    {
      // - Given
      var resources = new Resources();

      string filterDescription = string.Empty;

      // - Setup of Mock for IRepository
      List<CommodityGroup> commodityGroups = TestSetup.BuildCommodityGroups();
      resources.Repository.Setup(Rps => Rps.GetCommodityGroups(String.Empty))
                          .Returns(Task.FromResult<IEnumerable<CommodityGroup>>(commodityGroups));

      // - When
      var result = await resources.Controller.GetCommodityGroups("") ;

      // - Then
      Assert.IsNotNull(result);
      var okResult = result.Result as ObjectResult;
      Assert.AreEqual(200, okResult.StatusCode);
      var commodityGroupsResult = okResult.Value as List<CommodityGroup>;
      Assert.AreEqual(commodityGroups.Count, commodityGroupsResult.Count);
    }

    [TestMethod]
    public async Task GetCommodityGroup()
    {
      // - Given
      var resources = new Resources();
      var commodityGroupId = 2;

      // - Setup the Mock to Repository
      CommodityGroup commodityGroup = TestSetup.BuildCommodityGroup(commodityGroupId);

      resources.Repository.Setup(Rps => Rps.GetCommodityGroup(commodityGroupId))
                          .Returns(Task.FromResult<CommodityGroup>(commodityGroup));

      // - When
      var result = await resources.Controller.GetCommodityGroup(commodityGroupId);
      
      // - Then
      Assert.IsNotNull(result);
      var okObjectResult = result.Result as OkObjectResult;
      Assert.AreEqual(200,okObjectResult.StatusCode);
      var commodityGroupResult = okObjectResult.Value as CommodityGroup;
      Assert.AreEqual(commodityGroupId,commodityGroupResult.CommodityGroupId);
      Assert.AreEqual(commodityGroup.CommodityGroupCode,commodityGroupResult.CommodityGroupCode);
      Assert.AreEqual(commodityGroup.CommodityGroupDescription, commodityGroupResult.CommodityGroupDescription);
      Assert.AreEqual(commodityGroup.Commodities.Count, commodityGroupResult.Commodities.Count);
    }

    [TestMethod]
    public async Task CreateCommodityGroup()
    {
      // - Given
      var resources        = new Resources();
      var commodityGroupId = 2;

      // - Setup the Mock to Repository
      CommodityGroup commodityGroupIn = TestSetup.BuildCommodityGroup(0);
      CommodityGroup commodityGroupOut = TestSetup.BuildCommodityGroup(commodityGroupId);

      resources.Repository.Setup(Rps => Rps.CreateCommodityGroup(commodityGroupIn))
               .Returns(Task.FromResult<CommodityGroup>(commodityGroupOut));

      // - When
      var result = await resources.Controller.CreateCommodityGroup(commodityGroupIn);
      
      // - Then
      Assert.IsNotNull(result);
      var okObjectResult = result.Result as OkObjectResult;
      Assert.AreEqual(200,okObjectResult.StatusCode);
      var commodityGroupResult = okObjectResult.Value as CommodityGroup;
      Assert.AreEqual(commodityGroupId,commodityGroupResult.CommodityGroupId);
      Assert.AreEqual(commodityGroupOut.CommodityGroupCode,commodityGroupResult.CommodityGroupCode);
      Assert.AreEqual(commodityGroupOut.CommodityGroupDescription, commodityGroupResult.CommodityGroupDescription);
      Assert.AreEqual(commodityGroupOut.Commodities.Count, commodityGroupResult.Commodities.Count);
    }

    [TestMethod]
    public async Task UpdateCommodityGroup()
    {
      // - Given
      var resources        = new Resources();
      var commodityGroupId = 2;

      // - Setup the Mock to Repository
      CommodityGroup commodityGroupIn  = TestSetup.BuildCommodityGroup(0);
      CommodityGroup commodityGroupOut = TestSetup.BuildCommodityGroup(commodityGroupId);

      resources.Repository.Setup(Rps => Rps.UpdateCommodityGroup(commodityGroupIn))
               .Returns(Task.FromResult<CommodityGroup>(commodityGroupOut));

      // - When
      var result = await resources.Controller.UpdateCommodityGroup(commodityGroupIn);
      
      // - Then
      Assert.IsNotNull(result);
      var okObjectResult = result.Result as OkObjectResult;
      Assert.AreEqual(200,okObjectResult.StatusCode);
      var commodityGroupResult = okObjectResult.Value as CommodityGroup;
      Assert.AreEqual(commodityGroupId,commodityGroupResult.CommodityGroupId);
      Assert.AreEqual(commodityGroupOut.CommodityGroupCode,commodityGroupResult.CommodityGroupCode);
      Assert.AreEqual(commodityGroupOut.CommodityGroupDescription, commodityGroupResult.CommodityGroupDescription);
      Assert.AreEqual(commodityGroupOut.Commodities.Count, commodityGroupResult.Commodities.Count);
    }

    [TestMethod]
    public async Task DeleteCommodityGroup()
    {
      // - Given
      var resources = new Resources();
      var commodityGroupId = 2;

      // - Setup the Mock to Repository
      CommodityGroup commodityGroupOut  = TestSetup.BuildCommodityGroup(0);

      resources.Repository.Setup(Rps => Rps.DeleteCommodityGroup(commodityGroupId))
               .Returns(Task.FromResult<CommodityGroup>(commodityGroupOut));

      // - When
      var result = await resources.Controller.DeleteCommodityGroup(commodityGroupId);
      
      // - Then
      Assert.IsNotNull(result);
      var okObjectResult = result.Result as OkObjectResult;
      Assert.AreEqual(200,okObjectResult.StatusCode);
      var commodityGroupResult = okObjectResult.Value as CommodityGroup;
      Assert.AreEqual(0,commodityGroupResult.CommodityGroupId);   // - returns an undo object that is ready to be inserted again.

      Assert.AreEqual(commodityGroupOut.CommodityGroupCode,commodityGroupResult.CommodityGroupCode);
      Assert.AreEqual(commodityGroupOut.CommodityGroupDescription, commodityGroupResult.CommodityGroupDescription);
      Assert.AreEqual(commodityGroupOut.Commodities.Count, commodityGroupResult.Commodities.Count);
    }
  }
}
