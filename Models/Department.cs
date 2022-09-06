using System;
using System.Collections.Generic;

#nullable disable

namespace EMS.Models
{
    public partial class Department
    {
        public Department()
        {
            EmployeeDepartments = new HashSet<EmployeeDepartment>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }

        public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
    }
}
