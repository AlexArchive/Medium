using GoBlog.Areas.Admin.Controllers;
using GoBlog.Areas.Admin.Models;
using NUnit.Framework;
using SpecsFor.Mvc;
using SpecsFor.Mvc.Helpers;

namespace GoBlog.IntegrationTest
{
    [TestFixture]
    public class LoginControllerTest
    {
        [Test]
        public void Login_AuthenticationSuccess_RedirectsToAdmin()
        {
            var application = new MvcWebApp();

            application.NavigateTo<LoginController>(c => c.Index());
            application.FindFormFor<CredentialsModel>()
                .Field(m => m.Username).SetValueTo("byteblast")
                .Field(m => m.Password).SetValueTo("password")
                .Submit();
            
            application.Route.ShouldMapTo<AdminController>(c => c.Index());
        }
    }
}