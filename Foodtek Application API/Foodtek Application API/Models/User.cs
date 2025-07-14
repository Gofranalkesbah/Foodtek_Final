using System;
using System.Collections.Generic;

namespace Foodtek_Application_API.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime BirthOfDate { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? LastLoginTime { get; set; }

    public bool? IsLogedIn { get; set; }

    public string? ProfileImage { get; set; }

    public DateTime? JoinDate { get; set; }

    public string Status { get; set; } = null!;

    public bool? IsVerified { get; set; }

    public string? Otpcode { get; set; }

    public DateTime? Otpexpiry { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<DiscountOffer> DiscountOffersNavigation { get; set; } = new List<DiscountOffer>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<ItemOption> ItemOptions { get; set; } = new List<ItemOption>();

    public virtual ICollection<Item> ItemsNavigation { get; set; } = new List<Item>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Ticket> TicketClients { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketResolvedByAdmins { get; set; } = new List<Ticket>();

    public virtual ICollection<Category> CategoriesNavigation { get; set; } = new List<Category>();

    public virtual ICollection<DiscountOffer> DiscountOffers { get; set; } = new List<DiscountOffer>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
