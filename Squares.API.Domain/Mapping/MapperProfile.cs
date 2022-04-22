using AutoMapper;
using Squares.API.DataLayer.Entities;
using Squares.API.Domain.Dto;
using Squares.API.Domain.Helper;

namespace Squares.API.Domain.Mapping
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<SignUpRequestDto, UserDetail>()
                .ForMember(des => des.Password, act => act.MapFrom(src => HashingHelper.Encryption(src.Password,src.Salt)));

            CreateMap<CoordinateRequestDto, Coordinate>().ReverseMap();
        }
    }
}
