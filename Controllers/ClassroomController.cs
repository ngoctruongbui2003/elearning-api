using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElearningAPI.DTOs;
using ElearningAPI.Models;
using ElearningAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElearningAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    public class ClassroomController : Controller
    {
        private readonly IClassroomService _classroomService;
       

        
        public ClassroomController(IClassroomService classroomService){
            _classroomService = classroomService;
         

        }
        // GET api/values/5
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
             return Ok(await _classroomService.GetAllClassroomAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _classroomService.GetClassroomByIdAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ClassroomDTO classroomDTO)
        {
            var result = await _classroomService.AddClassroomAsync(classroomDTO);
            if(result){
                return Ok();
            }else{
                return BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ClassroomDTO classroomDTO)
        {
            try
            {
                var classroom = await _classroomService.UpdateClassroomAsync(id,classroomDTO);
                return Ok(classroom);
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
            return Ok(await _classroomService.DeleteClassroomAsync(id));
        }
        
        [HttpGet("Code/{code}")]
        public async Task<IActionResult> GetCode(String code){
            return Ok(await _classroomService.GetClassroomByCodeAsync(code));
        }

        [HttpPost("Join/{code}")]
        public async Task<IActionResult> JoinClassroom(string code){
            var result = await _classroomService.JoinClassroomByCodeAsync(code);
            if(!result){
                return NotFound("Classroom not found or user already joined");
            }
            return  Ok("Joined classroom successfully");
        }

        [HttpGet("{classroomId}/member")]
        public async Task<IActionResult> GetUserByClassroomId(int classroomId){
            var userName = await _classroomService.GetUserByClassroomIdAsync(classroomId);
            if(userName == null || !userName.Any()){
                return NotFound("No user found");

            }
            return Ok(userName);
        }
    }
}

