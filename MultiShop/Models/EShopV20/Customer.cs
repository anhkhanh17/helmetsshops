    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

public class Customer
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Activated { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
    }