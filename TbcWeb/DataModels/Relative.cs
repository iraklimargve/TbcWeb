using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TbcWeb.DataModels
{
    public class Relative
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        public int PersonId { get; set; }
        public int RelativePersonId { get; set; }
        public RelativeTypeEnum RelativeType { get; set; }
    }
}
