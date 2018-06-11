using System;
using System.ComponentModel.DataAnnotations;

namespace TalPremium.API.Controllers.Dto
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        [Required]
        public bool? IsMale { get; set; }
    }
}