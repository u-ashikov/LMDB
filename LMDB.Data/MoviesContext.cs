﻿namespace LMDB.Data
{
    using LMDB.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class MoviesContext : IdentityDbContext<ApplicationUser>
    {
        public MoviesContext()
            : base("MoviesContext", throwIfV1Schema: false)
        {
            //Test commit
        }

        public static MoviesContext Create()
        {
            return new MoviesContext();
        }
    }
}
