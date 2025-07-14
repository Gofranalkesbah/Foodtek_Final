using System;
using System.Collections.Generic;

namespace Foodtek_Application_API.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? NotificationType { get; set; }

    public string? Content { get; set; }

    public bool IsRead { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? UsersId { get; set; }

    public int? DiscountOffersId { get; set; }

    public virtual DiscountOffer? DiscountOffers { get; set; }

    public virtual User? Users { get; set; }
}
