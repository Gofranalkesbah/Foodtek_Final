using System;
using System.Collections.Generic;

namespace Foodtek_Application_API.Models;

public partial class Address
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? BuildingName { get; set; }

    public string? BuildingNumber { get; set; }

    public string? Floor { get; set; }

    public string? ApartmentNumber { get; set; }

    public string? AdditionalDetails { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? Province { get; set; }

    public string? Region { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? UsersId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? Users { get; set; }
}
