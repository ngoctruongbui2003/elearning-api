using System;
using System.Security.Claims;
using AutoMapper;
using ElearningAPI.Datas;
using ElearningAPI.DTOs;
using ElearningAPI.Repositories;
namespace ElearningAPI.Services.Impl
{
	public class CommentService : ICommentService
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IMapper _mapper;

		private readonly IHttpContextAccessor _httpContextAccessor;
		public CommentService(ICommentRepository commentRepository,IMapper mapper,IHttpContextAccessor httpContextAccessor)
		{
			_commentRepository = commentRepository;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
		}

        public async Task<CommentDTO> AddCommentAsync(string description ,int postId)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
			var user = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);	
			var comment = new Comment{
				Description = description,
				PostId = postId,
				CreatedAt = DateTime.Now,
				UserId = user,
				IsDelete= false,

			};
			await _commentRepository.Add(comment); 
			return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            return await _commentRepository.Delete(id);
        }

        public async Task<IEnumerable<CommentDTO>> GetAllCommentAsync()
        {
            var comment = await _commentRepository.getAll();
			return _mapper.Map<IEnumerable<CommentDTO>>(comment);
        }

        public async Task<CommentDTO> GetCommentByIdAsync(int id)
        {
            var comment =  await _commentRepository.GetById(id);
			return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<CommentDTO> UpdateCommentAsync(int id, CommentDTO model)
        {
            var comment = await _commentRepository.GetById(id);

			if(comment == null){
				throw new KeyNotFoundException("Comment not found");
			}
			_mapper.Map(model,comment);
			await _commentRepository.Update(comment);
			return _mapper.Map<CommentDTO>(comment);
        }
    }
}

