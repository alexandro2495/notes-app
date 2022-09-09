using System;
using Ninject;
using Notes.ViewModels;
using NUnit.Framework;

namespace Notes.UTests.ViewModels
{
    public class MainViewModelTests : BaseTests
    {
        public MainViewModelTests()
        {
        }

        [Test]
        public void GetNotes_Have_To_Return_Notes()
        {
            // 1 - Arrange
            var mainViewModel = Kernel.Get<MainViewModel>();

            // 2 - Act
            mainViewModel.GetNotesByUser();

            // 3 - Assert
            Assert.IsNotNull(mainViewModel.Notes);
            Assert.IsNotEmpty(mainViewModel.Notes);
            Assert.GreaterOrEqual(mainViewModel.Notes.Count, 1);
        }
    }
}

