using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace LM.Test
{
    public static class TestData
    {
        public static readonly Guid UserId = Guid.Parse("2a8fdbe3-14a4-0cda-8d36-39fa18266952");

        public static ClaimsPrincipal GetAuthenticatedUser()
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("oid", TestData.UserId.ToString()),
                new Claim("scp", "access_crm_as_user"),
            }, "TestAuthentication"));
        }

        //public static List<NibssUser> GetNibbsUser()
        //{
        //    return new List<NibssUser>
        //    {
        //        new NibssUser
        //        {
        //            Id = Guid.Parse("0b4ae023-7628-4c83-b300-ac8e0d4e26b8"),
        //            FirstName = "Ekene",
        //            LastName = "Anolue",
        //            User_Id = UserId,
        //            User = GetEbillsUsers().FirstOrDefault(),
        //            Address = "Oshodi Lagos",
        //            State = "Lagos State",
        //            IdentificationNumber = "2354678798",
        //            ProfilePictureId = Guid.Parse("2a8fdbe3-74a4-0cda-8d36-39fa18266952"),
        //            ProfilePicture = GetProfilePictures().FirstOrDefault(),
        //            Department = "Banking",
        //            RequestCorrelationCode = "346578",
        //            AssignedInstitutions =
        //            {

        //            }
        //        },
        //        new NibssUser
        //        {
        //            Id = Guid.Parse("66e704e4-3216-473b-83e6-c6a666c94d56"),
        //            FirstName = "Paul",
        //            LastName = "Oke",
        //            User_Id = Guid.Parse("2a8fdbe3-54a4-0cda-8d36-39fa18266952"),
        //            User = GetEbillsUsers().FirstOrDefault(),
        //            Address = "Apapa Lagos",
        //            State = "Lagos State",
        //            IdentificationNumber = "2359678798",
        //            Department = "Banking",
        //            RequestCorrelationCode = "349578",
        //            AssignedInstitutions =
        //            {

        //            }
        //        }
        //    };
        //}

        //public static List<EbillsRole> GetEbillsRole()
        //{
        //    return new List<EbillsRole>
        //    {
        //        new EbillsRole()
        //        {
        //            Id = Guid.Parse("84241c5a-3d32-4279-bf0f-10e6607eb1af"),
        //            UserType = Ebills.Shared.Enums.UserTypes.DEFAULT,
        //            CreatedOn = DateTime.Now,
        //            CreatedBy = Guid.NewGuid(),
        //            Name = "Default",
        //        },
        //        new EbillsRole()
        //        {
        //            Id = Guid.Parse("d4bcdc94-4454-4293-ab24-01495e034622"),
        //            UserType = Ebills.Shared.Enums.UserTypes.NIBBS_USER,
        //            CreatedOn = DateTime.Now,
        //            CreatedBy = Guid.NewGuid(),
        //            Name = "Nibbs_User"
        //        },
        //        new EbillsRole()
        //        {
        //            Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
        //            UserType = Ebills.Shared.Enums.UserTypes.INSTITUTION_USER,
        //            CreatedOn = DateTime.Now,
        //            CreatedBy = Guid.NewGuid(),
        //            Name = "Institution_User"
        //        },
        //        new EbillsRole()
        //        {
        //            UserType = Ebills.Shared.Enums.UserTypes.BILLER_USER,
        //            CreatedOn = DateTime.Now,
        //            CreatedBy = Guid.NewGuid(),
        //            Name = "Biller_User"
        //        }
        //    };

        //}

    }
}
