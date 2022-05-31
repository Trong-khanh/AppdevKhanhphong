using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace appdevKhanhphong.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}