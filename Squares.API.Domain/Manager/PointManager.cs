using AutoMapper;
using Squares.API.DataLayer.Core.Repository;
using Squares.API.DataLayer.Entities;
using Squares.API.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squares.API.Domain.Manager
{
    public class PointManager : IPointManager
    {
        private IEfRepository<Coordinate> _coordinateRepositroy;
        private IMapper _mapper;

        public PointManager(IEfRepository<Coordinate> coordinateRepositroy, IMapper mapper)
        {
            _coordinateRepositroy = coordinateRepositroy;
            _mapper = mapper;
        }

        #region Public Methods
        /// <summary>
        /// This method is for adding points
        /// </summary>
        /// <param name="coordinatesDto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> AddPoints(List<CoordinateRequestDto> coordinatesDto, int userId)
        {
            try
            {
                List<Coordinate> listCoordinates = _mapper.Map<List<Coordinate>>(coordinatesDto);

                // Parallel.ForEach(listCoordinates, new ParallelOptions { MaxDegreeOfParallelism = 8 }, async (coordinate) => {
                //   coordinate.UserId = userId;

                listCoordinates.ForEach(x => x.UserId = userId);

                await _coordinateRepositroy.AddRangeAsync(listCoordinates);

                //  });

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is for deleting point
        /// </summary>
        /// <param name="coordinateDto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> Delete(CoordinateRequestDto coordinateDto, int userId)
        {
            try
            {
                var data = await _coordinateRepositroy.FindSingleAsync(x => x.X == coordinateDto.X && x.Y == coordinateDto.Y && x.UserId == userId);

                if (data != null)
                {
                   await _coordinateRepositroy.Delete(data);

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// This method is for calculating number of squares
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CalculateSquare(int userId)
        {
            int count = 0;
            var points = _coordinateRepositroy.FindAllAsync().Where(x => x.UserId == userId).ToArray();
            int value = 0;

            Dictionary<string, int> map = new Dictionary<string, int>();

            for (int i = 0; i < points.Length - 1; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    if (!check(points[i], points[j])) continue;
                    string str = genStr(points[i], points[j]);
                    if (!map.TryGetValue(str, out value))
                        map.Add(str, 1);
                    else
                        map.Add(str, 1 + map[str]);
                }
            }
            for (int i = 0; i < points.Length - 1; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    if (!check(points[i], points[j])) continue;
                    string diag = createDiag(points[i], points[j]);
                    if (diag.Length == 0) continue;
                    if (map.TryGetValue(diag, out value))
                        count += map[diag];
                }
            }
            return count;
        } 
        #endregion


        #region Private Methods

        /// <summary>
        /// Checking point are same
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private bool check(Coordinate p1, Coordinate p2)
        {
            if (p1.X == p2.X && p1.Y == p2.Y) return false;
            return true;
        }

        /// <summary>
        /// Generating sting from coordinates
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private string genStr(Coordinate p1, Coordinate p2)
        {
            if (p1.X < p2.X || (p1.X == p2.X && p1.Y < p2.Y))
            {
                return string.Format("{0},{1};{2},{3}", p1.X, p1.Y, p2.X, p2.Y);
            }
            else
            {
                return string.Format("{0},{1};{2},{3}", p2.X, p2.Y, p1.X, p1.Y);
            }

        }

        /// <summary>
        /// Generating diagonal from coordinates
        /// </summary>
        /// <param name="a"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private string createDiag(Coordinate a, Coordinate c)
        {
            int midX = a.X + c.X;
            int midY = a.Y + c.Y;

            int Ax = 2 * a.X - midX;
            int Ay = 2 * a.Y - midY;
            int bX = midX - Ay;
            int bY = midY + Ax;

            int cX = 2 * c.X - midX;
            int cY = 2 * c.Y - midY;
            int dX = midX - cY;
            int dY = midY + cX;
            if ((bX & 1) == 0 && (bY & 1) == 0 && (dX & 1) == 0 && (dY & 1) == 0)
            {
                if (bX < dX || (bX == dX && bY < dY))
                {
                    return string.Format("{0},{1};{2},{3}", bX / 2, bY / 2, dX / 2, dY / 2);
                }
                else
                {
                    return string.Format("{0},{1};{2},{3}", dX / 2, dY / 2, bX / 2, bY / 2);
                }
            }
            else
            {
                return "";
            }
        } 
        #endregion

    }
}
