using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EMS.Models
{
    public partial class EmployeeDepartment
    {
        public int MapId { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime? DateOfAssignment { get; set; }

        public virtual Department Department { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
