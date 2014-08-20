using GoBlog.Controllers;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace GoBlog.UnitTest
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index_RendersCorrectView()
        {
            HomeController controller = new HomeController();
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }
    }
}