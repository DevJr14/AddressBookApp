using AddressBookWebApi.Entities;
using AddressBookWebApi.Mappings.DTOs.Contact;
using AddressBookWebApi.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressBookWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IAddressBookService _addressBookService;
        private readonly IMapper _mapper;

        public ContactsController(IAddressBookService addressBookService, IMapper mapper) 
        {
            _addressBookService = addressBookService;
            _mapper = mapper;
        }
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactReadDto>>> GetFullContacts()
        {
            try
            {
                IEnumerable<Contact> contactsFromService = await _addressBookService.GetFullContactAndContactDetails();
                return Ok(contactsFromService);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }*/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactReadDto>>> GetAllContacts(string name = "")
        {
            try
            {
                IEnumerable<Contact> contactsFromService;

                if (name.Any(c => char.IsLetter(c)))
                {
                    contactsFromService = await _addressBookService.SearchContactsByName(name);
                }
                else
                {
                    string nameQueryWithoutSpaces = new string(name.Where(c => !char.IsWhiteSpace(c)).ToArray());
                    string modifiedQuery = Regex.Replace(nameQueryWithoutSpaces, "^([+])", "");

                    contactsFromService = await _addressBookService.SearchContactsByName(modifiedQuery);
                }

                IEnumerable<ContactReadDto> contactReadDtos = _mapper.Map<IEnumerable<ContactReadDto>>(contactsFromService);
                return Ok(contactReadDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}", Name = "GetContactById")]
        public async Task<ActionResult<ContactReadDto>> GetContactById(int id)
        {
            try
            {
                Contact contactFromService = await _addressBookService.GetContactById(id);

                if (contactFromService == null)
                {
                    return NotFound();
                }

                ContactReadDto contactReadDto = _mapper.Map<ContactReadDto>(contactFromService);
                return Ok(contactReadDto);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{contactid}/contactdetail/{contactdetailid}")]
        public async Task<ActionResult<ContactReadDto>> GetContactAndContactDetails(int cId, int cdId)
        {
            try
            {
                bool contactExists = await _addressBookService.ContactExists(cId);

                if (!contactExists)
                {
                    return NotFound();
                }

                ContactDetail contactDetailFromService = await _addressBookService.GetContactDetail(cId, cdId);

                if (contactDetailFromService == null)
                {
                    return NotFound();
                }

                ContactReadDto contactReadDto = _mapper.Map<ContactReadDto>(contactDetailFromService.Contact);

                return Ok(contactReadDto);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ContactReadDto>> CreateContact(ContactCreateDto createContactDto)
        {
            try
            {
                if (createContactDto == null)
                {
                    return BadRequest();
                }
                createContactDto.UpdatedDate = DateTime.UtcNow;

                Contact contact = _mapper.Map<Contact>(createContactDto);
                await _addressBookService.CreateContact(contact);
                await _addressBookService.SaveAsync();

                Contact recentlyAddedContact = await _addressBookService.FindAsync(contact.ContactDetails.First().ContactId);

                ContactReadDto contactReadDto = _mapper.Map<ContactReadDto>(recentlyAddedContact);

                return CreatedAtAction(nameof(GetContactById), new { id = contactReadDto.ContactId }, contactReadDto);

            }
            catch(DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ContactReadDto>> UpdateContact(int id, ContactReadDto contactReadDto)
        {
            try
            {
                if (id != contactReadDto.ContactId)
                {
                    return BadRequest();
                }
                contactReadDto.UpdatedDate = DateTime.UtcNow;
                Contact contact = _mapper.Map<Contact>(contactReadDto);

                _addressBookService.UpdateContact(contact);
                await _addressBookService.SaveAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteContact(int id)
        {
            Contact contactFromService = await _addressBookService.GetContactById(id);
            if (contactFromService == null)
            {
                return BadRequest();
            }
            await _addressBookService.DeleteContact(contactFromService);
            await _addressBookService.SaveAsync();

            return NoContent();
        }
    }
}
