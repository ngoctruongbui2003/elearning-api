using System;
using ElearningAPI.DTOs;
namespace ElearningAPI.Services
{
	public interface ICommentService
	{
		Task<CommentDTO> AddCommentAsync(string description, int postId);

		Task<CommentDTO> UpdateCommentAsync(int id,CommentDTO model);

		Task<bool> DeleteCommentAsync(int id);

		Task<CommentDTO> GetCommentByIdAsync(int id);

		Task<IEnumerable<CommentDTO>> GetAllCommentAsync();
	}
}

