using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HR.PresentationModels.Dto.Users;
using System.Collections.Generic;

namespace HR.PresentationModels.Dto.Employees
{
	public class GetAllEmployeesDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        public string ManagerName { get; set; }
		public List<LogsDto> Attendance { get; set; }
	}
}
