using System;
using Ninject;
using Notes.ViewModels;
using NUnit.Framework;

namespace Notes.UTests.ViewModels
{
    public class SignUpViewModelTests : BaseTests
    {
        public SignUpViewModelTests()
        {
        }

        [Test]
        public void OnSignUp_Have_To_Register_New_User()
        {
            // 1 - Arrange
            var signUpViewModel = Kernel.Get<SignUpViewModel>();

            // 2 - Act
            signUpViewModel.OnSignUp();

            // 3 - Assert
            //Assert.IsNotNull(mainViewModel.Notes);
            //Assert.IsNotEmpty(mainViewModel.Notes);
            //Assert.GreaterOrEqual(mainViewModel.Notes.Count, 1);
        }

        public void OnSignUp_Have_To_Reject_New_User_With_Username_That_Already_Exist()
        {
            // 1 - Arrange
            var signUpViewModel = Kernel.Get<SignUpViewModel>();

            // 2 - Act
            signUpViewModel.OnSignUp();

            // 3 - Assert
            //Assert.IsNotNull(mainViewModel.Notes);
            //Assert.IsNotEmpty(mainViewModel.Notes);
            //Assert.GreaterOrEqual(mainViewModel.Notes.Count, 1);
        }
    }
}
