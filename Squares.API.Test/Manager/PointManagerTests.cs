using AutoMapper;
using Moq;
using Shouldly;
using Squares.API.DataLayer.Core.Repository;
using Squares.API.DataLayer.Entities;
using Squares.API.Domain.Dto;
using Squares.API.Domain.Manager;
using Squares.API.Domain.Mapping;
using Squares.API.Test.FixtureData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Squares.API.Test.Unit_Test_Cases
{
    public class PointManagerTests
    {
        private readonly IPointManager _pointManager;
        private readonly IMapper _mapper;
        private readonly Mock<IEfRepository<Coordinate>> _efRepositoryCoordinate;

        public PointManagerTests()
        {
            _mapper = new MapperConfiguration(c =>
                              c.AddProfile<MapperProfile>()).CreateMapper();
            _efRepositoryCoordinate = new Mock<IEfRepository<Coordinate>>();

            _pointManager = new PointManager(_efRepositoryCoordinate.Object, _mapper);
        }

        [Fact]
        public void AddPointsTest_Success()
        {
            _efRepositoryCoordinate.Setup(s => s.AddRangeAsync(It.IsAny<List<Coordinate>>())).Verifiable();

            Should.NotThrow(async () =>
            await _pointManager.AddPoints(GlobalData.GetListCoordinateRequestDto(), 1));
        }

        [Fact]
        public void GetSquareTest_Success()
        {
            _efRepositoryCoordinate.Setup(s => s.FindAllAsync()).Returns(GlobalData.GetListCoordinates().AsQueryable());

            var result = _pointManager.CalculateSquare(1);

            Assert.Equal(2, result);
        }

        [Fact]
        public void DeleteTest_Success()
        {
            _efRepositoryCoordinate.Setup(s => s.FindSingleAsync(It.IsAny<Expression<Func<Coordinate, bool>>>())).Returns(Task.FromResult(GlobalData.GetListCoordinates().FirstOrDefault()));

            _efRepositoryCoordinate.Setup(s => s.Delete(It.IsAny<Coordinate>())).Verifiable();

            Should.NotThrow(async () =>
            await _pointManager.Delete(new CoordinateRequestDto { X = 1, Y = 1 }, 1));

        }


        [Fact]
        public void AddPointsThrowErrorTest_Success()
        {
            _efRepositoryCoordinate.Setup(s => s.AddRangeAsync(It.IsAny<List<Coordinate>>())).ThrowsAsync(new Exception());

            Should.ThrowAsync<Exception>(async () =>
            await _pointManager.AddPoints(GlobalData.GetListCoordinateRequestDto(), 1)
            );
        }

        [Fact]
        public void DeleteDataNotFoundTest_Success()
        {
            _efRepositoryCoordinate.Setup(s => s.FindSingleAsync(It.IsAny<Expression<Func<Coordinate, bool>>>())).ReturnsAsync(GlobalData.GetListCoordinates().First());

            Should.NotThrow(async () =>
            await _pointManager.Delete(new CoordinateRequestDto { X = 1, Y = 1 }, 1));

        }

        [Fact]
        public void DeleteThrowExceptioTest_Success()
        {
            _efRepositoryCoordinate.Setup(s => s.FindSingleAsync(It.IsAny<Expression<Func<Coordinate, bool>>>())).Returns(Task.FromResult(GlobalData.GetListCoordinates().FirstOrDefault()));

            _efRepositoryCoordinate.Setup(s => s.Delete(It.IsAny<Coordinate>())).ThrowsAsync(new Exception());

            Should.ThrowAsync<Exception>(async () =>
            await _pointManager.Delete(new CoordinateRequestDto { X = 1, Y = 1 }, 1)
            );

        }
    }
}
