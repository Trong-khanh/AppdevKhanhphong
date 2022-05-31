using System;
using System.ComponentModel.DataAnnotations;
using appdevKhanhphong.Models;
using appdevKhanhphong.Utility;

namespace appdevKhanhphong.Models
{
    public class Trainee :ApplicationUser
    {
        [Required]
        public int Age { get; set; }
        [Required]
        public DateTime DateOfBirth  { get; set; }
        [Required]
        public string Education { get; set; }
        [Required]
        public string MainProgramingLanguage { get; set; }
        [Required]
        public float ToeicScore { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public Department Department{ get; set; }
        [Required]
        public string Location { get; set; }
        
    }
}