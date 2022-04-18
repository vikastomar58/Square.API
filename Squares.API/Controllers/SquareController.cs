using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Squares.API.Domain.Dto;
using Squares.API.Domain.Helper;
using Squares.API.Domain.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Squares.API.Controllers
{
    /// <summary>
    /// This contains the POST/GET/DELETE method for Square API
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SquareController : ControllerBase
    {
        private int _userId;
        private readonly IPointManager _pointManager;
        public SquareController(IPointManager pointManager)
        {
            _pointManager = pointManager;
        }

        /// <summary>
        /// This is a Post request to add points 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDto> Add(List<CoordinateRequestDto> points)
        {
            var response = new ResponseDto();
            try
            {
                setUser();
                if (await _pointManager.AddPoints(points, _userId))
                {
                    response.Data =GlobalConstant.CoordinateCreatedMessage;
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

        private void setUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity!=null)
            {
                var user = identity.Claims;

                _userId =Convert.ToInt32(user.FirstOrDefault(o => o.Type == "Id")?.Value);
            }
        }

        /// <summary>
        /// This is Get request to return no. of Square as per user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public ResponseDto GetSquareCount()
        {
            var response = new ResponseDto();
            try
            {
                setUser();
                response.Data = _pointManager.CalculateSquare(_userId).ToString();

                response.MetaData = new ResponseMetaData
                {
                    Code = GlobalConstant.SuccessCode
                };
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

        /// <summary>
        /// This is a Delete request to remove a Point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDto> Delete(CoordinateRequestDto point)
        {
            var response = new ResponseDto();

            try
            {
                setUser();
                if (await _pointManager.Delete(point, _userId))
                {
                    response.Data = GlobalConstant.CoordinateDeletedMessage;
                    response.MetaData = new ResponseMetaData
                    {
                        Code = GlobalConstant.SuccessCode
                    };
                }
                else
                {
                    response.Data = string.Empty;
                    response.MetaData = new ResponseMetaData
                    {
                        Code = GlobalConstant.ErrorCode,
                        Message=GlobalConstant.CoordinateNotFoundMessage
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

        /// <summary>
        /// This is a Get request to Upload Point from a file
        /// </summary>
        /// <param name="filePath">File format and extension must be json</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Upload")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDto> Upload([FromQuery]string filePath)
        {
            var response = new ResponseDto();

            try
            {
                FileInfo file = new FileInfo(filePath);
                if (file.Exists && file.Extension.EndsWith(".json"))
                {
                    List<CoordinateRequestDto> pointList = JsonConvert.DeserializeObject<List<CoordinateRequestDto>>(System.IO.File.ReadAllText(filePath));
                    setUser();
                    if(await _pointManager.AddPoints(pointList, _userId))
                    {
                        response.Data = GlobalConstant.CoordinateUploadedMessage;
                        response.MetaData = new ResponseMetaData
                        {
                            Code = GlobalConstant.SuccessCode
                        };
                    }
                }
                else
                {
                    response.Data = string.Empty;
                    response.MetaData = new ResponseMetaData
                    {
                        Code = GlobalConstant.ErrorCode,
                        Message = GlobalConstant.FileExtensionErrorMessage
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
