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

        private Mock<INavigationService> _navigationService;
        private Mock<IUserService> _userService;
        private Mock<IAppConfigurationService> _appConfigurationService;
        private Mock<IPageDialogService> _pageDialogService;
        private Mock<IAnalyticService> _analyticService;
        private Mock<ICrashReposrtService> _crashReposrtService;

        public SignUpViewModelTests()
        {
        }

        [SetUp]
        public void LoadServices()
        {
            _navigationService = new Mock<INavigationService>();
            _userService = new Mock<IUserService>();
            _appConfigurationService = new Mock<IAppConfigurationService>();
            _pageDialogService = new Mock<IPageDialogService>();
            _analyticService = new Mock<IAnalyticService>();
            _crashReposrtService = new Mock<ICrashReposrtService>();
        }

        [Test]
        public void OnSignUp_App_Config_Data_Is_Created()
        {
            // 1 - Arrange

            var signUpViewModel = new SignUpViewModel(
                _navigationService.Object,
                _userService.Object,
                _appConfigurationService.Object,
                _pageDialogService.Object,
                _analyticService.Object,
                _crashReposrtService.Object
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
            _appConfigurationService.Setup(Ia => Ia.Create(It.IsAny<AppConfiguration>())).Throws(new Exception());


            var signUpViewModel = new SignUpViewModel(
                _navigationService.Object,
                _userService.Object,
                _appConfigurationService.Object,
                _pageDialogService.Object,
                _analyticService.Object,
                _crashReposrtService.Object
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
            var signUpViewModel = new SignUpViewModel(
                _navigationService.Object,
                _userService.Object,
                _appConfigurationService.Object,
                _pageDialogService.Object,
                _analyticService.Object,
                _crashReposrtService.Object
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
            _userService.Setup(Iu => Iu.Save(It.IsAny<User>())).Throws(new Exception());

            var signUpViewModel = new SignUpViewModel(
                _navigationService.Object,
                _userService.Object,
                _appConfigurationService.Object,
                _pageDialogService.Object,
                _analyticService.Object,
                _crashReposrtService.Object
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
            var exception = FormatterServices.GetUninitializedObject(typeof(SQLiteException))
                as SQLiteException;

            _userService.Setup(Iu => Iu.Save(It.IsAny<User>())).Throws(exception);

            var signUpViewModel = new SignUpViewModel(
                _navigationService.Object,
                _userService.Object,
                _appConfigurationService.Object,
                _pageDialogService.Object,
                _analyticService.Object,
                _crashReposrtService.Object
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
            _navigationService = null;
            _userService = null;
            _appConfigurationService = null;
            _pageDialogService = null;
            _analyticService = null;
            _crashReposrtService = null;
        }
    }
}
