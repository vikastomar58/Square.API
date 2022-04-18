using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Squares.API.Domain.Dto;
using Squares.API.Domain.Helper;
using Squares.API.Domain.Manager;
using System;
using System.Threading.Tasks;

namespace Squares.API.Controllers
{
    /// <summary>
    /// This is a controller that contains POST requests
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        private readonly IRegistrationManager _registrationManager;
        private readonly ITokenGeneration _tokenGeneration;
        public RegisterController(IRegistrationManager registrationManager, ITokenGeneration tokenGeneration)
        {
            _registrationManager = registrationManager;
            _tokenGeneration = tokenGeneration;
        }

        /// <summary>
        /// This is a POST request to register user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SignUp")]
        [ProducesResponseType(typeof(ResponseDto),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDto> SignUp([FromBody] SignUpRequestDto model)
        {
            var response = new ResponseDto();

            try
            {
                Random salt = new Random(10000);
                model.Salt = salt.Next().ToString();

                if (await _registrationManager.SignUp(model))
                {
                    response.Data = GlobalConstant.UserCreatedMessage;
                    response.MetaData = new ResponseMetaData
                    {
                        Code = GlobalConstant.SuccessCode
                    };
                }
                else
                {
                    response.Data = GlobalConstant.UserExistsMessage;
                    response.MetaData = new ResponseMetaData
                    {
                        Code = GlobalConstant.SuccessCode
                    };
                }
            }
            catch(Exception ex)
            {
                response.Data = string.Empty;
                response.MetaData = new ResponseMetaData
                {
                    Code = GlobalConstant.ErrorCode,
                    Message=ex.Message
                };
            }
            return response;
        }

        /// <summary>
        /// This is a Post request to geneate token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LogIn")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public ResponseDto LogIn([FromBody] LoginRequestDto model)
        {
            var response = new ResponseDto();

            try
            {
                var user = _registrationManager.LogIn(model);
                if (user != null)
                {
                    response.Data = _tokenGeneration.GenerateToken(user);
                    response.MetaData = new ResponseMetaData
                    {
                        Code = GlobalConstant.SuccessCode
                    };
                }
                else
                {
                    response.Data = GlobalConstant.UserNotExistsMessage;
                    response.MetaData = new ResponseMetaData
                    {
                        Code = GlobalConstant.SuccessCode
                    };
                }
            }
            catch (Exception ex)
            {
                response.Data = string.Empty;
                response.MetaData = new ResponseMetaData
                {
                    Code = GlobalConstant.ErrorCode,
                    Message = ex.Message
                };
            }
            return response;
        }
    }
}
