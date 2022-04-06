using AutoMapper;
using Squares.API.DataLayer.Entities;
using Squares.API.DataLayer.EntityFrameworkCore;
using Squares.API.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squares.API.Domain.Manager
{
    public class PointManager : IPointManager
    {
        private SquareDbContext _context;
        private IMapper _mapper;

        public PointManager(SquareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> AddPoints(List<CoordinateRequestDto> coordinatesDto, int userId)
        {
            try
            {
                List<Coordinate> listCoordinates = _mapper.Map<List<Coordinate>>(coordinatesDto);

                 Parallel.ForEach(listCoordinates, new ParallelOptions { MaxDegreeOfParallelism = 8 }, async (coordinate) => {
                     coordinate.UserId = userId;

                    await _context.Points.AddRangeAsync(coordinate);

                    await _context.SaveChangesAsync();

                });

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(CoordinateRequestDto coordinateDto, int userId)
        {
            try
            {
                var data = _context.Points.Where(x => x.X == coordinateDto.X && x.Y == coordinateDto.Y && x.UserId == userId).First();

                if (data != null)
                {
                    _context.Points.Remove(data);

                    await _context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int CalculateSquare(int userId)
        {
            int count = 0;
            var points = _context.Points.Where(x => x.UserId == userId).ToList();

            if (points.Count < 4)
            {
                return count;
            }
            for (int i = 3; i < points.Count; i++)
            {
                for (int k = 2; k < points.Count; k++)
                {
                    for (int j = 1; j < points.Count; j++)
                    {
                        for (int l = 0; l < points.Count; l++)
                        {
                            if (isSquare(points[l],points[j],points[k],points[i]))
                            {
                                count = count + 1;
                            }
                        }
                    }
                }
            }

            return count;
        }



        // A utility function to find square of distance
        // from point 'p' to point 'q'
        private static int distSq(Coordinate p, Coordinate q)
        {
            return (p.X - q.X) * (p.X - q.X) + (p.Y - q.Y) * (p.Y - q.Y);
        }

        // This function returns true if (p1, p2, p3, p4) form a
        // square, otherwise false
        private static bool isSquare(Coordinate p1, Coordinate p2, Coordinate p3, Coordinate p4)
        {
            int d2 = distSq(p1, p2); // from p1 to p2
            int d3 = distSq(p1, p3); // from p1 to p3
            int d4 = distSq(p1, p4); // from p1 to p4

            if (d2 == 0 || d3 == 0 || d4 == 0)
                return false;

            // If lengths if (p1, p2) and (p1, p3) are same, then
            // following conditions must met to form a square.
            // 1) Square of length of (p1, p4) is same as twice
            // the square of (p1, p2)
            // 2) Square of length of (p2, p3) is same
            // as twice the square of (p2, p4)
            if (d2 == d3 && 2 * d2 == d4
                && 2 * distSq(p2, p4) == distSq(p2, p3))
            {
                return true;
            }

            // The below two cases are similar to above case
            if (d3 == d4 && 2 * d3 == d2
                && 2 * distSq(p3, p2) == distSq(p3, p4))
            {
                return true;
            }
            if (d2 == d4 && 2 * d2 == d3
                && 2 * distSq(p2, p3) == distSq(p2, p4))
            {
                return true;
            }
            return false;
        }
    }
}
