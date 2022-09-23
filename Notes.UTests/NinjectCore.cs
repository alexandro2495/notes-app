using System;
using System.Collections.Generic;
using Moq;
using Ninject.Modules;
using Notes.Data.Models;
using Notes.Services;
using Notes.Services.Implementations;
using Prism.Navigation;
using Prism.Services;

namespace Notes.UTests
{
    public class NinjectCore : NinjectModule
    {
        public NinjectCore()
        {
        }

        public override void Load()
        {
            var navigationService = new Mock<INavigationService>();
            var noteService = new Mock<INoteService>();
            var userService = new Mock<IUserService>();
            var authenticationService = new Mock<IAuthenticationService>();
            var appConfigurationService = new Mock<IAppConfigurationService>();
            var pageDialogService = new Mock<IPageDialogService>();

            

            authenticationService.Setup(Ia => Ia.SignIn(It.IsAny<string>(), It.IsAny<string>())).Returns(() => {
                return false;
            });

            authenticationService.Setup(Ia => Ia.SignIn("aagr2495_", "Aa123456")).Returns(() => {
                return true;
            });

            userService.Setup(Iu => Iu.GetLoggedUser()).Returns(() =>
            {
                var user = new User()
                {
                    Name = "Angel",
                    LastName = "Garcia",
                    UserName = "aagr2495_",
                    Password = "Aa123456",
                    Email = "agarciar95@gmail.com"
                };

                return user;
            });

            
            noteService.Setup(i => i.GetNotes(It.IsAny<long>())).Returns(() =>
            {
                return new List<Note>()
                {
                    new Note()
                    {
                        Title = "Hello world",
                        Content = "this is a hello world example",
                        Type = NoteType.Picture
                    }
                };
            });

            Bind<INavigationService>().ToConstant(navigationService.Object);
            Bind<INoteService>().ToConstant(noteService.Object);
            Bind<IUserService>().ToConstant(userService.Object);
            Bind<IAuthenticationService>().ToConstant(authenticationService.Object);
            Bind<IAppConfigurationService>().ToConstant(appConfigurationService.Object);
            Bind<IPageDialogService>().ToConstant(pageDialogService.Object);
        }
    }
}

