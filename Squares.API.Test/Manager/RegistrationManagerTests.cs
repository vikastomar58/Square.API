using AutoMapper;
using Moq;
using Shouldly;
using Squares.API.DataLayer.Core.Repository;
using Squares.API.DataLayer.Entities;
using Squares.API.Domain.Manager;
using Squares.API.Domain.Mapping;
using Squares.API.Test.FixtureData;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Squares.API.Test.Unit_Test_Cases
{
    public class RegistrationManagerTests
    {
        private readonly IIdentityManager _registrationManager;
        private readonly IMapper _mapper;
        private readonly Mock<IEfRepository<UserDetail>> _efRepositoryUserDetail;

        public RegistrationManagerTests()
        {
            _mapper = new MapperConfiguration(c =>
                              c.AddProfile<MapperProfile>()).CreateMapper();
            _efRepositoryUserDetail = new Mock<IEfRepository<UserDetail>>();

            _registrationManager = new IdentityManager(_efRepositoryUserDetail.Object,_mapper);

        }

        [Fact]
        public void SignUpTest_Success()
        {
            _efRepositoryUserDetail.Setup(s => s.FindSingleAsync(It.IsAny<Expression<Func<UserDetail, bool>>>())).Verifiable();

            _efRepositoryUserDetail.Setup(s => s.AddAsync(It.IsAny<UserDetail>())).Verifiable();

            var result = _registrationManager.SignUp(GlobalData.GetSignUpRequestDto()).Result;

            Assert.True(result);
        }

        [Fact]
        public void LogInTest_Success()
        {
            _efRepositoryUserDetail.Setup(s => s.FindSingleAsync(It.IsAny<Expression<Func<UserDetail, bool>>>())).ReturnsAsync(GlobalData.GetUserDetail());

            var result = _registrationManager.LogIn(GlobalData.GetLoginRequestDto());

            Assert.NotNull(result);
        }

        [Fact]
        public void SignUpWithUserExistTest_Success()
        {
            _efRepositoryUserDetail.Setup(s => s.FindSingleAsync(It.IsAny<Expression<Func<UserDetail, bool>>>())).ReturnsAsync(GlobalData.GetUserDetail());

            var result = _registrationManager.SignUp(GlobalData.GetSignUpRequestDto()).Result;

            Assert.False(result);
        }

        [Fact]
        public void LogInUserNotExistsTest_Success()
        {
            _efRepositoryUserDetail.Setup(s => s.FindSingleAsync(It.IsAny<Expression<Func<UserDetail, bool>>>())).Verifiable();

            var result = _registrationManager.LogIn(GlobalData.GetLoginRequestDto());

            Assert.Null(result);
        }

        [Fact]
        public void SignUpWithExceptionTest_Success()
        {
            _efRepositoryUserDetail.Setup(s => s.FindSingleAsync(It.IsAny<Expression<Func<UserDetail, bool>>>())).Verifiable();

            _efRepositoryUserDetail.Setup(s => s.AddAsync(It.IsAny<UserDetail>())).ThrowsAsync(new Exception());

            Should.ThrowAsync<Exception>(async () =>
             await _registrationManager.SignUp(GlobalData.GetSignUpRequestDto())
             );

        }
    }
}
