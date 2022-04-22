using Squares.API.DataLayer.Entities;
using Squares.API.Domain.Dto;
using System.Collections.Generic;

namespace Squares.API.Test.FixtureData
{
    public class GlobalData
    {
        public static List<CoordinateRequestDto> GetListCoordinateRequestDto()
        {
            return new List<CoordinateRequestDto>()
            {
                new CoordinateRequestDto
                {
                    X=1,
                     Y=-1
                },
                new CoordinateRequestDto
                {
                    X=1,
                    Y=1
                },
                new CoordinateRequestDto
                {
                    X=-1,
                     Y=-1
                },
                new CoordinateRequestDto
                {
                    X=-1,
                    Y=1
                }
            };
        }

        public static List<Coordinate> GetListCoordinates()
        {
            return new List<Coordinate>()
            {
                new Coordinate
                {
                    X=1,
                     Y=1,
                     UserId=1
                },
                new Coordinate
                {
                    X=1,
                    Y=-1,
                     UserId=1
                },
                new Coordinate
                {
                    X=-1,
                     Y=-1,
                     UserId=1
                },
                new Coordinate
                {
                    X=-1,
                    Y=1,
                     UserId=1
                }
            };
        }

        public static SignUpRequestDto GetSignUpRequestDto()
        {
            return new SignUpRequestDto
            {
                Email = "test@gmail.com",
                FirstName = "Test",
                Password = "Test@123"
            };
        }

        public static UserDetail GetUserDetail()
        {
            return new UserDetail
            {
                Email = "test@gmail.com",
                FirstName = "Test",
                Password = "wEWyPwhjpVqWAhb3optsy0AqVUbMzjSjdzeUjzDPiKk=",
                Salt = "2334243"
            };
        }

        public static LoginRequestDto GetLoginRequestDto()
        {
            return new LoginRequestDto
            {
                Email = "test@gmail.com",
                Password = "Test@123"
            };
        }
    }
}
