using AspNetCoreHero.ToastNotification.Abstractions;
using EMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EMSContext _EMSContext;
        private readonly INotyfService _notyfService;

        public EmployeeController(EMSContext EMSContext, INotyfService notyfService)
        {
            _EMSContext = EMSContext;
            _notyfService = notyfService;
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            var employees = _EMSContext.Employees.ToList();

            return View(employees);
        }
        public ActionResult SearchEmployee(string searchText)
        {
            var employees = _EMSContext.Employees.Where(e => e.FirstName.Contains(searchText)).ToList();

            return Json(employees);
            //return View(employees);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Upsert(int? employeeId)
        {
            ViewBag.PageName = employeeId == null ? "Add Employee" : "Edit Employee";
            ViewBag.IsEdit = employeeId == null ? false : true;
            if (employeeId == null)
            {
                return View();
            }
            else
            {
                var employee = _EMSContext.Employees.Find(employeeId);

                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(int employeeId, Employee employeeData)
        {
            bool IsEmployeeExist = false;

            Employee employee = _EMSContext.Employees.Find(employeeId);

            if (employee != null)
            {
                IsEmployeeExist = true;
            }
            else
            {
                employee = new Employee();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employee.FirstName = employeeData.FirstName;
                    employee.LastName = employeeData.LastName;
                    employee.Email = employeeData.Email;
                    employee.Address = employeeData.Address;
                    employee.Phone = employeeData.Phone;
                    employee.DOB = employeeData.DOB;
                    employee.DOJ = employeeData.DOJ;

                    if (IsEmployeeExist)
                    {
                        _EMSContext.Update(employee);
                    }
                    else
                    {
                        _EMSContext.Add(employee);
                    }
                    _EMSContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                _notyfService.Success("You have successfully saved the data.");
                return RedirectToAction(nameof(Index));
            }
            return View(employeeData);
        }

        // GET: Employees/Delete/1
        public async Task<IActionResult> Delete(int? employeeId)
        {
            if (employeeId == null)
            {
                return NotFound();
            }

            var employee = await _EMSContext.Employees.FirstOrDefaultAsync(m => m.EmployeeId == employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeDepartments = _EMSContext.EmployeeDepartments.Where(ed => ed.EmployeeId == employeeId).ToList();
            if (employeeDepartments != null)
            {
                _EMSContext.EmployeeDepartments.RemoveRange(employeeDepartments);
                _EMSContext.SaveChanges();
            }

            
            _EMSContext.Employees.Remove(employee);
            await _EMSContext.SaveChangesAsync();
            _notyfService.Success("You have successfully deleted the employee.");
            return RedirectToAction(nameof(Index));
        }

        // POST: Employees/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int employeeId)
        {
            var employee = await _EMSContext.Employees.FindAsync(employeeId);
            _EMSContext.Employees.Remove(employee);
            await _EMSContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public ActionResult MapDepartment(int? employeeId)
        {

            EMS.ViewModels.EmployeeDepartment employeeDepartment = new ViewModels.EmployeeDepartment();

            employeeDepartment.Employiees = _EMSContext.Employees.ToList();
            employeeDepartment.Departments = _EMSContext.Departments.ToList();

            if(employeeId != null)
            {
                var departmentIds = string.Join(",", _EMSContext.EmployeeDepartments.Where(ed => ed.EmployeeId == employeeId).Select(ed => ed.DepartmentId).ToList());
                employeeDepartment.DepartmentIds = _EMSContext.EmployeeDepartments.Where(ed => ed.EmployeeId == employeeId).Select(ed => ed.DepartmentId).ToArray();
            }

            return View(employeeDepartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MapDepartment(int employeeId, int[] departmentIds)
        {

            var employeeDepartments = _EMSContext.EmployeeDepartments.Where(ed => ed.EmployeeId == employeeId && !departmentIds.Contains(ed.DepartmentId)).ToList();
            if (employeeDepartments != null)
            {
                _EMSContext.EmployeeDepartments.RemoveRange(employeeDepartments);
                _EMSContext.SaveChanges();
            }

            foreach (int departmentId in departmentIds)
            {
                bool isEmployeeDepartmentExist = _EMSContext.EmployeeDepartments.Any(ed => ed.EmployeeId == employeeId && ed.DepartmentId == departmentId);

                if (!isEmployeeDepartmentExist)
                {
                    EmployeeDepartment employeeDepartment = new EmployeeDepartment();
                    employeeDepartment.EmployeeId = employeeId;
                    employeeDepartment.DepartmentId = departmentId;

                    _EMSContext.Add(employeeDepartment);
                    _EMSContext.SaveChanges();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult UpdateDepartment(int employeeId)
        {
       

            EMS.ViewModels.EmployeeDepartment employeeDepartment = new ViewModels.EmployeeDepartment();
            var departmentIds = _EMSContext.EmployeeDepartments.Where(ed => ed.EmployeeId == employeeId).Select(ed => ed.DepartmentId).ToList();
            return Json(departmentIds);



            /*employeeDepartment.Employiees = _EMSContext.Employees.ToList();
            employeeDepartment.Departments = _EMSContext.Departments.ToList();

            return View(employeeDepartment);*/
        }
    }
}
