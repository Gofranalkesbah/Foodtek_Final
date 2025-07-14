using System;
using System.Collections.Generic;

namespace Foodtek_Application_API.Models;

public partial class ItemOption
{
    public int Id { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public bool IsRequired { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? UsersId { get; set; }

    public int? ItemId { get; set; }

    public virtual Item? Item { get; set; }

    public virtual User? Users { get; set; }
}
