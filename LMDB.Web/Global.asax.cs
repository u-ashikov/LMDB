using System.Linq;
using LMDB.ViewModels.Movie;

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

                cfg.CreateMap<Movie, MovieIndexViewModel>()
                .ForMember(dest => dest.DirectorName,
                        mo => mo.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName))
                .ForMember(dest => dest.Actors,
                        mo => mo.MapFrom(src => src.Actors.Select(a => a.FirstName + " " + a.LastName).ToList()))
                .ForMember(dest => dest.Genres,
                        mo => mo.MapFrom(src => src.Genres.Select(g => g.Name).ToList()))
                .ForMember(dest => dest.Likes, mo => mo.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.Dislikes, mo => mo.MapFrom(src => src.Dislikes.Count));
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
