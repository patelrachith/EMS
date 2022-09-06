using System;
using System.Collections.Generic;

#nullable disable

namespace EMS.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeDepartments = new HashSet<EmployeeDepartment>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? DOJ { get; set; }

        public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
    }
}
