using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace bank.Models {
    
    public class Key {
        public int KeyId { get; set; }
        public int User_id { get; set; }
        public User user { get; set; }
        public int Functionid { get; set; }
        public Function function { get; set; }
    }
}