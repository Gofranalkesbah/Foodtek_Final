using System;
using System.Collections.Generic;

namespace Foodtek_Application_API.Models;

public partial class Payment
{
    public int Id { get; set; }

    public decimal? Amount { get; set; }

    public string PaymentStatues { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? UsersId { get; set; }

    public int? OrderId { get; set; }

    public int? PaymentMethodsId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual PaymentMethod? PaymentMethods { get; set; }

    public virtual User? Users { get; set; }
}
