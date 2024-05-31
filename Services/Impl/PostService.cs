using System;
using System.Security.Claims;
using AutoMapper;
using ElearningAPI.Datas;
using ElearningAPI.DTOs;
using ElearningAPI.Repositories;
using ElearningAPI.Repositories.Impl;
namespace ElearningAPI.Services
{
	public class PostService : IPostService
	{
		private readonly IPostRepository _postRepository;
		private readonly IMapper _mapper;

		

		private readonly IHttpContextAccessor _httpContextAccessor;
		
		public PostService(IPostRepository postRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)

		{
			
			_httpContextAccessor = httpContextAccessor;
			_postRepository = postRepository;
			_mapper = mapper;
		}

        public async Task<PostDTO> AddPostAsync(string description , int classroomId)
        {
			
			HttpContext httpContext = _httpContextAccessor.HttpContext;
			var user = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);	
            var post = new Post{
				Description = description,
				ClassroomId = classroomId,
				CreatedAt = DateTime.Now,
				IsDelete = false,
				UserId = user,
			};
			await _postRepository.Add(post);
			var postDTO  = _mapper.Map<PostDTO>(post);
			return postDTO;

			
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            return await _postRepository.Delete(id);
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostAsync()
        {
            var post = await _postRepository.GetAll();
			return _mapper.Map<IEnumerable<PostDTO>>(post);
        }

        public async Task<PostDTO> GetByIdPostAsync(int id)
        {
            var post = await _postRepository.GetById(id);
			return _mapper.Map<PostDTO>(post);
        }

        public async Task<PostDTO> UpdatePostAsync(int classroomId,PostDTO model)
        {
            var post = await _postRepository.GetById(classroomId);
			if(post == null){
				throw new KeyNotFoundException("Post not found");
			}
			_mapper.Map(model,post);

			await _postRepository.Update(post);
			return _mapper.Map<PostDTO>(post);
        }
    }
}

