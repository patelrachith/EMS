using EMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.ViewModels
{
    public class EmployeeDepartment
    {
        public int MapId { get; set; }
        public int EmployeeId { get; set; }
        public int[] DepartmentIds { get; set; }
        public List<Department> Departments { get; set; }
        public List<Employee> Employiees { get; set; }
    }
    
}
