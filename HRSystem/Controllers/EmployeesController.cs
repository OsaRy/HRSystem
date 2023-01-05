using HR.DataAccess.Data;
using HR.PresentationModels.Dto.Employees;
using HR.Services.Implementation;
using HRSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HRSystem.Controllers
{
	[Authorize]
	public class EmployeesController : Controller
    {
        private readonly EmployeesServices _employeesServices;

		private readonly ApplicationDbContext _db;

		public EmployeesController(EmployeesServices employeesServices, ApplicationDbContext db)
        {
            _employeesServices = employeesServices;
            _db = db;
        }

        public IActionResult Index()
        {
			ViewBag.managers = new SelectList( _db.Employees.ToList(), "ID", "Name");
      
			return View();
        }
        public IActionResult RefreshEmployees()
		{
            var emps = new SelectList(_db.Employees.ToList(), "ID", "Name");
			return Ok(new { data = emps });
		}
		public async Task<IActionResult> GetAll()
        {
            var userid = await _employeesServices.GetUserId(User);
            var employees = await _employeesServices.GetAll(userid);

            return Ok(new { data = employees });
        }
		public async Task<IActionResult> GetAllManagers()
		{
			var employees = await _employeesServices.GetAllManagers();

			return Ok(new { data = employees });
		}
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> Save(EmployeeDto obj)
        {
            var result = await _employeesServices.Save(obj);

            return Ok(result);
		}
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> GetById(int id)
        {
            var employees = await _employeesServices.GetById(id);

            return Ok(employees);
		}
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> Delete(int id)
        {
            var employees = await _employeesServices.Delete(id);

            return Ok(employees);
        }
		

	}
}
