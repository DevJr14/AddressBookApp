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
            CreateMap<ContactReadDto, Contact>();
            CreateMap<ContactCreateDto, Contact>();

            CreateMap<ContactDetail, ContactDetailReadDto>();
            CreateMap<ContactDetailReadDto, ContactDetail>();
            CreateMap<ContactDetailCreateDto, ContactDetail>();
        }
    }
}
