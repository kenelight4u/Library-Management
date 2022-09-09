using LM.Domain.Common;
using LM.Domain.Entities;
using LM.DTOs.Request.AuthDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LM.Test.ControllerTests.AuthControllerTest
{
    public class SignUpTests
    {
        private readonly AuthControllerFactory _fac;

        public SignUpTests()
        {
            this._fac = new AuthControllerFactory();
        }

        [Fact]
        public async Task SignUp_ModelUserExistCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var users = TestData.GetLMUser().FirstOrDefault();
           
            var bookDto = new SignUpDTO { Email = "dev@sbsc.com" };

            var resultModel = new ResultModel<bool>("Email belongs to an existing customer");
            _fac.UserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(users));

            // Act
            var result = await _fac.AuthController.SignUp(bookDto) as OkObjectResult;
            
            // Assert
            _fac.UserManager.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            Assert.Null(result); 
        }

        [Fact]
        public async Task SignUp_ModelValidCase_CreatAsyncFailed()
        {
            // Arrange
            var userId = TestData.UserId;

            var user = new SignUpDTO
            {
                Email = "ken@sbsc.com",
                Password = "Dev__5678"
            };

            _fac.UserManager.Setup(x => x.CreateAsync(It.IsAny<LMUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            // Act
            var result = await _fac.AuthController.SignUp(user) as OkObjectResult;

            // Assert
            _fac.UserManager.Verify(x => x.CreateAsync(It.IsAny<LMUser>(), It.IsAny<string>()), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public async Task SignUp_ModelValidCase_CreatAsyncPassed_RoleExistTrue()
        {
            // Arrange
            var userId = TestData.UserId;

            var user = new SignUpDTO
            {
                Email = "ken@sbsc.com",
                Password = "Dev__5678"
            };

            _fac.UserManager.Setup(x => x.CreateAsync(It.IsAny<LMUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _fac.RoleManager.Setup(x => x.RoleExistsAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            _fac.UserManager.Setup(x => x.AddToRoleAsync(It.IsAny<LMUser>(), It.IsAny<string>()))
              .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _fac.AuthController.SignUp(user) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task SignUp_ModelValidCase_CreatAsyncPassed_RoleExistFalse()
        {
            // Arrange
            var userId = TestData.UserId;

            var user = new SignUpDTO
            {
                Email = "ken@sbsc.com",
                Password = "Dev__5678"
            };

            _fac.UserManager.Setup(x => x.CreateAsync(It.IsAny<LMUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _fac.RoleManager.Setup(x => x.RoleExistsAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(false));
            _fac.RoleManager.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);
            _fac.UserManager.Setup(x => x.AddToRoleAsync(It.IsAny<LMUser>(), It.IsAny<string>()))
               .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _fac.AuthController.SignUp(user) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}

