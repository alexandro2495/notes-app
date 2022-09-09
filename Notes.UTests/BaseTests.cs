using System.Reflection;
using Ninject;
using NUnit.Framework;

namespace Notes.UTests
{
    public class BaseTests
    {
        protected IKernel Kernel { get; private set; }

        [SetUp]
        public void Setup()
        {
            //prepare mock and interfaces to be used
            //for scenarios later
            //Kernel is from Niject is our guy
            //to specify the dependencies and their
            //concrete classes by using modules or manual
            //references
            Kernel = new StandardKernel();
            Kernel.Load(Assembly.GetExecutingAssembly());
        }
    }
}
