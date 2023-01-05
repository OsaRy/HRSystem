using HR.DataAccess.Data;
using HR.DomainModels.Models;
using HR.PresentationModels.Dto.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HR.Shared.Enums;

namespace HR.Services.Implementation
{
    public class AccountServices
	{
        private readonly ApplicationDbContext _db;

        public AccountServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<EmployeeDto> GetEmployee(EmployeesUsers user)
        {
			var dto = new EmployeeDto();
			if (user.Emp_ID != 0)
            {
                var obj = await _db.Employees.FindAsync(user.Emp_ID);

                dto.ID = obj.ID;
                dto.Name = obj.Name;
                dto.Address = obj.Address;
                dto.EmailAddress = obj.EmailAddress;
                dto.Manager_id = obj.Manager_id;
                dto.Mobile = obj.Mobile;
                dto.BirthDate = obj.BirthDate;
            }else
            {
				dto.Name = user.UserName;
				dto.EmailAddress = user.Email;
				
			}
            return dto;

        }
		public async Task Check(int id,CheckType type)
		{
			var dto = new EmployeeLogs();
			
				
				dto.Date = DateTime.Now;
				dto.Type = type;
				dto.Emp_ID = id;
			
          await  _db.EmployeeLogs.AddAsync(dto);
          await  _db.SaveChangesAsync();


		}

	}
}
