using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace bank.Models
{
    public class loginVal
    {
        [Key]
        public int Userid { get; set; }

        [Display(Name="Email Address:")]
        [Required]
        
        public string emailLogin { get; set; }
        
        [Display(Name="Password:")]
        [Required]
        
        
        public string passwordLogin { get; set; }
    }
}