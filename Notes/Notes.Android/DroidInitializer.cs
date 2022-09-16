using System;
using Notes.Droid.Implementations;
using Notes.Services;
using Prism;
using Prism.Ioc;

namespace Notes.Droid
{
    public class DroidInitializer : IPlatformInitializer
    {
        public DroidInitializer()
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILoadingService, LoadingServiceDroid>();
        }
    }
}
