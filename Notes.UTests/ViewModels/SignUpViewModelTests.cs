using System;
using System.Runtime.Serialization;
using Moq;
using Ninject;
using Notes.Data.Models;
using Notes.Services;
using Notes.ViewModels;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
using SQLite;

namespace Notes.UTests.ViewModels
{
    public class SignUpViewModelTests : BaseTests
    {


        public SignUpViewModelTests()
        {
        }

        [Test]
        public void OnSignUp_App_Config_Data_Is_Created()
        {
            // 1 - Arrange
            var navigationService = new Mock<INavigationService>();
            var userService = new Mock<IUserService>();
            var appConfigurationService = new Mock<IAppConfigurationService>();
            var pageDialogService = new Mock<IPageDialogService>();
            var analyticService = new Mock<IAnalyticService>();
            var crashReposrtService = new Mock<ICrashReposrtService>();

            var signUpViewModel = new SignUpViewModel(
                navigationService.Object,
                userService.Object,
                appConfigurationService.Object,
                pageDialogService.Object,
                analyticService.Object,
                crashReposrtService.Object
            );

            // 2 - Act
            var config = signUpViewModel.ValidateCreateAppConfiguration();

            // 3 - Assert
            Assert.IsNotNull(config);
            Assert.IsInstanceOf(typeof(AppConfiguration), config);
        }

        [Test]
        public void OnSignUp_App_Config_Data_Is_Not_Created()
        {
            // 1 - Arrange
            var navigationService = new Mock<INavigationService>();
            var userService = new Mock<IUserService>();
            var appConfigurationService = new Mock<IAppConfigurationService>();
            var pageDialogService = new Mock<IPageDialogService>();
            var analyticService = new Mock<IAnalyticService>();
            var crashReposrtService = new Mock<ICrashReposrtService>();

            appConfigurationService.Setup(Ia => Ia.Create(It.IsAny<AppConfiguration>())).Throws(new Exception());


            var signUpViewModel = new SignUpViewModel(
                navigationService.Object,
                userService.Object,
                appConfigurationService.Object,
                pageDialogService.Object,
                analyticService.Object,
                crashReposrtService.Object
            );

            // 2 - Act
            var config = signUpViewModel.ValidateCreateAppConfiguration();

            // 3 - Assert
            Assert.IsNull(config);
        }

        [Test]
        public void OnSignUp_User_Data_Is_Created()
        {
            // 1 - Arrange
            var navigationService = new Mock<INavigationService>();
            var userService = new Mock<IUserService>();
            var appConfigurationService = new Mock<IAppConfigurationService>();
            var pageDialogService = new Mock<IPageDialogService>();
            var analyticService = new Mock<IAnalyticService>();
            var crashReposrtService = new Mock<ICrashReposrtService>();

            var signUpViewModel = new SignUpViewModel(
                navigationService.Object,
                userService.Object,
                appConfigurationService.Object,
                pageDialogService.Object,
                analyticService.Object,
                crashReposrtService.Object
            );

            signUpViewModel.Name = "angel";
            signUpViewModel.LastName = "garcia";
            signUpViewModel.UserName = "alexandro_1";
            signUpViewModel.Password = "Aa123456";
            signUpViewModel.Email = "alex@gmail.com";

            // 2 - Act
            var user = signUpViewModel.ValidateCreateUser(It.IsAny<long>());

            // 3 - Assert
            Assert.IsNotNull(user);
            Assert.IsInstanceOf(typeof(User), user);
        }

        [Test]
        public void OnSignUp_User_Data_Is_Not_Created()
        {
            // 1 - Arrange
            var navigationService = new Mock<INavigationService>();
            var userService = new Mock<IUserService>();
            var appConfigurationService = new Mock<IAppConfigurationService>();
            var pageDialogService = new Mock<IPageDialogService>();
            var analyticService = new Mock<IAnalyticService>();
            var crashReposrtService = new Mock<ICrashReposrtService>();

            userService.Setup(Iu => Iu.Save(It.IsAny<User>())).Throws(new Exception());

            var signUpViewModel = new SignUpViewModel(
                navigationService.Object,
                userService.Object,
                appConfigurationService.Object,
                pageDialogService.Object,
                analyticService.Object,
                crashReposrtService.Object
            );

            signUpViewModel.Name = "angel";
            signUpViewModel.LastName = "garcia";
            signUpViewModel.UserName = "alexandro_1";
            signUpViewModel.Password = "Aa123456";
            signUpViewModel.Email = "alex@gmail.com";

            // 2 - Act
            var user = signUpViewModel.ValidateCreateUser(It.IsAny<long>());

            // 3 - Assert
            Assert.IsNull(user);
        }

        [Test]
        public void OnSignUp_User_Data_Is_Not_Created_UNIQUE_USERNAME()
        {
            // 1 - Arrange
            var navigationService = new Mock<INavigationService>();
            var userService = new Mock<IUserService>();
            var appConfigurationService = new Mock<IAppConfigurationService>();
            var pageDialogService = new Mock<IPageDialogService>();
            var analyticService = new Mock<IAnalyticService>();
            var crashReposrtService = new Mock<ICrashReposrtService>();

            var exception = FormatterServices.GetUninitializedObject(typeof(SQLiteException))
                as SQLiteException;

            userService.Setup(Iu => Iu.Save(It.IsAny<User>())).Throws(exception);

            var signUpViewModel = new SignUpViewModel(
                navigationService.Object,
                userService.Object,
                appConfigurationService.Object,
                pageDialogService.Object,
                analyticService.Object,
                crashReposrtService.Object
            );

            signUpViewModel.Name = "Angel";
            signUpViewModel.LastName = "Garcia";
            signUpViewModel.UserName = "aagr2495_";
            signUpViewModel.Password = "Aa123456";
            signUpViewModel.Email = "agarciar95@gmail.com";

            // 2 - Act
            var user = signUpViewModel.ValidateCreateUser(It.IsAny<long>());

            // 3 - Assert
            Assert.IsNull(user);
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
