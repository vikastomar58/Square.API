using Squares.API.DataLayer.Entities;
using Squares.API.Domain.Dto;
using System.Threading.Tasks;

namespace Squares.API.Domain.Manager
{
   public interface IIdentityManager
    {
        Task<bool> SignUp(SignUpRequestDto signUpRequest);

        UserDetail LogIn(LoginRequestDto loginRequest);
    }
}
