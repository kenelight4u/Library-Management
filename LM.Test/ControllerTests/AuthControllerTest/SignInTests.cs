using LM.Domain.Common;
using LM.Domain.Entities;
using LM.DTOs.Request.AuthDTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LM.Test.ControllerTests.AuthControllerTest
{
    public class SignInTests
    {
        private readonly AuthControllerFactory _fac;

        public SignInTests()
        {
            _fac = new AuthControllerFactory();
        }

        [Fact]
        public async Task SignIn_ModelnullCase()
        {
            // Arrange
            var userId = TestData.UserId;

            LMUser user = null;

            var signInDto = new SignInDTO { Email = "dev@sbsc.com" };

            var resultModel = new ResultModel<bool>("User with this email doesn't exixts");
            _fac.UserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));

            // Act
            var result = await _fac.AuthController.SignIn(signInDto) as OkObjectResult;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SignIn_ModelValidCase_InvalidPassword()
        {
            // Arrange
            var userId = TestData.UserId;

            var user = TestData.GetLMUser().FirstOrDefault();

            var signInDto = new SignInDTO { Email = "dev@sbsc.com" };

            _fac.UserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            _fac.UserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<LMUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            // Act
            var result = await _fac.AuthController.SignIn(signInDto) as OkObjectResult;

            // Assert
            Assert.Null(result);
            //Assert.Equal(200, result.StatusCode);
        }

        //[Fact]
        //public async Task SignIn_ModelValidCase_ValidPassword()
        //{
        //    // Arrange
        //    var userId = TestData.UserId;

        //    var user = TestData.GetLMUser().FirstOrDefault();

        //    var signInDto = new SignInDTO { Email = "dev@sbsc.com" };

        //    _fac.UserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
        //        .Returns(Task.FromResult(user));
        //    _fac.UserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<LMUser>(), It.IsAny<string>()))
        //        .Returns(Task.FromResult(true));
            
            

        //    // Act
        //    var result = await _fac.AuthController.SignIn(signInDto) as OkObjectResult;

        //    // Assert
        //    Assert.Null(result);
        //    //Assert.Equal(200, result.StatusCode);
        //}
    }
}
