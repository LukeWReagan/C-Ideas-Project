using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace bank.Models
{
    public class User
    {
        [Key]
        public int User_id { get; set; }
        
        [Display(Name="Name:")]
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        
        [Display(Name="Alias:")]
        [Required]
        [MinLength(2)]
        public string Alias { get; set; }
        
        [Display(Name="Email Address:")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Display(Name="Password:")]
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public List<Function> AllPosts{get; set;}
        
        
        public List<Key> Like { get; set; }
        
        public User()
        {
            Like = new List<Key>();
            AllPosts = new List<Function>();
        }
        
    }
}