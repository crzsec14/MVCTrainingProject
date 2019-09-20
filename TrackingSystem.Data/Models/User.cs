using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystem.Data.Enums;

namespace TrackingSystem.Data.Models
{
    public class User
    {        
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [StringLength(255)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; }
        
        [Display(Name = "User Type")]
        public UserType UserType { get; set; }
        public Status Status { get; set; }
        public int Attempts { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
