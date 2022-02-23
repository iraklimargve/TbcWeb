using TbcWeb.DataModels;

namespace TbcWeb.ViewModels
{
    public class PhoneNumberModel
    {
        public int PersonId { get; set; }
        public string Number { get; set; }
        public PhoneNumberEnum PhoneNumberType { get; set; }
    }
}
