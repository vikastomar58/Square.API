using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Squares.API.Domain.Constant;
using Squares.API.Domain.Dto;
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
        private readonly IIdentityManager _identityManager;
        private readonly ITokenGeneration _tokenGeneration;
        public RegisterController(IIdentityManager identityManager, ITokenGeneration tokenGeneration)
        {
            _identityManager = identityManager;
            _tokenGeneration = tokenGeneration;
        }

        /// <summary>
        /// This is a POST request to register user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SignUp")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDto> SignUp([FromBody] SignUpRequestDto model)
        {
            var response = new ResponseDto();

            Random salt = new Random(10000);
            model.Salt = salt.Next().ToString();

            if (await _identityManager.SignUp(model))
            {
                response.Data = Constants.UserCreatedMessage;
                response.MetaData = new ResponseMetaData
                {
                    Code = Constants.SuccessCode
                };
            }
            else
            {
                response.Data = Constants.UserExistsMessage;
                response.MetaData = new ResponseMetaData
                {
                    Code = Constants.SuccessCode
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

            var user = _identityManager.LogIn(model);
            if (user != null)
            {
                response.Data = _tokenGeneration.GenerateToken(user);
                response.MetaData = new ResponseMetaData
                {
                    Code = Constants.SuccessCode
                };
            }
            else
            {
                response.Data = Constants.UserNotExistsMessage;
                response.MetaData = new ResponseMetaData
                {
                    Code = Constants.SuccessCode
                };
            }

            return response;
        }
    }
}
