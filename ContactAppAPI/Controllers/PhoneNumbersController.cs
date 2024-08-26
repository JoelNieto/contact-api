using Microsoft.AspNetCore.Mvc;
using ContactAppAPI.Models;
using ContactAppAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumbersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhoneNumbersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PhoneNumbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneNumber>>> Get()
        {
            // TODO: Implement logic to retrieve all phone numbers from the database
            // and return them as a list of PhoneNumber objects
            // Example:
            // var phoneNumbers = await _context.PhoneNumbers.ToListAsync();
            // return Ok(phoneNumbers);

            return Ok();
        }

        // GET: api/PhoneNumbers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneNumber>> GetAsync(int id)
        {
            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);

            if (phoneNumber == null)
            {
                return NotFound();
            }
            return Ok(phoneNumber);

        }

        // POST: api/PhoneNumbers
        [HttpPost]
        public async Task<ActionResult<PhoneNumber>> Post([FromBody] PhoneNumber phoneNumber)
        {
            var contact = await _context.Contacts.Include(c => c.PhoneNumbers).FirstOrDefaultAsync();
            if (contact == null)
            {
                return NotFound();
            }

            contact.PhoneNumbers.Add(phoneNumber);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = phoneNumber.Id }, phoneNumber);
        }

        // PUT: api/PhoneNumbers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PhoneNumber phoneNumber)
        {
            var existingPhoneNumber = await _context.PhoneNumbers.FindAsync(id);
            if (existingPhoneNumber == null)
            {
                return NotFound();
            }
            existingPhoneNumber.Number = phoneNumber.Number;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/PhoneNumbers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }
            _context.PhoneNumbers.Remove(phoneNumber);
            _context.SaveChanges();
            return NoContent();
        }
    }
}