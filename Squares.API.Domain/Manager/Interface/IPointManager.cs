using Squares.API.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squares.API.Domain.Manager
{
    public interface IPointManager
    {
        Task AddPoints(List<CoordinateRequestDto> coordinatesDto, int userId);

        Task Delete(CoordinateRequestDto coordinateDto, int userId);

        int CalculateSquare(int userId);
    }
}
