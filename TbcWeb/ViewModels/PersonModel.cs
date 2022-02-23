using TbcWeb.DataModels;

namespace TbcWeb.ViewModels
{
    public class PersonModel
    {
        public PersonModel()
        {
            PhoneNumbers = new List<PhoneNumberModel>();
            Relatives = new List<RelativeModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string lastName { get; set; }
        public string IdNumber { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public string ImagePath { get; set; }
        public List<PhoneNumberModel> PhoneNumbers { get; set; }
        public List<RelativeModel> Relatives { get; set; }
        
    }
}
