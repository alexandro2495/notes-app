using System;
using Notes.iOS.Implementations;
using Notes.Services;
using Prism;
using Prism.Ioc;

namespace Notes.iOS
{
    public class IOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILoadingService, LoadingServiceIOS>();
        }
    }
}
