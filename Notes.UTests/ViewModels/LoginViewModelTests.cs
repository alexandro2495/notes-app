using System;
using Ninject;
using Notes.ViewModels;
using NUnit.Framework;

namespace Notes.UTests.ViewModels
{
    public class LoginViewModelTests : BaseTests
    {
        public LoginViewModelTests()
        {
        }

        [Test]
        public void OnLoginCommand_Have_To_Be_Successful()
        {
            // 1 - Arrange
            string username = "aagr2495_";
            string password = "Aa123456";
            var loginViewModel = Kernel.Get<LoginViewModel>();
            loginViewModel.UserName = username;
            loginViewModel.Password = password;

            // 2 - Act
            loginViewModel.OnLoginCommand();

            // 3 - Assert
            Assert.IsNotNull(loginViewModel.loginWasSuccess);
            Assert.IsTrue(loginViewModel.loginWasSuccess);
        }

        [Test]
        public void OnLoginCommand_Should_Failed_With_Wrong_User_Credentials()
        {
            // 1 - Arrange
            string username = "aagr2495asdasd";
            string password = "123456asdasda";
            var loginViewModel = Kernel.Get<LoginViewModel>();
            loginViewModel.UserName = username;
            loginViewModel.Password = password;

            // 2 - Act
            loginViewModel.OnLoginCommand();

            // 3 - Assert
            Assert.IsNotNull(loginViewModel.loginWasSuccess);
            Assert.IsFalse(loginViewModel.loginWasSuccess);
        }
    }
}
