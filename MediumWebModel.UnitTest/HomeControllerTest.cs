using TestStack.FluentMVCTesting;
using Xunit;

namespace Medium.WebModel.UnitTest
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexReturnsCorrectModel()
        {
            var sut = new HomeController();
            sut.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<HomeModel>(model => model.WelcomeMessage == "Welcome to Medium");
        } 
    }
}