using Squares.API.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squares.API.Domain.Manager
{
    public interface IPointManager
    {
        Task<bool> AddPoints(List<CoordinateRequestDto> points, int userId);

        Task<bool> Delete(CoordinateRequestDto point, int userId);

        int CalculateSquare(int userId);
    }
}
