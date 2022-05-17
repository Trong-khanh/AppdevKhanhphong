using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace appdevKhanhphong.Models
{
    public class ApplicationUser:IdentityUser
    {
        [NotMapped]
        public string Role { get; set; }
    }
}