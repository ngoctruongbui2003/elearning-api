using System;
using AutoMapper;
using ElearningAPI.Datas;
using ElearningAPI.DTOs;
namespace ElearningAPI.Helper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Classroom,ClassroomDTO>().ReverseMap();
			CreateMap<Classroom,ClassroomCreateDTO>().ReverseMap();
			CreateMap<ClassroomCreate,ClassroomCreateDTO>().ReverseMap();
			CreateMap<User,UserDTO>().ReverseMap();
			CreateMap<Post,PostDTO>().ReverseMap();
			CreateMap<Comment,CommentDTO>().ReverseMap();
			CreateMap<Assignment,AssignmentDTO>().ReverseMap();
		}
	}
}

