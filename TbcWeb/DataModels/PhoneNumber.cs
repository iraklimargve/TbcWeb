using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TbcWeb.DataModels
{
    public class PhoneNumber
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public PhoneNumberEnum PhoneNumberType { get; set; }

        [Required(ErrorMessage = "Required")]
        [MinLength(4)]
        [MaxLength(50)]
        public string Number { get; set; }
        
    }
}
