using HR.DataAccess.Data;
using HR.DomainModels.Models;
using HR.Services.Implementation;
using HRSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HRSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
		private readonly EmployeesServices _employeesServices;

		private readonly ApplicationDbContext _db;

		public HomeController(EmployeesServices employeesServices, ApplicationDbContext db)
		{
			_employeesServices = employeesServices;
            _db = db;
        }
		public async Task<IActionResult> Index()
        {
            var userid = await _employeesServices.GetUserId(User);
            var attendance =await _employeesServices.GetAttendance(userid);
            var last = attendance.FirstOrDefault(x => x.Type == HR.Shared.Enums.CheckType.In);
            if(last!=null)
            {
                ViewBag.last = last.Date;
            }
            return View(attendance);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
