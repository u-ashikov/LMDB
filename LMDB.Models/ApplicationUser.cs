namespace LMDB.Models
{
    using Enums;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.LikedMovies = new HashSet<Movie>();
            this.DislikedMovies = new HashSet<Movie>();
            this.FavouriteMovies = new HashSet<Movie>();
            //this.Comments = new HashSet<Comment>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int? OriginCountryId { get; set; }

        public byte[] ProfilePicture { get; set; }

        public virtual Country OriginCountry { get; set; }

        public virtual ICollection<Movie> LikedMovies { get; set; }

        public virtual ICollection<Movie> DislikedMovies { get; set; }

        public virtual ICollection<Movie> FavouriteMovies { get; set; }

        //public virtual ICollection<Comment> Comments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
