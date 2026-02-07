using System;
using System.Collections.Generic;

namespace SEP_Restaurant_management.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? Fullname { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public string? Profiling { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Role { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<ImportBill> ImportBills { get; set; } = new List<ImportBill>();

    public virtual ICollection<OrderStaff> OrderStaffs { get; set; } = new List<OrderStaff>();
}
