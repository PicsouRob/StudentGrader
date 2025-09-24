using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;

namespace Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
            CreateMap<User, Users>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap()
                .ValidateMemberList(MemberList.Source);
        }
	}
}

