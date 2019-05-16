    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

public partial class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public string UnitBrief { get; set; }
        public double UnitPrice { get; set; }
        [Required]
        public string Image { get; set; }
        public System.DateTime ProductDate { get; set; }
        public bool Available { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string SupplierId { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public bool Special { get; set; }
        public bool Latest { get; set; }
        public int Views { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
