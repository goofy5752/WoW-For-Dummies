namespace WoWForDummies.Data.Models
{
    using System;
    using Common.Contracts;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}