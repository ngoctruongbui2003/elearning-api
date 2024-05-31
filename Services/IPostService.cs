using System;
using ElearningAPI.DTOs;
namespace ElearningAPI.Services
{
	public interface IPostService
	{
		Task<PostDTO> AddPostAsync(string Description ,int classroomId);

		Task<PostDTO> UpdatePostAsync(int classroomId, PostDTO model);

		Task<bool> DeletePostAsync(int id);

		Task<IEnumerable<PostDTO>> GetAllPostAsync();

		Task<PostDTO> GetByIdPostAsync(int id);


	}
}

