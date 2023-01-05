using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HR.PresentationModels.Dto.Users;
using System.Collections.Generic;

namespace HR.PresentationModels.Dto.Employees
{
	public class GetAllManagersDto
	{
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        public int Count { get; set; }
		public List<GetAllEmployeesDto> Employees { get; set; }
	}
}
