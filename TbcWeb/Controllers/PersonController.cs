using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TbcWeb.DataModels;
using TbcWeb.ViewModels;

namespace TbcWeb.Controllers
{
    public class PersonController : BaseController
    {
        [HttpGet]
        [Route("all")]
        public IEnumerable<PersonListModel> GetAll()
        {
            var result = new List<PersonListModel>();
            var persons = db.Persons.Include(x => x.City).Include(x => x.PhoneNumbers).Include(x => x.Relatives).ToList();
            foreach (var person in persons)
            {
                result.Add(new PersonListModel
                {
                    Id = person.Id,
                    FullName = person.Name + " " + person.LastName,
                    IdNumber = person.IdNumber,
                    BirthDate = person.BirthDate,
                    Gender = person.Gender,
                    City = person.City.Name,
                    NumberOfPhones = person.PhoneNumbers.Count(),
                    NumberOfRelatives = person.Relatives.Count()
                });
            }

            return result;
        }

        [HttpGet]
        [Route("detail")]
        public PersonModel GetPerson(int id)
        {
            
            var person = db.Persons.Where(x => x.Id == id).Include(x => x.City).Include(x => x.PhoneNumbers).Include(x => x.Relatives).FirstOrDefault();

            var result = new PersonModel
            {
                Id = person.Id,
                Name = person.Name,
                lastName = person.LastName,
                IdNumber = person.IdNumber,
                Gender = person.Gender,
                BirthDate = person.BirthDate,
                CityId = person.CityId,
                ImagePath = person.ImagePath,
            };
            foreach (var phones in person.PhoneNumbers)
            {
                result.PhoneNumbers.Add(new PhoneNumberModel
                {
                    PersonId = person.Id,
                    Number = phones.Number,
                    PhoneNumberType = phones.PhoneNumberType
                });
            }

            foreach (var relative in person.Relatives)
            {
                var rel = db.Persons.Find(relative.RelativePersonId);
                result.Relatives.Add(new RelativeModel
                {
                    PersonId = person.Id,
                    RelativePersonId = rel.Id,
                    RelativeIdNumber = rel.IdNumber,
                    RelativeFullName = rel.Name + " " + rel.LastName,
                    RelativeType = relative.RelativeType
                });
            }

            return result;
        }

        [HttpPost]
        [Route("updatepersondetails")]
        public JsonResult UpdatePersonDetails(PersonModel personModel)
        {
            try
            {
                var person = db.Persons.Find(personModel.Id);
                if (person == null)
                    return Json(new { status = 1, message = "Invalid Person" });

                var city = db.Cities.Find(personModel.CityId);
                if (city == null)
                    return Json(new { status = 1, message = "Invalid City" });

                person.Name = personModel.Name;
                person.LastName = personModel.lastName;
                person.IdNumber = personModel.IdNumber;
                person.Gender = personModel.Gender;
                person.BirthDate = personModel.BirthDate;
                person.CityId = personModel.CityId;
                person.City = city;
                person.ImagePath = personModel.ImagePath;
                db.SaveChanges();
                return Json(new { status = 0, message = "successfully updated" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 2, message = ex.Message });
            }

            
        }

        [HttpPost]
        [Route("addphonenumber")]
        public JsonResult AddPhoneNumber(PhoneNumberModel phoneNumber)
        {
            try
            {
                var person = db.Persons.Where(x => x.Id == phoneNumber.PersonId).Include(x => x.PhoneNumbers).FirstOrDefault();
                if (person == null)
                    return Json(new { status = 1, message = "Invalid Person" });

                if (person.PhoneNumbers.Any(x => x.Number == phoneNumber.Number))
                    return Json(new { status = 1, message = "Phone Number Already Exist" });

                var phone = new PhoneNumber
                {
                    Number = phoneNumber.Number,
                    PersonId = person.Id,
                    Person = person,
                    PhoneNumberType = phoneNumber.PhoneNumberType
                };
                db.PhoneNumbers.Add(phone);
                person.PhoneNumbers.Add(phone);
                db.SaveChanges();
                return Json(new { status = 0, message = "successfully added phone number" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 2, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("removephonenumber")]
        public JsonResult RemovePhoneNumber(PhoneNumberModel phoneNumber)
        {
            try
            {
                var person = db.Persons.Where(x => x.Id == phoneNumber.PersonId).Include(x => x.PhoneNumbers).FirstOrDefault();
                if (person == null)
                    return Json(new { status = 1, message = "Invalid Person" });

                var phone = db.PhoneNumbers.Where(x => x.PersonId == person.Id && x.Number == phoneNumber.Number).FirstOrDefault();
                if (phone == null)
                    return Json(new { status = 1, message = "this person does not have this phone number" });

                person.PhoneNumbers.Remove(phone);
                db.PhoneNumbers.Remove(phone);
                db.SaveChanges();
                return Json(new { status = 0, message = "successfully removed phone number" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 2, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("addrelative")]
        public JsonResult AddRelative(RelativeModel relative)
        {
            try
            {
                var person = db.Persons.Where(x => x.Id == relative.PersonId).Include(x => x.Relatives).FirstOrDefault();
                if (person == null)
                    return Json(new { status = 1, message = "Invalid Person" });

                var relPerson = db.Persons.Where(x => x.Id == relative.RelativePersonId).Include(x => x.Relatives).FirstOrDefault();
                if (relPerson == null)
                    return Json(new { status = 1, message = "Invalid Relative Person" });

                if (person.Relatives.Any(x => x.RelativePersonId == relative.RelativePersonId))
                    return Json(new { status = 1, message = "relative person already exists" });

                var rel = new Relative
                {
                    Person = person,
                    PersonId = person.Id,
                    RelativePersonId = relPerson.Id,
                    RelativeType = relative.RelativeType
                };
                db.Relatives.Add(rel);
                person.Relatives.Add(rel);
                db.SaveChanges();

                rel = new Relative
                {
                    Person = relPerson,
                    PersonId = relPerson.Id,
                    RelativePersonId = person.Id,
                    RelativeType = relative.RelativeType
                };
                db.Relatives.Add(rel);
                relPerson.Relatives.Add(rel);
                db.SaveChanges();

                return Json(new { status = 0, message = "successfully added relative" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 2, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("removerelative")]
        public JsonResult RemoveRelative(RelativeModel relative)
        {
            try
            {
                var person = db.Persons.Where(x => x.Id == relative.PersonId).Include(x => x.Relatives).FirstOrDefault();
                if (person == null)
                    return Json(new { status = 1, message = "Invalid Person" });

                var relPerson = db.Persons.Where(x => x.Id == relative.RelativePersonId).Include(x => x.Relatives).FirstOrDefault();
                if (relPerson == null)
                    return Json(new { status = 1, message = "Invalid Relative Person" });

                var rel = person.Relatives.Where(x => x.PersonId == person.Id && x.RelativePersonId == relPerson.Id).FirstOrDefault();
                var relrel = relPerson.Relatives.Where(x => x.PersonId == relPerson.Id && x.RelativePersonId == person.Id).FirstOrDefault();
                if (rel == null || relrel == null)
                    return Json(new { status = 1, message = "this relationship does not exists" });

                person.Relatives.Remove(rel);
                relPerson.Relatives.Remove(relrel);
                db.Relatives.Remove(rel);
                db.Relatives.Remove(relrel);
                db.SaveChanges();
                    
                return Json(new { status = 0, message = "successfully removed relative" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 2, message = ex.Message });
            }
        }
    }
}
