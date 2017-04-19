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
    using System.Linq;
    using LMDB.ViewModels.Movie;
    using System;
    using ViewModels.Comment;

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
                .ForMember(dest => dest.UserName, mo => mo.MapFrom(src => src.Username));

                cfg.CreateMap<Movie, MovieIndexViewModel>()
                    .ForMember(dest => dest.DirectorName,
                        mo => mo.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName))
                    .ForMember(dest => dest.Actors,
                        mo => mo.MapFrom(src => src.Actors.Select(a => a.FirstName + " " + a.LastName).ToList()))
                    .ForMember(dest => dest.Genres,
                        mo => mo.MapFrom(src => src.Genres.Select(g => g.Name).ToList()))                   
                    .ForMember(dest => dest.Poster,
                        mo => mo.MapFrom(src => $"../../Posters/{src.Title.Replace(" ", string.Empty).Replace(":", string.Empty)}.jpg"))
                    .ForMember(dest => dest.Likes,
                        mo => mo.MapFrom(src => src.Likes.Count))
                    .ForMember(dest => dest.Dislikes,
                        mo => mo.MapFrom(src => src.Dislikes.Count));

                cfg.CreateMap<Movie, MovieDetailsViewModel>()
                .ForMember(dest => dest.Year, mo => mo.MapFrom(src => src.DateReleased.Year))
                .ForMember(d => d.Director, mo => mo.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName))
                .ForMember(d => d.Review, mo => mo.MapFrom(src => src.Review.Content))
                .ForMember(d => d.Actors, mo => mo.MapFrom(src => src.Actors.Select(a => $"{a.FirstName} {a.LastName}").ToList()))
                .ForMember(d => d.Genres, mo => mo.MapFrom(src => src.Genres.Select(g => g.Name).ToList()))
                .ForMember(d => d.Awards, mo => mo.MapFrom(src => src.Awards.Select(a => a.Category.Name).ToList()))
                .ForMember(d=>d.Likes, mo=>mo.MapFrom(src=>src.Likes.Select(l=>l.Id).ToList()))
                .ForMember(d => d.Dislikes, mo => mo.MapFrom(src => src.Dislikes.Select(l => l.Id).ToList()))
                .ForMember(d => d.Fans, mo => mo.MapFrom(src => src.MovieFans.Select(l => l.Id).ToList()))
                .ForMember(dest => dest.Poster,
                        mo => mo.MapFrom(src => $"../../Posters/{src.Title.Replace(" ", string.Empty).Replace(":", string.Empty)}.jpg"));

                cfg.CreateMap<Movie, MovieEditViewModel>()
                .ForMember(d => d.Director, mo => mo.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName))
                .ForMember(d => d.Genres, mo => mo.MapFrom(src => String.Join(",", src.Genres.Select(g => g.Name).ToList()).ToString()))
                .ForMember(d => d.Actors, mo => mo.MapFrom(src => String.Join(",", src.Actors.Select(a => $"{a.FirstName} {a.LastName}").ToList()).ToString()))
                .ForMember(d => d.Review, mo => mo.MapFrom(src => src.Review.Content));

                cfg.CreateMap<CommentCreateViewModel, Comment>();

                cfg.CreateMap<Comment, CommentEditViewModel>();
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
