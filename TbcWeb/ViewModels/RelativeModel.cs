using TbcWeb.DataModels;

namespace TbcWeb.ViewModels
{
    public class RelativeModel
    {
        public int PersonId { get; set; }
        public int RelativePersonId { get; set; }
        public string RelativeIdNumber { get; set; }
        public string RelativeFullName { get; set; }
        public RelativeTypeEnum RelativeType { get; set; }
    }
}
