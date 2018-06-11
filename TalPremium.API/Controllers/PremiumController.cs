using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TalPremium.API.Controllers.Dto;

namespace TalPremium.API.Controllers
{
  /// <summary>
  /// Controller that provides premium computation functionality
  /// </summary>
  [Route("api/premium")]
  public class PremiumController : Controller
  {
    private readonly ILogger<PremiumController> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <returns>
    /// none
    /// </returns>
    public PremiumController(ILogger<PremiumController> logger)
    {
      this._logger = logger;
    }

    /// <summary>
    /// Calculate the age given the date of birth
    /// </summary>
    /// <param name="birthDate"></param>
    /// <returns>
    /// int
    /// </returns>
    private int CalculateAge(DateTime birthDate)
    {
      int age = 0;
      age = DateTime.Now.Year - birthDate.Year;
      if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
        age--;

      return age;
    }

    //POST : /api/premium
    /// <summary>
    /// Action method to compute the Premium
    /// - GenderFactor calculated as 1.1 for Female and 1.2 for Male
    /// - Premium not applicable if age is between 18 and 65
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns>
    /// IActionResult
    /// </returns>
    [HttpPost]
    public IActionResult CalculatePremium([FromBody]UserDto userDto)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);
      
      if (userDto == null) 
        return BadRequest("Unable to compute the premium as user Data is not provided");

      var premiumData = new PremiumDataDto { PremiumAmount = 0};

      _logger.LogDebug(
        String.Format("CalculatePremium::Input, Name={0}, DOB={1}, IsMale={2}",
                      userDto.Name, userDto.BirthDate, userDto.IsMale));

      double genderFactor = (bool)userDto.IsMale ? 1.2 : 1.1;
      int age = CalculateAge((DateTime)userDto.BirthDate);
      
      if (age >=18 && age <=65) 
      {
        premiumData.PremiumAmount = Math.Round(age * genderFactor * 100);
      } 
      else 
      {
        premiumData.PremiumAmount = 0;
        premiumData.ErrorString = "Premium applicable only if age is between 18 and 65 years";
      }

      _logger.LogDebug(
        String.Format("CalculatePremium::Output, PremiumAmount={0}, PremiumErrorStr={1}",
                      premiumData.PremiumAmount, premiumData.ErrorString));

      return Ok(premiumData);
    }

  }
}