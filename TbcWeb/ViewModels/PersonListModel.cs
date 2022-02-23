using TbcWeb.DataModels;

namespace TbcWeb.ViewModels
{
    public class PersonListModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string IdNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        public string City { get; set; }
        public int NumberOfPhones { get; set; }
        public int NumberOfRelatives { get; set; }
        
    }
}
