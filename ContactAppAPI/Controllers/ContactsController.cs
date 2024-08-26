using Microsoft.AspNetCore.Mvc;
using ContactAppAPI.Models;
using ContactAppAPI.Data;
using Microsoft.EntityFrameworkCore;
namespace ContactAppAPI.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contacts = await _context.Contacts.Include(c => c.PhoneNumbers).ToListAsync();
            return contacts;
        }

        // GET api/contacts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var contact = await _context.FindAsync<Contact>(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        // POST api/contacts
        [HttpPost]
        public async Task<IActionResult> CreateContact(Contact contact)
        {
            await _context.AddAsync(contact);
            foreach (var phoneNumber in contact.PhoneNumbers)
            {
                phoneNumber.ContactId = contact.Id;
                await _context.AddAsync(phoneNumber);                 
            }
             await _context.SaveChangesAsync();  
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        // PUT api/contacts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
             var updated = await _context.FindAsync<Contact>(id);
            if (updated == null)
            {
                return NotFound();
            }

            _context.Update(contact);
            await _context.SaveChangesAsync();
            return Ok(contact);
        }

        // DELETE api/contacts/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contact = _context.Find<Contact>(id);
            if (contact == null)
            {
                return NotFound();
            }
            _context.Remove(contact);
            _context.SaveChanges();
            return Ok();
        }
    }
}