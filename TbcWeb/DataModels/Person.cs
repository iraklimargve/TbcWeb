using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TbcWeb.DataModels
{
    public class Person
    {
        public Person()
        {
            PhoneNumbers = new List<PhoneNumber>();
            Relatives = new List<Relative>();
        }


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [MinLength(2, ErrorMessage = "Minimum Name length should be 2")]
        [MaxLength(50, ErrorMessage = "Maximum Name length should be 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [MinLength(2, ErrorMessage = "Minimum LastName length should be 2")]
        [MaxLength(50, ErrorMessage = "Maximum LastName length should be 50")]
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "IdNumber length should be 11")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime BirthDate { get; set; }
        public virtual City City { get; set; }
        public int CityId { get; set; }

        public virtual List<PhoneNumber> PhoneNumbers { get; set; }

        public string ImagePath { get; set; }
        [InverseProperty("Person")]
        public virtual List<Relative> Relatives { get; set; }

    }
}
