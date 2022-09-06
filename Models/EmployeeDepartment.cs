using System;
using System.Collections.Generic;

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
        public virtual Employee DepartmentNavigation { get; set; }

  /*      public List<Department> Departments { get; set; }
        public List<Employee> Employiees { get; set; }*/

        
    }
}
