using AddressBookWebApi.Entities;
using AddressBookWebApi.Mappings.DTOs.ContactDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookWebApi.Mappings.DTOs.Contact
{
    public class ContactReadDto
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ICollection<ContactDetailReadDto> ContactDetails { get; set; }
    }
}
