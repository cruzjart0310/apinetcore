using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class User 
    {
        [Key]
        [Required]
        [Display(Name="UserName")]
        [StringLength(20, ErrorMessage = "El valor para {0} debe contener al menos {2} y máximo {1} caracteres", MinimumLength=6)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El valor para {0} debe contener al menos {2} y máximo {1} caracteres", MinimumLength=6)]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}