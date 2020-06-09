﻿namespace WoWForDummies.Dtos.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}