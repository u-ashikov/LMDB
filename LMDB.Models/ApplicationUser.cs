namespace LMDB.Models
{
    using Enums;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.LikedMovies = new HashSet<Movie>();
            this.DislikedMovies = new HashSet<Movie>();
            this.FavouriteMovies = new HashSet<Movie>();
            this.Comments = new HashSet<Comment>();
            this.ReviewsWrited = new HashSet<Review>();
        }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "First name must contain only letters with maximum length 50!")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "Last name must contain only letters with maximum length 50!")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]{5,15}$", ErrorMessage = "The username must contain only letters, numbers and underscore!")]
        public string Username { get; set; }

        [Required]
        [StringLength(20,MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public int? OriginCountryId { get; set; }

        public byte[] ProfilePicture { get; set; }

        public virtual Country OriginCountry { get; set; }

        public virtual ICollection<Movie> LikedMovies { get; set; }

        public virtual ICollection<Movie> DislikedMovies { get; set; }

        public virtual ICollection<Movie> FavouriteMovies { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Review> ReviewsWrited { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
