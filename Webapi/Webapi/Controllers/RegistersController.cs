using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Webapi.Models;

namespace Webapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly ApiContext _context;
  
        public RegistersController(ApiContext context)
        {
            _context = context;
  
        }

        //PATCH: api/Registers/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(string id)
        {
            var register = await _context.Registers.FindAsync(id);
            JsonPatchDocument<Register> patchDoc = new JsonPatchDocument<Register>();
            if (register != null)
            {
                patchDoc.Replace(e => e.Password, register.Password);
                //registerPatch.ApplyTo(register);
                await _context.SaveChangesAsync();
                return Ok(register);
            }
            else
            {
                return BadRequest();
            }

        }

        // GET: api/Registers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Register>>> GetRegister()
        {
            return await _context.Registers.ToListAsync();
        }

        // GET: api/Registers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Register>> GetRegister(string id)
        {
            var register = await _context.Registers.FindAsync(id);

            if (register == null)
            {
                return NotFound();
            }

            return register;
        }

        // PUT: api/Registers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegister(string id, Register register)
        {
            if (id != register.Email)
            {
                return BadRequest();
            }

            _context.Entry(register).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Registers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Register>> PostRegister(Register register)
        {
            register.Password = Hashing.HashSHA(register.Password);
            _context.Registers.Add(register);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if ((RegisterExists(register.Email)) && (RegisterExist(register.Password)))
                {
                    return Ok();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRegister", new { id = register.Email }, register);
        }

        // DELETE: api/Registers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Register>> DeleteRegister(string id)
        {
            var register = await _context.Registers.FindAsync(id);
            if (register == null)
            {
                return NotFound();
            }

            _context.Registers.Remove(register);
            await _context.SaveChangesAsync();

            return register;
        }

        private bool RegisterExists(string id)
        {
            return _context.Registers.Any(e => e.Email == id);
        }
        private bool RegisterExist(string password)
        {
            return _context.Registers.Any(e => e.Password == password);
        }
    }
}
