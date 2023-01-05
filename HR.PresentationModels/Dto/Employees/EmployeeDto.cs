using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.PresentationModels.Dto.Employees
{
    public class EmployeeDto
    {
        public int ID { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(120)]
        public string Address { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        [Required, MaxLength(50)]
        public string EmailAddress { get; set; }
        [Required, MaxLength(14)]
        public string Mobile { get; set; }
        public int? Manager_id { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }

	}
}
