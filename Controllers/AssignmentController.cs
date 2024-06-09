using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElearningAPI.DTOs;
using ElearningAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElearningAPI.Controllers
{
    [Route("api/[controller]")]
    public class AssignmentController : Controller
    {
        private readonly IAssignmentService _assignmentService;
        public AssignmentController(IAssignmentService assignmentService){
            _assignmentService = assignmentService;
        }
        // GET: api/values
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _assignmentService.GetAllAssignmentAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _assignmentService.GetAssignmentByIdAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AssignmentDTO model,int classroomId)
        {
            var result = await _assignmentService.AddAssignmentAsync(model,classroomId);
            if(result != null){
                return Ok();
            }else{
                return BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AssignmentDTO model)
        {
            try
            {
                var assignment = await _assignmentService.UpdateAssignmentAsync(id,model);
                return Ok(assignment);
            }
            catch (KeyNotFoundException)
            {
                
                return NotFound();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _assignmentService.DeleteAssignmentAsync(id) );
        }
    }
}

