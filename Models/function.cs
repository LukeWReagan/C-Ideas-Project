using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace bank.Models
{
    public class Function
    {
        [Key]
        public int Functionid {get; set;}
        public string Posts {get; set;}
        // public int Likes {get; set;}

        public int Userid {get; set;}

        public User user {get; set;} 
        public List<Key> post { get; set; }
        public Function() {
            post = new List<Key>();
        }
    }
}