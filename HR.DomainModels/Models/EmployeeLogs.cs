using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HR.Shared.Enums;

namespace HR.DomainModels.Models
{
    public class EmployeeLogs
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public CheckType Type { get; set; }
        [Required]
        public DateTime Date { get; set; }

		[ForeignKey("Employee")]
		public int Emp_ID { get; set; }

		public Employee Employee { get; set; }

	}
}
