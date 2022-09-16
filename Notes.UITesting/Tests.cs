using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Notes.UITesting
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void ShouldBeAbleToLogin()
        {
            //app.Repl();
            //Arrange
            app.Tap("signupbutton");
            app.WaitForElement(x => x.Marked("nameentry"));
            app.Tap("nameentry");
            app.EnterText("Carlos");
            app.DismissKeyboard();
            app.Tap("lastnentry");
            app.EnterText("Lopez");
            app.DismissKeyboard();
            app.Tap("userprofile");
            app.EnterText("Carlos_1");
            app.DismissKeyboard();
            app.Tap("passwordprofile");
            app.EnterText("Cagl1581");
            app.DismissKeyboard();
            app.Tap("emailentry");
            app.EnterText("carlos@");
            app.WaitForElement(c => c.Marked("createbutton"), postTimeout:TimeSpan.FromSeconds(3));
            app.EnterText("algo.com");
            app.DismissKeyboard();
            app.Tap("createbutton");

            app.Tap("userentry");
            app.EnterText("Carlos_1");
            app.DismissKeyboard();
            app.Tap("passwordentry");
            app.EnterText("Cagl1581");
            app.DismissKeyboard();

            //Act
            app.Tap("signinbutton");
            app.WaitForElement("newbutton");

            //Assert
            bool result = app.Query(e => e.Marked("newbutton")).Any();
            Assert.IsTrue(result);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }
    }
}
