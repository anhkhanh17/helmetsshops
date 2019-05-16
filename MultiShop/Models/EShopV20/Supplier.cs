    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Supplier
    {
    [StringLength(50)]
    public string Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    public string Logo { get; set; }

    [Required]
    [StringLength(50)]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Phone { get; set; }

    public virtual ICollection<Product> Products { get; set; }
    }