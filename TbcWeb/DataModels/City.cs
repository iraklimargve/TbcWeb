using System.ComponentModel.DataAnnotations;

namespace TbcWeb.DataModels
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
