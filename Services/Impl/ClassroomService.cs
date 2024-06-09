using System.Security.Claims;
using AutoMapper;
using ElearningAPI.Datas;
using ElearningAPI.DTOs;
using ElearningAPI.Models;
using ElearningAPI.Repositories;
using ElearningAPI.Repositories.Impl;

namespace ElearningAPI.Services
{
    public class ClassroomService : IClassroomService
    {
		private readonly IClassroomRepository _classroomRepository;
		private readonly IMapper _mapper;
		
		private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IClassroomCreateRepository _classroomCreateRepository;

        private readonly IUserRepository _userRepository;

        private readonly IPostRepository _postRepository;

		public ClassroomService(
            IClassroomRepository classroomRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IClassroomCreateRepository classroomCreateRepository,
            IUserRepository userRepository,
            IPostRepository postRepository
            )
        {
                _classroomRepository = classroomRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
                _classroomCreateRepository = classroomCreateRepository;
                _userRepository = userRepository;
                _postRepository = postRepository;
		}
        public async Task<bool> AddClassroomAsync(ClassroomCreateDTO model)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
			var user = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var classroom = _mapper.Map<Classroom>(model);
            classroom.Code = "1ID";
			classroom.CreatedAt = DateTime.Now;
			classroom.CreatedUser = user;
			classroom.IsDeleted = false;
			classroom.IsTurnOnCode = true;

            var result = await _classroomRepository.Add(classroom);
            if(!result){
                return false;
            }
            
            var classroomCreate = new ClassroomCreate{
                IsTeacher = true,
                IsExit = false,
                ClassroomId = classroom.Id,
                UserId = classroom.CreatedUser,
      
            };
           
			return await _classroomCreateRepository.Add(classroomCreate);
        }

        public async Task<bool> DeleteClassroomAsync(int id)
        {
            
            return await _classroomRepository.Delete(id);
        }

        public async Task<IEnumerable<ClassroomDTO>> GetAllClassroomAsync()
        {
            var classrooms = await _classroomRepository.GetAll();
            return _mapper.Map<IEnumerable<ClassroomDTO>>(classrooms);
        }

        public async Task<ClassroomDTO> GetClassroomByCodeAsync(string code)
        {
            var checkClassroom = await _classroomRepository.GetByCode(code);
            return _mapper.Map<ClassroomDTO>(checkClassroom);
        }
        

        public async Task<ClassroomDTO> GetClassroomByIdAsync(int id)
        {
            var classrooms = await _classroomRepository.GetById(id);
            // IEnumerable<Post> listPost = classrooms.Posts; 
            
            return _mapper.Map<ClassroomDTO>(classrooms);
        }

        public async Task<ClassroomDTO> GetClassroomByIdAsync(string code)
        {
            var checkClassroom = await _classroomRepository.GetByCode(code);
            return _mapper.Map<ClassroomDTO>(checkClassroom);
        }

        public async Task<IEnumerable<UserDTO>> GetUserByClassroomIdAsync(int classroomId)
        {
            var classroomCreate = await _classroomCreateRepository.GetById(classroomId);
            var userId = classroomCreate.Select(x => x.UserId).Distinct();
            var users = await _userRepository.GetAllById(userId);
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<bool> JoinClassroomByCodeAsync(string code)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
			var user = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var joinInClassroom = await _classroomRepository.GetByCode(code);


            if(joinInClassroom != null){
                
                var existing = await _classroomCreateRepository.GetByUser(user,joinInClassroom.Id);
                if(existing != null){
                    return false;
                }
                var classroomCreate = new ClassroomCreate{
                    IsExit = false,
                    IsTeacher = false,
                    UserId =user,
                    ClassroomId = joinInClassroom.Id

                };
                var result = await _classroomCreateRepository.Add(classroomCreate);
                
                return result;
            }
            return false;
            
        }

        public async Task<ClassroomDTO> UpdateClassroomAsync(int id,ClassroomDTO model)
        {
            var classrooms = await _classroomRepository.GetById(id);
            if(classrooms == null){
                throw new KeyNotFoundException("Classroom not found");
            }
            // Ánh xạ các thuộc tính từ ClassroomDTO sang đối tượng Classroom
            _mapper.Map(model,classrooms);

            await _classroomRepository.Update(classrooms);
            return _mapper.Map<ClassroomDTO>(classrooms);
        } 
    }
}
