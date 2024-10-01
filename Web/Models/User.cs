using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Web.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)]
        [MaxLength(100)]

        public string? Name { get; set; }
        public string Address { get; set; }
    }
}