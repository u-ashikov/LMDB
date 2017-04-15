namespace LMDB.Web
{
    using AutoMapper;
    using LMDB.Models;
    using LMDB.ViewModels.Account;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterMappings();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, mo => mo.MapFrom(src => src.Username))
                .ForMember(dest => dest.ProfilePicture, mo => mo.MapFrom(src => GetBytesFromFile(src.ProfilePicture)));
            });

        }

        private static byte[] GetBytesFromFile(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return null;
            }

            MemoryStream stream = new MemoryStream();
            file.InputStream.CopyTo(stream);
            byte[] data = stream.ToArray();
            return data;
        }
    }
}
