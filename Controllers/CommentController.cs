using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElearningAPI.DTOs;
using ElearningAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElearningAPI.Controllers
{
    [Route("api/Classroom/Post/{postId}/[controller]")]
    [Authorize(Roles = "User")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService){
            _commentService = commentService;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _commentService.GetAllCommentAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(string description,int postId)
        {
            var result = await _commentService.AddCommentAsync(description,postId);
            return Ok(result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CommentDTO model)
        {
            try
            {
                var comment = await _commentService.UpdateCommentAsync(id,model);
                return Ok(comment);
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
            return Ok(await _commentService.DeleteCommentAsync(id));
        }
    }
}

