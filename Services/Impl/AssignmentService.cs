using System.Security.Claims;
using AutoMapper;
using ElearningAPI.Datas;
using ElearningAPI.DTOs;
using ElearningAPI.Repositories;

namespace ElearningAPI.Services.Impl
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;

		private readonly IHttpContextAccessor _httpContextAccessor;

        public AssignmentService(IAssignmentRepository assignmentRepository,IMapper mapper,IHttpContextAccessor httpContextAccessor){
            _assignmentRepository = assignmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<AssignmentDTO> AddAssignmentAsync(AssignmentDTO model, int classroomId)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
			var user = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);	
           
			var assignment = _mapper.Map<Assignment>(model);
            assignment.ClassroomId = classroomId;
            assignment.UserId = user;
            
            await _assignmentRepository.Add(assignment);
            return _mapper.Map<AssignmentDTO>(assignment);
            
        }

        public async Task<bool> DeleteAssignmentAsync(int id)
        {
            return await _assignmentRepository.Delete(id);
        }

        public async Task<IEnumerable<AssignmentDTO>> GetAllAssignmentAsync()
        {
            var assignments = await _assignmentRepository.GetAll();
			return _mapper.Map<IEnumerable<AssignmentDTO>>(assignments);
        }

        public async Task<AssignmentDTO> GetAssignmentByIdAsync(int id)
        {
            var assignment = await _assignmentRepository.GetById(id);
            return _mapper.Map<AssignmentDTO>(assignment);
        }

        public async Task<AssignmentDTO> UpdateAssignmentAsync(int classroomId,AssignmentDTO model)
        {
            var assignment = await _assignmentRepository.GetById(classroomId);
            if(assignment == null){
				throw new KeyNotFoundException("assignment not found");
			}
			_mapper.Map(model,assignment);

			await _assignmentRepository.Update(assignment);
			return _mapper.Map<AssignmentDTO>(assignment);
        }
    }
}