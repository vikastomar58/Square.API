using AutoMapper;
using Squares.API.DataLayer.Entities;
using Squares.API.DataLayer.EntityFrameworkCore;
using Squares.API.Domain.Dto;
using Squares.API.Domain.Helper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Squares.API.Domain.Manager
{
    public class RegistrationManager : IRegistrationManager
    {
        private SquareDbContext _context;
        private IMapper _mapper;
        public RegistrationManager(SquareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public UserDetail LogIn(LoginRequestDto loginRequest)
        {
           var user=_context.UserDetails.FirstOrDefault(x=>x.Email.ToLower()==loginRequest.Email.ToLower());

            if (user != null && ValidatePassword(user, loginRequest.Password))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        private bool ValidatePassword(UserDetail user, string password)
        {
            if (string.Equals(user.Password, CommonMethod.Encryption(password,user.Salt), StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        public async Task<bool> SignUp(SignUpRequestDto signUpRequest)
        {
            try
            {
                var user = _context.UserDetails.FirstOrDefault(x => x.Email.ToLower() == signUpRequest.Email.ToLower());
                if (user == null)
                {                    
                    UserDetail userDetail = _mapper.Map<UserDetail>(signUpRequest);

                    await _context.UserDetails.AddAsync(userDetail);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
