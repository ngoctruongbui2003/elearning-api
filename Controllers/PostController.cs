using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElearningAPI.DTOs;
using ElearningAPI.Repositories.Impl;
using ElearningAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElearningAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService){
            _postService = postService;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetAllPostAsync());
        }

        // GET api/values/5
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetById(int id)
        // {
        //     return Ok(await _postService.GetByIdPostAsync(id));
        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdClassroom(int id)
        {
            return Ok(await _postService.GetByIdClassroomAsync(id));
        }
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> AddPost(string description, int classroomId)
        {
            try
            {
                var result = await _postService.AddPostAsync(description,classroomId);
                return Ok(result);
            }
            catch (Exception e)
            {
                
                return StatusCode(500, "An error occurred while adding the post.");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PostDTO model)
        {
            try
            {
                var post = await _postService.UpdatePostAsync(id,model);
                return Ok(post);
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
            return Ok(await _postService.DeletePostAsync(id));
        }

        
    }
}

