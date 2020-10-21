using AddressBookWebApi.Entities;
using AddressBookWebApi.Mappings.DTOs.ContactDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookWebApi.Mappings.DTOs.Contact
{
    public class ContactCreateDto
    {
        [MaxLength(64)]
        public string FirstName { get; set; }
        [MaxLength(64)]
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ICollection<ContactDetailCreateDto> ContactDetails { get; set; }
    }
}
