using AutoMapper;
using Squares.API.DataLayer.Entities;
using Squares.API.Domain.Dto;
using Squares.API.Domain.Helper;

namespace Squares.API.Domain.Mapping
{
    public class MappingClass:Profile
    {
        public MappingClass()
        {
            CreateMap<SignUpRequestDto, UserDetail>()
                .ForMember(des => des.Password, act => act.MapFrom(src => CommonMethod.Encryption(src.Password,src.Salt)));

            CreateMap<CoordinateRequestDto, Coordinate>().ReverseMap();
               // .ForMember(des => des.UserId, act => act.MapFrom(src => CommonMethod.GetUserId()));
        }
    }
}
