using NUnit.Framework;
using TalPremium.API.Controllers.Dto;
using System;
using TalPremium.API.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace TalPremium.API.UnitTests.Controllers
{
    [TestFixture]
    public class PremiumControllerTests
    {
      private PremiumController _controller;
      private readonly Mock<ILogger<PremiumController>> _loggerMock;

      public PremiumControllerTests()
      {
        this._loggerMock = new Mock<ILogger<PremiumController>>();
      }

      [SetUp]
      public void TestSetup()
      {
        _controller = new PremiumController(_loggerMock.Object);

      }

      [Test]
      public void CalculatePremium_WhenPassedBadData_ShouldReturnBadRequestError()
      {
        //Arrange
        UserDto userData = null;

        //Act
        var result = _controller.CalculatePremium(userData);

        //Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
      }

      [Test]
      public void CalculatePremium_WhenPassedValidDataForMale_ShouldReturnPremiumForMale()
      {
        //Arrange
        var userData = new UserDto
        {
          Name = "user",
          BirthDate = new DateTime(2000, 1, 20),
          IsMale = true
        };

        //Act
        var result = _controller.CalculatePremium(userData);

        //Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okObjectResult = result as OkObjectResult;
        var premiumData = okObjectResult.Value as PremiumDataDto;
        Assert.NotNull(premiumData);
        Assert.AreEqual(premiumData.PremiumAmount, 2160);
        Assert.IsNull(premiumData.ErrorString);
      }

      [Test]
      public void CalculatePremium_WhenPassedValidDataForFemale_ShouldReturnPremiumForFemale()
      {
        //Arrange
        var userData = new UserDto
        {
          Name = "user",
          BirthDate = new DateTime(2000, 1, 20),
          IsMale = false
        };

        //Act
        var result = _controller.CalculatePremium(userData);

        //Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okObjectResult = result as OkObjectResult;

        var premiumData = okObjectResult.Value as PremiumDataDto;
        Assert.NotNull(premiumData);
        Assert.AreEqual(premiumData.PremiumAmount, 1980);
        Assert.IsNull(premiumData.ErrorString);
      }

      [Test]
      public void CalculatePremium_WhenAgeLessThan18Years_ShouldReturnError()
      {
        //Arrange
        var userData = new UserDto
        {
          Name = "user",
          BirthDate = new DateTime(2010, 1, 20),
          IsMale = true
        };

        //Act
        var result = _controller.CalculatePremium(userData);

        //Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okObjectResult = result as OkObjectResult;

        var premiumData = okObjectResult.Value as PremiumDataDto;
        Assert.NotNull(premiumData);
        Assert.NotNull(premiumData.ErrorString);
        Assert.IsTrue(premiumData.ErrorString.Length > 0);
      }

      [Test]
      public void CalculatePremium_WhenAgeGreaterThan65Years_ShouldReturnError()
      {
        //Arrange
        var userData = new UserDto
        {
          Name = "user",
          BirthDate = new DateTime(1950, 1, 20),
          IsMale = true
        };

        //Act
        var result = _controller.CalculatePremium(userData);

        //Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okObjectResult = result as OkObjectResult;

        var premiumData = okObjectResult.Value as PremiumDataDto;
        Assert.NotNull(premiumData);
        Assert.NotNull(premiumData.ErrorString);
        Assert.IsTrue(premiumData.ErrorString.Length > 0);
      }
        
    }
}