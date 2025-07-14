using System;
using System.Collections.Generic;

namespace Foodtek_Application_API.Models;

public partial class Role
{
    public int Id { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
