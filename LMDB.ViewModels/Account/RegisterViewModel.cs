namespace LMDB.ViewModels.Account
{
    using Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;
    using Compare = System.ComponentModel.DataAnnotations.CompareAttribute;

    public class RegisterViewModel
    {
        [Required]
        [StringLength(50,MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z0-9\-\._]+$",ErrorMessage = "Username must contain only letters, digits, dash, underscore or dot!")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "First name must contain only letters with maximum length 50!")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "Last name must contain only letters with maximum length 50!")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Display(Name = "Country")]
        public int? OriginCountryId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Profile picture")]
        public HttpPostedFileBase ProfilePicture { get; set; }
    }
}