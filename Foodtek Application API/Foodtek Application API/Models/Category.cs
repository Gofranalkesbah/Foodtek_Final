using System;
using System.Collections.Generic;

namespace Foodtek_Application_API.Models;

public partial class Category
{
    public int Id { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? UsersId { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual User? Users { get; set; }

    public virtual ICollection<DiscountOffer> DiscountOffers { get; set; } = new List<DiscountOffer>();

    public virtual ICollection<User> UsersNavigation { get; set; } = new List<User>();
}
