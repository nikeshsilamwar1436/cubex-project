using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webapi.Models;

namespace Webapi.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDetailsController : ControllerBase
    {
        private readonly ApiContext _context;

        public TaskDetailsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/TaskDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDetail>>> GetTask()
        {
            return await _context.TaskDetails.ToListAsync();
        }

        // GET: api/TaskDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDetail>> GetTaskDetail(string id)
        {
            var taskDetail = await _context.TaskDetails.FindAsync(id);

            if (taskDetail == null)
            {
                return NotFound();
            }

            return taskDetail;
        }

        // PUT: api/TaskDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskDetail(string id, TaskDetail taskDetail)
        {
            if (id != taskDetail.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(taskDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskDetailExists(id))
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

        // POST: api/TaskDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TaskDetail>> PostTaskDetail(TaskDetail taskDetail)
        {
            _context.TaskDetails.Add(taskDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaskDetailExists(taskDetail.TaskId))
                {
                    return Ok("already exists");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTaskDetail", new { id = taskDetail.TaskId }, taskDetail);
        }

        // DELETE: api/TaskDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskDetail>> DeleteTaskDetail(string id)
        {
            var taskDetail = await _context.TaskDetails.FindAsync(id);
            if (taskDetail == null)
            {
                return NotFound();
            }

            _context.TaskDetails.Remove(taskDetail);
            await _context.SaveChangesAsync();

            return taskDetail;
        }

        private bool TaskDetailExists(string id)
        {
            return _context.TaskDetails.Any(e => e.TaskId == id);
        }
    }
}
