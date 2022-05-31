using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using appdevKhanhphong.Utility;

namespace appdevKhanhphong.Models
{
    public class Trainer: ApplicationUser
    {
        [Required]
        public TypeOfTrainer Type { get; set; }
        [Required]
        public string WorkingPlace { get; set; }
    }
}