﻿using AutoMapper;
using Squares.API.DataLayer.Core.Repository;
using Squares.API.DataLayer.Entities;
using Squares.API.Domain.Dto;
using Squares.API.Domain.Helper;
using System;
using System.Threading.Tasks;

namespace Squares.API.Domain.Manager
{
    public class RegistrationManager : IRegistrationManager
    {
        private IEfRepository<UserDetail> _userDetailRepository;
        private IMapper _mapper;

        public RegistrationManager(IEfRepository<UserDetail> userDetailRepository, IMapper mapper)
        {
            _userDetailRepository = userDetailRepository;
            _mapper = mapper;
        }
        #region Public Methods

        /// <summary>
        /// This method is for validating the user
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public UserDetail LogIn(LoginRequestDto loginRequest)
        {
            var user = _userDetailRepository.FindSingleAsync(x => x.Email.ToLower() == loginRequest.Email.ToLower()).Result;

            if (user != null && ValidatePassword(user, loginRequest.Password))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method is for adding user record
        /// </summary>
        /// <param name="signUpRequest"></param>
        /// <returns></returns>
        public async Task<bool> SignUp(SignUpRequestDto signUpRequest)
        {
            try
            {
                var user = _userDetailRepository.FindSingleAsync(x => x.Email.ToLower() == signUpRequest.Email.ToLower()).Result;
                if (user == null)
                {
                    UserDetail userDetail = _mapper.Map<UserDetail>(signUpRequest);

                    await _userDetailRepository.AddAsync(userDetail);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        } 
        #endregion

        #region Private Method

        /// <summary>
        /// This method is for validating the password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool ValidatePassword(UserDetail user, string password)
        {
            if (string.Equals(user.Password, CommonMethod.Encryption(password, user.Salt), StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        } 

        #endregion
    }
}