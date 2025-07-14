using System;
using System.Collections.Generic;

namespace Foodtek_Application_API.Models;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string PaymentType { get; set; } = null!;

    public string? Name { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
