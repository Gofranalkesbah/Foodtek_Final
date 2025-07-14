using System;
using System.Collections.Generic;

namespace Foodtek_Application_API.Models;

public partial class Order
{
    public int Id { get; set; }

    public decimal TotalPrice { get; set; }

    public int? Rate { get; set; }

    public string Status { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? UsersId { get; set; }

    public int? AddressId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual User? Users { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
