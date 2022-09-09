using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }
        [Required]
        [Display(Name = "Date of Join")]
        public DateTime? DOJ { get; set; }

        public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
    }
}
