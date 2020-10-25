using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AddressBookWebApi.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ICollection<ContactDetail> ContactDetails { get; set; }
    }
}
