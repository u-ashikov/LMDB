namespace LMDB.ViewModels.Director
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    public class DirectorCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "First name must contain only letters with maximum length 50!")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z\.\-]{1,50}$", ErrorMessage = "Last name must contain only letters with maximum length 50!")]
        public string LastName { get; set; }

        public string Biography { get; set; }

        public DateTime? Birthdate { get; set; }

        public string Country { get; set; }

        [Display(Name = "Country")]
        public IEnumerable<SelectListItem> Countries { get; set; }

        public HttpPostedFileBase Picture { get; set; }
    }
}
