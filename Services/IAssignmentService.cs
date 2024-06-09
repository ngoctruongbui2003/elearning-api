using ElearningAPI.DTOs;

namespace ElearningAPI.Services
{
    public interface IAssignmentService
    {
        Task<AssignmentDTO> AddAssignmentAsync(AssignmentDTO model, int classroomId);

		Task<AssignmentDTO> UpdateAssignmentAsync(int classroomId,AssignmentDTO model);

		Task<bool> DeleteAssignmentAsync(int id);

		Task<AssignmentDTO> GetAssignmentByIdAsync(int id);

		Task<IEnumerable<AssignmentDTO>> GetAllAssignmentAsync();
    }
}