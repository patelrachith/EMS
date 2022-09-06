using AspNetCoreHero.ToastNotification.Abstractions;
using EMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly EMSContext _EMSContext;
        private readonly INotyfService _notyfService;

        public DepartmentController(EMSContext EMSContext, INotyfService notyfService)
        {
            _EMSContext = EMSContext;
            _notyfService = notyfService;
        }

        // GET: DepartmentController
        public ActionResult Index()
        {
            var departments = _EMSContext.Departments.ToList();
            return View(departments);
        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Upsert(int? departmentId)
        {
            ViewBag.PageName = departmentId == null ? "Add Department" : "Edit Department";
            ViewBag.IsEdit = departmentId == null ? false : true;
            if (departmentId == null)
            {
                return View();
            }
            else
            {
                var department = _EMSContext.Departments.Find(departmentId);

                if (department == null)
                {
                    return NotFound();
                }
                return View(department);
            }
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(int departmentId, Department departmentData)
        {
            bool IsEmployeeExist = false;

            Department department = _EMSContext.Departments.Find(departmentId);

            if (department != null)
            {
                IsEmployeeExist = true;
            }
            else
            {
                department = new Department();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    department.Name = departmentData.Name;
                    department.Descriptions = departmentData.Descriptions;
                   

                    if (IsEmployeeExist)
                    {
                        _EMSContext.Update(department);
                    }
                    else
                    {
                        _EMSContext.Add(department);
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
            return View(departmentData);
        }

        
    }
}
