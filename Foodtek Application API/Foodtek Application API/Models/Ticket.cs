using System;
using System.Collections.Generic;

namespace Foodtek_Application_API.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? CreationDate { get; set; }

    public string Type { get; set; } = null!;

    public string? Status { get; set; }

    public string? Response { get; set; }

    public string ActionType { get; set; } = null!;

    public decimal? RefundAmount { get; set; }

    public DateOnly? RefundExpirationDate { get; set; }

    public int? ResolvedByAdminId { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? Client { get; set; }

    public virtual User? ResolvedByAdmin { get; set; }
}
