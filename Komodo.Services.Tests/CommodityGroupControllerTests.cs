// - Required Assemblies

using System;
using System.Collections;
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
    //   and cause tests to fail that should pass.
    // - Normally will run test in transaction and rollback after test.
    // - I cannot justify the time for this small project
    // - This is a conceptual proof that I know he inner workings of TDD...
    // - I have done one test thoroughly the rest just minimum.

    [TestMethod]
    public async Task GetCommodityGroups_All()
    {
      // - Given
      var resources = new Resources();

      string filterDescription = string.Empty;
      List<CommodityGroup> commodityGroups = TestSetup.BuildCommodityGroups();

      resources.Repository.Setup(Rps => Rps.GetCommodityGroups(String.Empty)).Returns(Task.FromResult<IEnumerable<CommodityGroup>>(commodityGroups));

      // - When
      var result = await resources.Controller.GetCommodityGroups("") ;

      // - Then
      Assert.IsNotNull(result);
      var okResult = result.Result as ObjectResult;
      Assert.AreEqual(200, okResult.StatusCode );
      Assert.AreEqual(commodityGroups.Count,result.Value.Count);
    }
  }
}
