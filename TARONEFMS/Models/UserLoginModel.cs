using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TARONEFMS.Models
{
    public class UserLoginModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public string Name { get; set; }

        public string WorkgroupId { get; set; }
    }
}