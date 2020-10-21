using AddressBookWebApi.Entities;
using AddressBookWebApi.Mappings.DTOs.Contact;
using AddressBookWebApi.Mappings.DTOs.ContactDetail;
using AutoMapper;

namespace AddressBookWebApi.Mappings
{
    public class AddressBookProfile : Profile
    {
        public AddressBookProfile()
        {
            CreateMap<Contact, ContactReadDto>();
            CreateMap<ContactCreateDto, Contact>();

            CreateMap<ContactDetail, ContactDetailReadDto>();
            CreateMap<ContactDetailCreateDto, ContactDetail>();
        }
    }
}
