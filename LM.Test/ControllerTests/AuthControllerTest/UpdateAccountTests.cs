﻿using LM.Domain.Common;
using LM.Domain.Entities;
using LM.DTOs.Request.AuthDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LM.Test.ControllerTests.AuthControllerTest
{
    public class UpdateAccountTests
    {
        private readonly AuthControllerFactory _fac;

        public UpdateAccountTests()
        {
            _fac = new AuthControllerFactory();
        }

        [Fact]
        public async Task UpdateAccount_ModelnullCase()
        {
            // Arrange
            var userId = TestData.UserId;

            LMUser user = null;

            var editDTO = new EditDTO {  };

            var resultModel = new ResultModel<bool>("User doesn't Exist");
            _fac.UserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            _fac.ContextAccessor.Setup(x => x.GetCurrentUserId()).Returns(userId);

            // Act
            var result = await _fac.AuthController.UpdateAccount(editDTO) as OkObjectResult;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAccount_ModelValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            LMUser user = TestData.GetLMUser().FirstOrDefault();

            var editDTO = new EditDTO { FirstName = "change" };

            var resultModel = new ResultModel<bool>("User doesn't Exist");
            _fac.UserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            _fac.ContextAccessor.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _fac.UserManager.Setup(x => x.UpdateAsync(It.IsAny<LMUser>()))
                .ReturnsAsync(IdentityResult.Failed());

            // Act
            var result = await _fac.AuthController.UpdateAccount(editDTO) as OkObjectResult;

            // Assert
            Assert.Null(result);
        }
    }
}
