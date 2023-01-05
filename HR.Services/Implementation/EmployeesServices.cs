using HR.DataAccess.Data;
using HR.DomainModels.Models;
using HR.PresentationModels.Dto.Employees;
using HR.PresentationModels.Dto.Users;
using Microsoft.AspNetCore.Identity;
using HR.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HR.Shared.Enums;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HR.Services.Implementation
{
    public class EmployeesServices
    {
        private readonly ApplicationDbContext _db;
		private readonly SignInManager<EmployeesUsers> _signInManager;
		private readonly UserManager<EmployeesUsers> _userManager;
		public EmployeesServices(SignInManager<EmployeesUsers> signInManager,
			UserManager<EmployeesUsers> UserManager, ApplicationDbContext db)
        {
            _db = db;
			_signInManager = signInManager;
			_userManager = UserManager;
		}

        public async Task<int> GetUserId(ClaimsPrincipal User)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            return user.Emp_ID ?? 0;
        }

        public async Task<List<GetAllEmployeesDto>> GetAll(int id)
        {
			var obj = await _db.Employees.Include(x=>x.EmployeeLogs).Where(x => id != 0 ? x.Manager_id == id : true).ToListAsync();
			var result = obj.Select(x => new GetAllEmployeesDto
			{
				ID = x.ID,
				Address = x.Address,
				BirthDate = x.BirthDate.ToString("dd/MM/yyyy"),
				EmailAddress = x.EmailAddress,
				Mobile = x.Mobile,
				Name = x.Name,
				ManagerName = x.Manager != null ? x.Manager.Name : "",
				Attendance= x.EmployeeLogs.Select(x => new LogsDto
				{
					ID = x.ID,
					Date = x.Date,
					Type = x.Type,
					Message = x.Type == CheckType.In ? "Check In" : "Check Out"
				}).OrderByDescending(x => x.Date).ToList()

			}).ToList();

            return result;
    
    }
		public async Task<List<GetAllManagersDto>> GetAllManagers()
		{
			var obj = await _db.Employees.Include(x => x.Managed_Employees).Where(x => x.Managed_Employees.Count()>0).ToListAsync();

			var result = obj.Select(x => new GetAllManagersDto
			{
				ID = x.ID,
				Address = x.Address,
				BirthDate = x.BirthDate.ToString("dd/MM/yyyy"),
				EmailAddress = x.EmailAddress,
				Mobile = x.Mobile,
				Name = x.Name,
				Count = x.Managed_Employees.Count(),
				Employees = x.Managed_Employees.Select(z => new GetAllEmployeesDto
				{
					ID = z.ID,
					Address = z.Address,
					BirthDate = z.BirthDate.ToString("dd/MM/yyyy"),
					EmailAddress = z.EmailAddress,
					Mobile = z.Mobile,
					Name = z.Name,
				}).ToList()

			}).ToList();

			return result;

		}
		public async Task<int> Save(EmployeeDto employeeDto)
        {
            if (employeeDto.ID == 0)
            {
				using var transaction = _db.Database.BeginTransaction();
				try
				{
					var obj = new Employee();
					obj.Name = employeeDto.Name;
					obj.Address = employeeDto.Address;
					obj.EmailAddress = employeeDto.EmailAddress;
					obj.Manager_id = employeeDto.Manager_id;
					obj.Mobile = employeeDto.Mobile;
					obj.BirthDate = employeeDto.BirthDate;

					await _db.Employees.AddAsync(obj);
					var result = await _db.SaveChangesAsync();
					var user = await CreateUser(obj.ID, employeeDto);
					if (user && result == 1)
					{
						await transaction.CommitAsync();

						return result;
					}
					return 0;
				}
				catch
				{
					transaction.Rollback();
					return 0;


				}
				finally
				{
					await transaction.DisposeAsync();
				}
			}
            else
            {
				using var transaction = _db.Database.BeginTransaction();

				try
				{
					var obj = await _db.Employees.FindAsync(employeeDto.ID);
					obj.Name = employeeDto.Name;
					obj.Address = employeeDto.Address;
					obj.EmailAddress = employeeDto.EmailAddress;
					obj.Manager_id = employeeDto.Manager_id;
					obj.Mobile = employeeDto.Mobile;
					obj.BirthDate = employeeDto.BirthDate;

					_db.Employees.Update(obj);
					var result = await _db.SaveChangesAsync();
					var user = await UpdateUser(employeeDto);
					if (user && result == 1)
						await transaction.CommitAsync();

					return result;
				}
				catch
				{
					transaction.Rollback();
					return 0;


				}
				finally
				{
					await transaction.DisposeAsync();
				}
			}

        }

        public async Task<EmployeeDto> GetById(int id)
        {
            var obj = await _db.Employees.FindAsync(id);
            var dto = new EmployeeDto();

            dto.ID = obj.ID;
			dto.Name = obj.Name;
			dto.Address = obj.Address;
			dto.EmailAddress = obj.EmailAddress;
			dto.Manager_id = obj.Manager_id;
			dto.Mobile = obj.Mobile;
			dto.BirthDate = obj.BirthDate;

            return dto;

        }
		public async Task<bool> CreateUser(int id, EmployeeDto employee)
		{
			PasswordHasher<EmployeesUsers> passwordHasher = new PasswordHasher<EmployeesUsers>();

			EmployeesUsers user = new EmployeesUsers
			{
				Id = Guid.NewGuid().ToString(),
				Email = employee.EmailAddress,
				NormalizedEmail = employee.EmailAddress.ToUpper(),
				UserName = employee.EmailAddress,
				NormalizedUserName = employee.EmailAddress.ToUpper(),
				EmailConfirmed = true,
				PhoneNumber = employee.Mobile,
				AccessFailedCount = 0,
				PhoneNumberConfirmed = false,
				Emp_ID = id,
				

		};
            user.PasswordHash = passwordHasher.HashPassword(user, employee.Password);
			var obj = await _userManager.CreateAsync(user);
            if (obj.Succeeded)
                return true;
            else return false;

		}
		public async Task<bool> UpdateUser(EmployeeDto employee)
		{
			PasswordHasher<EmployeesUsers> passwordHasher = new PasswordHasher<EmployeesUsers>();


			EmployeesUsers user = _db.EmployeesUsers.FirstOrDefault(x => employee.ID == x.Emp_ID);
			if (user != null)
			{

				user.Email = employee.EmailAddress;
				user.NormalizedEmail = employee.EmailAddress.ToUpper();
				user.UserName = employee.EmailAddress;
				user.NormalizedUserName = employee.EmailAddress.ToUpper();
				user.PhoneNumber = employee.Mobile;
				if(employee.Password!=null)
				user.PasswordHash = passwordHasher.HashPassword(user, employee.Password);

				var obj = await _userManager.UpdateAsync(user);
				if (obj.Succeeded)
					return true;

			}
			
		 return false;

		}
		public async Task<int> Delete(int id)
        {
			using var transaction = _db.Database.BeginTransaction();

			try
			{
				var obj = await _db.Employees.Include(x => x.EmployeeLogs).Include(x => x.Managed_Employees).FirstOrDefaultAsync(x => x.ID == id);
				var managed = obj.Managed_Employees.ToList();
				managed.ForEach(x => x.Manager_id = null);

				_db.UpdateRange(managed);
				await _db.SaveChangesAsync();
				_db.EmployeeLogs.RemoveRange(obj.EmployeeLogs);
				await _db.SaveChangesAsync();
				_db.Employees.Remove(obj);

				var result = await _db.SaveChangesAsync();

				EmployeesUsers user = _db.EmployeesUsers.FirstOrDefault(x => id == x.Emp_ID);

				var del = await _userManager.DeleteAsync(user);

				if (result == 1 && del.Succeeded)
				{
					transaction.Commit();
					return result;
				}
				return 0;

			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return 0;

			}
			finally
			{
				await transaction.DisposeAsync();
			}

        }
        public async Task<List<LogsDto>> GetAttendance(int id)
        {
            var obj = await _db.EmployeeLogs.Where(x=>x.Emp_ID== id).ToListAsync();

			var result = obj.Select(x => new LogsDto
			{
				ID = x.ID,
				Date = x.Date,
				Type = x.Type,
				Message = x.Type == CheckType.In ? "Check In" : "Check Out"
			}).OrderByDescending(x=>x.Date).ToList();

            return result;

        }

    }
}
