using LM.API.Controllers.v1;
using LM.Application.Interfaces.Utilities;
using LM.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Test.ControllerTests.AuthControllerTest
{
    public class AuthControllerFactory
    {
        public Mock<IUserClaimsPrincipalFactory<LMUser>> UserPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<LMUser>>();
        public Mock<UserManager<LMUser>> UserManager = new Mock<UserManager<LMUser>>(new Mock<IUserStore<LMUser>>().Object,
          null, null, null, null, null, null, null, null);
        public SignInManager<LMUser> SignInManager;
        public Mock<IContextAccessor> ContextAccessor = new Mock<IContextAccessor>();
        public Mock<IConfiguration> Configuration = new Mock<IConfiguration>();

        public AuthControllerFactory()
        {
            SignInManager = new SignInManager<LMUser>(
                UserManager.Object,
                null,
                UserPrincipalFactory.Object,
                null,
                null,
                null,
                null);

            //AuthController = new AuthController(
            //    UserManager.Object,
            //    SignInManager,
            //    null);

            AuthController.ControllerContext.HttpContext =
                new DefaultHttpContext { User = TestData.GetAuthenticatedUser() };
        }

        public AuthController AuthController { get; set; }
    }
}
