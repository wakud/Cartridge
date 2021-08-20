using System.ComponentModel.DataAnnotations;

namespace Cartridge.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string IsAdmin { get; set; }

    }
}
