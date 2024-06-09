using System;
using ElearningAPI.DTOs;
namespace ElearningAPI.Services
{
	public interface IClassroomService
	{
		Task <bool> AddClassroomAsync(ClassroomCreateDTO model);
        Task <ClassroomDTO> UpdateClassroomAsync(int id,ClassroomDTO model);

        Task<bool> DeleteClassroomAsync(int id);

        Task<IEnumerable<ClassroomDTO>> GetAllClassroomAsync();

        Task<ClassroomDTO> GetClassroomByIdAsync(int id);

        Task<bool> JoinClassroomByCodeAsync(string code);

        Task<ClassroomDTO> GetClassroomByCodeAsync(String code);

        Task<IEnumerable<UserDTO>> GetUserByClassroomIdAsync(int classroomId);

        

        
	}
}

