using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Linq;

namespace Eletrobid.Installers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
            container.Register(Classes.FromAssemblyNamed("Eletrobid.Dal").Where(type => type.Name.EndsWith("Dal")).WithServiceDefaultInterfaces().LifestylePerWebRequest());
            //container.Register(Classes.FromAssemblyNamed("MobileCursos.Web.Nfe").Where(type => type.Name.EndsWith("Nfe")).WithServiceDefaultInterfaces().LifestylePerWebRequest());
        }
    }
}